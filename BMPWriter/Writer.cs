using System;
using System.IO;
using PhotoConverter;

namespace BMPWriter
{
    public class Writer : IWriter
    {
        struct BITMAPINFOHEADER
        {
            public uint biSize { get; set; }
            public int biWidth { get; set; }
            public int biHeight { get; set; }
            public ushort biPlanes { get; set; }
            public ushort biBitCount { get; set; }
            public uint biCompression { get; set; }
            public uint biSizeImage { get; set; }
            public int biXPelsPerMeter { get; set; }
            public int biYPelsPerMeter { get; set; }
            public uint biClrUsed { get; set; }
            public uint biClrImportant { get; set; }
        }
        public void Write(Color[][] matrix, string output)
        {
            using (FileStream file = new FileStream(output, FileMode.Create))
            {
                file.Write(new byte[] { (byte)'B', (byte)'M' }); //bfType
                file.Write(BitConverter.GetBytes((uint)(54 + matrix.Length * matrix[0].Length))); //bfSize
                file.Write(new byte[] { 0, 0, 0, 0 }); //bfReserved
                file.Write(BitConverter.GetBytes((uint)(54))); // bfOffBits
                file.Write(BitConverter.GetBytes((uint)(40))); // biSize
                file.Write(BitConverter.GetBytes(matrix[0].Length)); // biHeight
                file.Write(BitConverter.GetBytes(matrix.Length)); // biWidth
                file.Write(new byte[] { 1, 0 }); //biPlanes
                file.Write(new byte[] { 32, 0 }); //biBitCount
                file.Write(new byte[] { 0, 0, 0, 0 }); //biCompression
                file.Write(new byte[] { 0, 0, 0, 0 }); //biSizeImage
                file.Write(new byte[] { 0, 0, 0, 0 }); //biXPelsPerMeter
                file.Write(new byte[] { 0, 0, 0, 0 }); //biYPelsPerMeter
                file.Write(new byte[] { 0, 0, 0, 0 }); //biClrUsed
                file.Write(new byte[] { 0, 0, 0, 0 }); //biClrImportant
                Array.Reverse(matrix);
                foreach (Color[] line in matrix)
                {
                    foreach (Color color in line)
                    {
                        byte[] bytes = { (byte)color.Blue, (byte)color.Green, (byte)color.Red, 0 };
                        file.Write(bytes);
                    }
                }
                file.Close();
            }
        }
        public string GetFormat() { return "bmp"; }
    }
}
