using System;
using System.Collections.Generic;

namespace MyProgram;
{
  class Program
  {
    static void Main()
    {
         string line = Console.ReadLine();
         var str = line.Split(" ");
         int a = int.Parse(str[0]);
         int b = int.Parse(str[1]);
         Console.WriteLine(a+b); 

    }
  }
}

