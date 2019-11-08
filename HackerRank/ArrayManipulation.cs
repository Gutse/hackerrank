using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class ArrayManipulationSample
    {
        public Int32[][] queries;
        public Int32 n;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class ArrayManipulationAnswer
    {
        public Int64 MaxValue;
        public override string ToString()
        {
            return MaxValue.ToString();
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


    class ArrayManipulation : TProblem<ArrayManipulationSample, ArrayManipulationAnswer>
    {
        private Random rnd = new Random();
        private void LoadSamples()
        {

            using (System.IO.StreamReader reader = new System.IO.StreamReader(@"d:\Dia\Projects\hackerrank\am_input12"))
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"d:\Dia\Projects\hackerrank\RKSumInput"))
            {
                string[] nm = reader.ReadLine().Split(' ');
                Int32 n = Convert.ToInt32(nm[0]);
                Int32 m = Convert.ToInt32(nm[1]);
                Int32[][] queries = new Int32[m][];

                for (int i = 0; i < m; i++)
                {
                    Int32[] query = Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt32(sTemp));
                    queries[i] = query;
                }

                ArrayManipulationSample sample = new ArrayManipulationSample() { n = n, queries = queries };
                //sample.seq = Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt64(sTemp));
                Samples.Add(sample);
            }


            using (System.IO.StreamReader reader = new System.IO.StreamReader(@"d:\Dia\Projects\hackerrank\am_output12"))
            {
                ArrayManipulationAnswer ans = new ArrayManipulationAnswer() { MaxValue = Convert.ToInt64(reader.ReadLine()) };
                Answers.Add(ans);
            }


        }

        public override void GenSamples()
        {
            Int32[] q1 = new Int32[] { 1, 2, 100 };
            Int32[] q2 = new Int32[] { 2, 5, 100 };
            Int32[] q3 = new Int32[] { 3, 4, 100 };
            Int32[][] qs = new Int32[3][];
            qs[0] = q1;
            qs[1] = q2;
            qs[2] = q3;

            // Samples.Add(new ArrayManipulationSample() { n = 5, queries = qs });
            // Answers.Add(new ArrayManipulationAnswer() { MaxValue = 200 });

            q1 = new Int32[] { 2, 3, 603 };
            q2 = new Int32[] { 1, 1, 286 };
            q3 = new Int32[] { 4, 4, 882 };
            qs = new Int32[3][];
            qs[0] = q1;
            qs[1] = q2;
            qs[2] = q3;

            //Samples.Add(new ArrayManipulationSample() { n = 4, queries = qs });
            // Answers.Add(new ArrayManipulationAnswer() { MaxValue = 882 });
            LoadSamples();
        }

        //[SolutionMethod]
        public ArrayManipulationAnswer BruteForce(ArrayManipulationSample sample)
        {

            Int32 n = sample.n;
            Int32[][] queries = sample.queries;

            Int32 m = queries.Length;

            Array.Sort(queries, new Comparison<Int32[]>((x, y) =>
            {
                if (x[0] - y[0] != 0)
                {
                    return x[0] - y[0];
                }
                else
                {
                    return x[1] - y[1];
                }

            }));

            List<Int64[]> ranges = new List<Int64[]>() { new Int64[] { 0, n - 1, 0 } };



            for (int i = 0; i < m; i++)
            {


                Int32 Start = queries[i][0] - 1;
                Int32 End = queries[i][1] - 1;
                Int32 Value = queries[i][2];

                List<Int64[]> new_ranges = new List<Int64[]>();


                for (int j = 0; j < ranges.Count; j++)
                {
                    Int64 CurrentRangeStart = ranges[j][0];
                    Int64 CurrentRangeEnd = ranges[j][1];
                    Int64 CurrentRangeValue = ranges[j][2];

                    //если нет пересеченй
                    if ((Start < CurrentRangeStart && End < CurrentRangeStart) || ((Start > CurrentRangeEnd && End > CurrentRangeEnd)))
                    {
                        new_ranges.Add(ranges[j]);
                        continue;
                    }


                    if (Start <= CurrentRangeStart)
                    {
                        if (End >= CurrentRangeEnd)
                        {
                            ranges[j][2] = CurrentRangeValue + Value;
                            new_ranges.Add(ranges[j]);
                        }
                        else
                        {
                            Int64[] new_range = new Int64[] { CurrentRangeStart, End, CurrentRangeValue + Value };
                            ranges[j][0] = End + 1;
                            new_ranges.Add(new_range);
                            new_ranges.Add(ranges[j]);

                            for (int k = j + 1; k < ranges.Count; k++)
                            {
                                new_ranges.Add(ranges[k]);
                            }
                            ranges = new_ranges;
                            break;
                        }
                    }
                    else
                    {// if Start > RangeStart
                        Int64[] new_range = new Int64[] { CurrentRangeStart, Start - 1, CurrentRangeValue };
                        new_ranges.Add(new_range);
                        if (End >= CurrentRangeEnd)
                        {
                            ranges[j][0] = Start;
                            ranges[j][1] = CurrentRangeEnd;
                            ranges[j][2] = CurrentRangeValue + Value;
                            new_ranges.Add(ranges[j]);
                        }
                        else
                        {
                            ranges[j][0] = Start;
                            ranges[j][1] = End;
                            ranges[j][2] = CurrentRangeValue + Value;
                            new_ranges.Add(ranges[j]);
                            Int64[] new_range2 = new Int64[] { End + 1, CurrentRangeEnd, CurrentRangeValue };
                            new_ranges.Add(new_range2);
                        }
                    }
                }


                ranges = new_ranges;

                //слияние одинаковых

                new_ranges = new List<Int64[]>();
                new_ranges.Add(ranges[0]);
                for (int k = 1; k < ranges.Count; k++)
                {
                    if (new_ranges[new_ranges.Count - 1][2] == ranges[k][2])
                    {
                        new_ranges[new_ranges.Count - 1][1] = ranges[k][1];
                    }
                    else
                    {
                        new_ranges.Add(ranges[k]);
                    }
                }


                ranges = new_ranges;

            }

            Int64 MaxValue = Int64.MinValue;
            for (int i = 0; i < ranges.Count; i++)
            {
                if (i < ranges.Count - 1 && ranges[i] == ranges[i + 1])
                {
                    Console.WriteLine("equal!!!");
                }

                if (ranges[i][2] > MaxValue)
                {
                    MaxValue = ranges[i][2];
                }
            }


            return new ArrayManipulationAnswer() { MaxValue = MaxValue };
        }


        // [SolutionMethod]
        public ArrayManipulationAnswer TotalBruteForce(ArrayManipulationSample sample)
        {

            Int32 n = sample.n;
            Int32[][] queries = sample.queries;

            Int32 m = queries.Length;
            Int64[] vals = new Int64[n];

            for (int i = 0; i < m; i++)
            {
                for (int j = queries[i][0] - 1; j <= queries[i][1] - 1; j++)
                {
                    vals[j] += queries[i][2];
                }
            }

            Int64 MaxValue = Int64.MinValue;
            for (int i = 0; i < n; i++)
            {
                if (vals[i] > MaxValue)
                {
                    MaxValue = vals[i];
                }
            }
            return new ArrayManipulationAnswer() { MaxValue = MaxValue };
        }

        //[SolutionMethod]
        public ArrayManipulationAnswer Mitm(ArrayManipulationSample sample)
        {

            Int32 n = sample.n;
            Int32[][] queries = sample.queries;

            Int32 m = queries.Length;


            Array.Sort(queries, new Comparison<Int32[]>((x, y) =>
            {
                /*
                if (x[0] - y[0] != 0)
                {
                    return x[0] - y[0];
                }
                else
                {
                    return x[1] - y[1];
                }
                */
                if (x[1] - y[1] != 0)
                {
                    return x[1] - y[1];
                }
                else
                {
                    return x[0] - y[0];
                }

            }));

            Int32 Middle = queries.Length / 2;

            if (queries[Middle][1] > n / 2)
            {
                while (queries[Middle][1] > n / 2) { Middle--; };
            }
            else
            {
                while (queries[Middle][1] < n / 2) { Middle++; };
            }



            Int64[] vLeft = new Int64[queries[Middle][1]];
            Int64 vLeftMax = 0;
            for (int i = 0; i <= Middle; i++)
            {
                for (int j = queries[i][0] - 1; j <= queries[i][1] - 1; j++)
                {
                    vLeft[j] += queries[i][2];
                    if (vLeft[j] > vLeftMax)
                    {
                        vLeftMax = vLeft[j];
                    }
                }
            }

            Int64[] vRight = new Int64[n - queries[Middle][1]];
            Int64 vRightMax = 0;
            for (int i = Middle + 1; i < n; i++)
            {
                for (int j = queries[i][0] - 1; j <= queries[i][1] - 1; j++)
                {
                    vRight[j] += queries[i][2];
                    if (vRight[j] > vRightMax)
                    {
                        vRightMax = vRight[j];
                    }
                }
            }


            return new ArrayManipulationAnswer() { MaxValue = Math.Max(vLeftMax, vRightMax) };



        }

        [SolutionMethod]
        public ArrayManipulationAnswer test1(ArrayManipulationSample sample)
        {

            Int32 n = sample.n;
            Int32[][] queries = sample.queries;

            Int32 m = queries.Length;

            Int64[] numList = new Int64[n + 1];

            for (int i = 0; i < m; i++)
            {
                
                Int64 a = queries[i][0];
                Int64 b = queries[i][1];
                Int64 k = queries[i][2];

                numList[a] += k;
                if (b + 1 <= n) numList[b + 1] -= k;
            }

            long tempMax = 0;
            long max = 0;
            for (int i = 1; i <= n; i++)
            {
                tempMax += numList[i];
                if (tempMax > max) max = tempMax;
            }

            return new ArrayManipulationAnswer() { MaxValue = max };

        }
        //[SolutionMethod]
        public ArrayManipulationAnswer DP(ArrayManipulationSample sample)
        {

            Int32 n = sample.n;
            Int32[][] queries = sample.queries;

            Int32 m = queries.Length;

            Array.Sort(queries, new Comparison<Int32[]>((x, y) =>
            {
                Int32 FirstIndex = 0;
                Int32 SecondIndex = 1;
                Int32 ThirdIndex = 2;
                if (x[FirstIndex] - y[FirstIndex] != 0)
                {
                    return x[FirstIndex] - y[FirstIndex];
                }
                else
                {
                    if (x[SecondIndex] - y[SecondIndex] != 0)
                    {
                        return x[SecondIndex] - y[SecondIndex];
                    }
                    else
                    {
                        return x[ThirdIndex] - y[ThirdIndex];
                    }

                }
            }));

            Int64 MaxRS = 0;
            Int64[] RS = new Int64[n + 1];

            Int32[] Points = queries.Select(q => q[0]).Distinct().ToArray();
            Int32 next = 0;
            Boolean nextFound = false;
            for (int i = 0; i < m; i++)
            {
                Int32 j = next;
                while (j < Points.Length && Points[j] >= queries[i][0] && Points[j] <= queries[i][1])
                {

                    RS[Points[j]] += queries[i][2];
                    if (!nextFound && i < m - 1 && queries[i + 1][0] == Points[j])
                    {
                        next = j;
                        nextFound = true;
                    }
                    j++;
                }
                if (!nextFound && i < m - 1 && j < Points.Length)
                {

                    while (j < Points.Length && queries[i + 1][0] != Points[j])
                    {
                        j++;
                    }
                    next = j;
                }
                nextFound = false;

            }



            MaxRS = RS.Max();
            return new ArrayManipulationAnswer() { MaxValue = MaxRS };


        }

        // [SolutionMethod]
        public ArrayManipulationAnswer DP2(ArrayManipulationSample sample)
        {

            Int32 n = sample.n;
            Int32[][] queries = sample.queries;

            Int32 m = queries.Length;

            Array.Sort(queries, new Comparison<Int32[]>((x, y) =>
            {
                Int32 FirstIndex = 0;
                Int32 SecondIndex = 1;
                Int32 ThirdIndex = 2;
                if (x[FirstIndex] - y[FirstIndex] != 0)
                {
                    return x[FirstIndex] - y[FirstIndex];
                }
                else
                {
                    if (x[SecondIndex] - y[SecondIndex] != 0)
                    {
                        return x[SecondIndex] - y[SecondIndex];
                    }
                    else
                    {
                        return x[ThirdIndex] - y[ThirdIndex];
                    }

                }
            }));

            Int64 MaxRS = 0;


            Int32[] Points = queries.Select(q => q[0]).Distinct().ToArray();

            Int64[] RS = new Int64[n + 1];
            Int32 next = 0;
            Boolean nextfound = false;

            for (int i = 0; i < Points.Length; i++)
            {

                Int32 QH = 0;
                for (int j = 0; j < m; j++)
                {
                    if (queries[j][0] > Points[i])
                    {
                        continue;
                    }
                    if (queries[j][0] <= Points[i] && queries[j][1] >= Points[i])
                    {
                        RS[Points[i]] += queries[j][2];
                        QH++;
                    }
                }
                if (QH == m)
                {
                    Console.WriteLine("QGF");
                    return new ArrayManipulationAnswer() { MaxValue = RS.Max() };
                }
            }

            MaxRS = RS.Max();
            return new ArrayManipulationAnswer() { MaxValue = MaxRS };


        }

        [SolutionMethod]
        public ArrayManipulationAnswer DP3(ArrayManipulationSample sample)
        {

            Int32 n = sample.n;
            Int32[][] queries = sample.queries;

            Int32 m = queries.Length;

            Array.Sort(queries, new Comparison<Int32[]>((x, y) =>
            {
                Int32 FirstIndex = 0;
                Int32 SecondIndex = 1;
                Int32 ThirdIndex = 2;
                if (x[FirstIndex] - y[FirstIndex] != 0)
                {
                    return x[FirstIndex] - y[FirstIndex];
                }
                else
                {
                    if (x[SecondIndex] - y[SecondIndex] != 0)
                    {
                        return x[SecondIndex] - y[SecondIndex];
                    }
                    else
                    {
                        return x[ThirdIndex] - y[ThirdIndex];
                    }

                }
            }));

            Int64 MaxRS = 0;

            var Points = queries.Select(q => q[0]).Distinct().ToArray();


            //Int64[] RS = new Int64[Points.Length];
            unsafe
            {
                Int64* RS = stackalloc Int64[Points.Length];


                Int32 next = 0;
                Int32 j;
                Boolean nextFound = false;
                for (int i = 0; i < m; i++)
                {
                    j = next;
                    
                    while (j < Points.Length && Points[j] >= queries[i][0] && Points[j] <= queries[i][1])
                    {
                        RS[j] += queries[i][2];
                        if (!nextFound && i < m - 1 && queries[i + 1][0] == Points[j])
                        {
                            next = j;
                            nextFound = true;
                        }
                        j++;
                        if (MaxRS < RS[j])
                        {
                            MaxRS = RS[j];
                        }
                    }
                    if (!nextFound && i < m - 1 && j < Points.Length)
                    {

                        while (j < Points.Length && queries[i + 1][0] != Points[j])
                        {
                            j++;
                        }
                        next = j;
                    }
                    nextFound = false;

                }



                //MaxRS = RS.Max();
            }
            return new ArrayManipulationAnswer() { MaxValue = MaxRS };


        }

        public override bool CheckAnswer(int SampleID, ArrayManipulationAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
            return Answer?.MaxValue == Answers?[SampleID]?.MaxValue;
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
