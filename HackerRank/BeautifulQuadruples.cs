using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class BeautifulQuadruplesSample
    {
        public Int32[] arr;
    }

    public class BeautifulQuadruplesAnswer
    {
        public Int64 result;
        public override string ToString()
        {
            return result.ToString();
        }
    }


    class BeautifulQuadruples : TProblem<BeautifulQuadruplesSample, BeautifulQuadruplesAnswer>
    {
        private Random rnd = new Random();


        public override void GenSamples()
        {
            /*
            
            Samples.Add(new BeautifulQuadruplesSample() { arr = new Int32[] { 1, 2, 3, 4 } });
            Answers.Add(new BeautifulQuadruplesAnswer() { result = 11 });

            Samples.Add(new BeautifulQuadruplesSample() { arr = new Int32[] { 2, 3, 4, 5 } }); // повторов 78 + 15 нолей = 93
            Answers.Add(new BeautifulQuadruplesAnswer() { result = 34 });
            

            Samples.Add(new BeautifulQuadruplesSample() { arr = new Int32[] { 5, 6, 7, 8 } }); // всего 1680, повторов 1405, уникальных 275 Ответ 243
            Answers.Add(new BeautifulQuadruplesAnswer() { result = 243 });
            */
            Samples.Add(new BeautifulQuadruplesSample() { arr = new Int32[] { 1951, 2709, 1793, 129 } }); // всего 1680, повторов 1405, уникальных 275 Ответ 243
            Answers.Add(new BeautifulQuadruplesAnswer() { result = 317714055759 });


            

                


        }

        //[SolutionMethod]
        public BeautifulQuadruplesAnswer Straight(BeautifulQuadruplesSample sample)
        {
            Array.Sort(sample.arr);
            Int32 a = sample.arr[0];
            Int32 b = sample.arr[1];
            Int32 c = sample.arr[2];
            Int32 d = sample.arr[3];

            Int32 res = 0;


            for (Int32 ia = 1; ia <= a; ia++)
            {
                for (Int32 ib = ia; ib <= b; ib++)
                {
                    for (Int32 ic = ib; ic <= c; ic++)
                    {
                        for (Int32 id = ic; id <= d; id++)
                        {
                            Int32 X = ((id ^ ic) ^ ib) ^ ia;
                            if (X != 0)
                            {
                                res++;
                            }
                        }
                    }
                }
            }


            return new BeautifulQuadruplesAnswer() { result = res };
        }


        public override bool CheckAnswer(int SampleID, BeautifulQuadruplesAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
            return Answer.result == Answers[SampleID].result;
        }

        //[SolutionMethod]
        public BeautifulQuadruplesAnswer DP(BeautifulQuadruplesSample sample)
        {
            Array.Sort(sample.arr);
            Int32 a = sample.arr[0];
            Int32 b = sample.arr[1];

            Int32 c = sample.arr[2];
            Int32 d = sample.arr[3];

            Int32[] RP = new Int32[d + 1];
            RP[0] = 0;
            RP[1] = 0;
            RP[2] = 1;
            for (int i = 3; i < d + 1; i++)
            {
                RP[i] = RP[i - 1] + i - 1;
            }

            /*
            Int32 MaxXor = (1 << (int)(Math.Log(d, 2) + 1)) - 1;
            
            Int32[,] dp = new Int32[4, MaxXor];


            Int32 NP = a * c + a * b;
            Int32 TotalCombinations = a * b * c * d;
            //Int32 doubles = a * ((b - a) * (d - c));
            Int32 doubles = 0;
            for (int ia = 1; ia <= a; ia++)
            {
                for (int ib = ia; ib <= b; ib++)
                {
                    for (int ic = ib; ic <= c; ic++)
                    {
                        for (int id = ic; id <= d; id++)
                        {
                            doubles++;
                        }
                    }
                }
            }
            */


            //Int32[] ar = Enumerable.Range(1, a).Concat(Enumerable.Range(1, b)).Concat(Enumerable.Range(1, c)).Concat(Enumerable.Range(1, d)).ToArray();
            Int32[] ar = Enumerable.Range(1, d).ToArray();
            Int32 MaxXor = (1 << (int)(Math.Log(d, 2) + 1)) - 1;

            Int32[] counts = new Int32[d + 1];
            foreach (var val in ar)
            {
                counts[val]++;
            }
            ar = ar.Distinct().ToArray();




            Int32 n = ar.Length;



            Int64[,] dp = new Int64[n + 1, MaxXor + 1];

            dp[0, 0] = 1;


            for (Int32 i = 1; i <= n; i++)
            {
                for (int j = 0; j <= MaxXor; j++)
                {
                    dp[i, j] =
                        (((dp[i - 1, j] * (1 + counts[ar[i - 1]] / 2)))
                        +
                        ((dp[i - 1, j ^ ar[i - 1]] * ((counts[ar[i - 1]] + 1) / 2))))
                        ;
                }
            }


            Int32 res = 0;

            Int32 doubles = 0;

            for (int ia = 1; ia <= a; ia++)
            {
                for (int ib = 1; ib <= b; ib++)
                {
                    for (int ic = 1; ic <= c; ic++)
                    {
                        for (int id = 1; id <= d; id++)
                        {
                            if ((id < ic) || (id < ib) || (id < ia) || (ic < ib) || (ic < ia) || (ib < ia))
                            {
                                doubles++;
                                continue;
                            }
                            Int32 X = ia ^ ib;
                            X = X ^ ic;
                            X = X ^ id;
                            if (X == 0)
                            {
                                Console.WriteLine($"{ia}^{ib}^{ic}^{id} == 0");
                            }
                            else
                            {
                                res++;
                            }


                        }
                    }
                }
            }


            Int32 bBccBd = 0;
            for (int x = 1; x <= b - 2; x++)
            {
                for (int z = 3; z <= b; z++)
                {
                    for (int y = x + 1; y < z; y++)
                    {
                        Console.WriteLine($"{z} {y} {x}");
                        bBccBd++;
                    }

                }
            }

            Int32 uniques = 0;
            Int32 uniques1 = 0;
            for (int ia = 1; ia <= a; ia++)
            {
                for (int ib = ia; ib <= b; ib++)
                {
                    for (int ic = ib; ic <= c; ic++)
                    {
                        uniques1 += d - ic + 1;
                    }
                }
            }

            Int32[] FB = new Int32[d + 1];
            Int32[] FBFB = new Int32[d + 1];
            FB[0] = 0;
            FBFB[0] = 0;
            for (int i = 1; i <= d; i++)
            {
                FB[i] = FB[i - 1] + i;
                FBFB[i] = FBFB[i - 1] + FB[i];
            }



            Int32[] sc = new Int32[c + 1];
            sc[0] = 0;
            for (int ic = 1; ic <= c; ic++)
            {
                sc[ic] = ic * d - FB[ic] + ic;
            }

            Int32[] sb = new Int32[b + 1];
            sb[0] = sc[c];
            for (int ib = 1; ib <= b; ib++)
            {
                sb[ib] = sb[ib - 1] - ib + sc[b] + 1;
            }



            for (int ia = 1; ia <= a; ia++)
            {
                for (int ib = ia; ib <= b; ib++)
                {
                    uniques += 123;
                }
            }


            Int32[,] fbdp = new Int32[d + 1, d + 1];
            for (int i = 0; i <= d; i++)
            {
                for (int j = i; j <= d; j++)
                {
                    fbdp[i, j] = FB[j] - FB[i];
                }
            }



            Console.WriteLine($"{uniques} {uniques1}");
            Int32 NP = a * c + a * b;
            res = uniques1 - NP;

            //Console.WriteLine(bBccBd);
            //doubles = RP[a] * 3 + RP[b] * 2 + RP[c];


            //res = TotalCombinations - doubles - NP;

            return new BeautifulQuadruplesAnswer() { result = res };
        }

        static void sampleInfo(BeautifulQuadruplesSample sample)
        {
            Array.Sort(sample.arr);
            Int32 a = sample.arr[0];
            Int32 b = sample.arr[1];

            Int32 c = sample.arr[2];
            Int32 d = sample.arr[3];

            Int32 res = 0;
            Int32 doubles = 0;
            Int32 uniques = 0;
            Int32 Zeroes1 = 0;
            Int32 Zeroes2 = 0;
            Int32 Zeroes = 0;


            for (int ia = 1; ia <= a; ia++)
            {
                for (int ib = 1; ib <= b; ib++)
                {
                    for (int ic = 1; ic <= c; ic++)
                    {
                        for (int id = 1; id <= d; id++)
                        {
                            if ((id < ic) || (id < ib) || (id < ia) || (ic < ib) || (ic < ia) || (ib < ia))
                            {
                                doubles++;
                                continue;
                            }
                            uniques++;
                            Int32 X = ia ^ ib;
                            X = X ^ ic;
                            X = X ^ id;
                            if (X == 0)
                            {
                                Zeroes++;
                                Zeroes1++;
                                if (ia != ib && ia != ic)
                                {
                                    Zeroes2++;
                                }
                                Console.WriteLine($"{ia}^{ib}^{ic}^{id} == 0");


                            }
                            else
                            {
                                res++;
                            }


                        }
                    }
                }
            }
            Console.WriteLine($"Total combs {a * b * c * d} Uniques {uniques} Doubles {doubles} Right answer {res } Total Zeroes {Zeroes} Zeroes2 {Zeroes2}");
        }
        [SolutionMethod]
        public BeautifulQuadruplesAnswer DP2(BeautifulQuadruplesSample sample)
        {
            
            //sampleInfo(sample);
            Array.Sort(sample.arr);
            Int32 a = sample.arr[0];
            Int32 b = sample.arr[1];

            Int32 c = sample.arr[2];
            Int32 d = sample.arr[3];

            Int64[] F = new Int64[d + 1];

            F[0] = 0;
            for (int i = 1; i <= d; i++)
            {
                F[i] = F[i - 1] + i;
            }

            Int64[,] SB = new Int64[b + 1, 2];
            SB[1, 0] = d * c - F[c - 1];
            SB[1, 1] = SB[1, 0];
            for (int i = 2; i <= b; i++)
            {
                SB[i, 0] = d * c - F[c - 1] - (d * (i - 1) - F[i - 2]);
                SB[i, 1] = SB[i - 1, 1] + SB[i, 0];
            }


            Int64[,] SA = new Int64[a + 1, 2];

            SA[1, 0] = SB[b, 1];
            SA[1, 1] = SB[b, 1];

            for (int i = 2; i <= a; i++)
            {
                SA[i, 0] = SA[i - 1, 0] - SB[i - 1, 0];
                SA[i, 1] = SA[i - 1, 1] + SA[i, 0];

            }

            Int64 Nulls = a * c - F[a - 1];



            Int64 result = SA[a, 1] - Nulls;

            Int32 MaxXor = (1 << (int)(Math.Log(d, 2) + 1)) - 1;
           

            Int32[,] counts1 = new Int32[MaxXor + 1, b+1];
            for (int i = 1; i <= a; i++)
            {
                for (int j = i+1; j <= b; j++)
                {
                    counts1[i ^ j, j]++;
                }
            }



            Int32[,] counts2 = new Int32[MaxXor + 1, c + 1];

            for (int i = 1; i <= c; i++)
            {
                for (int j = i + 1; j <= d; j++)
                {
                    counts2[i ^ j, i]++;
                }
            }

            for (int i = 1; i <= MaxXor; i++)
            {
                for (int j = c; j >= 1; j--)
                {
                    counts2[i, j-1] += counts2[i, j];
                }


            }


            for (int i = 1; i <= MaxXor; i++)
            {
                for (int j = 1; j <= b; j++) {
                    result -= counts2[i, j] * counts1[i, j];
                }
                    
                
            }


            return new BeautifulQuadruplesAnswer() { result = result };
        }

        [SolutionMethod]
        public BeautifulQuadruplesAnswer DP3(BeautifulQuadruplesSample sample)
        {
            
            Array.Sort(sample.arr);
            Int32 a = sample.arr[0];
            Int32 b = sample.arr[1];

            Int32 c = sample.arr[2];
            Int32 d = sample.arr[3];

            Int32 MaxXor = (1 << (int)(Math.Log(d, 2) + 1)) - 1;

            Int32[,] cnt = new Int32[MaxXor + 1, MaxXor + 1];

            Int32[] tot = new Int32[MaxXor + 1];
            Int32 ans = 0;

            for (int i = 1; i <= a; ++i)
            {
                for (int j = i; j <= b; ++j)
                {
                    ++cnt[i ^ j,j];
                    ++tot[j];
                }
            }

            for (int i = 0; i < MaxXor; ++i)
            {
                for (int j = 1; j < MaxXor; ++j)
                {
                    cnt[i,j] += cnt[i,j - 1];
                }
                if (i > 0)
                {
                    tot[i] += tot[i - 1];
                }
            }

            for (int i = 1; i <= c; ++i)
            {
                for (int j = i; j <= d; ++j)
                {
                    ans += tot[i] - cnt[i ^ j, i];
                }
            }
            return new BeautifulQuadruplesAnswer() { result = ans };
        }
    }

}
