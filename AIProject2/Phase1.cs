using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject2
{
    class Phase1
    {
        private int capacity;
        private int fitnessCounter;
        private int alpacalypseCntr;
        private Random rando;
        private List<ReadCSV.item> knapsack;
        private List<StringBuilder> population;
        private StringBuilder chosenOne;

        public Phase1 ()
        {
            fitnessCounter = alpacalypseCntr = 0;
            rando = new Random();
            population = new List<StringBuilder>();
        }

        private double unnaturalSelection (string input)
        {
            int i = 0;
            double cnt = 0, val = 0;

            foreach (var c in input)
            {
                if( c =='1')
                {
                    cnt += knapsack[i].cost;
                    val += knapsack[i].value;
                }
                i++;
            }

//Console.WriteLine("{0} {1}", cnt > capacity ? 0 : val, cnt > capacity ? 0 : cnt);
            return cnt > capacity ? 0 : val;
        }

        public void Genenis (int cap, List<ReadCSV.item> knap)
        {
            capacity = cap;
            knapsack = knap;

            for (int i=0; i<100; i++)
            {
                StringBuilder temp = new StringBuilder();
                for (int q = 0; q < knap.Count; q++)
                    temp.Append( rando.Next(2));

                population.Add(temp);
            }

            fitnessCounter++;
            population.Sort((a, b) => -1 * unnaturalSelection(a.ToString()).CompareTo(unnaturalSelection(b.ToString())));

foreach(var q in population)
{
    Console.WriteLine("{0} {1}",q.ToString(), unnaturalSelection(q.ToString()));
}
        }

        private StringBuilder stork(StringBuilder a, StringBuilder b)
        {
            int xover = rando.Next(1, b.Length);
            StringBuilder kid = new StringBuilder();
            kid.Append(a.ToString(0, xover+1) + b.ToString(xover, b.Length-1-xover));

            for (int i=0; i<kid.Length; i++)                    //mutate
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
            return kid;
        }

        private void giantMeteor (List<StringBuilder> pop)
        {
            StringBuilder hold = pop[0];

            if (hold.Equals(chosenOne))
                alpacalypseCntr++;
            else
                alpacalypseCntr = 0;

            foreach (var member in pop.Skip(1))
            {
                if ( !member.Equals(hold) )
                    return;
            }

            foreach (var member in pop.Skip(1))
            {
                for (int i=0; i<member.Length; i++)
                {
                    if (rando.Next(5) == 0)
                        member[i] = member[i] == '1' ? '0' : '1';
                }
            }

            chosenOne = hold;

            fitnessCounter++;
            population.Sort((a, b) =>-1 * unnaturalSelection(a.ToString()).CompareTo(unnaturalSelection(b.ToString())));
/*
Console.WriteLine("Sp0lsion\n");
*/
Console.Beep();
        }

        private void endOfDays()
        {
            string knap ="";
            double count = 0, value = 0;

            for (int i=0; i<chosenOne.Length; i++)
            {
                if(chosenOne[i] == '1')
                {
                    knap += knapsack[i].ToString();
                    count += knapsack[i].cost;
                    value += knapsack[i].value;
                }
            }

            Console.WriteLine("Best Knapsack Found:\n{0}", knap);
            Console.WriteLine("Cost: {0} | Value: {1}", count, value);
            Console.Beep();
        }

        public void circleOfLife ()
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();

            while (true)
            {
                int a = Math.Max(rando.Next(100), rando.Next(100));
                int b = Math.Max(rando.Next(100), rando.Next(100));

                population.Add( stork(population[99-a], population[99-b]) );
                fitnessCounter++;
                population.Sort( (x, y) => -1 * unnaturalSelection( x.ToString() ).CompareTo( unnaturalSelection( y.ToString() ) ) );

                population.RemoveAt(100);

                if (population[0].Equals(population[99]))
                    giantMeteor(population);

                if (alpacalypseCntr == 3 || TimeSpan.Compare(timer.Elapsed, new TimeSpan(0, 10, 0)) == 1)
                    break;


            }
            timer.Stop();

            endOfDays();
        }
    }
}
