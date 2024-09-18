using System;
using System.Collections.Generic;

namespace Utopia
{
    class Program
    {
        static void Main()
        {
            List<string> ids = new List<string>();
            string input = Console.ReadLine();

            while (input != "End")
            {
                string[] parts = input.Split(' ');
                string id = parts[parts.Length - 1];
                ids.Add(id);
                input = Console.ReadLine();
            }

            string fakeIdEnd = Console.ReadLine();

            foreach (string id in ids)
            {
                if (id.EndsWith(fakeIdEnd))
                {
                    Console.WriteLine(id);
                }
            }
        }
    }
}