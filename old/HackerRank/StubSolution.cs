using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{

    class StubSolution : TProblem<List<Int32>, Int32>
    {
        private Random rnd;

        public StubSolution() {
            rnd = new Random();
            Solution s1 = new Solution(Solution1);
            Solution s2 = new Solution(Solution2);
            Solutions.Add(s1);
            Solutions.Add(s2);
        }

        public override void GenSamples()
        {
            Int32 n = rnd.Next();
            Samples.Add(new List<Int32>() { n });
            Answers.Add(n);
        }

        public Int32 Solution1(List<Int32> sample) {
            return 0;
        }

        public Int32 Solution2(List<Int32> sample)
        {
            return sample[0];
        }


    }
}
