using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class GreedyMaxMinSample
    {
        public Int32[] arr;
        public Int32 k;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class GreedyMaxMinAnswer
    {
        public Int32 result;
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


    class GreedyMaxMin : TProblem<GreedyMaxMinSample, GreedyMaxMinAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, GreedyMaxMinAnswer Answer)
        {
            
            return Answer?.result == Answers?[SampleID]?.result;
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
                    int n = Convert.ToInt32(reader.ReadLine());

                    int k = Convert.ToInt32(reader.ReadLine());

                    int[] arr = new int[n];

                    for (int i = 0; i < n; i++)
                    {
                        int arrItem = Convert.ToInt32(reader.ReadLine());
                        arr[i] = arrItem;
                    }
                    GreedyMaxMinSample sample = new GreedyMaxMinSample() { arr = arr, k = k};
                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    GreedyMaxMinAnswer ans = new GreedyMaxMinAnswer() { result = Convert.ToInt32(reader.ReadLine())};
                    Answers.Add(ans) ;
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
            LoadSamples("16");
        }

        [SolutionMethod]
        public GreedyMaxMinAnswer Greedy(GreedyMaxMinSample sample)
        {
            
            Int32 k = sample.k;
            Int32[] arr = sample.arr;
            

            Array.Sort(arr);
            Int32 result = arr[k-1] - arr[0];

            for (int i = 0; i <= arr.Length - k; i++)
            {
                if (arr[i+k-1] - arr[i] < result)
                {
                    result = arr[i + k-1] - arr[i];
                }
            }

            return new GreedyMaxMinAnswer() { result = result };
        }

        //[SolutionMethod]
        public GreedyMaxMinAnswer stub(GreedyMaxMinSample sample) {
            Int32 k = sample.k;
            Int32[] arr = sample.arr;
            Int32 result = 0;


            return new GreedyMaxMinAnswer() { result = result};
        }


    }
}
