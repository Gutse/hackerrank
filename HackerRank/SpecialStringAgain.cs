using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class SpecialStringAgainSample
    {
        public String s;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class SpecialStringAgainAnswer
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


    class SpecialStringAgain : TProblem<SpecialStringAgainSample, SpecialStringAgainAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, SpecialStringAgainAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
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
            void LoadFile(String InputFile, String OutputFile)
            {
                if (!System.IO.File.Exists(InputFile) || !System.IO.File.Exists(OutputFile))
                {
                    Console.WriteLine($"files {InputFile} or {OutputFile} not found");
                    return;
                }

                using (System.IO.StreamReader reader = new System.IO.StreamReader(InputFile))
                {
                    reader.ReadLine();
                    SpecialStringAgainSample sample = new SpecialStringAgainSample() { s = reader.ReadLine() };
                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {

                    SpecialStringAgainAnswer ans = new SpecialStringAgainAnswer() { result = Convert.ToInt64(reader.ReadLine()) };
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
        }



        [SolutionMethod]
        public SpecialStringAgainAnswer DP(SpecialStringAgainSample sample)
        {
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

        //[SolutionMethod]
        public SpecialStringAgainAnswer stub(SpecialStringAgainSample sample)
        {
            String s = sample.s;
            Int64 result = 0;


            return new SpecialStringAgainAnswer() { result = result };
        }


    }
}
