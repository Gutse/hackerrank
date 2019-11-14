using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class MaximumSubarraySumSample
    {
        public Int64[] arr;
        public Int64 m;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class MaximumSubarraySumAnswer
    {
        public Int64 result;
        public override string ToString()
        {
            return result.ToString();
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


    class MaximumSubarraySum : TProblem<MaximumSubarraySumSample, MaximumSubarraySumAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, MaximumSubarraySumAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
            return Answer?.result == Answers?[SampleID]?.result;
            /*
             if (Answer?.arr?.Length != Answers?[SampleID]?.arr?.Length)
            {
                return false;
            }

            for (int i = 0; i < Answer.arr?.Length; i++)
            {
                if (Answer.arr[i] != Answers[SampleID].arr[i])
                {
                    return false;
                }
            }

            return true; 
             */
        }
        private void LoadSamples(String opt, String opt2 = "")
        {
            void LoadFile(String InputFile, String OutputFile)
            {
                if (!System.IO.File.Exists(InputFile) || !System.IO.File.Exists(OutputFile))
                {
                    Console.WriteLine($"files {InputFile} or {OutputFile} not found");
                    return;
                }

                using (System.IO.StreamReader reader = new System.IO.StreamReader(InputFile))
                {
                    int q = Convert.ToInt32(reader.ReadLine());

                    for (int qItr = 0; qItr < q; qItr++)
                    {
                        string[] nm = reader.ReadLine().Split(' ');

                        int n = Convert.ToInt32(nm[0]);

                        long m = Convert.ToInt64(nm[1]);

                        long[] a = Array.ConvertAll(reader.ReadLine().Split(' '), aTemp => Convert.ToInt64(aTemp));
                        MaximumSubarraySumSample sample = new MaximumSubarraySumSample() { arr = a, m = m };
                        Samples.Add(sample);

                    }

                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    while (!reader.EndOfStream)
                    {
                        MaximumSubarraySumAnswer ans = new MaximumSubarraySumAnswer() { result = Convert.ToInt64(reader.ReadLine()) };
                        Answers.Add(ans);
                    }
                }
            }



            //String SamplesDir = $"{AppDomain.CurrentDomain.BaseDirectory}\\Samples\\{this.GetType().Name}\\";
            String SamplesDir = $"{Properties.Settings.Default["SamplesBaseDir"]}\\{this.GetType().Name}\\";
            if (!System.IO.Directory.Exists(SamplesDir))
            {
                Console.WriteLine($"Directory {SamplesDir} not exists");
                return;
            }

            if (opt == "All")
            {
                foreach (var file in System.IO.Directory.GetFiles(SamplesDir, "*input*"))
                {
                    LoadFile(file, file.Replace("input", "output"));
                }
            }
            else
            {
                if (Int32.TryParse(opt, out Int32 sampleNumber))
                {
                    LoadFile($"{SamplesDir}input{opt}.txt", $"{SamplesDir}output{opt}.txt");
                }

                else
                {
                    LoadFile(opt, opt2);
                }
            }
        }

        public override void GenSamples()
        {
            LoadSamples("11");
            Samples.Add(new MaximumSubarraySumSample() { m = 7, arr = new Int64[] { 1, 3, 5, 3, 2, 3, 5 } });
            Answers.Add(new MaximumSubarraySumAnswer() { result = 6 });
        }

        //[SolutionMethod]
        public MaximumSubarraySumAnswer DP(MaximumSubarraySumSample sample)
        {
            Int64[] arr = sample.arr;
            Int64 m = sample.m;


            Int64[,] DP = new Int64[2, arr.Length + 1];
            Int64 Max = 0;
            Int32 r = 1;

            for (int i = 1; i <= arr.Length; i++)
            {
                for (int j = i; j <= arr.Length; j++)
                {
                    DP[r, j] = (DP[r ^ 1, j - 1] + arr[j - 1] % m) % m;
                    if (DP[r, j] > Max)
                    {
                        Max = DP[r, j];
                    }

                }
                if (Max == m - 1)
                {
                    break;
                }

                r ^= 1;
            }

            return new MaximumSubarraySumAnswer() { result = Max };
        }

        //[SolutionMethod]
        public MaximumSubarraySumAnswer OpDP(MaximumSubarraySumSample sample)
        {
            Int64[] arr = sample.arr;
            Int64 m = sample.m;

            Int32 nc = 0;
            Int64 maxm = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[nc] = arr[i] % m;
                if (arr[nc] != 0)
                {
                    if (maxm < arr[nc])
                    {
                        maxm = arr[nc];
                    }
                    nc++;
                }
            }

            Int64 result = maxm;


            if (result != m - 1)
            {


                Int64[] v = new Int64[arr.Length];
                Int32 vc = 1;
                v[0] = arr[0];


                for (int i = 1; i < nc; i++)
                {
                    Int32 vptr = -1;
                    for (int j = 0; j < vc; j++)
                    {
                        Int64 ts = v[j] + arr[i];
                        if (ts == m)
                        {
                            continue;
                        }
                        if (ts > m)
                        {
                            ts = ts - m;
                        }

                        vptr++;
                        v[vptr] = ts;
                        if (v[vptr] > result)
                        {
                            result = v[vptr];
                        }

                    }
                    vptr++;
                    v[vptr] = arr[i];
                    vc = vptr + 1;
                }

                Int64[] vcd = v.Take(vc).Distinct().ToArray();
                Console.WriteLine($"vc = {vc} vcd count = {vcd.Count()}");


            }

            return new MaximumSubarraySumAnswer() { result = result };
        }



        static void Swap(Int64[] arr, Int32 index1, Int32 index2, Int32[] idx)
        {
            Int64 temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;

            Int32 idxtemp = idx[index1];
            idx[index1] = idx[index2];
            idx[index2] = idxtemp;
        }

        static void QS(Int64[] arr, Int32 left, Int32 right, Int32[] idx)
        {

            do
            {
                int index1 = left;
                int index2 = right;
                int index3 = index1 + (index2 - index1 >> 1);


                if (arr[index1] > arr[index3])
                {

                    Swap(arr, index1, index3, idx);
                }
                if (arr[index1] > arr[index2])
                {
                    Swap(arr, index1, index2, idx);
                }
                if (arr[index3] > arr[index2])
                {
                    Swap(arr, index2, index3, idx);
                }
                Int64 p = arr[index3];
                do
                {
                    while (arr[index1] < p)
                        ++index1;
                    while (arr[index2] > p)
                        --index2;
                    if (index1 <= index2)
                    {
                        if (index1 < index2)
                        {
                            Swap(arr, index1, index2, idx);
                        }
                        ++index1;
                        --index2;
                    }
                    else
                        break;
                }
                while (index1 <= index2);

                if (index2 - left <= right - index1)
                {
                    if (left < index2)
                        QS(arr, left, index2, idx);
                    left = index1;
                }
                else
                {
                    if (index1 < right)
                        QS(arr, index1, right, idx);
                    right = index2;
                }
            }
            while (left < right);
        }

        static Int64 SumSubArray(Int64[] sums, Int64 m, Int32 Start, Int32 End)
        {
            if (Start == 0)
            {
                return sums[End];
            }
            else
            {
                return (sums[End] - sums[Start - 1] + m) % m;
            }
        }

        [SolutionMethod]
        public MaximumSubarraySumAnswer SumsArray(MaximumSubarraySumSample sample)
        {
            Int64[] arr = sample.arr;

            Int64[] sums = new Int64[arr.Length];
            Int64 m = sample.m;
            Int64 result = 0;

            Int64 temp = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i] % m;
                temp = (arr[i] + temp) % m;
                sums[i] = temp;
                if (temp > result)
                {
                    result = temp;
                }
                if (arr[i] > result)
                {
                    result = arr[i];
                }
            }

            /*
            //141706835
            for (int i = 0; i < sums.Length-2; i++)
            {
                for (int j = i+1; j < sums.Length; j++)
                {
                    temp = (sums[j] - sums[i] + m) % m;
                    if (temp == 141706835)
                    {
                        Console.WriteLine(1);
                    }
                }
            }
            */
            Int32[] idx = Enumerable.Range(0, sums.Length).ToArray();
            QS(sums, 0, sums.Length - 1, idx);
            
            for (int i = 0; i < sums.Length-1; i++)
            {
                if (idx[i] > idx[i + 1])
                {
                    temp = (sums[i] - sums[i + 1] + m) % m;
                    if (temp > result)
                    {
                        result = temp;
                    }
                }

            }



            return new MaximumSubarraySumAnswer() { result = result };
        }




    }
}
