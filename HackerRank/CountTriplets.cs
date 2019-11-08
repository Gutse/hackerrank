using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class CountTripletsSample
    {
        public Int64 r;
        public List<Int64> arr;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CountTripletsAnswer
    {
        public Int64 res;
        public override string ToString()
        {
            return res.ToString();
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


    class CountTriplets : TProblem<CountTripletsSample, CountTripletsAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, CountTripletsAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
            return Answer?.res == Answers?[SampleID]?.res;
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
        private void LoadSamples()
        {

            using (System.IO.StreamReader reader = new System.IO.StreamReader(@"d:\dia\projects\hackerrank\ctr_input06.txt"))
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(@"d:\Dia\Projects\hackerrank\RKSumInput"))
            {
                string[] nm = reader.ReadLine().Split(' ');
                Int64 n = Convert.ToInt32(nm[0]);
                Int64 r = Convert.ToInt32(nm[1]);

                List<Int64> list = new List<Int64>();

                List<long> arr = reader.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt64(arrTemp)).ToList();


                CountTripletsSample sample = new CountTripletsSample() { arr = arr, r = r };
                //sample.seq = Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt64(sTemp));
                Samples.Add(sample);
            }


            using (System.IO.StreamReader reader = new System.IO.StreamReader(@"d:\dia\projects\hackerrank\ctr_output06.txt"))
            {
                CountTripletsAnswer ans = new CountTripletsAnswer() { res = Convert.ToInt64(reader.ReadLine()) };
                Answers.Add(ans);
            }


        }

        public override void GenSamples()
        {
            //166661666700000
            Samples.Add(new CountTripletsSample() { arr = new List<long> { 1, 2, 4, 6, 3,8,6,12,24,24 }, r = 2 });
            Answers.Add(new CountTripletsAnswer() { res = 7 });

            LoadSamples();
            Samples.Add(new CountTripletsSample() { arr = new List<long> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, r = 1 });
            Answers.Add(new CountTripletsAnswer() { res = 161700 });

            Samples.Add(new CountTripletsSample() { arr = new List<long> { 1, 2, 2, 4 }, r = 2 });
            Answers.Add(new CountTripletsAnswer() { res = 2 });

            Samples.Add(new CountTripletsSample() { arr = new List<long> { 1, 3, 9, 9, 27, 81 }, r = 3 });
            Answers.Add(new CountTripletsAnswer() { res = 6 });

            Samples.Add(new CountTripletsSample() { arr = new List<long> { 1, 5, 5, 25, 125 }, r = 5 });
            Answers.Add(new CountTripletsAnswer() { res = 4 });
        }

       // [SolutionMethod]
        public CountTripletsAnswer BruteForce(CountTripletsSample sample)
        {
            Int64 r = sample.r;
            List<Int64> arr = sample.arr;
            //arr.Sort();
            Int64 c2 = r;
            Int64 c3 = r * r;
            Int64 res = 0;
            for (int i = 0; i < arr.Count - 2; i++)
            {
                for (int j = i + 1; j < arr.Count - 1; j++)
                {
                    if (arr[i] * c2 == arr[j])
                    {
                        for (int k = j + 1; k < arr.Count; k++)
                        {
                            if (arr[j] == arr[i] * c2 && arr[k] == arr[i] * c3)
                            {
                                res++;
                            }

                        }

                    }

                }
            }


            return new CountTripletsAnswer() { res = res };
        }

        [SolutionMethod]
        public CountTripletsAnswer BruteForce2(CountTripletsSample sample)
        {
            Int64 r = sample.r;
            List<Int64> arr = sample.arr;
            List<Int64> darr = arr.Distinct().ToList();

            Int64 res = 0;

            Dictionary<Int64, Int64> cnt = new Dictionary<long, long>();
            if (r == 1)
            {
                foreach (var item in darr)
                {
                    cnt.Add(item, 0);
                }
            }
            else
            {
                Int64 value = 1;
                cnt.Add(1, 0);
                while (value < arr[arr.Count - 1])
                {
                    value = value * r;
                    cnt.Add(value, 0);
                }
            }

            for (int i = 0; i < arr.Count; i++)
            {
                if (cnt.ContainsKey(arr[i]))
                {
                    cnt[arr[i]]++;
                }
            }



            if (r == 1)
            {
                for (int i = 0; i < darr.Count; i++)
                {
                    res += cnt[darr[i]] * (cnt[darr[i]] - 1) * (cnt[darr[i]] - 2) / 6;
                }
            }
            else
            {
                for (int i = 0; i < darr.Count - 2; i++)
                {
                    if (darr[i] * r == darr[i + 1] && darr[i] * r * r == darr[i + 2])
                    {
                        res += cnt[darr[i]] * cnt[darr[i + 1]] * cnt[darr[i + 2]];
                    }
                }
            }


            return new CountTripletsAnswer() { res = res };
        }
//        [SolutionMethod]
        public CountTripletsAnswer BruteForce3(CountTripletsSample sample)
        {
            Int64 r = sample.r;
            List<Int64> arr = sample.arr;
            Int64 res = 0;


            //arr.Sort();
            List<Int64> darr = arr.Distinct().ToList();
            List<HashSet<Int64>> progressions = new List<HashSet<Int64>>();
            List<HashSet<Int64>> arrays = new List<HashSet<Int64>>();

            Dictionary<Int64, Int64> cnt = new Dictionary<Int64, Int64>();

            if (r == 1)
            {
                foreach (var item in darr)
                {
                    cnt.Add(item, 0);
                }
                for (int i = 0; i < arr.Count; i++)
                {
                    if (cnt.ContainsKey(arr[i]))
                    {
                        cnt[arr[i]]++;
                    }
                }
                for (int i = 0; i < darr.Count; i++)
                {
                    res += cnt[darr[i]] * (cnt[darr[i]] - 1) * (cnt[darr[i]] - 2) / 6;
                }
            }
            else
            {

                for (int i = 0; i < arr.Count; i++)
                {
                    if (cnt.ContainsKey(arr[i]))
                    {
                        cnt[arr[i]]++;
                    }
                    else
                    {
                        cnt.Add(arr[i], 1);
                    }

                    Int32 CPIndex = -1;

                    for (int j = 0; j < progressions.Count; j++)
                    {
                        if (progressions[j].Contains(arr[i]))
                        {
                            CPIndex = j;
                            break;
                        }
                    }
                    if (CPIndex == -1)
                    {

                        progressions.Add(new HashSet<Int64>());
                        arrays.Add(new HashSet<Int64>());
                        CPIndex = progressions.Count - 1;
                        Int64 CurrentElem = arr[i];

                        progressions[CPIndex].Add(CurrentElem);
                        /*
                        while (CurrentElem % r == 0) {
                            CurrentElem /= r;
                            progressions[CPIndex].Add(CurrentElem);
                        }
                        CurrentElem = arr[i];
                        */



                        while (CurrentElem < arr[arr.Count - 1])
                        {
                            CurrentElem *= r;
                            progressions[CPIndex].Add(CurrentElem);

                            if (!cnt.ContainsKey(CurrentElem))
                            {
                                cnt.Add(CurrentElem, 0);
                            }
                        }
                    }

                    for (int j = 0; j < progressions.Count; j++)
                    {
                        if (progressions[j].Contains(arr[i]))
                        {
                            arrays[j].Add(arr[i]);
                        }
                    }
                    
                }

                List<Int64[]> AProgressions = new List<long[]>();
                List<Int64[]> AArrays = new List<long[]>();
                for (int i = 0; i < progressions.Count; i++)
                {
                    AProgressions.Add(progressions[i].ToArray());
                    AArrays.Add(arrays[i].ToArray());
                }


                for (int i = 0; i < arrays.Count; i++)
                {
                    var ca = AArrays[i];
                    for (int j = 0; j < ca.Length-2; j++)
                    {
                        Int32 CPIndex = -1;
                        for (int k = 0; k < progressions.Count; k++)
                        {
                            if (progressions[k].Contains(ca[j]))
                            {
                                //CPIndex = k;
                                //break;
                            }
                        }
                        if (ca[j] * r == ca[j + 1] && ca[j] * r * r == ca[j + 2])
                        {
                            res += cnt[ca[j]] * cnt[ca[j + 1]] * cnt[ca[j + 2]];
                        }

                    }



                }


               
            }



            return new CountTripletsAnswer() { res = res };
        }
        [SolutionMethod]
        public CountTripletsAnswer BruteForce4(CountTripletsSample sample)
        {
            Int64 r = sample.r;
            List<Int64> arr = sample.arr;
            Int64 res = 0;

            if (r == 1)
            {
                List<Int64> darr = arr.Distinct().ToList();
                Dictionary<Int64, Int64> cnt = new Dictionary<Int64, Int64>();
                foreach (var item in darr)
                {
                    cnt.Add(item, 0);
                }
                for (int i = 0; i < arr.Count; i++)
                {
                    if (cnt.ContainsKey(arr[i]))
                    {
                        cnt[arr[i]]++;
                    }
                }
                for (int i = 0; i < darr.Count; i++)
                {
                    res += cnt[darr[i]] * (cnt[darr[i]] - 1) * (cnt[darr[i]] - 2) / 6;
                }
            }
            else
            {

                Dictionary<Int64, Int64> aw1 = new Dictionary<long, long>();
                Dictionary<Int64, Int64> aw2 = new Dictionary<long, long>();

                for (int i = 0; i < arr.Count; i++)
                {
                    //число триплетов ожидающих ai*r +1
                    if (aw1.ContainsKey(arr[i] * r))
                    {
                        aw1[arr[i] * r]++;
                    }
                    else
                    {
                        aw1.Add(arr[i] * r, 1);
                    }


                    //
                    if (aw1.ContainsKey(arr[i]))
                    {
                        if (aw2.ContainsKey(arr[i] * r))
                        {
                            aw2[arr[i] * r]+=aw1[arr[i]];
                        }
                        else
                        {
                            aw2.Add(arr[i] * r, aw1[arr[i]]);
                        }
                        // aw1[arr[i]] = 0;
                    }

                    if (aw2.ContainsKey(arr[i]))
                    {
                        res += aw2[arr[i]];
                        //aw2[arr[i]] = 0;
                    }


                }
            }

            return new CountTripletsAnswer() { res = res };
        }

    }
}
