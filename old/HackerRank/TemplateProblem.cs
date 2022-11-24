using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class TemplateProblemSample : TSample
    {
        public Int32[] arr;
        public Int32 k;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class TemplateProblemAnswer: TAnswer
    {
        public Int32[] arr;
        public Int32 result;
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
            return (obj as TemplateProblemAnswer)?.result == this.result;

            /*
             TemplateProblemAnswer Answer = obj as TemplateProblemAnswer;
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
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    class TemplateProblem : TProblem
    {
        public override void CreateSamples(System.IO.StreamReader reader)
        {
            string[] nm = reader.ReadLine().Split(' ');
            Int32 n = Convert.ToInt32(nm[0]);
            Int32 r = Convert.ToInt32(nm[1]);

            TemplateProblemSample sample = new TemplateProblemSample() { };
            /*
            sample.d = r;
            sample.ex = Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt32(sTemp));
            */
            Samples.Add(sample);
        }

        public override void CreateAnswers(System.IO.StreamReader reader)
        {
            TemplateProblemAnswer ans = new TemplateProblemAnswer() { };
            Int32.TryParse(reader.ReadLine(), out ans.result);
            Answers.Add(ans);

        }

        public override void AddManualSamples()
        {
        }

        public override void TargetedSamples()
        {
            
        }

        [SolutionMethod]
        public TAnswer Stub(TSample Sample) {
            TemplateProblemSample sample = Sample as TemplateProblemSample;

            return new TemplateProblemAnswer() { };
        }

        

    }
}
