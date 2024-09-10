using System;
using System.Collections.Generic;
using System.Linq;

namespace Utopia
{
    class Program
    {
        static void Main()
        {
            List<string> detainedIds = new List<string>();
            string input;

            while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            {
                if (input == "End")
                {
                    break;
                }
                
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;

                string id = parts[parts.Length - 1];
                detainedIds.Add(id);
            }

            string fakeIdEnd = Console.ReadLine();
            if (string.IsNullOrEmpty(fakeIdEnd)) return;
            
            var result = detainedIds.Where(id => id.EndsWith(fakeIdEnd)).ToList();
            
            foreach (var id in result)
            {
                Console.WriteLine(id);
            }
        }
    }
}