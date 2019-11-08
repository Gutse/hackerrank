using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{

    class MiniMaxSum : TProblem<Int32[], String>
    {
        private Random rnd;

        public MiniMaxSum() {
            rnd = new Random();
            Solution s1 = new Solution(Solution1);
            Solution s2 = new Solution(Solution2);
            Solutions.Add(s1);
            Solutions.Add(s2);
        }

        public override void GenSamples()
        {
            /*Int32 n = rnd.Next(20)+4;
            //Int32 n = 5;
            Int32[] arr = new Int32[n];
            Int32 sumMin = 0;
            Int32 sumMax = 0;
            for (int i = 0; i < n; i++)
            {
                arr[i] = i + 5;
                
            }

            sumMin = (5 + (5 + n - 2))*(n-1)/2;
            sumMax = (6 + (5 + n - 1)) * (n - 1) / 2;
            Samples.Add(arr);
            Answers.Add($"{sumMin} {sumMax}");
            */
            Int32[] arr = new Int32[5] { 140537896, 243908675, 670291834, 923018467, 520718469 };
            Samples.Add(arr);
            Answers.Add($"1575456874 2357937445");
        }

        public String Solution1(Int32[] sample) {
            Int64 SumAll = 0;
            Int32 Min = Int32.MaxValue;
            Int32 Max = Int32.MinValue;

            foreach (var val in sample)
            {
                SumAll += val;
                if (Min > val)
                {
                    Min = val;
                }
                if (Max < val)
                {
                    Max = val;
                }
            }



            return $"{SumAll - Max} {SumAll - Min}";
        }

        public String Solution2(Int32[] sample)
        {
            return "";
        }


    }
}
