using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoConverter
{
    public struct Color
    {
        public Color(int red, int green, int blue)
        {
            Red = red; 
            Green = green;
            Blue = blue;
        }
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }
        public override string ToString()
        {
            return $"{Red} {Green} {Blue} " ;
        }
    }
}
