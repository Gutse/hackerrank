using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class CountingInversionsSample:TSample
    {
        public Int32[] arr;

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CountingInversionsAnswer: TAnswer
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
            return (obj as CountingInversionsAnswer)?.result == this.result;

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


    class CountingInversions : TProblem
    {


        public override void CreateSamples(System.IO.StreamReader reader)
        {
            int t = Convert.ToInt32(reader.ReadLine());

            for (int tItr = 0; tItr < t; tItr++)
            {
                int n = Convert.ToInt32(reader.ReadLine());

                int[] arr = Array.ConvertAll(reader.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));
                CountingInversionsSample sample = new CountingInversionsSample() { arr = arr };
                Samples.Add(sample);
            }
        }

        public override void CreateAnswers(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                CountingInversionsAnswer ans = new CountingInversionsAnswer() { result = Convert.ToInt64(reader.ReadLine()) };
                Answers.Add(ans);

            }

        }

        //[SolutionMethod]
        public CountingInversionsAnswer BruteForce(CountingInversionsSample sample)
        {
            Int32[] arr = sample.arr;
            Int64 result = 0;

            Int32[] sorted = new Int32[arr.Length];
            Array.Copy(arr, sorted, arr.Length);

            Array.Sort(sorted);

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = arr.Length - 1; j > i; j--)
                {
                    if (arr[i] > arr[j])
                    {
                        for (int k = i; k < j; k++)
                        {
                            Int32 temp = arr[k + 1];
                            arr[k + 1] = arr[k];
                            arr[k] = temp;
                            result++;
                        }
                        i = 0;
                    }
                }
            }
            return new CountingInversionsAnswer() { result = result };
        }

        //[SolutionMethod]
        public CountingInversionsAnswer Greedy(CountingInversionsSample sample)
        {
            Int32[] arr = new Int32[sample.arr.Length];
            Array.Copy(sample.arr, arr, arr.Length);
            Int64 result = 0;
            Int32 start = 0;
            List<Int64> res = new List<long>();

            for (int i = start; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    Int32 ptr = i;
                    do
                    {
                        Int32 temp = arr[ptr + 1];
                        arr[ptr + 1] = arr[ptr];
                        arr[ptr] = temp;
                        result++;
                        ptr--;
                    } while (ptr >= 0 && arr[ptr] > arr[ptr + 1]);
                }
                res.Add(result);
            }
            /*
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter("d:\\g1.txt"))
            {
                foreach (var item in res)
                {
                    wr.WriteLine(item);
                }
            }
            */

            return new CountingInversionsAnswer() { result = result };
        }

        //[SolutionMethod]
        public CountingInversionsAnswer GreedyOpt(CountingInversionsSample sample)
        {
            Int32[] arr = new Int32[sample.arr.Length];
            Array.Copy(sample.arr, arr, arr.Length);
            Int64 result = 0;
            List<Int32> st = new List<int> { arr[0] };
            for (int i = 1; i < arr.Length; i++)
            {
                Int32 pos = st.Count;
                if (arr[i] >= st[st.Count - 1])
                {
                    st.Add(arr[i]);
                }
                else
                {
                    pos = st.BinarySearch(arr[i]);
                    if (pos < 0)
                    {
                        pos = ~pos;
                        result += st.Count - pos;
                    }
                    else
                    {
                        while (pos < st.Count && st[pos] == arr[i])
                        {
                            pos++;
                        }
                        result += st.Count - pos;
                    }
                    st.Insert(pos, arr[i]);
                }
            }
            return new CountingInversionsAnswer() { result = result };
        }

        static Int64 MSS(Int32[] arr, Int32 start, Int32 end, Int32 r)
        {
            Int64 result = 0;
            //Console.WriteLine(r);
            if (end == start)
            {
                return 0;
            }

            Int32 mid = (start+end) / 2;
            result += MSS(arr, start, mid, r + 1);
            result += MSS(arr, mid + 1, end, r + 1);

            return result;
        }

        //[SolutionMethod]
        public TAnswer MS2(TSample Sample)
        {
            CountingInversionsSample sample = Sample as CountingInversionsSample;

            Int32[] arr = new Int32[sample.arr.Length];
            Array.Copy(sample.arr, arr, arr.Length);

            Int64 result = MSS(arr, 0, arr.Length - 1, 0);


            return new CountingInversionsAnswer() { result = result };
        }


        //[SolutionMethod]
        public CountingInversionsAnswer MS(CountingInversionsSample sample)
        {

            Int32[] arr = new Int32[sample.arr.Length];
            Array.Copy(sample.arr, arr, arr.Length);

            Int64 result = 0;

            Int32 size = 3;
            Int32 Start = 0;
            Int32 End = Math.Min(arr.Length, size);


            do
            {
                List<Int32> RightList = new List<int>() { arr[Start] };

                for (int i = Start + 1; i < End; i++)
                {
                    Int32 pos = RightList.Count;
                    if (arr[i] >= RightList[RightList.Count - 1])
                    {
                        RightList.Add(arr[i]);
                    }
                    else
                    {
                        pos = RightList.BinarySearch(arr[i]);
                        if (pos < 0)
                        {
                            pos = ~pos;
                            result += RightList.Count - pos;
                        }
                        else
                        {
                            while (pos < RightList.Count && RightList[pos] == arr[i])
                            {
                                pos++;
                            }
                            result += RightList.Count - pos;
                        }
                        RightList.Insert(pos, arr[i]);
                    }
                }



                Start = End;
                End = Math.Min(arr.Length, End + size);
            } while (Start != arr.Length);

            return new CountingInversionsAnswer() { result = result };
        }



        //[SolutionMethod]
        public CountingInversionsAnswer MitM(CountingInversionsSample sample)
        {
            Int32[] arr = new Int32[sample.arr.Length];
            Array.Copy(sample.arr, arr, arr.Length);
            Int64 result = 0;
            Int32 Mid = arr.Length / 2;
            List<Int32> LeftList = new List<int> { arr[0] };
            List<Int32> RightList = new List<int> { arr[Mid] };

            LeftList.Capacity = arr.Length * 3;
            RightList.Capacity = arr.Length * 3;
            Int32 LastItem = arr[0];


            for (int i = 1; i < Mid; i++)
            {
                if (arr[i] >= LastItem)
                {
                    LeftList.Add(arr[i]);
                    LastItem = arr[i];
                }
                else
                {
                    Int32 pos = LeftList.BinarySearch(arr[i]);
                    if (pos < 0)
                    {
                        pos = ~pos;
                        result += i - pos;
                    }
                    else
                    {
                        while (pos < i && LeftList[pos] == arr[i])
                        {
                            pos++;
                        }
                        result += i - pos;
                    }
                    LeftList.Insert(pos, arr[i]);
                }
            }

            LastItem = arr[Mid];

            for (int i = Mid + 1; i < arr.Length; i++)
            {
                if (arr[i] >= LastItem)
                {
                    RightList.Add(arr[i]);
                    LastItem = arr[i];
                }
                else
                {
                    Int32 pos = RightList.BinarySearch(arr[i]);
                    if (pos < 0)
                    {
                        pos = ~pos;
                        result += i - Mid - pos;
                    }
                    else
                    {
                        while (pos < i - Mid && RightList[pos] == arr[i])
                        {
                            pos++;
                        }
                        result += i - Mid - pos;
                    }
                    RightList.Insert(pos, arr[i]);
                }
            }


            Int32 LeftPtr = 0;
            Int32 RightPtr = 0;


            Int32 NewPtr = 0;
            while (LeftPtr < LeftList.Count && RightPtr < RightList.Count)
            {
                if (LeftList[LeftPtr] <= RightList[RightPtr])
                {
                    LeftPtr++;
                }
                else
                {
                    result += Mid - NewPtr + RightPtr;
                    RightPtr++;
                }
                NewPtr++;
            }





            return new CountingInversionsAnswer() { result = result };
        }

        public static long mergeSort(int[] arr, int start, int end)
        {
            if (start == end)
                return 0;
            int mid = (start + end) / 2;
            long count = 0;
            count += mergeSort(arr, start, mid); 
            count += mergeSort(arr, mid + 1, end);
            count += merge(arr, start, end); 
            return count;
        }

        public static long merge(int[] arr, int start, int end)
        {
            int mid = (start + end) / 2;
            int[] newArr = new int[end - start + 1];
            int curr = 0;
            int i = start;
            int j = mid + 1;
            long count = 0;
            while (i <= mid && j <= end)
            {
                if (arr[i] > arr[j])
                {
                    newArr[curr++] = arr[j++];
                    count += mid - i + 1; 
                }
                else
                    newArr[curr++] = arr[i++];
            }
            
            while (i <= mid)
            {
                newArr[curr++] = arr[i++];
            }

            while (j <= end)
            {
                newArr[curr++] = arr[j++];
            }


            Array.Copy(newArr, 0, arr, start, end - start + 1); 
            return count;
        }
        [SolutionMethod]
        public TAnswer stub(TSample Sample)
        {
            CountingInversionsSample sample = Sample as CountingInversionsSample;

            Int32[] arr = sample.arr;
            Int64 result = mergeSort(arr, 0, arr.Length - 1);
            return new CountingInversionsAnswer() { result = result };
        }

    }
}
