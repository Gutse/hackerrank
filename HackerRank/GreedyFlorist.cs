using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class GreedyFloristSample
    {
        public Int32[] c;
        public Int32 k;
        public Int32 n;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class GreedyFloristAnswer
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


    class GreedyFlorist : TProblem<GreedyFloristSample, GreedyFloristAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, GreedyFloristAnswer Answer)
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
                    string[] nk = reader.ReadLine().Split(' ');

                    int n = Convert.ToInt32(nk[0]);

                    int k = Convert.ToInt32(nk[1]);

                    int[] c = Array.ConvertAll(reader.ReadLine().Split(' '), cTemp => Convert.ToInt32(cTemp));

                    GreedyFloristSample sample = new GreedyFloristSample() { c = c, k = k, n = n};
                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    GreedyFloristAnswer ans = new GreedyFloristAnswer() { result = Convert.ToInt32(reader.ReadLine())};
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
            LoadSamples("All");
        }

        [SolutionMethod]
        public GreedyFloristAnswer Greedy(GreedyFloristSample sample)
        {
            Int32 n = sample.n;
            Int32 k = sample.k;
            Int32[] c = sample.c;
            Int32 result = 0;

            Array.Sort(c);

            if (k >= n)
            {
                for (int i = 0; i < n; i++)
                {
                    result += c[i];
                }
            }
            else {

                
                for (int i = c.Length-1; i >= 0; i--)
                {
                    Int32 mult = (c.Length-1 - i) / k +1;
                    
                    result += mult * c[i];
                }

            }


            return new GreedyFloristAnswer() { result = result };
        }

        //[SolutionMethod]
        public GreedyFloristAnswer stub(GreedyFloristSample sample) {
            Int32 n = sample.n;
            Int32 k = sample.k;
            Int32[] c = sample.c;
            Int32 result = 0;


            return new GreedyFloristAnswer() { result = result};
        }


    }
}
