using ConsolePixie.Setup;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePixie.Image
{
    public class ImageBuilder
    {
        private ConsoleColor currentColor = ConsoleColor.White;
        private Color _currentPixelColor;
        private Color _lastPixelColor;
        public void BuildImage(Bitmap Image, bool usingCymbols = true)
        {
            if (Image != null)
            {
                Console.WriteLine();
                for (int y = 0; y < Image.Height; y += 2)
                {
                    Console.Write("{0}\t", y/2+1);

                    for (int x = 0; x < Image.Width; x += 2)
                    {
                        _currentPixelColor = Image.GetPixel(x, y);
                        _lastPixelColor = _currentPixelColor;
                        currentColor = ApproximateColor(_currentPixelColor.R, _currentPixelColor.G, _currentPixelColor.B);

                        Console.ForegroundColor = currentColor;

                        if (usingCymbols)
                            PrintCymbol("5 ");
                        else
                            PrintCymbol("██");
                    }

                    FinishImageLine();
                }
                FinishImageBuild();
            }
            else
            {
                CommonText.ShowErrorMessage("Не могу построить изображение, нет примера!");
                FinishImageBuild();
            }

        }
        private void FinishImageBuild() => FinishImageLine();

        private void FinishImageLine()
        {
            Console.ForegroundColor = Settings.DefaultColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        private void PrintCymbol(string cymbol)
        {
            Console.Write(cymbol);
        }

        public static ConsoleColor ApproximateColor(int r, int g, int b)
        {
            ConsoleColor closestColor = ConsoleColor.Black;
            double closestDistance = double.MaxValue;

            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                var (cr, cg, cb) = GetRgbFromConsoleColor(color);
                double distance = Math.Sqrt(Math.Pow(cr - r, 2) + Math.Pow(cg - g, 2) + Math.Pow(cb - b, 2));

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestColor = color;
                }
            }

            return closestColor;
        }

        public static (int R, int G, int B) GetRgbFromConsoleColor(ConsoleColor color)
        {
            return color switch
            {
                ConsoleColor.Black => (0, 0, 0),
                ConsoleColor.DarkBlue => (0, 0, 128),
                ConsoleColor.DarkGreen => (0, 128, 0),
                ConsoleColor.DarkCyan => (0, 128, 128),
                ConsoleColor.DarkRed => (128, 0, 0),
                ConsoleColor.DarkMagenta => (128, 0, 128),
                ConsoleColor.DarkYellow => (128, 128, 0),
                ConsoleColor.Gray => (192, 192, 192),
                ConsoleColor.DarkGray => (128, 128, 128),
                ConsoleColor.Blue => (0, 0, 255),
                ConsoleColor.Green => (0, 255, 0),
                ConsoleColor.Cyan => (0, 255, 255),
                ConsoleColor.Red => (255, 0, 0),
                ConsoleColor.Magenta => (255, 0, 255),
                ConsoleColor.Yellow => (255, 255, 0),
                ConsoleColor.White => (255, 255, 255),
                _ => (0, 0, 0), // По умолчанию черный
            };
        }
    }
}
