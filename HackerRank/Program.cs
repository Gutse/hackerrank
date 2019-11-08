using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class Program
    {
        static TProblem<MaximumSubarraySumSample, MaximumSubarraySumAnswer> GetProblem() {
            return new MaximumSubarraySum();
        }


        static void Main(string[] args)
        {
            var CurrentProblem = GetProblem();
            CurrentProblem.GenSamples();


            Console.WriteLine("--------------------------------------------------------------------------");
            for (int s = 0; s < CurrentProblem.Solutions.Count; s++)
            {
                Console.WriteLine($"Checking {CurrentProblem.Solutions[s].Method.Name}");
                var solution = CurrentProblem.Solutions[s];
                Stopwatch sw = Stopwatch.StartNew();
                Int32 pass = 0;
                Int32 fail = 0;


                for (int i = 0; i < CurrentProblem.Samples.Count; i++)
                //for (int i = 24; i < 25; i++)
                {

                    var answer = solution(CurrentProblem.Samples[i]);
                    if (CurrentProblem.CheckAnswer(i, answer))
                    {
                        //Console.WriteLine($"    Case №{i + 1} passed {answer} == {CurrentProblem.Answers[i]}");
                        Console.WriteLine($"    Case №{i + 1} passed");
                        pass++;
                    }
                    else {
                        Console.WriteLine($"    Case №{i+1} failed: {answer} != {CurrentProblem.Answers[i]}");
                        fail++;
                        
                    }
                    
                } 
                sw.Stop();
                Console.WriteLine($"Solution {CurrentProblem.Solutions[s].Method.Name} takes {sw.Elapsed}. pass={pass} fails={fail}");
                Console.WriteLine("--------------------------------------------------------------------------");

            }
            Console.ReadKey();

        }
    }
}
