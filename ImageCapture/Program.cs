using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter save directory: ");
            var directory = Console.ReadLine();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            
            Directory.SetCurrentDirectory(directory);
            
            Console.Write("Enter x-coordinate to capture: ");
            var x = int.Parse(Console.ReadLine());
            Console.Write("Enter y-coordinate to capture: ");
            var y = int.Parse(Console.ReadLine());
            Console.Write("Enter capture width: ");
            var width = int.Parse(Console.ReadLine());
            Console.Write("Enter capture height: ");
            var height = int.Parse(Console.ReadLine());
            
            Console.Write("Enter number of digits in file name: ");
            var pad = int.Parse(Console.ReadLine());
            Console.Write("Enter starting index: ");
            var index = int.Parse(Console.ReadLine());
            
            // TODO: This is kinda crappy and requires two screens or some funky work to keep the console on top for captures.
            // Would be good to get some kind of cross-platform global key intercepts to kep the console in the background.
            Console.WriteLine("Now running. Press 'C' to capture, 'Escape' to quit.");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true).Key;
                    if (keyInfo == ConsoleKey.Escape)
                        return;

                    if (keyInfo != ConsoleKey.C)
                        continue;

                    var indexString = index.ToString($"D{pad}");
                    Console.Write($"Capturing {indexString}.png...");
                    
                    var captureBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                    using var captureGraphic = Graphics.FromImage(captureBmp);
                    captureGraphic.CopyFromScreen(x, y, 0, 0, captureBmp.Size);
                    captureBmp.Save($"{indexString}.png", ImageFormat.Png);
                    Console.WriteLine("Saved!");
                    index++;
                }
            }
        }
    }
}
