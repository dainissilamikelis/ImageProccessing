using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImageProcessing
{
    class GaussianBlur
    {
        private double formatPoint(
            double[,]
            matrix,
            int x,
            int y,
            double[,] kernel,
            int direction
        )
        {
            double formattedResult = 0;
            int half = kernel.GetLength(0) / 2;
            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                int CoefficeintX = direction == 0 ? x + i - half : x;
                int CoefficientY = direction == 1 ? y + i - half : y;
                if (
                    CoefficeintX >= 0
                    && CoefficientY < matrix.GetLength(0)
                    && CoefficientY >= 0
                    && CoefficeintX < matrix.GetLength(1)
                   )
                {
                    formattedResult += matrix[CoefficeintX, CoefficientY] * kernel[i, 0];
                }
            }
            return formattedResult;
        }
        public double[,] ConvoluteImage(double[,] matrix, double deviation)
        {
            double[,] kernel = CalculateNormalized1DSampleKernel(deviation);
            double[,] res1 = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double[,] res2 = new double[matrix.GetLength(0), matrix.GetLength(1)];
            //x-direction
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    res1[i, j] = formatPoint(matrix, i, j, kernel, 0);
            }
            //y-direction
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    res2[i, j] = formatPoint(res1, i, j, kernel, 1);
            }
            return res2;
        }
       
        private Color grayscale(Color cr)
        {
            return Color.FromArgb(cr.A, (int)(cr.R * .3 + cr.G * .59 + cr.B * 0.11),
               (int)(cr.R * .3 + cr.G * .59 + cr.B * 0.11),
              (int)(cr.R * .3 + cr.G * .59 + cr.B * 0.11));
        }

        
        public double[,] Calculate1DSampleKernel(double deviation, int size)
        {
            double[,] ret = new double[size, 1];
            double sum = 0;
            int half = size / 2;
            for (int i = 0; i < size; i++)
            {
                ret[i, 0] = 1 / (Math.Sqrt(2 * Math.PI) * deviation) * Math.Exp(-(i - half) * (i - half) / (2 * deviation * deviation));
                sum += ret[i, 0];
            }
            return ret;
        }
        public double[,] Calculate1DSampleKernel(double deviation)
        {
            int size = (int)Math.Ceiling(deviation * 3) * 2 + 1;
            return Calculate1DSampleKernel(deviation, size);
        }

        public double[,] CalculateNormalized1DSampleKernel(double deviation)
        {
            return NormalizeMatrix(Calculate1DSampleKernel(deviation));
        }
        public double[,] NormalizeMatrix(double[,] matrix)
        {
            double[,] ret = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double sum = 0;
            for (int i = 0; i < ret.GetLength(0); i++)
            {
                for (int j = 0; j < ret.GetLength(1); j++)
                    sum += matrix[i, j];
            }
            if (sum != 0)
            {
                for (int i = 0; i < ret.GetLength(0); i++)
                {
                    for (int j = 0; j < ret.GetLength(1); j++)
                        ret[i, j] = matrix[i, j] / sum;
                }
            }
            return ret;
        }

        private void ApplyGaussianBlur(string copyFileName, string copyGaussianFileName)
        {
            if(File.Exists(copyFileName))
            {
                var img = Bitmap.FromFile(copyFileName);
                var image = new Bitmap(img);
                var ret = new Bitmap(image.Width, image.Height);

                var matrix = new double[image.Width, image.Height];
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                        matrix[i, j] = grayscale(image.GetPixel(i, j)).R;
                }
                matrix = ConvoluteImage(matrix, 4);
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        int val = (int)Math.Min(255, matrix[i, j]);
                        ret.SetPixel(i, j, Color.FromArgb(255, val, val, val));
                    }
                }
                ret.Save(copyGaussianFileName);
            }

        }

        public bool DoGaussianBlur()
        {
            Console.WriteLine();
            Console.WriteLine("###### Gaussian Blur #####");
            Console.WriteLine("Please Wait for 'Homework finished' text to continue");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string runningPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = string.Format("{0}Resources\\moon.jpg", Path.GetFullPath(Path.Combine(runningPath, @"..\..\")));
            string copyFileName = string.Format("{0}\\moon-ds11021.jpg", desktopPath);
            string copyGaussianFileName = string.Format("{0}\\moon-gaussian-blur-ds11021.jpg", desktopPath);
            try
            {
                if (File.Exists(copyGaussianFileName)) File.Delete(copyGaussianFileName);
                if (!File.Exists(copyFileName)) File.Copy(fileName, copyFileName);
            }
            catch (IOException e)
            {
                Utils.handleError(e.Message, true);
            }

            ApplyGaussianBlur(copyFileName, copyGaussianFileName);
            Console.WriteLine("Homework finished");
            Console.WriteLine("Picture moon-ds11021.jpg should be visible on your desktop");
            Console.WriteLine("Picture mooon-gaussian-blur-ds11021.jpg should be visible on your desktop");
            Console.WriteLine();
            return Utils.doAnotherHomeWork();
        }
    }
}
