using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
     class Program
    {
        public class Employee
        {
            public int id;
            public string surname;
            public int departmentId;

            public Employee(int _id, string _surname, int _departmentId)
            {
                id = _id;
                surname = _surname;
                departmentId = _departmentId;
            }
        }

        public class Department
        {
            public int id;
            public string name;

            public Department(int _id, string _name)
            {
                id = _id;
                name = _name;
            }
        }

        public class DepartmentEmployeeRelation
        {
            public int employeeId;
            public int departmentId;

            public DepartmentEmployeeRelation(int _empId, int _depId)
            {
                employeeId = _empId;
                departmentId = _depId;
            }
        }

        public static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee(1, "Тrofimova", 1),
                new Employee(2, "Abramenkov", 1),
                new Employee(3, "Chernishev", 1),
                new Employee(4, "Akushko", 1),
                new Employee(5, "Obrezkova", 2),
                new Employee(6, "Golubkova", 2),
                new Employee(7, "Orlova", 2),
                new Employee(8, "Savushkin", 3),
                new Employee(9, "Gudilin", 3),
            };

            List<Department> departments = new List<Department>
            {
                new Department(1, "Development"),
                new Department(2, "Sales"),
                new Department(3, "Marketing")
            };

            List<DepartmentEmployeeRelation> depEmpRels = new List<DepartmentEmployeeRelation>
            {
                new DepartmentEmployeeRelation(1, 1),
                new DepartmentEmployeeRelation(1, 2),
                new DepartmentEmployeeRelation(2, 1),
                new DepartmentEmployeeRelation(2, 3),
                new DepartmentEmployeeRelation(3, 2),
                new DepartmentEmployeeRelation(4, 1),
                new DepartmentEmployeeRelation(5, 1),
                new DepartmentEmployeeRelation(6, 2),
                new DepartmentEmployeeRelation(6, 3),
                new DepartmentEmployeeRelation(7, 2),
                new DepartmentEmployeeRelation(7, 3),
                new DepartmentEmployeeRelation(8, 2),
                new DepartmentEmployeeRelation(9, 3),
            };
            
            
            Console.WriteLine("Departments:");
            var q1 = from dep in departments 
                     orderby dep.id
                     select dep.name;
            foreach (var department in q1)
            {    
                Console.WriteLine(department);
            }
            
            Console.WriteLine("\nEmployees:");
            var q2 = from emp in employees
                     orderby emp.departmentId
                     select emp.surname;
            foreach (var employee in q2)
            {    
                Console.WriteLine(employee);
            }
            
            Console.WriteLine("\nEmployees with surnames starting with \"A\":");
            var q3 = from emp in employees
                     where emp.surname[0] == 'A'
                     select emp.surname;
            foreach (var employee in q3)
            {    
                Console.WriteLine(employee);
            }
            
            Console.WriteLine("\nNumber of employees in the departments: ");
            var q4 = from dep in departments
                join emp in employees on dep.id equals emp.departmentId into temp
                select new { Name = dep.name, Cnt = temp.Count()};
            foreach (var dep in q4)
            {    
                Console.WriteLine("{0}: {1} employees", dep.Name, dep.Cnt);
            }
            

            Console.WriteLine("\nDepartments where all employees have surnames starting with \"A\":");
            var q5 = from dep in departments
                join emp in employees on dep.id equals emp.departmentId into temp
                where temp.All(x => x.surname[0] == 'A')
                select dep.name;
            foreach (var dep in q5)
            {
                Console.WriteLine(dep);
            }
            
            
            Console.WriteLine("\nDepartments with employees with surnames starting with \"A\":");
            var q6 = from dep in departments
                join emp in employees on dep.id equals emp.departmentId into temp
                where temp.Any(x => x.surname[0] == 'A')
                select dep.name;
            foreach (var dep in q6)
            {
                Console.WriteLine(dep);
            }
            
            Console.WriteLine("\nMany-to-many relation:");
            var q7 = from dep in departments
                join depEmpRel in depEmpRels on dep.id equals depEmpRel.departmentId into matchingRels

                from depEmpRel in matchingRels
                join emp in employees on depEmpRel.employeeId equals emp.id into matchingEmps
                from link in matchingEmps
                select new {Dep = dep.name, Emps = link.surname};
            var q8 = from line in q7
                group line by line.Dep into depEmps
                select new { Dep = depEmps.Key, Emps = depEmps};

            foreach (var dep in q8)
            {
                Console.WriteLine(dep.Dep);
                foreach (var emp in dep.Emps)
                {
                    Console.WriteLine("\t"+emp.Emps);
                }
            }
            
            Console.WriteLine("\nMany-to-many relation (count):");
            var q9 = from dep in departments
                join depEmpRel in depEmpRels on dep.id equals depEmpRel.departmentId into matchingRels
                from depEmpRel in matchingRels
                join emp in employees on depEmpRel.employeeId equals emp.id into matchingEmps
                from link in matchingEmps
                select new {Dep = dep.name, Emps = link.surname};
            var q10 = from line in q9
                group line by line.Dep into depEmps
                select new { Dep = depEmps.Key, Emps = depEmps.Count()};
            
            foreach (var dep in q10)
            {
                Console.WriteLine(dep.Dep);
                Console.WriteLine("Employee count : {0}\n", dep.Emps);
            }
            Console.ReadKey();
        }
    }
}
