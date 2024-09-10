
using System;

namespace Telephony
{
interface ICallable
{
    void Call(string phoneNumber);
}

interface IBrowsable
{
    void Browse(string url);
}

class Smartphone : ICallable, IBrowsable
{
    public void Call(string phoneNumber)
    {
        foreach (char c in phoneNumber)
        {
            if (!char.IsDigit(c))
            {
                Console.WriteLine("Invalid number!");
                return;
            }
        }
        Console.WriteLine($"Calling... {phoneNumber}");
    }

    public void Browse(string url)
    {
        Console.WriteLine($"Browsing: {url}!");

        foreach (char c in url)
        {
            if (char.IsDigit(c))
            {
                Console.WriteLine("Invalid URL!");
                return;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        string[] phoneNumbers = Console.ReadLine().Split(' ');
        string[] urls = Console.ReadLine().Split(' ');

        Smartphone smartphone = new Smartphone();

        foreach (var number in phoneNumbers)
        {
            smartphone.Call(number);
        }

        foreach (var url in urls)
        {
            smartphone.Browse(url);
        }
    }
}
}