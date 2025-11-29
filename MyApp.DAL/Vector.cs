using System;

namespace MyApp.DAL
{
    
    public class Vector
    {
        public string LineColor { get; set; } = "black";
        public double EndX { get; set; }
        public double EndY { get; set; }

        public Vector() { }

        public Vector(string color, double x, double y)
        {
            LineColor = color;
            EndX = x;
            EndY = y;
        }

        public double Length()
        {
            return Math.Sqrt(EndX * EndX + EndY * EndY);
        }

        public void Increase(double delta)
        {
            var len = Length();
            if (len == 0) return;
            var factor = (len + delta) / len;
            EndX *= factor;
            EndY *= factor;
        }

        public override string ToString()
            => $"Color: {LineColor}, End: ({EndX:F2}, {EndY:F2}), Length: {Length():F2}";
    }
}
