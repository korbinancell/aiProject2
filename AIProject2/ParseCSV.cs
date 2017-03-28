using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject2
{
    class ReadCSV
    {
        public struct item
        {
            public string name;
            public double cost;
            public double value;

            public override string ToString()
            {
                return name + "," + Convert.ToString(cost) + "," + Convert.ToString(value) + "\n";
            }
        }

        public Tuple<int, List<item>> readCSV(string name)
        {
            int capacity;
            List<item> knapsack = new List<item>();

            string[] lines = System.IO.File.ReadAllLines(@name);

            capacity = int.Parse(lines[0]);

            foreach (var line in lines.Skip(1))
            {
                var hold = line.Split(',');

                item temp;

                temp.name = hold[0];
                temp.cost = int.Parse(hold[1]);
                temp.value = int.Parse(hold[2]);

                knapsack.Add(temp);
            }

            return Tuple.Create(capacity, knapsack);
        }

        public Tuple<StringBuilder, int> readNurikabe(string name)
        {
            int width;
            StringBuilder puzzle = new StringBuilder();

            string[] lines = System.IO.File.ReadAllLines(@name);

            width = lines[0].Length;

            foreach (var line in lines)
                puzzle.Append(line);

            Console.WriteLine("{0}, {1}", puzzle.ToString(), width);

            return Tuple.Create(puzzle, width);
        }
    }
}
