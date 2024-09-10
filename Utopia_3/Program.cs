using System;
using System.Collections.Generic;
using System.Linq;

namespace Utopia_3
{
    public interface IBuyer
    {
        int Food { get; set; }
        void BuyFood();
    }
    public class Citizen : IBirthable, IBuyer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public string Birthdate { get; set; }
        public int Food { get; set; }

        public Citizen(string name, int age, string id, string birthdate)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
            Food = 0;
        }

        public void BuyFood()
        {
            Food += 10;
        }
    }
    public class Rebel : IBuyer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }
        public int Food { get; set; }

        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
            Food = 0;
        }

        public void BuyFood()
        {
            Food += 5;
        }
    }
    public class Pet : IBirthable
    {
        public string Name { get; set; }
        public string Birthdate { get; set; }

        public Pet(string name, string birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }
    }
    public interface IBirthable
    {
        string Birthdate { get; }
    }

    class Program
    {
        static void Main()
        {
            Dictionary<string, IBuyer> buyers = new Dictionary<string, IBuyer>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] parts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;
                
                if (parts.Length == 4)
                {
                    string name = parts[0];
                    int age = int.Parse(parts[1]);
                    string id = parts[2];
                    string birthdate = parts[3];
                    buyers[name] = new Citizen(name, age, id, birthdate);
                }
                else if (parts.Length == 3)
                {
                    string name = parts[0];
                    int age = int.Parse(parts[1]);
                    string group = parts[2];
                    buyers[name] = new Rebel(name, age, group);
                }
            }

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                if (buyers.ContainsKey(input))
                {
                    buyers[input].BuyFood();
                }
            }

            int totalFood = buyers.Values.Sum(b => b.Food);
            Console.WriteLine(totalFood);
        }
    }
}
