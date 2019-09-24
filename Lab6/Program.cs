using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    delegate string MyDeleg(string p1, int p2);

    class Program
    {
        static void TestFunction(string desription, string p1, int p2, MyDeleg func)
        {
            Console.Write(desription + ": ");
            Console.WriteLine(func(p1, p2));
        }

        static void TestFunction1(string desription, string p1, int p2, Func<string, int, string> func)
        {
            Console.Write(desription + ": ");
            Console.WriteLine(func(p1, p2));
        }

        //метод, соответствующий делегату MyDeleg: возвращает строку, состоящую из строки p1, повторенной p2 раз
        static string MyDelegFunc(string p1, int p2)
        {
            string str = "";
            for (int i = 0; i < p2; ++i)
            {
                str += p1;
            }

            return str;
        }
        static void Main(string[] args)
        {
            string p1 = "Hello";
            int p2 = 3;

            Console.WriteLine("Делегат MyDeleg:");

            TestFunction("Передача функции", p1, p2, MyDelegFunc);
            TestFunction("Передача лямбда-выражения", p1, p2, (x, y) => x + y.ToString());

            Console.WriteLine("\nОбобщенный делегат:");

            TestFunction1("Передача функции", p1, p2, MyDelegFunc);
            TestFunction1("Передача лямбда-выражения", p1, p2, (x, y) => x + y.ToString());
            Console.ReadKey();
        }
    }
}
