using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace ImageProcessing
{
    static class Utils
    {
        public static void handleError(string error, bool debug = false)
        {
            Console.Clear();
            Console.WriteLine("###############################################");
            if (debug) Console.WriteLine(error);
            Console.WriteLine("Somewhere, something went terribly wrong. Please contant me dainis.silamikelis@gmail.com");
            Console.WriteLine("###############################################");
        }

        public static bool doAnotherHomeWork()
        {
            Console.WriteLine("Press Y to do another homework");
            Console.WriteLine("Press any other key to quit application");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'y')
            {
                Console.WriteLine();
                return true;
            }
            return false;

        }
    }
}
