using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class ValidStringsSample
    {
        
        public String s;
        public override string ToString()
        {
            return s;
        }
    }

    public class ValidStringsAnswer
    {
        public String s;
        public override string ToString()
        {
            return s;
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


    class ValidStrings : TProblem<ValidStringsSample, ValidStringsAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, ValidStringsAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
            return Answer?.s == Answers?[SampleID]?.s;
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

                    ValidStringsSample sample = new ValidStringsSample() { s = reader.ReadLine() };
                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    ValidStringsAnswer ans = new ValidStringsAnswer() { s = reader.ReadLine() };
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
            //Samples.Add(new ValidStringsSample() { });
            //Answers.Add(new ValidStringsAnswer() { });
        }

        [SolutionMethod]
        public ValidStringsAnswer BruteForce(ValidStringsSample sample) {
            String s = sample.s;

            Byte start = (byte)'a';
            Byte end = (byte)'z';
            Int32[] fr = new Int32[end - start+1];
            foreach (var c in s)
            {
                fr[(byte)c - start]++;
            }

            fr = fr.Where(x => x > 0).ToArray();

            Array.Sort(fr);
            Int32 r = 0;
            Int32 mid = fr[fr.Length / 2];

            for (int i = 0; i < fr.Length; i++)
            {
                Int32 diff = Math.Abs(fr[i] - mid);
                if (diff == 0)
                {
                    continue;
                }
                if (diff == 1)
                {
                    r++;
                }
                else
                {
                    return new ValidStringsAnswer() { s = "NO"};
                }

            }

            if (r <= 1)
            {
                return new ValidStringsAnswer() { s = "YES" };
            }
            else
            {
                return new ValidStringsAnswer() { s = "NO" };

            }

            
        }


    }
}
