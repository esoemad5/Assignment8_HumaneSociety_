﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            // PointOfEntry.Run();               
            Query.UpdateShot("Fdlu", Query.GetAnimalByID(2));
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
