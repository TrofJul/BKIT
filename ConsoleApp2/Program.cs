using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b, c;
            while (true)
            {
                Console.WriteLine("Введите коэффициенты уравнения ");
                if (!double.TryParse(Console.ReadLine(), out a))
                { Console.WriteLine("Неверно введен коэффициент a"); continue; }
                if (!double.TryParse(Console.ReadLine(), out b))
                { Console.WriteLine("Неверно введен коэффициент b"); continue; }
                if (!double.TryParse(Console.ReadLine(), out c))
                { Console.WriteLine("Неверно введен коэффициент c"); continue; }
                break;
            }

            if (a == 0)
            {
                Console.WriteLine("Линейное уравнение\n");
                double x = -c / b;
                Console.WriteLine("Корень уравнения x = {0}", x);
            }
            else
            {
                double d = b * b - 4 * a * c;
                if (d > 0)
                {
                    double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(d)) / (2 * a);
                    Console.WriteLine("Дискриминант d = {0} , корни уравнения x1 = {1} , x2 = {2}", d, x1, x2);
                }
                else if (d == 0)
                {
                    double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                    Console.WriteLine("Дискриминант равен нулю, корень уравнения x = {0}", x1);
                }
                else
                {
                    Console.WriteLine("Действительных корней нет.");
                }
            }
            Console.ReadKey();
        }
    }
}
