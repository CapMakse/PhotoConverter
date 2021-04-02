using System;
using PhotoConverter;
using System.IO;
using System.Text.RegularExpressions;

namespace PPMReader
{
    public class Reader : IReader
    {
        int maxColor;
        public Color[][] Read(string input)
        {
            using (StreamReader file = new StreamReader(input))
            {
                string lines = file.ReadToEnd();
                lines = Regex.Replace(lines, @"#.*?\n", " ");
                string[] photo = Regex.Split(lines, @"[\n\s]+");
                if (photo.Length < 4) ThrowExcIncorrectFile();
                if (photo[0] != "P3") ThrowExcIncorrectFile();
                int width = GetNum(photo[1]);
                int height = GetNum(photo[2]);
                maxColor = GetNum(photo[3]) + 1;
                if (width < 1 || height < 1|| maxColor < 1) ThrowExcIncorrectFile();
                Color[][] matrix = new Color[height][];
                for (int i = 0; i < height; i++)
                {
                    matrix[i] = new Color[width];
                }
                int pos = 4;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (pos + 2 >= photo.Length) matrix[i][j] = new Color(0, 0, 0);
                        else matrix[i][j] = new Color(GetColor(GetNum(photo[pos])), GetColor(GetNum(photo[pos + 1])), GetColor(GetNum(photo[pos + 2])));
                        pos += 3;
                    }
                }
                return matrix;
            }
        }
        private void ThrowExcIncorrectFile()
        {
            throw new Exception("Выбранный файл имеет некоректное содержание");
        }
        private int GetNum(string str)
        {
            int ret;
            if (!Int32.TryParse(str, out ret)) ThrowExcIncorrectFile();
            return ret;
        }
        private int GetColor(int col)
        {
            return ((col + 1) * 256 / maxColor) - 1;
        }
        public string GetFormat() { return "ppm"; }
    }
}
