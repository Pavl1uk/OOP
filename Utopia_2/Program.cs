using System;
using System.Collections.Generic;

interface IBirthable
{
    string BirthDate { get; }
}

class Citizen : IBirthable
{
    public string Name { get; }
    public int Age { get; }
    public string Id { get; }
    public string BirthDate { get; }

    public Citizen(string name, int age, string id, string birthDate)
    {
        Name = name;
        Age = age;
        Id = id;
        BirthDate = birthDate;
    }
}

class Robot
{
    public string Model { get; }
    public string Id { get; }

    public Robot(string model, string id)
    {
        Model = model;
        Id = id;
    }
}

class Pet : IBirthable
{
    public string Name { get; }
    public string BirthDate { get; }

    public Pet(string name, string birthDate)
    {
        Name = name;
        BirthDate = birthDate;
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
            string[] parts = input.Split(' ');
            string type = parts[0];

            if (type == "Citizen")
            {
                string name = parts[1];
                int age = int.Parse(parts[2]);
                string id = parts[3];
                string birthDate = parts[4];
                Citizen citizen = new Citizen(name, age, id, birthDate);
                birthables.Add(citizen);
            }
            else if (type == "Pet")
            {
                string name = parts[1];
                string birthDate = parts[2];
                Pet pet = new Pet(name, birthDate);
                birthables.Add(pet);
            }
        }

        string year = Console.ReadLine();
        
        foreach (var birthable in birthables)
        {
            if (birthable.BirthDate.EndsWith(year))
            {
                Console.WriteLine(birthable.BirthDate);
            }
        }
    }
}
