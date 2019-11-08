using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class sherlockAndAnagramsSample
    {
        public String s;
        public override string ToString()
        {
            return s;
        }
    }

    public class sherlockAndAnagramsAnswer
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


    class sherlockAndAnagrams : TProblem<sherlockAndAnagramsSample, sherlockAndAnagramsAnswer>
    {
        private Random rnd = new Random();


        public override void GenSamples()
        {
            Samples.Add(new sherlockAndAnagramsSample() {s = "abba" });
            Answers.Add(new sherlockAndAnagramsAnswer() { result = 4 });

            Samples.Add(new sherlockAndAnagramsSample() { s = "abcd" });
            Answers.Add(new sherlockAndAnagramsAnswer() { result = 0});

            Samples.Add(new sherlockAndAnagramsSample() { s = "kkkk" });
            Answers.Add(new sherlockAndAnagramsAnswer() { result = 10});

            Samples.Add(new sherlockAndAnagramsSample() { s = "ifailuhkqq" });
            Answers.Add(new sherlockAndAnagramsAnswer() { result = 3});

            Samples.Add(new sherlockAndAnagramsSample() { s = "cdcd" });
            Answers.Add(new sherlockAndAnagramsAnswer() { result = 5 });
        }

        [SolutionMethod]
        public sherlockAndAnagramsAnswer DP(sherlockAndAnagramsSample sample) {
            String s = sample.s;

            Int32 result = 0;
            Dictionary<Char, Int32> Counts = new Dictionary<Char, Int32>();
            Int32[] Stats = new Int32[s.Length+1];

            //Int32 MaxLen = s.Length / 2;
            Int32 MaxLen = s.Length -1;

            Int32[] F = new Int32[s.Length];
            F[0] = 1;
            F[1] = 1;
            Int32 Counter = 0;


            foreach (var c in s)
            {
                if (Counts.ContainsKey(c))
                {
                    Counts[c]++;
                }
                else {
                    Counts.Add(c, 1);
                }
                Stats[Counts[c]]++;
                if (Counter > 1 )
                {
                    F[Counter] = F[Counter - 1] + Counter;
                }
                Counter++;



            }



            Int32[,] DP = new Int32[Counts.Count+1, MaxLen + 1];


            Counter = 1;
            foreach (var c in Counts.Keys)
            {
                DP[Counter, 1] = (Counts[c] - 1) * Counts[c] / 2;
                result += DP[Counter, 1];
                Counter++;
            }

            Counter = 1;
            foreach (var c in Counts.Keys)
            {
                for (int i = 2; i <= MaxLen; i++)
                {
                    DP[Counter, i] = DP[Counter-1, i];


                    Int32 SCI = Counts[c] - i + 1;
                    if (SCI > 1)
                    {
                        Int32 ACount = F[SCI - 1];
                        DP[Counter, i] += ACount;
                    }

                    for (int j = 1; j <= i-1; j++)
                    {
                        DP[Counter, i] += DP[Counter, j] * DP[Counter-1, i-j];
                    }
                }
                Counter++;
            }

            for (int i = 2; i <= MaxLen; i++)
            {
                result += DP[Counter-1, i];
            }

            

            return new sherlockAndAnagramsAnswer() { result = result };
        }


        [SolutionMethod]
        public sherlockAndAnagramsAnswer BF(sherlockAndAnagramsSample sample)
        {
            String s = sample.s;

            //HashSet<String> Subs = new HashSet<string>();
            Dictionary<String, Int32> Subs = new Dictionary<String, Int32>();
            Int32 result = 0;
            for (int i = 0; i <= s.Length - 1; i++)
            {
                for (int j = i+1; j <= s.Length; j++)
                {
                    char[] ss = s.Substring(i, j - i).ToArray();
                    Array.Sort(ss);
                    String SortedSubString = new string(ss);
                    if (Subs.ContainsKey(SortedSubString))
                    {
                        Subs[SortedSubString]++;
                        result += Subs[SortedSubString];
                    }
                    else {
                        Subs.Add(SortedSubString, 0);
                    }
                }
            }
            

            return new sherlockAndAnagramsAnswer() { result = result };
        }
        public override bool CheckAnswer(int SampleID, sherlockAndAnagramsAnswer Answer)
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


    }
}
