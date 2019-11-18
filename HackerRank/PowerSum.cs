using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class PowerSumSample: TSample
    {
        public Int32 X;
        public Int32 N;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class PowerSumAnswer: TAnswer
    {
        public Int32 ps;
        public override string ToString()
        {
            return ps.ToString();
        }
        public override Boolean Equals(Object obj)
        {
            return (obj as PowerSumAnswer)?.ps == this.ps;

            
        }
    }


    class PowerSum : TProblem
    {


        public override void AddManualSamples()
        {

            
            Samples.Add(new PowerSumSample() { N = 2, X = 10 });
            Answers.Add(new PowerSumAnswer() { ps = 1 });
            
            Samples.Add(new PowerSumSample() { N = 2, X = 13 });
            Answers.Add(new PowerSumAnswer() { ps = 1 });
           

            Samples.Add(new PowerSumSample() { N = 2, X = 100 });
            Answers.Add(new PowerSumAnswer() { ps = 3 });
            

            Samples.Add(new PowerSumSample() { N = 3, X = 100 });
            Answers.Add(new PowerSumAnswer() { ps = 1 });

            Samples.Add(new PowerSumSample() { N = 2, X = 800 });
            Answers.Add(new PowerSumAnswer() { ps = 561 });
            
        }




        //[SolutionMethod]
        public PowerSumAnswer BruteForce(PowerSumSample sample)
        {
            Int32 N = sample.N;
            Int32 X = sample.X;

            Int32 result = 0;

            //Int32 MaxAddendumCount = (Int32) Math.Ceiling ((Math.Sqrt(8 * X + 1) - 1) / 2);

            List<Int32> Pows = new List<Int32>() { 0, 1 };
            Int32 SummPows = 1;
            Int32 MaxAddendumCount = 2;
            Int32 i = 2;
            while (true)
            {
                Int32 pow = (Int32)Math.Pow(i, N);
                SummPows += pow;
                Pows.Add(pow);
                if (pow >= X)
                {
                    break;
                }

                if (SummPows <= X)
                {
                    MaxAddendumCount++;
                }

                i++;
            }


            for (int j = 1; j <= MaxAddendumCount; j++)
            {
                result += CountAddendums(Pows, j, X, 1);
            }

            return new PowerSumAnswer() { ps = result };


        }

        static Int32 CountAddendums(List<Int32> Addendums, Int32 AddendumsCount, Int32 X, Int32 StartIndex)
        {
            Int32 result = 0;

            if (AddendumsCount == 1)
            {
                if (Addendums.Contains(X))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }


            if (AddendumsCount == 2)
            {
                for (int i = StartIndex; i < Addendums.Count - 1; i++)
                {
                    for (int j = i + 1; j < Addendums.Count; j++)
                    {
                        Int32 Summ = (Addendums[i] + Addendums[j]);
                        if (Summ > X)
                        {
                            break;
                        }
                        if (Summ == X)
                        {
                            return 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = StartIndex; i < Addendums.Count - 1; i++)
                {
                    result += CountAddendums(Addendums, AddendumsCount - 1, X - Addendums[i], i + 1);
                }
            }

            return result;
        }

       
        [SolutionMethod]
        public TAnswer DP(TSample Sample)
        {
            PowerSumSample sample = Sample as PowerSumSample;
            Int32 N = sample.N;
            Int32 X = sample.X;

            Int32 result = 0;
            List<Int32> Pows = new List<Int32>() { 0, 1 };
            Int32 SummPows = 1;
            Int32 MaxAddendumCount = 2;
            Int32 Counter = 2;
            while (true)
            {
                Int32 pow = (Int32)Math.Pow(Counter, N);
                if (pow == X)
                {
                    result++;
                }

                SummPows += pow;
                Pows.Add(pow);

                if (pow >= X)
                {
                    break;
                }

                if (SummPows <= X)
                {
                    MaxAddendumCount++;
                }

                Counter++;
            }

            Int32[,] DP = new Int32[Pows.Count + 1, Pows[Pows.Count - 1] + 1];

            /*
            for (int i = 0; i < Pows.Count; i++)
            {
                DP[1, Pows[i]] = 1;
            }
            */
            
            DP[0, Pows[0]] = 1;

            for (int i = 1; i < Pows.Count; i++)
            {
                for (int j = 0; j <= X; j++)
                {
                    if (Pows[i] <= j)
                    {
                        DP[i, j] = DP[i - 1, j] + DP[i - 1, j - Pows[i]];
                    }
                    else
                    {
                        DP[i, j] = DP[i - 1, j];

                    }

                }
            }


            result = DP[Pows.Count-1, X];

            return new PowerSumAnswer() { ps = result };
        }


    }
}
