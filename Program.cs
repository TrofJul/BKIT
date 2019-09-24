using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab3
{
    class Program
    {
        interface IPrint
        {
            void Print();
        }
        
        abstract class Figure: IComparable
        {
            public string Type { get; protected set; }
      
            public abstract double Area();

            public override string ToString()
            {
                return this.Type + " площадью " +
               this.Area().ToString();
            }
            public int CompareTo(object obj)
            {
                /// -1 - если левый параметр меньше правого
                /// 0 - параметры равны
                /// 1 - правый параметр меньше левого
                Figure p = (Figure)obj;
                if (this.Area() < p.Area()) return -1;
                else if (this.Area() == p.Area()) return 0;
                else return 1;
            }
        }
        class Rectangle : Figure, IPrint
        {
            double height;
            double width;
            public Rectangle(double ph, double pw)
            {
                height = ph;
                width = pw;
                Type = "Прямоугольник";
            }

            public override double Area()
            {
                double Result = width * height;
                return Result;
            }
            public void Print()
            {
                Console.WriteLine(ToString());
            }
        }
        class Square : Rectangle, IPrint
        {
            public Square(double size): base(size, size)
            {
                Type = "Квадрат";
            }
        }
        class Circle : Figure, IPrint
        {
            double radius;
            public Circle(double pr)
            {
                radius = pr;
                Type = "Круг";
            }
            public override double Area()
            {
                double Result = Math.PI * radius * radius;
                return Result;
            }
            public void Print()
            {
                Console.WriteLine(ToString());
            }
        }

        class FigureMatrixCheckEmpty : IMatrixCheckEmpty<Figure>
        {
            public Figure getEmptyElement()
            {
                return null;
            }
            public bool checkEmptyElement(Figure element)
            {
                bool Result = false;
                if (element == null)
                {
                    Result = true;
                }
                return Result;
            }
        }

        static void Main(string[] args)
         {
            Rectangle rect = new Rectangle(5, 4);
            Square square = new Square(5);
            Circle circle = new Circle(5);

            Console.WriteLine("Сортировка коллекции класса List<Figure>:");
            List <Figure> fl = new List<Figure>();
            fl.Add(circle);
            fl.Add(rect);
            fl.Add(square);
            fl.Sort();
            foreach (var x in fl) Console.WriteLine(x);

            Console.WriteLine("\nСортировка коллекции класса ListArray:");
            ArrayList li = new ArrayList();
            li.Add(circle);
            li.Add(rect);
            li.Add(square);
            li.Sort();
            foreach (var x in li) Console.WriteLine(x);

            Console.WriteLine("\nМатрица:");
            Matrix<Figure> matrix = new Matrix<Figure>(3, 3, 3, new FigureMatrixCheckEmpty());
            matrix[0, 0, 0] = rect;
            matrix[1, 1, 1] = square;
            matrix[2, 2, 2] = circle;
            Console.WriteLine(matrix.ToString());

            Console.WriteLine("\nСтек:");
            SimpleStack<Figure> stack = new SimpleStack<Figure>();
            stack.Push(rect);
            stack.Push(square);
            stack.Push(circle);
            while (stack.Count > 0)
            {
                Figure f = stack.Pop();
                Console.WriteLine(f);
            }
            Console.ReadKey();

        }

    }

}



