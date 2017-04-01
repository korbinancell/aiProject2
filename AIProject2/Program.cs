using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AIProject2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            /*
            int capacity;
            string filename = "k10.csv";
            List<ReadCSV.item> knapsack;
            ReadCSV parse = new ReadCSV();
            Phase1 knap = new Phase1();

            var that = parse.readCSV(filename);

            capacity = that.Item1;
            knapsack = that.Item2;

            knap.Genenis(capacity, knapsack);
            Console.WriteLine();
            knap.circleOfLife();

            Phase2 nura = new Phase2();

            StringBuilder sb = new StringBuilder();
            sb.Append("0000030600000000010200000");
            nura.Genenis(5, sb);
            nura.circleOfLife();
             */

            Console.WriteLine("Enter filename (nTest2.txt is one already in there)");
            String filename = Console.ReadLine();   //"nTest2.txt";
            ReadCSV parse = new ReadCSV();
            Phase3 nura = new Phase3();
            var puzzle = parse.readNurikabe(filename);

            nura.Genenis(puzzle.Item2, puzzle.Item1, filename);
            nura.circleOfLife();

            Console.ReadKey();
        }
    }
}
