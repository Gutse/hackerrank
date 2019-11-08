using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class FairCutSample
    {
        public Int32[] arr;
        public Int32 k;
    }



    class FairCut : TProblem<FairCutSample, Int32>
    {
        private Random rnd = new Random();


        public override void GenSamples()
        {

           
            Samples.Add(new FairCutSample() { arr = new Int32[] { 4, 3, 1, 2 }, k = 2 });
            Answers.Add(6);
            
            Samples.Add(new FairCutSample() { arr = new Int32[] { 3, 3, 3, 1 }, k = 1 });
            Answers.Add(2);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 3, 3, 3, 1 }, k = 1 });
            Answers.Add(2);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 63, 79, 37, 21, 26, 5 }, k = 2 });
            Answers.Add(232);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 0, 95, 99, 16 }, k = 2 });
            Answers.Add(198);
            

            Samples.Add(new FairCutSample() { arr = new Int32[] { 28, 12, 56, 6, 33 }, k = 3 });
            Answers.Add(121);

           
            Samples.Add(new FairCutSample() { arr = new Int32[] { 35, 51, 31, 31, 24, 98 }, k = 1 });
            Answers.Add(98);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 26, 21, 99, 70, 95, 14, 77 }, k = 2 });
            Answers.Add(369);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 46, 99, 35, 75, 4 }, k = 3 });
            Answers.Add(230);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 11, 98, 39, 48, 19 }, k = 1 });
            Answers.Add(116);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 91, 17, 30, 27, 27 }, k = 1 });
            Answers.Add(77);
            
            Samples.Add(new FairCutSample() { arr = new Int32[] { 2, 7, 29, 9 }, k = 3 });
            Answers.Add(29);
            
            Samples.Add(new FairCutSample() { arr = new Int32[] { 99, 45, 32, 64, 50, 83 }, k = 3 });
            Answers.Add(253);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 33, 15, 45, 31, 11, 19, 19 }, k = 2 });
            Answers.Add(116);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 33, 16, 94, 94, 8 }, k = 2 });
            Answers.Add(250);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 86, 50, 23, 79, 43, 92 }, k = 2 });
            Answers.Add(224);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 30, 64, 13, 65 }, k = 2 });
            Answers.Add(104);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 97, 35, 45, 66, 58, 1 }, k = 3 });
            Answers.Add(332);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 15, 86, 70, 77, 48, 96, 92 }, k = 3 });
            Answers.Add(347);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 91, 92, 67, 77, 0, 60, 6 }, k = 3 });
            Answers.Add(463);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 10, 79, 0, 47 }, k = 2 });
            Answers.Add(158);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 59, 83, 35, 80 }, k = 2 });
            Answers.Add(96);
            Samples.Add(new FairCutSample() { arr = new Int32[] { 50, 52, 81, 90, 34 }, k = 1 });
            Answers.Add(87);
            /*
            Samples.Add(new FairCutSample() { arr = new Int32[] { 691259308, 801371251, 345390019 ,162749471, 998969126, 308205008, 430442891 ,404642721, 532566673, 266540863, 702197285, 749105392, 775025448, 20453591, 582291534 ,132855413, 747557193, 129094259, 474372133, 788391070 }, k = 11 });
            Answers.Add(30481712493);
            
            */
            //CreateSamples();

        }


        public void CreateSamples()
        {

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(@"D:\hackerrank\faircutsamples.txt"))
            {


                for (int i = 0; i < 20; i++)
                {
                    Int32 SampleLenght = rnd.Next(4) + 4;
                    Int32 SampleK = rnd.Next(3) + 1;
                    Int32[] SampleArr = new Int32[SampleLenght];

                    for (int j = 0; j < SampleLenght; j++)
                    {
                        SampleArr[j] = rnd.Next(100);
                    }

                    FairCutSample sample = new FairCutSample() { arr = SampleArr, k = SampleK };
                    Int32 SampleAnswer = Straight(sample);

                    writer.Write("Samples.Add(new FairCutSample() { arr = new Int32[] {");
                    for (int j = 0; j < SampleArr.Length - 1; j++)
                    {
                        writer.Write($"{SampleArr[j]}, ");
                    }
                    writer.Write($"{SampleArr[SampleArr.Length - 1]}");

                    writer.Write("}, k = ");
                    writer.Write($"{SampleK}");
                    writer.WriteLine("});");
                    writer.WriteLine($"Answers.Add({SampleAnswer});");

                }
            }


        }

        public static void PrintDP(Int32[,] dp, Int32[] arr)
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
        public Int32 Solution1(FairCutSample sample)
        {

            Int32[] arr = sample.arr;
            Int32 k = sample.k;

            Array.Sort(arr, new Comparison<Int32>((x, y) => y.CompareTo(x)));

            Int32[,] Diffs = new Int32[arr.Length, arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    Diffs[i, j] = Math.Abs(arr[i] - arr[j]);
                }
            }



            Int32[] Taken = new Int32[k];
            Int32[] Remains = new Int32[arr.Length - k];

            for (int i = 0; i < k; i++)
            {
                Taken[i] = i;
            }

            for (int i = 0; i < arr.Length - k; i++)
            {
                Remains[i] = i + k;
            }

            Int32 CurrentDiff = SetDiffs(Taken, Remains, Diffs);


            for (int i = 0; i < k; i++)
            {

                for (int j = 0; j < arr.Length - k; j++)
                {
                    Int32 t = Taken[i];
                    Taken[i] = Remains[j];
                    Remains[j] = t;

                    Int32 NewDiff = SetDiffs(Taken, Remains, Diffs);
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


            return CurrentDiff;

        }


        static Int32 SetDiffs(Int32[] Taken, Int32[] Remains, Int32[,] diffs)
        {
            Int32 res = 0;
            foreach (var t in Taken)
            {
                foreach (var r in Remains)
                {
                    res += diffs[t, r];
                }

            }
            return res;
        }
        static Int32 SetDiffs(List<Int32> Taken, List<Int32> Remains, Int32[,] diffs)
        {
            Int32 res = 0;
            foreach (var t in Taken)
            {
                foreach (var r in Remains)
                {
                    res += diffs[t, r];
                }

            }
            return res;
        }

        [SolutionMethod]
        public Int32 Straight(FairCutSample sample)
        {

            Int32[] arr = sample.arr;
            Int32 k = sample.k;



            return FCut(k, new List<Int32>(), arr.ToList());
        }

        public Int32 FCut(Int32 k, List<Int32> left, List<Int32> right)
        {


            if (k == left.Count)
            {
                return CountCut(left, right);
            }


            Int32 MinCut = Int32.MaxValue;
            for (int i = 0; i < right.Count; i++)
            {
                List<Int32> nl = new List<int>(left);
                List<Int32> nr = new List<int>(right);
                nl.Add(right[i]);
                nr.RemoveAt(i);

                Int32 X = FCut(k, nl, nr);
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

        Int32 CountCut(List<Int32> left, List<Int32> right)
        {
            Int32 res = 0;
            foreach (var li in left)
            {
                foreach (var ri in right)
                {
                    res += Math.Abs(li - ri);
                }
            }
            return res;
        }


        [SolutionMethod]
        public Int32 Greedy(FairCutSample sample)
        {

            Int32[] arr = sample.arr;
            Int32 k = sample.k;

            if (k > (arr.Length / 2))
            {
                k = arr.Length - k;
            }


            Array.Sort(arr);


            Int32[,] Diffs = new Int32[arr.Length, arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    Diffs[i, j] = Math.Abs(arr[i] - arr[j]);
                }
            }

            /*
            Int32 MassCenter = arr.Length / 2;

            List<Int32> Taken = new List<int>() { MassCenter };
            List<Int32> Remains = new List<int>();
            bool[] takes = new bool[arr.Length];
            takes[MassCenter] = true;
            Int32 distance = -2;
            for (int i = 0; i < k; i++)
            {
                Int32 toTake = Math.Min(MassCenter + distance, arr.Length-1);
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


            Int32 d = SetDiffs(Taken, Remains, Diffs);
            return d;*/

            Boolean[] takes = new Boolean[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                takes[i] = false;
            }

            Int32 len01 = k * 2;

            Int32 Start01 = (arr.Length - len01) / 2;
            Boolean take = false;
            for (int i = 0; i < k * 2; i++)
            {
                takes[i + Start01] = take;
                take = !take;
            }

            List<Int32> Taken = new List<int>() ;
            List<Int32> Remains = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (takes[i])
                {
                    Taken.Add(i);
                }
                else {
                    Remains.Add(i);
                }
            }
            Int32 d = SetDiffs(Taken, Remains, Diffs);
            return d;
        }

        [SolutionMethod]
        public Int32 DP(FairCutSample sample) {
            Int32[] arr = sample.arr;
            Int32 k = sample.k;


            /*
            if (k > (arr.Length / 2))
            {
                k = arr.Length - k;
            }
            */

            Int32 n = arr.Length;

            Array.Sort(arr);

            Int32[,] dp = new Int32[arr.Length+1, arr.Length +1];

            for (int i = 0; i < arr.Length+1; i++)
            {
                for (int j = 0; j < arr.Length+1; j++)
                {
                    dp[i, j] = Int32.MaxValue;
                }
            }

            dp[0, 0] = 0;


            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < i+1; j++)
                {

                    if ((j > k) || ((i - j) > (n - k))) {
                        continue;
                    }
                    


                    Int32 temp_li = dp[i, j] + arr[i] * (i - j - (n - k - (i - j)));

                    Int32 temp_lu = dp[i, j] + arr[i] * (j - (k - j));

                    if (dp[i + 1, j + 1] > temp_li) {
                        dp[i + 1,j + 1] = temp_li;
                    }
                    if (dp[i + 1,j] > temp_lu)
                    {
                        dp[i + 1,j] = temp_lu;
                    }
            



                }
            }



            return dp[n, k];
        }



    }
}
