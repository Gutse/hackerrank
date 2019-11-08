using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class CoinChangeSample
    {
        public Int64[] c;
        public Int32 n;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CoinChangeAnswer
    {
        public Int64 w;
        public override string ToString()
        {
            return w.ToString();
            /*
            if (arr == null)
            {
                return "[]";
            }
            StringBuilder SB = new StringBuilder();
            foreach (var item in arr)
            {
                SB.Append($"{item} ");
            }
            return SB.ToString().Trim();
            */
        }
    }


    class CoinChange : TProblem<CoinChangeSample, CoinChangeAnswer>
    {
        private Random rnd = new Random();


        public override void GenSamples()
        {
            //Samples.Add(new CoinChangeSample() { c = new Int64[] { 1, 2, 3 }, n = 4 });
            //Answers.Add(new CoinChangeAnswer() { w = 4 });

            Samples.Add(new CoinChangeSample() { c = new Int64[] { 2, 5, 3, 6 }, n = 10 });
            Answers.Add(new CoinChangeAnswer() { w = 5 });
        }

        [SolutionMethod]
        public CoinChangeAnswer BruteForce(CoinChangeSample sample) {
            Int64[] c = sample.c;
            Int32 n = sample.n;
            Int64 result = 0;
            Array.Sort(c);

            Int64 MaxCoin = c[c.Length - 1];
            Int64[,] DP = new Int64[c.Length+1, n + 1];
            for (int i = 1; i <= c.Length; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    DP[i, j] = DP[i - 1, j];
                    if (j % c[i - 1] == 0)
                    {
                        DP[i, j] ++;
                    }
                    if (j-c[i-1]>0)
                    {
                        for (int k = 1; k <= j/c[i-1]; k++)
                        {
                            DP[i, j] += DP[i - 1, j - c[i - 1]*k];
                        }
                        
                    }
                    
                }
            }
            result = DP[c.Length, n];
            return new CoinChangeAnswer() { w = result};
        }

        public override bool CheckAnswer(int SampleID, CoinChangeAnswer Answer)
        {
            return Answer.w == Answers[SampleID].w;
            //return base.CheckAnswer(SampleID, Answer);
            /*
             if (Answer?.arr?.Length != Answers?[SampleID]?.arr?.Length)
            {
                return false;
            }

            for (int i = 0; i < Answer.arr.Length; i++)
            {
                if (Answer.arr[i] != Answers[SampleID].arr[i])
                {
                    return false;
                }
            }

            return true; 
             */
        }


    }
}
