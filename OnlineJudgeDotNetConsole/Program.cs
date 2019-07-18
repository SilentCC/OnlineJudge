using System;
using System.Collections.Generic;

namespace MyTestBenchMarks
{
    class Program
    {
        static void Main()
        {
            string line = Console.ReadLine();

            var str = line.Split(" ");
            
            int a = Int32.Parse(str[0]);
            int b = Int32.Parse(str[1]);
            Console.WriteLine(a+b);
        }
    }
}

