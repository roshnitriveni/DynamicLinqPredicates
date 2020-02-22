using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Helper;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            List<Employee> result = new List<Employee>();
            Type t = typeof(Employee);
            var andCriteria = new List<Predicate<Employee>>();
            Expression<Func<Employee, bool>> predicate;
            employees.Add(new Employee("Riya", ".Net", 20, 10000));
            employees.Add(new Employee("Jiya", "Php", 19, 20000));
            employees.Add(new Employee("Tiya", "Php", 25, 80000));
            employees.Add(new Employee("Siya", ".Net", 23, 10000));
            employees.Add(new Employee("Aisha", "Java", 20, 50000));
            employees.Add(new Employee("Nisha", "Java", 21, 10000));
            employees.Add(new Employee("Priya", ".Net", 26, 10000));
            employees.Add(new Employee("Gita", ".Net", 30, 20000));
           
            print(employees);

            string input;
            Console.WriteLine("1.Search 2.Sort");
            input = Console.ReadLine();
            string Fieldname = string.Empty, FieldValue = string.Empty;

            
            switch (input)
            {
                case "1":
                    Console.WriteLine("Syntax:Name Jiya ");
                    input = Console.ReadLine();
                    Fieldname = input.Split(' ').ToList()[0];
                    FieldValue = input.Split(' ').ToList()[1];
                    var type = t.GetProperty(Fieldname);
                    andCriteria.Add(c => Cast(type.GetValue(c), type.PropertyType) == Cast(FieldValue, type.PropertyType));
                    predicate = c => andCriteria.All(pred => pred(c));

                    result = employees.AsQueryable().Where(predicate).ToList();
                    break;
                case "2":
                    Console.WriteLine("Syntax:Name Asc ");
                    List<GridSort> sort = new List<GridSort>();
                    input = Console.ReadLine();
                    Fieldname = input.Split(' ').ToList()[0];
                    FieldValue = input.Split(' ').ToList()[1];
                    type = t.GetProperty(Fieldname);
                    sort.Add(new GridSort { Field = Fieldname, Dir = FieldValue });
                    result = employees.AsQueryable().MultiSort(sort).ToList();
                    break;
            }


            print(result);
        
            Console.ReadKey();
        }
        public static void print(List<Employee> e)
        {
            Console.WriteLine("Name\t Dev\t Age\t Salary");
            e.ForEach(x =>
            {
                Console.WriteLine($"{x.Name}\t {x.Dev}\t {x.Age}\t {x.Salary}");
            });
        }

        public static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
        public static dynamic Cast(dynamic source, Type dest)
        {
            if (source == null)
            {
                source = GetDefaultValue(dest);
                return source;
            }
            var type = Nullable.GetUnderlyingType(dest) ?? dest;
            return Convert.ChangeType(source, type);
        }


    }


}

