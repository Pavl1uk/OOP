using System;
using System.Collections.Generic;
using System.Linq;

namespace Utopia_2
{
    public interface IBirthable
    {
        string Birthdate { get; }
    }
    public class Citizen : IBirthable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public string Birthdate { get; set; }

        public Citizen(string name, int age, string id, string birthdate)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
        }
    }
    public class Robot
    {
        public string Model { get; set; }
        public string Id { get; set; }

        public Robot(string model, string id)
        {
            Model = model;
            Id = id;
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
    class Program
    {
        static void Main()
        {
            List<IBirthable> birthables = new List<IBirthable>();
            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;
                
                if (parts[0] == "Citizen")
                {
                    string name = parts[1];
                    int age = int.Parse(parts[2]);
                    string id = parts[3];
                    string birthdate = parts[4];
                    birthables.Add(new Citizen(name, age, id, birthdate));
                }
                else if (parts[0] == "Robot")
                {
                    string model = parts[1];
                    string id = parts[2];
                }
                else if (parts[0] == "Pet")
                {
                    string name = parts[1];
                    string birthdate = parts[2];
                    birthables.Add(new Pet(name, birthdate));
                }
            }

            string year = Console.ReadLine();
            
            var result = birthables.Where(b => b.Birthdate.EndsWith(year)).Select(b => b.Birthdate).ToList();
            
            foreach (var birthdate in result)
            {
                Console.WriteLine(birthdate);
            }
        }
    }
}
