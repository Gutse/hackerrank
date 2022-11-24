using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class AlmostSortedSample: TSample
    {
        public Int32[] arr;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class AlmostSortedAnswer : TAnswer
    {
        public String Verdict;
        public String Answer;
        public override Boolean Equals(Object obj)
        {
            AlmostSortedAnswer ans = obj as AlmostSortedAnswer;
            return ans?.Answer == this.Answer && ans?.Verdict == this.Verdict;
        }
        public override string ToString()
        {
            return $"{Verdict} {Answer}";
        }

    }


    class AlmostSorted : TProblem
    {
        public override void CreateSamples(StreamReader reader)
        {
            int n = Convert.ToInt32(reader.ReadLine());

            int[] arr = Array.ConvertAll(reader.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));

            Samples.Add(new AlmostSortedSample() { arr = arr});
        }

        public override void CreateAnswers(StreamReader reader)
        {
            String verdict = reader.ReadLine();
            String answer = reader.ReadLine();
            if (answer == null)
            {
                answer = "";
            }
            
            Answers.Add(new AlmostSortedAnswer() { Answer = answer , Verdict = verdict});

        }

        public override void AddManualSamples()
        {
        }

        public override void TargetedSamples()
        {

        }

        [SolutionMethod]
        public TAnswer BruteForce(TSample Sample)
        {
            AlmostSortedSample sample = Sample as AlmostSortedSample;

            Int32[] arr = sample.arr;

            if (arr.Length == 1)
            {
                return new AlmostSortedAnswer() { Verdict = "yes", Answer = "" };
            }
            if (arr.Length == 2)
            {
                String ans = "";
                if (arr[1] < arr[0])
                {
                    ans = $"swap 1 2";
                }
                return new AlmostSortedAnswer() { Verdict = "yes", Answer = ans };
            }

            List<Int32> pop = new List<Int32>();

            if (arr[1] < arr[0])
            {
                pop.Add(0);
            }

            Boolean poprowbroken = false;

            for (int i = 1; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    pop.Add(i);
                    if (!poprowbroken && pop.Count > 1 && pop[pop.Count - 1] - pop[pop.Count - 2] != 1)
                    {
                        poprowbroken = true;
                    }
                }
            }

            if (pop.Count == 0)
            {
                return new AlmostSortedAnswer() { Verdict = "yes", Answer = "" };
            }

            if (pop.Count == 1)
            {
                if (pop[0] == 0)
                {
                    if (arr[0] < arr[2])
                    {
                        return new AlmostSortedAnswer() { Verdict = "yes", Answer = $"swap {1} {2}" };
                    }
                }
                else
                {
                    if (pop[0] == arr.Length - 2)
                    {
                        if (arr[arr.Length - 1] > arr[pop[0] - 1])
                        {
                            return new AlmostSortedAnswer() { Verdict = "yes", Answer = $"swap {pop[0] + 1} {pop[0] + 2}" };
                        }
                    }
                    else
                    {
                        if (arr[pop[0] + 1] > arr[pop[0] - 1] && arr[pop[0]] < arr[pop[0] + 2])
                        {
                            return new AlmostSortedAnswer() { Verdict = "yes", Answer = $"swap {pop[0] + 1} {pop[0] + 2}" };
                        }
                    }
                }

                return new AlmostSortedAnswer() { Verdict = "no", Answer = "" };
            }

            if (pop.Count == 2)
            {

                Boolean Swap1LeftOk = true;
                Boolean Swap1RightOk = true;

                for (int i = 0; i < 4; i++)
                {

                    Int32 pop0 = pop[0]+ i % 2;
                    Int32 pop1 = pop[1]+ i / 2;


                    if (pop0 != 0)
                    {
                        Swap1LeftOk = arr[pop0 - 1] < arr[pop1];
                    }
                    Swap1RightOk = arr[pop0 + 1] > arr[pop1];

                    Boolean Swap2LeftOk = true;
                    Boolean Swap2RightOk = true;

                    

                    if (pop0 != arr.Length - 1)
                    {
                        Swap2RightOk = arr[pop1 + 1] > arr[pop0];
                    }

                    Swap2LeftOk = arr[pop1 - 1] < arr[pop0];
                    if (Swap1LeftOk && Swap2LeftOk && Swap2RightOk && Swap1RightOk)
                    {
                        return new AlmostSortedAnswer() { Verdict = "yes", Answer = $"swap {pop0 + 1} {pop1 + 1}" };
                    }


                }

                return new AlmostSortedAnswer() { Verdict = "no", Answer = $"" };

            }

            if (pop.Count > 2 && !poprowbroken)
            {
                return new AlmostSortedAnswer() { Verdict = "yes", Answer = $"reverse {pop[0] + 1} {pop[pop.Count - 1] + 2}" };
            }
            return new AlmostSortedAnswer() { Verdict = "no", Answer = "" };

        }


    }
}
