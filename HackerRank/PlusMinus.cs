using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class tripleanswer {
        public double positives = 0;
        public double nulls = 0;
        public double negatives = 0;
        public override bool Equals(object obj)
        {
            tripleanswer test = obj as tripleanswer;
            if (test == null)
            {
                return false;
            }

            return this.negatives == test.negatives && this.nulls == test.nulls && this.positives == test.positives;
        }


    } 
    public class PlusMinus:TProblem<int[], tripleanswer>
    {
        public PlusMinus() {
            Solution s1 = new Solution(Sol1);
            Solutions.Add(s1);


        }
        private Random rnd = new Random();

        public override void GenSamples()
        {
            Int32 n = rnd.Next(100) + 1;
            Int32[] arr = new Int32[n];

            double positives = 0;
            double nulls = 0;
            double negatives = 0;


            for (int i = 0; i < n; i++)
            {
                Int32 s = rnd.Next(10);
                Int32 newval = rnd.Next();
                if (s > 5)
                {
                    arr[i] = newval;
                    positives++;
                }
                if (s < 5)
                {
                    arr[i] = -newval;
                    negatives++;
                }
                if (s == 5)
                {
                    arr[i] = 0;
                    nulls++;
                }

            }

            Samples.Add(arr);
            tripleanswer answer = new tripleanswer
            {
                negatives = negatives / n,
                positives = positives / n,
                nulls = nulls / n
            };
            Answers.Add(answer);
        }

        tripleanswer Sol1(Int32[] arr) {
            tripleanswer answer = new tripleanswer();
            foreach (var item in arr)
            {
                if (item > 0)
                {
                    answer.positives++;
                }
                if (item == 0)
                {
                    answer.nulls++;
                }
                if (item < 0)
                {
                    answer.negatives++;
                }

            }
            answer.negatives /= arr.Length;
            answer.positives /= arr.Length;
            answer.nulls /= arr.Length;



            return answer;
        }


    }


}
