using System;

namespace Shapes
{
    public class Rectangle : Shape
    {
        public Rectangle(double height, double width)
        {
            this.Height = height;
            this.Width = width;
        }

        public double Height { get; private set; }
        public double Width { get; private set; }
        public override double CalculatePerimeter()
        {
            return 2 * Height + 2 * Width;
        }

        public override double CalculateArea() 
        {
            return Height * Width;
        }

        public sealed override string Draw()
        {
            return base.Draw() + $"{this.GetType().Name}";
        }
    }
}
