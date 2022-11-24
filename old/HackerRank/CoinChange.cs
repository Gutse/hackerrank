using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class CoinChangeSample:TSample
    {
        public Int64[] c;
        public Int32 n;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CoinChangeAnswer:TAnswer
    {
        public Int64 w;
        public override string ToString()
        {
            return w.ToString();
        }

        public override Boolean Equals(Object obj)
        {
            CoinChangeAnswer Answer = obj as CoinChangeAnswer;
            return Answer.w == this.w;
        }

    }


    class CoinChange : TProblem
    {
        public override void CreateSamples(System.IO.StreamReader reader)
        {
            string[] nm = reader.ReadLine().Split(' ');

            int n = Convert.ToInt32(nm[0]);

            int m = Convert.ToInt32(nm[1]);

            Int64[] coins = Array.ConvertAll(reader.ReadLine().Split(' '), coinsTemp => Convert.ToInt64(coinsTemp));
            Samples.Add(new CoinChangeSample() { c= coins, n = n} );
        }

        public override void CreateAnswers(System.IO.StreamReader reader)
        {
            Answers.Add(new CoinChangeAnswer() { w = Convert.ToInt64(reader.ReadLine()) });
        }

        [SolutionMethod]
        public TAnswer DP(TSample Sample) {
            CoinChangeSample sample = Sample as CoinChangeSample;

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

    }
}
