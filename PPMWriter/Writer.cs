using System;
using System.IO;
using PhotoConverter;

namespace PPMWriter
{
    public class Writer : IWriter
    {
        public void Write(Color[][] matrix, string output)
        {
            using (StreamWriter file = new StreamWriter (output))
            {
                string info = "P3 " + matrix[0].Length + " " + matrix.Length + " 255 ";
                file.WriteLine(info);
                foreach (Color[] line in matrix)
                {
                    foreach (Color color in line)
                    {
                        file.WriteLine(color.ToString());
                    }
                }
                file.Close();
            }
        }
        public string GetFormat() { return "ppm"; }
    }
}
