using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class MinimumSwaps2Sample
    {
        public Int32[] arr;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class MinimumSwaps2Answer
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


    class MinimumSwaps2 : TProblem<MinimumSwaps2Sample, MinimumSwaps2Answer>
    {
        private Random rnd = new Random();


        public override void GenSamples()
        {
            Samples.Add(new MinimumSwaps2Sample() {arr = new Int32[] { 4, 3, 1, 2 } });
            Answers.Add(new MinimumSwaps2Answer() { result = 3});

            Samples.Add(new MinimumSwaps2Sample() { arr = new Int32[] { 2, 3, 4, 1, 5 } });
            Answers.Add(new MinimumSwaps2Answer() { result = 3});

            Samples.Add(new MinimumSwaps2Sample() { arr = new Int32[] { 1, 3, 5, 2, 4, 6, 7 } });
            Answers.Add(new MinimumSwaps2Answer() { result = 3});

        }

        [SolutionMethod]
        public MinimumSwaps2Answer BruteForce(MinimumSwaps2Sample sample) {
            Int32[] arr = sample.arr;

            Int32[] Positions = new Int32[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                Positions[arr[i]-1] = i;
            }

            Int32 SwapsCount = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                Int32 CurrentNumberPosition = Positions[i];

                if (CurrentNumberPosition != i)
                {
                    SwapsCount++;

                    arr[CurrentNumberPosition] = arr[i];
                    Positions[arr[i]-1] = CurrentNumberPosition;
                    arr[i] = i;
                    Positions[i] = i;
                }
            }

            return new MinimumSwaps2Answer() { result = SwapsCount};
        }

        Int32 Swaps(Int32[] arr) {
            Int32 MinSwaps = Int32.MaxValue;
            bool NoSwaps = true;
            for (int i = 0; i < arr.Length; i++)
            {
                if (i < arr.Length - 1 && arr[i] > arr[i + 1])
                {
                    NoSwaps = false;
                    Int32 temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;

                    Int32 sw = Swaps(arr) + 1;

                    arr[i+1] = arr[i];
                    arr[i] = temp;

                    if (MinSwaps > sw)
                    {
                        MinSwaps = sw;
                    }
                }
            }

            if (NoSwaps)
            {
                return 0;
            } else         return MinSwaps;

        }

        public override bool CheckAnswer(int SampleID, MinimumSwaps2Answer Answer)
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
