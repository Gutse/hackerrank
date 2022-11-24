using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SolutionMethodAttribute : Attribute
    {
    }

    public class TSample
    {
    }

    public class TAnswer
    {
    }

    public delegate TAnswer Solution(TSample sample);


    public class TProblem
    {
        public TProblem()
        {

            var solMethods = this.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(SolutionMethodAttribute), true).Length > 0);
            foreach (var method in solMethods)
            {
                var s = method.CreateDelegate(typeof(Solution), this);
                Solutions.Add(s as Solution);
            }
            LoadSamples();
            AddManualSamples();
            TargetedSamples();
        }

        private void LoadSamples()
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
                    CreateSamples(reader);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    CreateAnswers(reader);
                }
            }

            String SamplesDir = $"{Properties.Settings.Default["SamplesBaseDir"]}\\{this.GetType().Name}\\";

            if (!System.IO.Directory.Exists(SamplesDir))
            {
                Console.WriteLine($"Directory {SamplesDir} not exists");
                return;
            }

            foreach (var file in System.IO.Directory.GetFiles(SamplesDir, "*input*"))
            {
                LoadFile(file, file.Replace("input", "output"));
            }

        }

        public readonly List<Solution> Solutions = new List<Solution>();
        public List<TSample> Samples = new List<TSample>();
        public List<TAnswer> Answers = new List<TAnswer>();
        //methods to override
        public virtual void CreateSamples(System.IO.StreamReader reader)
        {
        }
        public virtual void CreateAnswers(System.IO.StreamReader reader)
        {
        }
        public virtual void AddManualSamples()
        {
        }
        public virtual void TargetedSamples()
        {
        }



        public virtual Boolean CheckAnswer(Int32 SampleID, TAnswer Answer)
        {
            return Answer.Equals(Answers[SampleID]);
        }


    }

}
