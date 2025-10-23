using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _10laba_dz_isrpo_gorishny
{
    public interface IPrintable
    {
        void PrintInfo();
    }

    public class Book : IPrintable
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Книга: '{Title}', автор: {Author}, год: {Year}");
        }
    }

    public class Magazine : IPrintable
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int IssueNumber { get; set; }

        public Magazine(string name, string category, int issueNumber)
        {
            Name = name;
            Category = category;
            IssueNumber = issueNumber;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Журнал: '{Name}', категория: {Category}, номер: {IssueNumber}");
        }
    }

    public interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
        string GetDescription();
    }

    public class Student : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }

        public Student(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }

        public string GetDescription()
        {
            return $"Студент: {Name}, возраст: {Age}, группа: {Group}";
        }
    }

    public class Teacher : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }

        public Teacher(string name, int age, string department)
        {
            Name = name;
            Age = age;
            Department = department;
        }

        public string GetDescription()
        {
            return $"Преподаватель: {Name}, возраст: {Age}, кафедра: {Department}";
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {

            // Задание 1
            var student = new { Name = "Иван Иванов", Age = 20, Group = "ИСРПО-20-1" };

            Console.WriteLine("=== Задание 1 - Анонимные типы ===");
            Console.WriteLine($"Имя: {student.Name}");
            Console.WriteLine($"Возраст: {student.Age}");
            Console.WriteLine($"Группа: {student.Group}");
            Console.WriteLine();

            var students = new[]
            {
                new { Name = "Алексей Воздуханов", Age = 19, Group = "ИСП-228" },
                new { Name = "Артем Жопоренкин", Age = 22, Group = "ЮСП-1488" },
                new { Name = "Андрей Яшкин", Age = 25, Group = "ИСП-228" },
                new { Name = "Роман Водолазов", Age = 18, Group = "ЗСП-1337" },
                new { Name = "Михаил ДЖАМБА", Age = 17, Group = "ЭСП-69" }
            };

            Console.WriteLine("Список студентов:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("| {0,-15} | {1,-8} | {2,-12} |", "Имя", "Возраст", "Группа");
            Console.WriteLine(new string('-', 50));

            foreach (var s in students)
            {
                Console.WriteLine("| {0,-15} | {1,-8} | {2,-12} |", s.Name, s.Age, s.Group);
            }
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();

            // Задание 2

            List<int> numbers = new List<int>();
            for (int i = 1; i <= 20; i++)
            {
                numbers.Add(i);
            }

            Console.WriteLine("Исходный список: " + string.Join(", ", numbers));
            Console.WriteLine();

            List<int> evenNumbers = numbers.FindAll(x => x % 2 == 0);
            Console.WriteLine("Чётные числа: " + string.Join(", ", evenNumbers));

            List<int> multiplesOfThree = numbers.FindAll(x => x % 3 == 0);
            Console.WriteLine("Числа, кратные 3: " + string.Join(", ", multiplesOfThree));

            Console.WriteLine();

            // Задание 3

            List<IPrintable> printableItems = new List<IPrintable>
            {
                new Book("Война и мир", "Лев Толстой", 1869),
                new Magazine("Крутой журнал", "Ваще крутой", 10),
                new Book("Преступление и наказание", "Фёдор Достоевский", 1866),
                new Magazine("Непоседа", "Не помню", 5)
            };

            foreach (var item in printableItems)
            {
                item.PrintInfo();
            }

            Console.WriteLine();

            // Задание 4

            string[] cities = {
                "Москва", "Санкт-Петербург", "Калуга", "Екатеринбург",
                "Казань"
            };

            Console.WriteLine("Исходный массив городов: " + string.Join(", ", cities));
            Console.WriteLine();

            var citiesStartingWithK = from city in cities
                                      where city.StartsWith("К")
                                      select city;

            Console.WriteLine("Города на букву 'К': " + string.Join(", ", citiesStartingWithK));

            var citiesSortedByLength = from city in cities
                                       orderby city.Length
                                       select city;

            Console.WriteLine("Города, отсортированные по длине: " + string.Join(", ", citiesSortedByLength));

            var citiesLongerThan6 = from city in cities
                                    where city.Length > 6
                                    select city;

            Console.WriteLine("Города длиной более 6 символов: " + string.Join(", ", citiesLongerThan6));

            Console.WriteLine();

            // Задание 5

            Console.WriteLine("Ждём данные...");

            int result = await GetDataAsync();

            Console.WriteLine("Данные получены!");
            Console.WriteLine($"Результат: {result}");
            Console.WriteLine();

            // Задание 6

            var studentsList = await LoadStudentsAsync();
            var teachers = new List<Teacher>
            {
                new Teacher("Денис Леонтьев", 38, "Программирование"),
                new Teacher("Ольга Трошина", 38, "Математика"),
                new Teacher("Елена Юханаева", 38, "Базы данных")
            };

            List<IPerson> people = new List<IPerson>();
            people.AddRange(studentsList);
            people.AddRange(teachers);

            Console.WriteLine("Все люди в университете:");
            foreach (var person in people)
            {
                Console.WriteLine(person.GetDescription());
            }
            Console.WriteLine();

            var studentsOver20 = from person in people
                                 where person is Student && person.Age > 20
                                 select person;

            Console.WriteLine("Студенты старше 20 лет:");
            foreach (var studentItem in studentsOver20)
            {
                Console.WriteLine(studentItem.GetDescription());
            }
            Console.WriteLine();

            var programmingTeachers = from person in people
                                      where person is Teacher teacher && teacher.Department == "Программирование"
                                      select person;

            Console.WriteLine("Преподаватели кафедры 'Программирование':");
            foreach (var teacher in programmingTeachers)
            {
                Console.WriteLine(teacher.GetDescription());
            }
            Console.WriteLine();

            var sortedByName = people.OrderBy(p => p.Name).ToList();

            Console.WriteLine("Люди, отсортированные по имени:");
            foreach (var person in sortedByName)
            {
                Console.WriteLine(person.GetDescription());
            }

            Console.WriteLine("\nВсе задания выполнены!");
        }

        static async Task<int> GetDataAsync()
        {
            await Task.Delay(2000);
            return 42;
        }

        static async Task<List<Student>> LoadStudentsAsync()
        {
            await Task.Delay(1500);

            return new List<Student>
            {
                new Student("Алексей Воздуханов", 19, "ИСП-228"),
                new Student("Артем Жопоренкин", 22, "ЮСП-1488"),
                new Student("Андрей Яшкин", 25, "ИСП-228"),
                new Student("Роман Водолазов", 18, "ЗСП-1337"),
                new Student("Михаил ДЖАМБА", 17, "ЭСП-69")
            };
        }
    }
}