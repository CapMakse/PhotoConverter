using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoConverter
{
    public interface IReader
    {
        Color[][] Read(string input);
        string GetFormat();
    }

    public interface IWriter 
    {
        void Write(Color[][] photo, string output);
        string GetFormat();
    }

}
