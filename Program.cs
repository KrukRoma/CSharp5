using System;
using System.Linq;

namespace CSharp5
{
    public class Worker
    {
        private string name;
        private string surname;
        private string fathername;
        private int age;
        private decimal salary;
        private DateTime dateOfEmployment;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Fathername
        {
            get { return fathername; }
            set { fathername = value; }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value > 18 && value < 65)
                    age = value;
                else
                    throw new Exception("Error age.");
            }
        }

        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (value > 0)
                    salary = value;
                else
                    throw new Exception("Incorrect salary.");
            }
        }

        public DateTime DateOfEmployment
        {
            get { return dateOfEmployment; }
            set
            {
                if (value < DateTime.Now)
                    dateOfEmployment = value;
                else
                    throw new Exception("Incorrect data.");
            }
        }

        public Worker(string name, string surname, string fathername, int age, decimal salary, DateTime dateOfEmployment)
        {
            Name = name;
            Surname = surname;
            Fathername = fathername;
            Age = age;
            Salary = salary;
            DateOfEmployment = dateOfEmployment;
        }

        public override string ToString()
        {
            return $"Worker: {Surname} {Name} {Fathername}, Age: {Age}, Salary: {Salary}, Date of employment: {dateOfEmployment.ToShortDateString()}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[5];

            for (int i = 0; i < 5; i++)
            {
                workers[i] = GetWorkerData();
            }

            Array.Sort(workers, (w1, w2) => w1.Surname.CompareTo(w2.Surname));

            Console.Write("Enter the minimum years of work experience: ");
            int experience;
            while (!int.TryParse(Console.ReadLine(), out experience) || experience < 0)
            {
                Console.WriteLine("Invalid format. Please enter a non-negative integer.");
            }

            DateTime today = DateTime.Today;
            Console.WriteLine("Workers with more than {0} years of experience:", experience);
            foreach (Worker worker in workers)
            {
                int workExperience = today.Year - worker.DateOfEmployment.Year;
                if (worker.DateOfEmployment > today.AddYears(-workExperience)) workExperience--;
                if (workExperience > experience)
                {
                    Console.WriteLine(worker.Surname);
                }
            }
        }

        static Worker GetWorkerData()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter worker's name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter worker's surname: ");
                    string surname = Console.ReadLine();

                    Console.Write("Enter worker's fathername: ");
                    string fathername = Console.ReadLine();

                    Console.Write("Enter worker's age: ");
                    int age;
                    while (!int.TryParse(Console.ReadLine(), out age) || age < 18 || age > 65)
                    {
                        Console.WriteLine("Invalid age. Please enter an age between 18 and 65.");
                    }

                    Console.Write("Enter worker's salary: ");
                    decimal salary;
                    while (!decimal.TryParse(Console.ReadLine(), out salary) || salary <= 0)
                    {
                        Console.WriteLine("Invalid salary. Please enter a positive decimal number.");
                    }

                    Console.Write("Enter date of employment (yyyy-mm-dd): ");
                    DateTime dateOfEmployment;
                    while (!DateTime.TryParse(Console.ReadLine(), out dateOfEmployment) || dateOfEmployment >= DateTime.Now)
                    {
                        Console.WriteLine("Invalid date. Please enter a date in the past in yyyy-mm-dd format.");
                    }

                    return new Worker(name, surname, fathername, age, salary, dateOfEmployment);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Data entry error: {ex.Message}");
                }
            }
        }
    }
}

