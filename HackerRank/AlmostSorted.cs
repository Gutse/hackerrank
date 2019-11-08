using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class AlmostSortedSample
    {
        public Int32[] arr;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class AlmostSortedAnswer
    {
        public String Verdict;
        public String Answer;
        public override string ToString()
        {
            return $"{Verdict} {Answer}";
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


    class AlmostSorted : TProblem<AlmostSortedSample, AlmostSortedAnswer>
    {
        private Random rnd = new Random();


        public override void GenSamples()
        {


            //            Samples.Add(new AlmostSortedSample() {arr = new Int32[] { 4 , 2 } });
            //Answers.Add(new AlmostSortedAnswer() { Verdict = "yes", Answer = "swap 1 2" });

            //Samples.Add(new AlmostSortedSample() { arr = new Int32[] { 3 ,1, 2 } });
            //Answers.Add(new AlmostSortedAnswer() { Verdict = "no", Answer = "" });

            Samples.Add(new AlmostSortedSample() { arr = new Int32[] { 3 ,2, 1 } });
            Answers.Add(new AlmostSortedAnswer() { Verdict = "yes", Answer = "" });


            Samples.Add(new AlmostSortedSample() { arr = new Int32[] { 1, 5, 4, 3, 2, 6 } });
            Answers.Add(new AlmostSortedAnswer() { Verdict = "yes", Answer = "reverse 2 5" });

            


            //Samples.Add(new AlmostSortedSample() { arr = new Int32[] { 1, 2, 4, 3, 5, 6 } });
            //Answers.Add(new AlmostSortedAnswer() { Verdict = "yes", Answer = "swap 3 4" });

            //Samples.Add(new AlmostSortedSample() { arr = new Int32[] { 4104, 8529, 49984, 54956, 63034, 82534, 84473, 86411, 92941, 95929, 108831, 894947, 125082, 137123, 137276, 142534, 149840, 154703, 174744, 180537, 207563, 221088, 223069, 231982, 249517, 252211, 255192, 260283, 261543, 262406, 270616, 274600, 274709, 283838, 289532, 295589, 310856, 314991, 322201, 339198, 343271, 383392, 385869, 389367, 403468, 441925, 444543, 454300, 455366, 469896, 478627, 479055, 484516, 499114, 512738, 543943, 552836, 560153, 578730, 579688, 591631, 594436, 606033, 613146, 621500, 627475, 631582, 643754, 658309, 666435, 667186, 671190, 674741, 685292, 702340, 705383, 722375, 722776, 726812, 748441, 790023, 795574, 797416, 813164, 813248, 827778, 839998, 843708, 851728, 857147, 860454, 861956, 864994, 868755, 116375, 911042, 912634, 914500, 920825, 979477 } });
            //Answers.Add(new AlmostSortedAnswer() { Verdict = "yes", Answer = "swap 12 95" });


        }

        //[SolutionMethod]
        public AlmostSortedAnswer BruteForce(AlmostSortedSample sample)
        {
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

        public override bool CheckAnswer(int SampleID, AlmostSortedAnswer Answer)
        {
            return Answer?.Verdict == Answers?[SampleID]?.Verdict && Answer?.Answer == Answers?[SampleID]?.Answer;
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

        [SolutionMethod]
        public AlmostSortedAnswer Solution2(AlmostSortedSample sample)
        {
            Int32[] arr = sample.arr;
            String s = "aba";
            Int64 n = 10;
            Int32[] a = new Int32[4] { 1, 2, 3, 4 };
            Int32 d = 17;
            Int32 r = d - a.Length * (d / a.Length);

            Int32 ACountInFull = 0;
            foreach (var ch in s)
            {
                if (ch == 'a') { ACountInFull++; }
            }
            long FullTimes = n / s.Length;
            long result = FullTimes + ACountInFull;
            long Remain = n - FullTimes * s.Length;
            for (int i = 0; i < Remain; i++)
            {
                if (s[i] == 'a') { result++; }
            }
            

            return new AlmostSortedAnswer() { Verdict = "no", Answer = "" };

        }


    }
}
