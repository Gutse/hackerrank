using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{

    class StairsSolution : TProblem<Int32, List<String>>
    {
        private Random rnd;

        public StairsSolution() {
            rnd = new Random();
            Solution s1 = new Solution(Solution1);
            Solution s2 = new Solution(Solution2);
            Solutions.Add(s1);
            Solutions.Add(s2);
        }

        public override void GenSamples()
        {
            //Int32 n = rnd.Next();
            Samples.Add(6);
            List<String> a6 = new List<string>();

            a6.Add("     #");
            a6.Add("    ##");
            a6.Add("   ###");
            a6.Add("  ####");
            a6.Add(" #####");
            a6.Add("######");
            Answers.Add(a6);
        }

        public List<String> Solution1(Int32 sample) {
            List<String> result = new List<string>();
            char[] template = new char[sample];
            for (int i = 0; i < sample - 1; i++)
            {
                template[i] = ' ';
            }
            template[sample - 1] = '#';

            for (int i = sample - 2; i >= 0 ; i--)
            {
                result.Add(new string(template));
                template[i] = '#';
            }
            result.Add(new string(template));

            return result;
        }

        public List<String> Solution2(Int32 sample)
        {
            return new List<string>();
        }


    }
}
