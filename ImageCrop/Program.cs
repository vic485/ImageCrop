using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageCrop
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

            Console.Write("Enter top x-coordinate: ");
            var x = int.Parse(Console.ReadLine());
            Console.Write("Enter left y-coordinate: ");
            var y = int.Parse(Console.ReadLine());
            Console.Write("Enter width: ");
            var w = int.Parse(Console.ReadLine());
            Console.Write("Enter height: ");
            var h = int.Parse(Console.ReadLine());

            var fileList = Directory.GetFiles(directory);

            foreach (var file in fileList)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine(Path.GetFileName(file));
                var image = new Bitmap(file);
                var newImage = CropImage(image, new Rectangle(x, y, w, h));

                // TODO: Support other file types
                using var fileStream = File.Create(Path.Combine(directory, $"{fileName}-crop.png"));
                newImage.Save(fileStream, ImageFormat.Png);
            }
        }

        private static Bitmap CropImage(Bitmap original, Rectangle cropArea)
            => original.Clone(cropArea, original.PixelFormat);
    }
}
