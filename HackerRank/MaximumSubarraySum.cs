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
            void LoadFile(String InputFile, String OutputFile) {
                if (!System.IO.File.Exists(InputFile) || !System.IO.File.Exists(OutputFile)  )
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
                        MaximumSubarraySumSample sample = new MaximumSubarraySumSample() { arr = a, m = m};
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
            Samples.Add(new MaximumSubarraySumSample() {m = 7, arr = new Int64[] {3,4,5,3,2,3,7} });
            Answers.Add(new MaximumSubarraySumAnswer() {result = 6 });
        }

        //[SolutionMethod]
        public MaximumSubarraySumAnswer DP(MaximumSubarraySumSample sample) {
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
                    if (DP[r,j] > Max)
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

            return new MaximumSubarraySumAnswer() { result = Max};
        }

        [SolutionMethod]
        public MaximumSubarraySumAnswer ON(MaximumSubarraySumSample sample)
        {
            Int64[] arr = sample.arr;
            Int64 m = sample.m;
            
            Int64 s = arr[0] % m;
            Int64 result = s;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i] % m;
            }
            if (result != m-1)
            {

                Int64[] v = new Int64[ arr.Length * (arr.Length+1) /2] ;
                Int32 vc = 1;
                v[0] = arr[0];

                for (int i = 1; i < arr.Length; i++)
                {
                    for (int j = 0; j < vc; j++)
                    {
                        v[j] = (v[j] + arr[i]) % m;
                        if (v[j] == m - 1)
                        {
                            result = v[j];
                            break;
                        }
                        if (v[j] > result)
                        {
                            result = v[j];
                        }
                    }
                    v[vc] = arr[i];
                    vc++;


                    if (result == m - 1)
                    {
                        break;
                    }
                }

            }

            return new MaximumSubarraySumAnswer() { result = result };
        }

        //[SolutionMethod]
        public MaximumSubarraySumAnswer stub(MaximumSubarraySumSample sample)
        {
            Int64[] arr = sample.arr;
            Int64 m = sample.m;
            Int64 result = 0;

            return new MaximumSubarraySumAnswer() { result = result };
        }


    }
}
