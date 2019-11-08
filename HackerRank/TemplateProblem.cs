using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class TemplateProblemSample
    {
        public Int32[] arr;
        public Int32 k;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class TemplateProblemAnswer
    {
        public Int32[] arr;
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
    }


    class TemplateProblem : TProblem<TemplateProblemSample, TemplateProblemAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, TemplateProblemAnswer Answer)
        {
            return base.CheckAnswer(SampleID, Answer);
            //return Answer?.result == Answers?[SampleID]?.result;
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
                    string[] nm = reader.ReadLine().Split(' ');
                    Int64 n = Convert.ToInt32(nm[0]);
                    Int64 r = Convert.ToInt32(nm[1]);

                    //List<long> arr = reader.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt64(arrTemp)).ToList();
                    //Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt64(sTemp));

                    TemplateProblemSample sample = new TemplateProblemSample() { };
                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    TemplateProblemAnswer ans = new TemplateProblemAnswer() { };
                    Answers.Add(ans);
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
            //Samples.Add(new TemplateProblemSample() { });
            //Answers.Add(new TemplateProblemAnswer() { });
        }

        [SolutionMethod]
        public TemplateProblemAnswer BruteForce(TemplateProblemSample sample) {
            return new TemplateProblemAnswer();
        }


    }
}
