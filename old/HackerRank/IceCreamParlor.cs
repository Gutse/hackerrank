using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class IceCreamParlorSample : TSample
    {
        public Int32[] cost;
        public Int32 money;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class IceCreamParlorAnswer : TAnswer
    {
        public Int32[] arr;
        public override string ToString()
        {


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

        }

        public override Boolean Equals(Object obj)
        {
            IceCreamParlorAnswer Answer = obj as IceCreamParlorAnswer;

            if (Answer?.arr?.Length != arr?.Length)
            {
                return false;
            }

            for (int i = 0; i < Answer.arr.Length; i++)
            {
                if (Answer.arr[i] != arr[i])
                {
                    return false;
                }
            }

            return true;
        }
    }


    class IceCreamParlor : TProblem
    {
        private Random rnd = new Random();

        public override void CreateSamples(System.IO.StreamReader reader)
        {
            int t = Convert.ToInt32(reader.ReadLine());

            for (int tItr = 0; tItr < t; tItr++)
            {
                int money = Convert.ToInt32(reader.ReadLine());

                int n = Convert.ToInt32(reader.ReadLine());

                int[] cost = Array.ConvertAll(reader.ReadLine().Trim().Split(' '), costTemp => Convert.ToInt32(costTemp));

                IceCreamParlorSample sample = new IceCreamParlorSample() { cost = cost, money = money };
                Samples.Add(sample);

            }
        }

        public override void CreateAnswers(System.IO.StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                int[] res = Array.ConvertAll(reader.ReadLine().Trim().Split(' '), costTemp => Convert.ToInt32(costTemp));

                IceCreamParlorAnswer ans = new IceCreamParlorAnswer() { arr = res };
                Answers.Add(ans);

            }
        }

        
        static void swap(Int32[] arr, Int32 index1, Int32 index2, Int32[] idx)
        {
            Int32 temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;

            Int32 idxtemp = idx[index1];
            idx[index1] = idx[index2];
            idx[index2] = idxtemp;
        }


        static void QS(Int32[] arr, Int32 left, Int32 right, Int32[] idx)
        {

            do
            {
                int index1 = left;
                int index2 = right;
                int index3 = index1 + (index2 - index1 >> 1);


                if (arr[index1] > arr[index3])
                {

                    swap(arr, index1, index3, idx);
                }
                if (arr[index1] > arr[index2])
                {
                    swap(arr, index1, index2, idx);
                }
                if (arr[index3] > arr[index2])
                {
                    swap(arr, index2, index3, idx);
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
                            swap(arr, index1, index2, idx);
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
        [SolutionMethod]
        public TAnswer BruteForce(TSample Sample)
        {
            IceCreamParlorSample sample = Sample as IceCreamParlorSample;
            Int32[] cost = sample.cost;
            Int32 money = sample.money;
            Int32[] result = new Int32[2];

            Int32[] idx = Enumerable.Range(0, cost.Length).ToArray();

            for (int i = 0; i < cost.Length; i++)
            {
                idx[i] = i + 1;

            }

            QS(cost, 0, cost.Length - 1, idx);
            Int32 LeftPtr = 0;
            Int32 RightPtr = cost.Length - 1;
            while (cost[RightPtr] + cost[LeftPtr] > money)
            {
                RightPtr--;
            }

            for (int i = RightPtr; i > LeftPtr; i--)
            {
                for (int j = LeftPtr; j < i; j++)
                {
                    if (cost[i] + cost[j] == money)
                    {
                        //Console.WriteLine($"{cost[j]} {cost[i]} ");
                        //Console.WriteLine($"{Math.Min(idx[i], idx[j])} {Math.Max(idx[i], idx[j])}");
                        return new IceCreamParlorAnswer() { arr = new Int32[] { Math.Min(idx[i], idx[j]), Math.Max(idx[i], idx[j]) } };
                    }
                }
            }
            return new IceCreamParlorAnswer() { arr = result };
        }

       

    }
}
