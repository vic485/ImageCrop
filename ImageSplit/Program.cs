using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageSplit
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter the path to the images to crop: ");
            var directory = Console.ReadLine();

            if (!Directory.Exists(directory))
            {
                Console.WriteLine("Could not find the provided directory!");
                return;
            }

            var outputDir = Path.Combine(directory, "output");
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            var fileList = Directory.GetFiles(directory);
            var counter = 1;

            foreach (var file in fileList)
            {
                Console.WriteLine("Open: " + Path.GetFileName(file));
                var image = new Bitmap(file);
                var half = (int) Math.Floor(image.Width / 2d);
                var leftImage = CropImage(image, new Rectangle(0, 0, half, image.Height));
                var rightImage = CropImage(image, new Rectangle(half + 1, 0, image.Width - (half + 1), image.Height));

                // TODO: other formats, and only pad the needed amount
                Console.WriteLine($"Save: {counter:D3}.png");
                // Since this is meant for manga, the right image is saved first as the lower number file
                using var firstFileStream = File.Create(Path.Combine(outputDir, $"{counter:D3}.png"));
                rightImage.Save(firstFileStream, ImageFormat.Png);
                counter++;

                Console.WriteLine($"Save: {counter:D3}.png");
                using var secondFileStream = File.Create(Path.Combine(outputDir, $"{counter:D3}.png"));
                leftImage.Save(secondFileStream, ImageFormat.Png);
                counter++;
            }
        }

        private static Bitmap CropImage(Bitmap original, Rectangle cropArea)
            => original.Clone(cropArea, original.PixelFormat);
    }
}
