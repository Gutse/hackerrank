using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class RKSumSample: TSample
    {
        public Int64[] seq;
        public Int32 N;
        public Int32 K;
    }

    public class RKSumAnswer: TAnswer
    {
        public Int64[] a;
        public override string ToString()
        {
            StringBuilder SB = new StringBuilder();
            foreach (var item in a)
            {
                SB.Append($"{item} ");
            }
            return SB.ToString().Trim();
        }
        public override Boolean Equals(Object obj)
        {

            RKSumAnswer Answer = obj as RKSumAnswer;
             if (Answer?.a?.Length != a?.Length)
            {
                return false;
            }

            for (int i = 0; i < Answer.a.Length; i++)
            {
                if (Answer.a[i] != a[i])
                {
                    return false;
                }
            }
            return true;
        }


    }


    class RKSum : TProblem
    {
        private Random rnd = new Random();
        public override void CreateSamples(System.IO.StreamReader reader)
        {
            Int32 sq = 0;
            Int32.TryParse(reader.ReadLine(), out sq);
            for (int i = 0; i < sq; i++)
            {
                string[] nm = reader.ReadLine().Split(' ');
                RKSumSample sample = new RKSumSample() { N = Convert.ToInt32(nm[0]), K = Convert.ToInt32(nm[1]) };
                sample.seq = Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt64(sTemp));
                Samples.Add(sample);
            }
        }

        public override void CreateAnswers(System.IO.StreamReader reader)
        {
            while(!reader.EndOfStream)
            {
                RKSumAnswer ans = new RKSumAnswer() { a = Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt64(sTemp)) };
                Answers.Add(ans);
            }

        }



        public override void AddManualSamples()
        {

            Samples.Add(new RKSumSample() { N = 1, K = 3, seq = new Int64[] { 3 } });
            Answers.Add(new RKSumAnswer() { a = new Int64[] { 1 } });
            Samples.Add(new RKSumSample() { N = 2, K = 2, seq = new Int64[] { 12, 34, 56 } });
            Answers.Add(new RKSumAnswer() { a = new Int64[] { 6, 28 } });
            Samples.Add(new RKSumSample() { N = 3, K = 2, seq = new Int64[] { 2, 3, 4, 4, 5, 6 } });
            Answers.Add(new RKSumAnswer() { a = new Int64[] { 1, 2, 3 } });
        }

        public void SampleStats(RKSumSample sample)
        {
            Int32 LenShouldbe = 1 + (sample.N - 1) * sample.K;
            if (LenShouldbe == sample.seq.Length)
            {
                Console.WriteLine("sequence lenth is ok");
            }
            else
            {
                Console.WriteLine($"sequence lenth is NOT ok, should be {LenShouldbe}  and seq len = {sample.seq.Length}");
                Dictionary<Int64, Int32> counts = new Dictionary<long, int>();
                for (int i = 0; i < sample.seq.Length; i++)
                {
                    if (counts.ContainsKey(sample.seq[i]))
                    {
                        counts[sample.seq[i]]++;
                    }
                    else
                    {
                        counts.Add(sample.seq[i], 1);
                    }
                }

                foreach (var key in counts.Keys)
                {
                    if (counts[key] > 1)
                    {
                        Console.WriteLine($"Number {key} appers {counts[key]} times");
                    }
                }
            }

        }

        [SolutionMethod]
        public TAnswer BF3(TSample Sample)
        {
            RKSumSample sample = Sample as RKSumSample;
            Int32 n = sample.N;
            Int64 k = sample.K;
            Int64[] s = sample.seq;



            List<Int64> KnownValues = new List<Int64>();
            List<Tuple<Int64, List<Int64>>> KSumsWN = new List<Tuple<long, List<long>>>();

            //ez way
            if (1 + (n - 1) * k == s.Length)
            {
                for (int i = 0; i < n; i++)
                {
                    KnownValues.Add(s[i * k] / k);
                }
            }
            else
            {
                Int64 a1 = s[0] / k;
                Int64 a2 = s[1] - a1 * (k - 1);
                Int64 a3 = s[s.Length - 1] / k;
                Int64 a4 = s[s.Length - 1 - 1] - a3 * (k - 1);
                              
                if (n == 3)
                {
                    return new RKSumAnswer() { a = new Int64[3] { a1, a2, a3 } };
                }

                if (n == 2)
                {
                    return new RKSumAnswer() { a = new Int64[2] { a1, a2 } };
                }

                if (n == 1)
                {
                    return new RKSumAnswer() { a = new Int64[1] { a1 } };
                }

                KnownValues.Add(a1);

                KSumsWN.Add(new Tuple<long, List<long>>(a1, new List<long>() { a1 * k }));

                List<List<Int64>> Counts = new List<List<Int64>>() { new List<Int64>() { k } };

                for (int i = 1; i < n; i++)
                {
                    Counts.Add(new List<Int64>());
                    for (int j = 0; j < Counts[i - 1].Count; j++)
                    {
                        for (int l = 0; l < Counts[i - 1][j]; l++)
                        {
                            Counts[i].Add(l + 1);
                        }
                    }
                }

                Stopwatch sw = new Stopwatch();

                for (int i = 1; i < s.Length; i++)
                {
                    
                    if (s[i] != 0)
                    {
                        Int64 next = s[i] - a1 * (k - 1);
                        KnownValues.Add(next);
                        if (KnownValues.Count == n)
                        {
                            break;
                        }

                        List<Int64> NewKSums = new List<long>();
                        Int64 PrevValue = KnownValues[KnownValues.Count - 2];
                        List<Int64> PrevKSums = KSumsWN[KnownValues.Count - 2].Item2;

                        var CurrentCounts = Counts[KnownValues.Count - 2];
                        Console.WriteLine($"Generating combinations for {next}");
                        sw.Start();

                        for (int j = 0; j < PrevKSums.Count; j++)
                        {
                            Int64 PrevSum = PrevKSums[j];

                            Int64 ReplaceCount = CurrentCounts[j];
                            for (int m = 0; m < ReplaceCount; m++)
                            {
                                NewKSums.Add(PrevSum - PrevValue * (m + 1) + next * (m + 1));
                            }
                        }
                        sw.Stop();
                        
                        Console.WriteLine($"{NewKSums.Count} combinations generated for {next} by {sw.Elapsed}");

                        KSumsWN.Add(new Tuple<long, List<long>>(next, NewKSums));

                        Console.WriteLine($"clearing s");
                        sw.Start();
                        /*
                        for (int j = 0; j < NewKSums.Count; j++)
                        {
                            Boolean ValueWasSkipped = false;
                            for (int ptr = i; ptr < s.Length; ptr++)
                            {
                                if (s[ptr] > NewKSums[j])
                                {
                                    break;
                                }
                                if (s[ptr] == NewKSums[j])
                                {
                                    s[ptr] = 0;
                                    break;
                                }

                                if (s[ptr] < NewKSums[j] )
                                {
                                    ValueWasSkipped = true;
                                }

                                if (!ValueWasSkipped)
                                {
                                    i = ptr;
                                }
                            }

                            if (ValueWasSkipped && KnownValues.Count == n - 1 && j == NewKSums.Count-1)
                            {
                                break;
                            }
                        }
                        */
                        List<Int64> temp = new List<Int64>(NewKSums);
                        Int64 MaxSum = NewKSums[NewKSums.Count - 1];

                        for (int ptr = i; ptr < s.Length; ptr++)
                        {
                            if (s[ptr] == 0)
                            {
                                continue;
                            }

                            Int32 index = temp.IndexOf(s[ptr]);

                            if (index != -1)
                            {
                                s[ptr] = 0;
                                temp.RemoveAt(index);
                            }
                            else {
                                if (KnownValues.Count == n-1)
                                {
                                    break;
                                }
                            }
                            if (s[ptr] > MaxSum)
                            {
                                break;
                            }
                        }


                            sw.Stop();
                        Console.WriteLine($"s cleared in {sw.Elapsed}");
                    }
                }


                if (n > KnownValues.Count)
                {
                    Console.WriteLine("Need to find doubles");
                }

            }

            return new RKSumAnswer() { a = KnownValues.ToArray() };
        }

    }
}
