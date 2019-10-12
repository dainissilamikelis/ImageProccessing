using System;

namespace ImageProcessing
{
    class Program
    {
        static void AddHomeWorkText(int number, string title)
        {
            Console.WriteLine("Press {0} to see #{0} homework for {1}", number, title);
        }

        static void HomeWorkChooser()
        {
            var doAnotherHomeWork = false;
            var GaussianBlur = new GaussianBlur();
            Console.WriteLine("Home work solutions for Image proccessing course 2019");
            Console.WriteLine("Press 'X' to close or close the window");
            Console.WriteLine("Press 'R' to restart");
            AddHomeWorkText(1, "Gaussian Blur");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'x') Environment.Exit(0);
            if (key.KeyChar == 'r') HomeWorkChooser();
            if (key.KeyChar == '1') doAnotherHomeWork = GaussianBlur.DoGaussianBlur();

            if (doAnotherHomeWork) HomeWorkChooser();
            else Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            HomeWorkChooser();
        }
    }
}
