using System;
using System.IO;
using PhotoConverter;

namespace BMPReader
{
    public class Reader : IReader
    {
        public Color[][] Read(string input)
        {
            byte[] photo = File.ReadAllBytes(input);
            if (photo.Length <= 54) ThrowExcIncorrectFile(); // 54 = BITMAPFILEHEADER + BITMAPINFOHEADER
            if (photo[0] != 'B' || photo[1] != 'M') ThrowExcIncorrectFile();
            int width = BitConverter.ToInt32(photo, 18);
            int height = BitConverter.ToInt32(photo, 22);
            if (photo[28] != 32) throw new Exception("Даная программа поддерживает толькл 32 битный формат бмп");
            Color[][] matrix = new Color[height][];
            for (int i = 0; i < height; i++)
            {
                matrix[i] = new Color[width];
            }
            int pos = BitConverter.ToInt32(photo, 10);
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    if (pos + 3 >= photo.Length) ThrowExcIncorrectFile();
                    else matrix[i][j] = new Color(photo[pos + 2], photo[pos + 1], photo[pos]);
                    pos += 4;
                }
            }
            return matrix;
        }
        private void ThrowExcIncorrectFile()
        {
            throw new Exception("Выбранный файл имеет некоректное содержание");
        }
        public string GetFormat() { return "bmp"; }
    }
}
