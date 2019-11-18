using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class SpecialStringAgainSample: TSample
    {
        public String s;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class SpecialStringAgainAnswer: TAnswer
    {
        public Int64 result;
        public override string ToString()
        {
            return result.ToString();
        }
        public override Boolean Equals(Object obj)
        {
            return (obj as SpecialStringAgainAnswer)?.result == this.result;

        }
    }


    class SpecialStringAgain : TProblem
    {
        public override void CreateSamples(System.IO.StreamReader reader)
        {
            reader.ReadLine();
            SpecialStringAgainSample sample = new SpecialStringAgainSample() { s = reader.ReadLine() };
            Samples.Add(sample);

        }

        public override void CreateAnswers(System.IO.StreamReader reader)
        {
            SpecialStringAgainAnswer ans = new SpecialStringAgainAnswer() { result = Convert.ToInt64(reader.ReadLine()) };
            Answers.Add(ans);

        }
       

        [SolutionMethod]
        public TAnswer DP(TSample Sample)
        {
            SpecialStringAgainSample sample = Sample as SpecialStringAgainSample;

            String s = sample.s;
            Int64 result = 0;
            Int64[] F = new Int64[s.Length+1];
            F[0] = 1;
            F[1] = 1;
            Char CurrentChar = s[0];
            Int64 cc = 1;
            Boolean OneChar = true;
            for (int i = 1; i < s.Length; i++)
            {
                F[i+1] = F[i] + i+1;
                if (s[i] == CurrentChar)
                {
                    cc++;
                }
                else
                {
                    OneChar = false;
                    int j = i + 1;
                    result += F[cc];

                    while (j < s.Length && s[j] == CurrentChar)
                    {
                        j++;
                    }

                    int cc2 = j - i-1;
                    if (cc2 > 0)
                    {
                        result += Math.Min(cc, cc2);
                    }
                    CurrentChar = s[i];
                    cc = 1;
                }
            }

            if (OneChar)
            {
                result = F[s.Length];
            }
            else {
                result += F[cc];
            }


            return new SpecialStringAgainAnswer() { result = result };
        }

        


    }
}
