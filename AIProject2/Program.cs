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
            string filename = "k05.csv";
            List<ReadCSV.item> knapsack;
            ReadCSV parse = new ReadCSV();
            Phase1 knap = new Phase1();

            var that = parse.readCSV(filename);

            capacity = that.Item1;
            knapsack = that.Item2;

            knap.Genenis(capacity, knapsack);
            Console.WriteLine();
            knap.circleOfLife();
            */

            StringBuilder input = new StringBuilder();
            input.Append("1101111111100011000111111");
            int boardWidth = 5;
            int pointCntr = 0;
            StringBuilder hold = new StringBuilder();
            Queue<int> quayquay = new Queue<int>();
            hold.Append(input.ToString());
            /*
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1'
                    && !(i % boardWidth == boardWidth - 1 || i > input.Length - 1 - boardWidth)
                    && input[i + 1] == '1'
                    && input[i + 5] == '1'
                    && input[i + 6] == '1')

                    pointCntr++;
            }*/
            List<Tuple<int, int>> gardens = new List<Tuple<int, int>>();
            gardens.Add(Tuple.Create(2,3));
            gardens.Add(Tuple.Create(17,6));

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1'
                    && !(i % boardWidth == boardWidth - 1 || i > input.Length - 1 - boardWidth)
                    && input[i + 1] == '1'
                    && input[i + 5] == '1'
                    && input[i + 6] == '1')

                    pointCntr++;
            }

            int q;
            for (q = 0; q < input.Length; q++)
                if (hold[q] == '1')
                    break;

            quayquay.Enqueue(q);

            while (quayquay.Count != 0)
            {
                q = quayquay.Dequeue();

                if (!(q % boardWidth == boardWidth - 1) && hold[q + 1] == '1')
                    quayquay.Enqueue(q + 1);
                if (!(q % boardWidth == 0) && hold[q - 1] == '1')
                    quayquay.Enqueue(q - 1);
                if (!(q < boardWidth) && hold[q - boardWidth] == '1')
                    quayquay.Enqueue(q - boardWidth);
                if (!(q > input.Length - 1 - boardWidth) && hold[q + boardWidth] == '1')
                    quayquay.Enqueue(q + boardWidth);

                hold[q++] = '0';
            }

            for (int i = 0; i < input.Length; i++)
                if (hold[i] == '1')
                {
                    pointCntr++;
                    break;
                }

            quayquay = null;
            quayquay = new Queue<int>();

            foreach (var garden in gardens)
            {
                quayquay.Enqueue(garden.Item1);
                hold = null;
                hold = new StringBuilder();
                hold.Append(input.ToString());

                while (quayquay.Count != 0)
                {
                    q = quayquay.Dequeue();

                    if (!(q % boardWidth == boardWidth - 1) && hold[q + 1] == '0')              // ->
                        quayquay.Enqueue(q + 1);
                    if (!(q % boardWidth == 0) && hold[q - 1] == '0')                           // <-
                        quayquay.Enqueue(q - 1);
                    if (!(q < boardWidth) && hold[q - boardWidth] == '0')                       // ^
                        quayquay.Enqueue(q - boardWidth);
                    if (!(q > input.Length - 1 - boardWidth) && hold[q + boardWidth] == '0')    // v
                        quayquay.Enqueue(q + boardWidth);

                    hold[q] = '~';
                }

                int gardenCnt = 0;

                for (int i = 0; i < hold.Length; i++)
                {
                    if (hold[i] == '~')
                    {
                        gardenCnt++;
                        if (i != garden.Item1 && gardens.Exists(x => x.Item1 == i))
                            pointCntr++;
                    }
                }

                Console.WriteLine(hold.ToString(0, 5));
                Console.WriteLine(hold.ToString(5, 5));
                Console.WriteLine(hold.ToString(10, 5));
                Console.WriteLine(hold.ToString(15, 5));
                Console.WriteLine(hold.ToString(20, 5));
                Console.WriteLine();

                pointCntr += garden.Item2 > gardenCnt ? (garden.Item2 - gardenCnt) * 2 : gardenCnt - garden.Item2;
            }

            Console.WriteLine(pointCntr);
            /*
            Console.Beep(658, 125);
            Console.Beep(1320, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 250);
            Console.Beep(1320, 125);
            Console.Beep(1188, 125);
            Console.Beep(1056, 250);
            Console.Beep(990, 250);
            Console.Beep(880, 500);
            Console.Beep(880, 250);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 750);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(250);
            Console.Beep(1188, 500);
            Console.Beep(1408, 250);
            Console.Beep(1760, 500);
            Console.Beep(1584, 250);
            Console.Beep(1408, 250);
            Console.Beep(1320, 750);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(500);
            Console.Beep(1320, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 250);
            Console.Beep(1320, 125);
            Console.Beep(1188, 125);
            Console.Beep(1056, 250);
            Console.Beep(990, 250);
            Console.Beep(880, 500);
            Console.Beep(880, 250);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 750);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(250);
            Console.Beep(1188, 500);
            Console.Beep(1408, 250);
            Console.Beep(1760, 500);
            Console.Beep(1584, 250);
            Console.Beep(1408, 250);
            Console.Beep(1320, 750);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(500);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 1000);
            Console.Beep(440, 1000);
            Console.Beep(419, 1000);
            Console.Beep(495, 1000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 500);
            Console.Beep(660, 500);
            Console.Beep(880, 1000);
            Console.Beep(838, 2000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 1000);
            Console.Beep(440, 1000);
            Console.Beep(419, 1000);
            Console.Beep(495, 1000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 500);
            Console.Beep(660, 500);
            Console.Beep(880, 1000);
            Console.Beep(838, 2000);
            */
        }
    }
}
