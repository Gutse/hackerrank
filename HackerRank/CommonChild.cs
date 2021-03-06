﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class CommonChildSample: TSample
    {
        public String s1;
        public String s2;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CommonChildAnswer:TAnswer
    {
        public Int32 result;
        public override string ToString()
        {
            return result.ToString();
        }
        public override Boolean Equals(Object obj)
        {
            return (obj as CommonChildAnswer)?.result == this.result;
        }
    }


    class CommonChild : TProblem
    {
        public override void CreateSamples(StreamReader reader)
        {
            CommonChildSample sample = new CommonChildSample() { s1 = reader.ReadLine(), s2 = reader.ReadLine() };
            Samples.Add(sample);
        }

        public override void CreateAnswers(StreamReader reader)
        {
            CommonChildAnswer ans = new CommonChildAnswer() { result = Convert.ToInt32(reader.ReadLine()) };
            Answers.Add(ans);
        }


        static Int32 CheckStr(String s1, Int32 s1Start, String s2, Int32 s2Start)
        {

            Int32 Max = 0;

            for (int i = s1Start; i < s1.Length; i++)
            {
                for (int j = s2Start; j < s2.Length; j++)
                {
                    if (s1[i] == s2[j])
                    {
                        Int32 tmp = CheckStr(s1, i + 1, s2, j + 1) + 1;
                        if (tmp > Max)
                        {
                            Max = tmp;
                        }
                    }
                }
            }

            return Max;

        }
        //[SolutionMethod]
        public CommonChildAnswer BF(CommonChildSample sample)
        {
            Int32 result = 0;
            String s1 = sample.s1;
            String s2 = sample.s2;

            result = CheckStr(s1,0,s2,0);

            return new CommonChildAnswer() { result = result };
        }

        static Int32 CheckStrDP(String s1, Int32 s1Start, String s2, Int32 s2Start, Int32[,] DP)
        {

            if (DP[s1Start, s2Start] != -1)
            {
                return DP[s1Start, s2Start];
            }


            Int32 Max = 0;

            for (int i = s1Start; i < s1.Length; i++)
            {
                for (int j = s2Start; j < s2.Length; j++)
                {
                    if (s1[i] == s2[j])
                    {
                        Int32 tmp = CheckStrDP(s1, i + 1, s2, j + 1, DP) + 1;
                        if (tmp > Max)
                        {
                            Max = tmp;
                        }
                    }
                }
            }

            DP[s1Start, s2Start] = Max;
            return Max;

        }

        [SolutionMethod]
        public TAnswer DP(TSample Sample)
        {
            CommonChildSample sample = Sample as CommonChildSample;

            Int32 result = 0;
            String s1 = sample.s1;
            String s2 = sample.s2;
            Int32[,] DP = new Int32[s1.Length + 1, s2.Length + 1];
            /*
            Console.WriteLine();
            for (int i = 0; i < s1.Length; i++)
            {
                String str = i.ToString().PadLeft(2);
                Console.Write($"{str} ");
            }

            Console.WriteLine();
            */

            for (int i = 0; i < s1.Length; i++)
            {
                for (int j = 0; j < s2.Length; j++)
                {
                    if (s1[i] == s2[j])
                    {
                        DP[i, j] = 1;
                    }
                }
            }

            for (int j = s2.Length - 1; j >= 0; j--)
            {
                for (int i = s1.Length - 1; i >= 0; i--)
                {
                    Int32 M = Math.Max(DP[i + 1, j], DP[i + 1, j + 1] + DP[i, j]);
                    M = Math.Max(M, DP[i, j + 1]);
                    //DP[i, j] = Math.Max(DP[i+1,j], DP[i+1, j+1]+ DP[i,j]);
                    DP[i, j] = M;
                    if (DP[i,j]> result)
                    {
                        result = DP[i, j];
                    }
                }
                /*
                for (int i = 0; i < s1.Length; i++)
                {
                    Console.Write($"{DP[i, j].ToString().PadLeft(2)} ");
                }
                Console.WriteLine();
                */

            }


            /*
            for (int i = 0; i < s1.Length; i++)
            {
                if (DP[i, s2.Length] > result)
                {
                    result = DP[s1.Length-1, i];
                }
            }
            */


            return new CommonChildAnswer() { result = result };
        }

       


    }
}
