using System;
namespace PhotoConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = null;
            string goalFormat = null;
            string output = null;
            foreach (string arg in args)
            {
                if (arg.Length >= 9 && arg.Substring(0, 9) == "--source=")
                {
                    source = arg.Substring(9);
                }
                else if (arg.Length >= 14 && arg.Substring(0, 14) == "--goal-format=")
                {
                    goalFormat = arg.Substring(14);
                }
                if (arg.Length >= 9 && arg.Substring(0, 9) == "--output=")
                {
                    output = arg.Substring(9);
                }
            }
            Converter converter = new Converter(source, goalFormat, output);
            converter.Convert();

        }
    }
}
