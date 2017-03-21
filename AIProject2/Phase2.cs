using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject2
{
    class Phase2
    {
        private int fitnessCounter;
        private int alpacalypseCntr;
        private int boardWidth;
        private Random rando;
        private StringBuilder inputBoard;
        private List<Tuple<StringBuilder,int>> population;
        private List<Tuple<int,int>> gardens;
        private Tuple<StringBuilder, int> chosenOne;

        private int cntr;

        public Phase2()
        {
            fitnessCounter = alpacalypseCntr = 0;
            rando = new Random();
            population = new List<Tuple<StringBuilder,int>>();
            gardens = new List<Tuple<int, int>>();

            cntr = 0;
        }

        private int unnaturalSelection(StringBuilder input)
        {
            /* 2x2 square +1 :)
             * stranded filled +2 :)
             * touching gardens +2 :)
             * gardens wrong size +difference if over x1  if smaller x2 :)
             *
             * Things for later?
             * counts 2x2 squares separately
            */

            int pointCntr = 0;
            StringBuilder hold = new StringBuilder();
            Queue<int> quayquay = new Queue<int>();
            hold.Append(input.ToString());

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
                    pointCntr += 2;
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
                /*
                Console.WriteLine(hold.ToString(0, 5));
                Console.WriteLine(hold.ToString(5, 5));
                Console.WriteLine(hold.ToString(10, 5));
                Console.WriteLine(hold.ToString(15, 5));
                Console.WriteLine(hold.ToString(20, 5));
                Console.WriteLine();
                */
                pointCntr += garden.Item2 > gardenCnt ? (garden.Item2 - gardenCnt) * 2 : gardenCnt - garden.Item2;
            }

            //Console.WriteLine("{0} {1}", cnt > capacity ? 0 : val, cnt > capacity ? 0 : cnt);
            fitnessCounter++;
            return pointCntr;
        }

        public void Genenis(int width, StringBuilder board)
        {
            inputBoard = board;
            boardWidth = width;

            for (int i=0; i< board.Length; i++)
            {
                if (inputBoard[i] != '0')
                {
                    gardens.Add(Tuple.Create(i, inputBoard[i] - '0'));
                }
            }

            foreach (var gen in gardens)
                Console.WriteLine(gen);

            for (int i = 0; i < 100; i++)
            {
                StringBuilder temp = new StringBuilder();
                for (int q = 0; q < board.Length; q++)
                    temp.Append(rando.Next(2));

                foreach (var gard in gardens)
                    temp[gard.Item1] = '0';

                population.Add(Tuple.Create(temp, unnaturalSelection(temp)));
            }

            population.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            foreach (var q in population)
            {
                Console.WriteLine("{0}", q.ToString());
            }
            Console.WriteLine();
        }

        private Tuple<StringBuilder, int> stork(StringBuilder a, StringBuilder b)
        {
            int xover = rando.Next(1, b.Length);
            StringBuilder kid = new StringBuilder();
            kid.Append(a.ToString(0, xover + 1) + b.ToString(xover, b.Length - 1 - xover));

            for (int i = 0; i < kid.Length; i++)                    //mutate
            {
                if (rando.Next(200) == 0)
                    kid[i] = kid[i] == '1' ? '0' : '1';
            }
/*
Console.WriteLine("{0} {1}",kid.ToString(), unnaturalSelection(kid.ToString()));

blep++;
if(blep == 30)
{
        Console.WriteLine("---------------------------");
    foreach (var q in population)
    {
        Console.WriteLine("{0} {1}", q.ToString(), unnaturalSelection(q.ToString()));
    }
    Console.WriteLine("---------------------------");
                blep = 0;
}
*/
            foreach (var gard in gardens)
                inputBoard[gard.Item1] = '0';

            return Tuple.Create(kid, unnaturalSelection(kid));
        }

        private void giantMeteor(List<Tuple<StringBuilder,int>> pop)
        {
            var hold = pop[0];

            if (!object.ReferenceEquals(chosenOne,null) && hold.Item1.Equals(chosenOne.Item1))
                alpacalypseCntr++;
            else
                alpacalypseCntr = 0;

            foreach (var member in pop.Skip(1))
            {
                if (!member.Item1.Equals(hold.Item1))
                    return;
            }

            foreach (var member in pop.Skip(1))
            {
                for (int i = 0; i < member.Item1.Length; i++)
                {
                    if (rando.Next(5) == 0)
                        member.Item1[i] = member.Item1[i] == '1' ? '0' : '1';
                }
            }

            chosenOne = hold;

            population.Sort((a, b) => a.Item2.CompareTo(b.Item2));
/*
Console.WriteLine("Sp0lsion\n");
*/
            Console.Beep();
        }

        private void endOfDays()
        {
            Console.WriteLine(chosenOne.Item1);
            Console.Beep(1408, 250);
        }

        public void circleOfLife()
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();

            while (true)
            {
                if (population[0].Item2 == 0)
                {
                    chosenOne = population[0];
                    endOfDays();
                }
                int a = Math.Max(rando.Next(100), rando.Next(100));
                int b = Math.Max(rando.Next(100), rando.Next(100));

                population.Add(stork(population[99 - a].Item1, population[99 - b].Item1));

                if(cntr++ == 100)
                {
                    cntr = 0;
                    Console.WriteLine(population[0]);
                }

                population.Sort((x, y) =>x.Item2.CompareTo(y.Item2));

                population.RemoveAt(100);

                if (population[0].Item1.Equals(population[99].Item1))
                    giantMeteor(population);

                if (alpacalypseCntr == 3 || TimeSpan.Compare(timer.Elapsed, new TimeSpan(0, 10, 0)) == 1)
                    break;


            }
            timer.Stop();

            Console.WriteLine(Convert.ToString(timer.Elapsed));

            endOfDays();
        }
    }
}
