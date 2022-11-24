using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class GreedyMaxMinSample : TSample
    {
        public Int32[] arr;
        public Int32 k;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class GreedyMaxMinAnswer : TAnswer
    {
        public Int32 result;
        public override string ToString()
        {
            return result.ToString();
        }
        public override Boolean Equals(Object obj)
        {
            return (obj as GreedyMaxMinAnswer)?.result == this.result;
        }
    }


    class GreedyMaxMin : TProblem
    {
        public override void CreateSamples(StreamReader reader)
        {
            int n = Convert.ToInt32(reader.ReadLine());

            int k = Convert.ToInt32(reader.ReadLine());

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                int arrItem = Convert.ToInt32(reader.ReadLine());
                arr[i] = arrItem;
            }
            GreedyMaxMinSample sample = new GreedyMaxMinSample() { arr = arr, k = k };
            Samples.Add(sample);
        }

        public override void CreateAnswers(System.IO.StreamReader reader)
        {
            GreedyMaxMinAnswer ans = new GreedyMaxMinAnswer() { result = Convert.ToInt32(reader.ReadLine()) };
            Answers.Add(ans);

        }


        [SolutionMethod]
        public TAnswer Greedy(TSample Sample)
        {
            GreedyMaxMinSample sample = Sample as GreedyMaxMinSample;

            Int32 k = sample.k;
            Int32[] arr = sample.arr;


            Array.Sort(arr);
            Int32 result = arr[k - 1] - arr[0];

            for (int i = 0; i <= arr.Length - k; i++)
            {
                if (arr[i + k - 1] - arr[i] < result)
                {
                    result = arr[i + k - 1] - arr[i];
                }
            }
            

            return new GreedyMaxMinAnswer() { result = result };
        }

    }
}
