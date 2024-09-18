using System;
using System.Collections.Generic;

interface IBirthable
{
    string BirthDate { get; }
}

interface IBuyer
{
    int Food { get; }
    void BuyFood();
}

class Citizen : IBirthable, IBuyer
{
    public string Name { get; }
    public int Age { get; }
    public string Id { get; }
    public string BirthDate { get; }
    public int Food { get; private set; }

    public Citizen(string name, int age, string id, string birthDate)
    {
        Name = name;
        Age = age;
        Id = id;
        BirthDate = birthDate;
        Food = 0;
    }

    public void BuyFood()
    {
        Food += 10;
    }
}

class Rebel : IBuyer
{
    public string Name { get; }
    public int Age { get; }
    public string Group { get; }
    public int Food { get; private set; }

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

class Program
{
    static void Main()
    {
        Dictionary<string, IBuyer> buyers = new Dictionary<string, IBuyer>();
        int n = int.Parse(Console.ReadLine());
        
        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split(' ');

            if (parts.Length == 4)
            {
                string name = parts[0];
                int age = int.Parse(parts[1]);
                string id = parts[2];
                string birthDate = parts[3];

                Citizen citizen = new Citizen(name, age, id, birthDate);
                buyers[name] = citizen;
            }
            else if (parts.Length == 3)
            {
                string name = parts[0];
                int age = int.Parse(parts[1]);
                string group = parts[2];

                Rebel rebel = new Rebel(name, age, group);
                buyers[name] = rebel;
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
        
        int totalFood = 0;
        foreach (var buyer in buyers.Values)
        {
            totalFood += buyer.Food;
        }
        
        Console.WriteLine(totalFood);
    }
}
