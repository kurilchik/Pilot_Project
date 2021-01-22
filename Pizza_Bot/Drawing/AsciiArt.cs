using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Pizza_Bot.Drawing
{
    public static class AsciiArt
    {
        private static readonly int _time = 3_000;
        private static readonly string _savePath = @"..\..\..\Drawing\Images\";
        private static readonly string _logoFile = "logo.png";
        private static readonly string[] _timerFiles = new string[] { "wait.png", "3.png", "2.png", "1.png" };
        private static readonly string[] _ascii = new string[] { "#", "#", "#", "#", "#", " ", " ", " ", " ", " " };

        static AsciiArt()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_savePath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public static void Logo()
        {
            Ascii(_logoFile, _time);
        }

        public static void Timer()
        {
            for (int i = 0; i < _timerFiles.Length; i++)
            {
                Ascii(_timerFiles[i], _time);
            }
        }

        private static void Ascii(string file, int time)
        {            
            Image image = Image.FromFile($@"{_savePath}\{file}");
            Bitmap bitmap = new Bitmap(image);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int greyColor = (color.R + color.G + color.B) / 3;
                    int index = greyColor * (_ascii.Length - 1) / 255;
                    Console.Write(_ascii[index]);
                }
                Console.Write(Environment.NewLine);
            }

            Thread.Sleep(time);
            Console.Clear();
        }
    }
}
