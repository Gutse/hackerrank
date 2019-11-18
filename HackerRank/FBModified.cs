using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{
    class FBModifiedSample: TSample
    {
        public int t1;
        public int t2;
        public int n;
    }

    public class FBModifiedAnswer : TAnswer
    {

        public String result;
        public override string ToString()
        {
            return base.ToString();
        }

        public override Boolean Equals(Object obj)
        {
            return (obj as FBModifiedAnswer)?.result == this.result;
        }
    }

    class FBModified : TProblem
    {
        public override void CreateSamples(StreamReader reader)
        {
            string[] t1T2n = reader.ReadLine().Split(' ');

            int t1 = Convert.ToInt32(t1T2n[0]);

            int t2 = Convert.ToInt32(t1T2n[1]);

            int n = Convert.ToInt32(t1T2n[2]);
            Samples.Add(new FBModifiedSample() {t1 = t1, t2 = t2, n = n });
        }

        public override void CreateAnswers(StreamReader reader)
        {
            Answers.Add(new FBModifiedAnswer() { result = reader.ReadLine()});

        }

        [SolutionMethod]
        public TAnswer Solution1(TSample Sample)
        {

            FBModifiedSample sample = Sample as FBModifiedSample;

            Int32 n = sample.n;
            Int32 t1 = sample.t1;
            Int32 t2 = sample.t2;


            BigInteger[] F = new BigInteger[n];

            F[0] = t1;
            F[1] = t2;

            for (int i = 2; i < n; i++)
            {
                F[i] = F[i - 2] + F[i - 1] * F[i - 1];
            }

            return new FBModifiedAnswer() { result =  F[n - 1].ToString() };
        }

        public static String Solution2(FBModifiedSample sample)
        {
            return "";
        }


    }
}
