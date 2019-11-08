using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{

    class BirthdayCakeCandles : TProblem<Int32[], Int32>
    {
        private Random rnd;

        public BirthdayCakeCandles() {
            rnd = new Random();
            Solution s1 = new Solution(Solution1);
            Solution s2 = new Solution(Solution2);
            Solutions.Add(s1);
            Solutions.Add(s2);
        }

        public override void GenSamples()
        {
            Int32 n = rnd.Next(99)+1;

            Int32[] arr = new Int32[n];

            Int32 Min = Int32.MaxValue;
            Int32 Max = Int32.MinValue;
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next();
                if (Max < arr[i])
                {
                    Max = arr[i];
                }
                if (Min > arr[i])
                {
                    Min = arr[i];
                }
            }


            Samples.Add(arr);
            Answers.Add(arr.Where(v => v == Max).Count());
            
        }

        public Int32 Solution1(Int32[] sample) {
            Int32 Max = Int32.MinValue;

            foreach (var val in sample)
            {
                if (Max < val)
                {
                    Max = val;
                }
            }

            Int32 summax = 0;
            foreach (var val in sample)
            {
                if (Max == val)
                {
                    summax += 1;
                }
            }




            return summax;
        }

        public Int32 Solution2 (Int32[] sample)
        {
            Int32 Max = Int32.MinValue;

            foreach (var val in sample)
            {
                if (Max < val)
                {
                    Max = val;
                }
            }

            return sample.Where(v => v == Max).Count();
        }


    }
}
