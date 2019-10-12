using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageProcessing
{
    class GaussianBlur
    {
        public bool DoGaussianBlur()
        {
            Console.WriteLine();
            Console.WriteLine("###### Gaussian Blur #####");
            Console.WriteLine("Please Wait for 'Homework finished' text to continue");
            
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string runningPath = AppDomain.CurrentDomain.BaseDirectory;
                string fileName = string.Format("{0}Resources\\moon.jpg", Path.GetFullPath(Path.Combine(runningPath, @"..\..\")));
                string copyFileName = string.Format("{0}\\moon.jpg", desktopPath);
                string copyGaussianFileName = string.Format("{0}\\moon-gaussian-blur-ds11021.jpg", desktopPath);
                if(File.Exists(copyGaussianFileName)) File.Delete(copyGaussianFileName);
                if (File.Exists(copyFileName))
                {
                    Console.WriteLine("moon.jpg already exists on your desktop!");
                    Console.WriteLine("Do you wish to delete it?");
                    Console.WriteLine("Press d to delete");
                    Console.WriteLine("Or to cancel the action ?");
                    Console.WriteLine("Press any button to cancel");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.KeyChar == 'd')
                    {
                        Console.WriteLine();
                        File.Delete(copyFileName);
                    }
                    else return true;
                }
                System.IO.File.Copy(fileName, copyFileName);
                System.IO.File.Copy(fileName, copyGaussianFileName);
            }
            catch (IOException e)
            {
                utils.handleError(e.Message, true);
            }



            Console.WriteLine("Homework finished");
            Console.WriteLine("Picture moon.jpg should be visible on your desktop");
            Console.WriteLine("Picture mooon-gaussian-blur-ds11021.jpg should be visible on your desktop");
            Console.WriteLine();
            return utils.doAnotherHomeWork();
        }
    }
}
