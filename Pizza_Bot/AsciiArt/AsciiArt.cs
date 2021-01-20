using System;
using System.Drawing;
using System.Threading;

namespace Pizza_Bot.AsciiArt
{
    public static class AsciiArt
    {
        public static void Logo()
        {
            int time = 5000;
            string path = "logo.png";

            Ascii(path, time);
        }

        public static void Timer()
        {
            int time = 2000;
            string[] paths = new string[] { "wait.png", "3.png", "2.png", "1.png" };

            Console.Clear();
            for (int i = 0; i < paths.Length; i++)
            {
                Ascii(paths[i], time);
            }
        }

        private static void Ascii(string file, int time)
        {
            string[] ascii = new string[] { "#", "#", "#", "#", "#", " ", " ", " ", " ", " " };
            Image image = Bitmap.FromFile($@"..\..\..\AsciiArt\Images\{file}");
            Bitmap bitmap = new Bitmap(image);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int greyColor = (color.R + color.G + color.B) / 3;
                    int index = greyColor * 9 / 255;
                    Console.Write(ascii[index]);
                }
                Console.Write(Environment.NewLine);
            }

            Thread.Sleep(time);
            Console.Clear();
        }
    }
}
