using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    interface IPrint
    {
        void Print();
    }

    abstract class Geom_fig
    {
        public abstract double Square();
    }
    class Rectangle: Geom_fig, IPrint
    {
        double width;
        double height;
        public Rectangle(double w, double h)
        {
            width = w;
            height = h;
        }
        public override string ToString()
        {
            return ("Ширина = " + width + ", высота равна = " + height + ", площадь прямоугольника = " + Square());
        }
        public override double Square()
        {
            return width*height;
        }
        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }

    class Quadrate : Rectangle, IPrint
    {
        double length;
        public Quadrate(double l) : base(l, l)
        {
            length = l;
        }
        public override string ToString()
        {
            return ("Cторона = " + length + ", площадь квадрата = " + Square());
        }
    }
    class Circle:Geom_fig,IPrint
    {
        double radius;
        public Circle(double r)
        {
            radius = r;
        }
        public override string ToString()
        {
            return ("Радиус = " + radius.ToString() + ", площадь круга = " + Square());
        }
        public override double Square()
        {
            return Math.PI * radius * radius;
        }
        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle(3, 7);
            Quadrate qudr = new Quadrate(6);
            Circle cir = new Circle(4);
            rect.Print();
            qudr.Print();
            cir.Print();
            Console.ReadKey();
        }
    }
}
