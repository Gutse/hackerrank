using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class MaximumSubarraySumSample: TSample
    {
        public Int64[] arr;
        public Int64 m;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class MaximumSubarraySumAnswer: TAnswer
    {
        public Int64 result;
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
        public override Boolean Equals(Object obj)
        {
            MaximumSubarraySumAnswer Answer = obj as MaximumSubarraySumAnswer;
            return Answer.result == this.result;

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
            */
        }
    }


    class MaximumSubarraySum : TProblem
    {
        public override void CreateSamples(StreamReader reader)
        {
            int q = Convert.ToInt32(reader.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string[] nm = reader.ReadLine().Split(' ');

                int n = Convert.ToInt32(nm[0]);

                long m = Convert.ToInt64(nm[1]);

                long[] a = Array.ConvertAll(reader.ReadLine().Split(' '), aTemp => Convert.ToInt64(aTemp));
                MaximumSubarraySumSample sample = new MaximumSubarraySumSample() { arr = a, m = m };
                Samples.Add(sample);

            }
        }

        public override void CreateAnswers(StreamReader reader)
        {
            while (!reader.EndOfStream) {
                MaximumSubarraySumAnswer ans = new MaximumSubarraySumAnswer() { result = Convert.ToInt64(reader.ReadLine()) };
                Answers.Add(ans);
            }

        }

        
        //[SolutionMethod]
        public TAnswer OpDP(TSample Sample)
        {
            MaximumSubarraySumSample sample = Sample as MaximumSubarraySumSample;
            Int64[] arr = sample.arr;
            Int64 m = sample.m;

            Int32 nc = 0;
            Int64 maxm = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[nc] = arr[i] % m;
                if (arr[nc] != 0)
                {
                    if (maxm < arr[nc])
                    {
                        maxm = arr[nc];
                    }
                    nc++;
                }
            }

            Int64 result = maxm;


            if (result != m - 1)
            {


                Int64[] v = new Int64[arr.Length];
                Int32 vc = 1;
                v[0] = arr[0];


                for (int i = 1; i < nc; i++)
                {
                    Int32 vptr = -1;
                    for (int j = 0; j < vc; j++)
                    {
                        Int64 ts = v[j] + arr[i];
                        if (ts == m)
                        {
                            continue;
                        }
                        if (ts > m)
                        {
                            ts = ts - m;
                        }

                        vptr++;
                        v[vptr] = ts;
                        if (v[vptr] > result)
                        {
                            result = v[vptr];
                        }

                    }
                    vptr++;
                    v[vptr] = arr[i];
                    vc = vptr + 1;
                }

                Int64[] vcd = v.Take(vc).Distinct().ToArray();
                Console.WriteLine($"vc = {vc} vcd count = {vcd.Count()}");


            }

            return new MaximumSubarraySumAnswer() { result = result };
        }



        static void Swap(Int64[] arr, Int32 index1, Int32 index2, Int32[] idx)
        {
            Int64 temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;

            Int32 idxtemp = idx[index1];
            idx[index1] = idx[index2];
            idx[index2] = idxtemp;
        }

        static void QS(Int64[] arr, Int32 left, Int32 right, Int32[] idx)
        {

            do
            {
                int index1 = left;
                int index2 = right;
                int index3 = index1 + (index2 - index1 >> 1);


                if (arr[index1] > arr[index3])
                {

                    Swap(arr, index1, index3, idx);
                }
                if (arr[index1] > arr[index2])
                {
                    Swap(arr, index1, index2, idx);
                }
                if (arr[index3] > arr[index2])
                {
                    Swap(arr, index2, index3, idx);
                }
                Int64 p = arr[index3];
                do
                {
                    while (arr[index1] < p)
                        ++index1;
                    while (arr[index2] > p)
                        --index2;
                    if (index1 <= index2)
                    {
                        if (index1 < index2)
                        {
                            Swap(arr, index1, index2, idx);
                        }
                        ++index1;
                        --index2;
                    }
                    else
                        break;
                }
                while (index1 <= index2);

                if (index2 - left <= right - index1)
                {
                    if (left < index2)
                        QS(arr, left, index2, idx);
                    left = index1;
                }
                else
                {
                    if (index1 < right)
                        QS(arr, index1, right, idx);
                    right = index2;
                }
            }
            while (left < right);
        }

        static Int64 SumSubArray(Int64[] sums, Int64 m, Int32 Start, Int32 End)
        {
            if (Start == 0)
            {
                return sums[End];
            }
            else
            {
                return (sums[End] - sums[Start - 1] + m) % m;
            }
        }

        [SolutionMethod]
        public TAnswer SumsArray(TSample Sample)
        {
            MaximumSubarraySumSample sample = Sample as MaximumSubarraySumSample;
            Int64[] arr = sample.arr;
            Int64 m = sample.m;
            Int64[] sums = new Int64[arr.Length];
            
            Int64 result = 0;

            Int64 temp = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i] % m;
                temp = (arr[i] + temp) % m;
                sums[i] = temp;
                if (temp > result)
                {
                    result = temp;
                }
                if (arr[i] > result)
                {
                    result = arr[i];
                }
            }

            
            Int32[] idx = Enumerable.Range(0, sums.Length).ToArray();
            QS(sums, 0, sums.Length - 1, idx);
            
            for (int i = 0; i < sums.Length-1; i++)
            {
                if (idx[i] > idx[i + 1])
                {
                    temp = (sums[i] - sums[i + 1] + m) % m;
                    if (temp > result)
                    {
                        result = temp;
                    }
                }

            }



            return new MaximumSubarraySumAnswer() { result = result };
        }




    }
}
