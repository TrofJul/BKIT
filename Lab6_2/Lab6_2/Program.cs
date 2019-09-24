using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_2
{
    [AttributeUsage(AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]

    public class NewAttribute : Attribute
    {
        public NewAttribute() { }

        public NewAttribute(string DescriptionParam)
        {
            Description = DescriptionParam;
        }

        public string Description { get; set; }
    }
    public class ClassForInspection
    {
        public ClassForInspection() { }
        public ClassForInspection(string str) { }
        public ClassForInspection(double d) { }

        public double Divide(double a, double b)
        {
            return a / b;
        }

        [NewAttribute("Описание первого свойства")]
        public int prop1 { get; set; }

        [NewAttribute("Описание второго свойства")]
        public int prop2 { get; set; }

        public int prop3 { get; set; }
        public int[] data;
    }

    class Program
    {
        public static bool GetPropertyAttribute(PropertyInfo checkType,
            Type attributeType, out object attribute)
        {
            bool Result = false;
            attribute = null;

            var isAttribute = checkType.GetCustomAttributes(attributeType, false);
            if (isAttribute.Length > 0)
            {
                Result = true;
                attribute = isAttribute[0];
            }

            return Result;
        }

        static void Main(string[] args)
        {
            Type t = typeof(ClassForInspection);

            Console.WriteLine("\nИнформация о типе:");
            Console.WriteLine("Тип " + t.FullName + " унаследован от " + t.BaseType.FullName);

            Console.WriteLine("Пространство имен " + t.Namespace);

            Console.WriteLine("Находится в сборке " + t.AssemblyQualifiedName);

            Console.WriteLine("\nКонструкторы:");
            foreach (var x in t.GetConstructors())
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("\nМетоды:");
            foreach (var x in t.GetMethods())
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("\nСвойства:");
            foreach (var x in t.GetProperties())
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("\nПоля данных (public):");
            foreach (var x in t.GetFields())
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("\nСвойства, помеченные атрибутом:");
            foreach (var x in t.GetProperties())
            {
                object attrObj;
                if (GetPropertyAttribute(x, typeof(NewAttribute), out attrObj))
                {
                    NewAttribute attr = attrObj as NewAttribute;
                    Console.WriteLine(x.Name + " - " + attr.Description);
                }
            }

            Console.WriteLine("\nВызов метода с помощью рефлексии:");

            ClassForInspection cfi = new ClassForInspection();
            object[] parameters = new object[] { 3.0, 2.0 };

            object result = t.InvokeMember("Divide", BindingFlags.InvokeMethod, null, cfi, parameters);
            Console.WriteLine($"Divide(3.0, 2.0) = {result}");
            Console.ReadKey();

        }
    }
}
