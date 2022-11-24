using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class GreedyFloristSample: TSample
    {
        public Int32[] c;
        public Int32 k;
        public Int32 n;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class GreedyFloristAnswer: TAnswer
    {
        public Int32 result;
        public override string ToString()
        {
            return result.ToString();
        }
        public override Boolean Equals(Object obj)
        {
            return (obj as GreedyFloristAnswer)?.result == this.result;
        }
    }


    class GreedyFlorist : TProblem
    {
        private Random rnd = new Random();
        public override void CreateSamples(StreamReader reader)
        {
            string[] nk = reader.ReadLine().Split(' ');

            int n = Convert.ToInt32(nk[0]);

            int k = Convert.ToInt32(nk[1]);

            int[] c = Array.ConvertAll(reader.ReadLine().Split(' '), cTemp => Convert.ToInt32(cTemp));

            GreedyFloristSample sample = new GreedyFloristSample() { c = c, k = k, n = n };
            Samples.Add(sample);

        }

        public override void CreateAnswers(StreamReader reader)
        {
            GreedyFloristAnswer ans = new GreedyFloristAnswer() { result = Convert.ToInt32(reader.ReadLine()) };
            Answers.Add(ans);
        }
        [SolutionMethod]
        public TAnswer Greedy(TSample Sample)
        {
            GreedyFloristSample sample = Sample as GreedyFloristSample;
            Int32 n = sample.n;
            Int32 k = sample.k;
            Int32[] c = sample.c;
            Int32 result = 0;

            Array.Sort(c);

            if (k >= n)
            {
                for (int i = 0; i < n; i++)
                {
                    result += c[i];
                }
            }
            else {

                
                for (int i = c.Length-1; i >= 0; i--)
                {
                    Int32 mult = (c.Length-1 - i) / k +1;
                    
                    result += mult * c[i];
                }

            }


            return new GreedyFloristAnswer() { result = result };
        }

        


    }
}
