using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_1
{
    class Person : IComparable<Person>
    {
        public List<int> Gens;
        public int survCoef;
        public Person()
        {
            Gens = new List<int>();
            survCoef = -1;
        }
        int IComparable<Person>.CompareTo(Person other)
        {
            return this.survCoef.CompareTo(other.survCoef);
        }
        public void print()
        {
            foreach (var item in Gens)
            {
                Console.Write(item + " ");
            }
        }
    }

    static class GeneticAlgo
    {
        public static List<Person> Populat = new List<Person>();
        
        public static void GA_Start()
        {
            int sizeOfPersonGens = 3;
            int amountOfPersons = 10;

            Random r = new Random();

            for (int i = 0; i < amountOfPersons; i++)
            {
                Person tmp = new Person();
                for (int j = 0; j < sizeOfPersonGens; j++)
                    tmp.Gens.Add(r.Next(0, 10));
                FitnessFunc(tmp);
                Populat.Add(tmp);
            }
            Populat.Sort();
            int counter = 1;
            while (Populat[0].survCoef != 0)
            {
                LoopOfGA();
                counter++;
            }

            Populat[0].print();
            Console.WriteLine("  -  "+ Populat[0].survCoef);

            Console.WriteLine("iterations amount : "+counter);
        }

        public static void LoopOfGA()
        {
            for (int i = 3; i < 7; i+=2)
                Crossover(i);
            for (int i = 7; i < 10; i++)
                Mutate(i);
            Populat.Sort();
        }

        public static void Mutate(int exp)
        {
            Random r = new Random();
            Populat[exp].Gens[r.Next(0, Populat[exp].Gens.Count)] = r.Next(0, 10);
            FitnessFunc(Populat[exp]);
        }

        public static void Crossover(int exp)
        {        
            int crossIndex = 1;
            int AmountOfGens = Populat[0].Gens.Count;
          
            List<int> tmp = Populat[exp].Gens.GetRange(crossIndex , AmountOfGens - crossIndex );

            for (int i = crossIndex; i < AmountOfGens; i++)
                Populat[exp].Gens[i] = Populat[exp + 1].Gens[i];

            Populat[exp + 1].Gens.RemoveRange(crossIndex, AmountOfGens - crossIndex);
            Populat[exp + 1].Gens.AddRange(tmp);
        }

        public static void FitnessFunc(Person exp)
        {
            // x = 1    y = 2   z = 9

            //  3x + 6y^2 +9z = 108
            //  x + y + z = 12
            //  y^2 = 4
            //  ( x + z )*2 = 20

            exp.survCoef = Math.Abs( 108 - 3 * exp.Gens[0] - 6 * (int)Math.Pow(exp.Gens[1], 2) - 9 * exp.Gens[2] ) +
                    Math.Abs(12 - exp.Gens[0]- exp.Gens[1] - exp.Gens[2] ) +
                    Math.Abs(4 - (int)Math.Pow(exp.Gens[1], 2)) +
                    Math.Abs(20 - (exp.Gens[0] + exp.Gens[2]) * 2);
        } 
    }

    class Program
    {
        static List<Person> Population = new List<Person>();

        static void Main(string[] args)
        {
            GeneticAlgo.GA_Start();
        }
    }
}
