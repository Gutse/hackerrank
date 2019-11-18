using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class FairCutSample : TSample
    {
        public Int64[] arr;
        public Int64 k;
    }

    public class FairCutAnswer : TAnswer
    {
        public Int64 result;
        public override string ToString()
        {
            return base.ToString();
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

        public override Boolean Equals(Object obj)
        {
            return (obj as FairCutAnswer)?.result == this.result;

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
            */
        }
       
    }

    class FairCut : TProblem
    {


        public override void CreateSamples(StreamReader reader)
        {
            string[] nk = reader.ReadLine().Split(' ');

            Int64 n = Convert.ToInt64(nk[0]);

            Int64 k = Convert.ToInt64(nk[1]);

            Int64[] arr = Array.ConvertAll(reader.ReadLine().Split(' '), arrTemp => Convert.ToInt64(arrTemp));

            Samples.Add(new FairCutSample() { arr = arr, k = k});
        }

        public override void CreateAnswers(StreamReader reader)
        {
            FairCutAnswer ans = new FairCutAnswer() { };
            Int64.TryParse(reader.ReadLine(), out ans.result);
            Answers.Add(ans);
        }






        public static void PrintDP(Int64[,] dp, Int64[] arr)
        {
            Console.Write("    ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.WriteLine("");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}:  ");

                for (int j = 0; j <= arr.Length; j++)
                {
                    if (j == arr.Length)
                    {
                        Console.Write($" | ");
                    }
                    Console.Write($"{dp[i, j]} ");
                }
                Console.WriteLine();

            }

        }

        //[SolutionMethod]
        public FairCutAnswer Solution1(TSample Sample)
        {

            FairCutSample sample = Sample as FairCutSample;

            Int64[] arr = sample.arr;
            Int64 k = sample.k;

            Array.Sort(arr, new Comparison<Int64>((x, y) => y.CompareTo(x)));

            Int64[,] Diffs = new Int64[arr.Length, arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    Diffs[i, j] = Math.Abs(arr[i] - arr[j]);
                }
            }



            Int64[] Taken = new Int64[k];
            Int64[] Remains = new Int64[arr.Length - k];

            for (int i = 0; i < k; i++)
            {
                Taken[i] = i;
            }

            for (int i = 0; i < arr.Length - k; i++)
            {
                Remains[i] = i + k;
            }

            Int64 CurrentDiff = SetDiffs(Taken, Remains, Diffs);


            for (int i = 0; i < k; i++)
            {

                for (int j = 0; j < arr.Length - k; j++)
                {
                    Int64 t = Taken[i];
                    Taken[i] = Remains[j];
                    Remains[j] = t;

                    Int64 NewDiff = SetDiffs(Taken, Remains, Diffs);
                    if (NewDiff >= CurrentDiff)
                    {
                        Remains[j] = t;
                        Remains[j] = Taken[i];
                        Taken[i] = t;
                    }
                    else
                    {
                        CurrentDiff = NewDiff;
                    }
                }
            }


            return new FairCutAnswer() { result = CurrentDiff };

        }


        static Int64 SetDiffs(Int64[] Taken, Int64[] Remains, Int64[,] diffs)
        {
            Int64 res = 0;
            foreach (var t in Taken)
            {
                foreach (var r in Remains)
                {
                    res += diffs[t, r];
                }

            }
            return res;
        }
        static Int64 SetDiffs(List<Int64> Taken, List<Int64> Remains, Int64[,] diffs)
        {
            Int64 res = 0;
            foreach (var t in Taken)
            {
                foreach (var r in Remains)
                {
                    res += diffs[t, r];
                }

            }
            return res;
        }

        //[SolutionMethod]
        public Int64 Straight(FairCutSample sample)
        {

            Int64[] arr = sample.arr;
            Int64 k = sample.k;



            return FCut(k, new List<Int64>(), arr.ToList());
        }

        public Int64 FCut(Int64 k, List<Int64> left, List<Int64> right)
        {


            if (k == left.Count)
            {
                return CountCut(left, right);
            }


            Int64 MinCut = Int64.MaxValue;
            for (int i = 0; i < right.Count; i++)
            {
                List<Int64> nl = new List<Int64>(left);
                List<Int64> nr = new List<Int64>(right);
                nl.Add(right[i]);
                nr.RemoveAt(i);

                Int64 X = FCut(k, nl, nr);
                if (nl.Count == k)
                {
                    /*
                    Console.Write($"FCut(");
                    foreach (var item in nl)
                    {
                        Console.Write($"{item}");
                    }
                    Console.WriteLine($") = {X}");
                    */



                }
                if (X < MinCut)
                {
                    MinCut = X;
                }


            }
            return MinCut;
        }

        Int64 CountCut(List<Int64> left, List<Int64> right)
        {
            Int64 res = 0;
            foreach (var li in left)
            {
                foreach (var ri in right)
                {
                    res += Math.Abs(li - ri);
                }
            }
            return res;
        }


        //[SolutionMethod]
        public Int64 Greedy(FairCutSample sample)
        {

            Int64[] arr = sample.arr;
            Int64 k = sample.k;

            if (k > (arr.Length / 2))
            {
                k = arr.Length - k;
            }


            Array.Sort(arr);


            Int64[,] Diffs = new Int64[arr.Length, arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    Diffs[i, j] = Math.Abs(arr[i] - arr[j]);
                }
            }

            /*
            Int64 MassCenter = arr.Length / 2;

            List<Int64> Taken = new List<int>() { MassCenter };
            List<Int64> Remains = new List<int>();
            bool[] takes = new bool[arr.Length];
            takes[MassCenter] = true;
            Int64 distance = -2;
            for (int i = 0; i < k; i++)
            {
                Int64 toTake = Math.Min(MassCenter + distance, arr.Length-1);
                Taken.Add(toTake);
                takes[toTake] = true;
                if (distance > 0)
                {
                    distance *= -1;

                }
                else {
                    distance *= -1;
                    distance += 2;

                }
                if (Taken.Count == k)
                {
                    break;
                }

            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (!takes[i])
                {
                    Remains.Add(i);
                }
            }


            Int64 d = SetDiffs(Taken, Remains, Diffs);
            return d;*/

            Boolean[] takes = new Boolean[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                takes[i] = false;
            }

            Int64 len01 = k * 2;

            Int64 Start01 = (arr.Length - len01) / 2;
            Boolean take = false;
            for (int i = 0; i < k * 2; i++)
            {
                takes[i + Start01] = take;
                take = !take;
            }

            List<Int64> Taken = new List<Int64>();
            List<Int64> Remains = new List<Int64>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (takes[i])
                {
                    Taken.Add(i);
                }
                else
                {
                    Remains.Add(i);
                }
            }
            Int64 d = SetDiffs(Taken, Remains, Diffs);
            return d;
        }

        [SolutionMethod]
        public TAnswer DP(TSample Sample)
        {
            FairCutSample sample = Sample as FairCutSample;

            Int64[] arr = sample.arr;
            Int64 k = sample.k;


            /*
            if (k > (arr.Length / 2))
            {
                k = arr.Length - k;
            }
            */

            Int64 n = arr.Length;

            Array.Sort(arr);

            Int64[,] dp = new Int64[arr.Length + 1, arr.Length + 1];

            for (int i = 0; i < arr.Length + 1; i++)
            {
                for (int j = 0; j < arr.Length + 1; j++)
                {
                    dp[i, j] = Int64.MaxValue;
                }
            }

            dp[0, 0] = 0;


            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {

                    if ((j > k) || ((i - j) > (n - k)))
                    {
                        continue;
                    }



                    Int64 temp_li = dp[i, j] + arr[i] * (i - j - (n - k - (i - j)));

                    Int64 temp_lu = dp[i, j] + arr[i] * (j - (k - j));

                    if (dp[i + 1, j + 1] > temp_li)
                    {
                        dp[i + 1, j + 1] = temp_li;
                    }
                    if (dp[i + 1, j] > temp_lu)
                    {
                        dp[i + 1, j] = temp_lu;
                    }




                }
            }



            return new FairCutAnswer() { result = dp[n, k] };
        }



    }
}
