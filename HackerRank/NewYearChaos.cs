using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class NewYearChaosSample
    {
        public Int32[] q;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class NewYearChaosAnswer
    {
        public String ans;
        public override string ToString()
        {
            return ans;
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


    class NewYearChaos : TProblem<NewYearChaosSample, NewYearChaosAnswer>
    {
        private Random rnd = new Random();


        public override void GenSamples()
        {
            Samples.Add(new NewYearChaosSample() {q = new Int32[] { 2, 1, 5, 3, 4 } });
            Answers.Add(new NewYearChaosAnswer() { ans = "3"});
            Samples.Add(new NewYearChaosSample() { q = new Int32[] { 2, 5, 1, 3, 4 } });
            Answers.Add(new NewYearChaosAnswer() { ans = "Too chaotic" });
        }

        [SolutionMethod]
        public NewYearChaosAnswer BruteForce(NewYearChaosSample sample) {
            Int32[] q = sample.q;


            Int32[] Positions = new Int32[q.Length+1];
            for (int i = 1; i < q.Length+1; i++)
            {
                Positions[q[i-1]] = i;
            }

            Int32[] Bribes = new Int32[q.Length + 1];
            Int32 TotalBribes = 0;
            for (int i = 1; i < q.Length + 1; i++)
            {
                Int32 CurrentPersonPosition = Positions[i];
                while (CurrentPersonPosition != i)
                {
                    Int32 LeftPerson = q[CurrentPersonPosition-2];
                    if (Bribes[LeftPerson] < 2)
                    {
                        Bribes[LeftPerson]++;
                    }
                    else {
                            return new NewYearChaosAnswer() { ans = "Too chaotic" };
                    }
                    //swap 
                    Positions[i] = CurrentPersonPosition - 1;
                    Positions[LeftPerson] = CurrentPersonPosition;
                    q[CurrentPersonPosition-2] = i;
                    q[CurrentPersonPosition-1] = LeftPerson;
                    TotalBribes++;
                    CurrentPersonPosition--;
                }
            }

            return new NewYearChaosAnswer() { ans = TotalBribes.ToString()};
        }

        public override bool CheckAnswer(int SampleID, NewYearChaosAnswer Answer)
        {
            return Answer?.ans == Answers?[SampleID]?.ans;
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
