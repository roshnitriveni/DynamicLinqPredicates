using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Employee
    {
        public Employee(string Name, string specialization,int age, double salary)
        {
            this.Name = Name;
            this.Dev = specialization;
            this.Age = age;
            this.Salary = salary;

        }
        public string Name { get; set; }
        public string Dev { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

    }
}
