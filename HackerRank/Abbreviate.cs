using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class AbbreviateSample: TSample
    {
        public  String A;
        public  String B;
    }

    public class AbbreviateAnswer : TAnswer
    {
        public String Answer;
        public override string ToString()
        {
            return Answer;
        }

        public override Boolean Equals(Object obj)
        {
            AbbreviateAnswer Answer = obj as AbbreviateAnswer;
            return Answer.Answer == this.Answer;
        }

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }
    }



    class Abbreviate : TProblem
    {

        public override void CreateSamples(StreamReader reader)
        {
            int q = Convert.ToInt32(reader.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string a = reader.ReadLine();
                string b = reader.ReadLine();
                AbbreviateSample sample = new AbbreviateSample() { A = a, B = b };
                Samples.Add(sample);
            }


        }

        public override void CreateAnswers(StreamReader reader)
        {

            while (!reader.EndOfStream)
            {
                Answers.Add(new AbbreviateAnswer() { Answer = reader.ReadLine() });
            }
            

        }

        static byte[] NumStringToByteArray(String s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            Byte ZeroCode = (Byte)'0';
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= ZeroCode;
            }
            return bytes;
        }

        [SolutionMethod]
        public TAnswer Solution1(TSample Sample)
        {
            AbbreviateSample sample = Sample as AbbreviateSample;
            return new AbbreviateAnswer() { Answer = abbreviation(sample.A, sample.B) };
        }
        [SolutionMethod]
        public TAnswer Solution2(TSample Sample)
        {
            AbbreviateSample sample = Sample as AbbreviateSample;
            return new AbbreviateAnswer() { Answer = abbreviation2(sample.A, sample.B) };
        }
        [SolutionMethod]
        public TAnswer Solution3(TSample Sample)
        {
            AbbreviateSample sample = Sample as AbbreviateSample;
            return new AbbreviateAnswer() { Answer = abbreviation3(sample.A, sample.B) };
        }
        [SolutionMethod]
        public TAnswer Solution4(TSample Sample)
        {
            AbbreviateSample sample = Sample as AbbreviateSample;
            return new AbbreviateAnswer() { Answer = abbreviation4(sample.A, sample.B) };
        }

        static string abbreviation(string a, string b)
        {
            String aU = a.ToUpper();
            Int32 BPtr = 0;
            for (int j = 0; j < a.Length; j++)
            {
                if (aU[j] == b[BPtr])
                {
                    BPtr++;
                    if (BPtr == b.Length)
                    {
                        bool flag = true;
                        for (int k = j + 1; k < a.Length; k++)
                        {
                            if (Char.IsUpper(a[k]))
                            {
                                flag = false;
                                return ("NO");
                            }
                        }
                        if (flag)
                        {
                            return ("YES");
                        }
                    }
                }
                else
                {
                    if (Char.IsUpper(a[j]))
                    {
                        return "NO";
                    }
                }
            }
            if (BPtr < b.Length)
            {
                return ("NO");
            }
            else return "YES";
        }

        static string abbreviation2(string a, string b)
        {
            String aU = a.ToUpper();
            Int32 BPtr = 0;
            Int32 APtr = -1;
            Int32 APtrPrev = -1;
            while (true)
            {
                APtr++;
                if (APtr == a.Length)
                {
                    return "NO";
                }
                if (aU[APtr] == b[BPtr])
                {
                    BPtr++;
                    APtrPrev = APtr;
                    if (BPtr == b.Length)
                    {
                        bool flag = true;
                        for (int k = APtr + 1; k < a.Length; k++)
                        {
                            if (Char.IsUpper(a[k]))
                            {
                                flag = false;
                                //return ("NO");
                                if (Char.IsUpper(a[APtr]))
                                {
                                    return "NO";
                                }
                                else
                                {
                                    APtr = APtrPrev;
                                    BPtr--;

                                }

                            }
                        }
                        if (flag)
                        {
                            return ("YES");
                        }
                    }
                }
                else
                {
                    if (Char.IsUpper(a[APtr]))
                    {
                        //return "NO";
                        if (APtrPrev > -1 && Char.IsUpper(a[APtrPrev]))
                        {
                            return "NO";
                        }
                        BPtr--;
                        if (BPtr < 0)
                        {
                            return "NO";
                        }
                        APtr = APtrPrev;
                    }
                }
            }

            if (BPtr < b.Length)
            {
                return ("NO");
            }
            else return "YES";
        }

        static string abbreviation3(string a, string b)
        {
            if (a.Length == 0 || b.Length == 0)
            {
                return "NO";
            }

            if (a.Length < b.Length)
            {
                return "NO";
            }


            String aU = a.ToUpper();


            for (int i = 0; i < a.Length; i++)
            {
                if (aU[i] == b[0])
                {
                    if (b.Length == 1)
                    {
                        bool flag = true;
                        for (int k = i + 1; k < a.Length; k++)
                        {
                            if (Char.IsUpper(a[k]))
                            {
                                return ("NO");
                            }
                        }
                        if (flag)
                        {
                            return ("YES");
                        }
                    }

                    var ans = abbreviation3(a.Substring(i + 1), b.Substring(1));
                    if (ans == "YES")
                    {
                        return "YES";
                    }
                    else
                    {
                        if (Char.IsUpper(a[i]))
                        {
                            return "NO";
                        }

                    }
                }
                else
                {

                    if (Char.IsUpper(a[i]))
                    {
                        return "NO";
                    }
                }

            }
            return "NO";


        }


        static string abbreviation4(string a, string b)
        {

            Int32[,] d = new Int32[a.Length + 1, b.Length + 1];
            String aU = a.ToUpper();

            for (int j = 0; j <= b.Length; j++)
            {
                if (j == 0)
                {
                    d[0, j] = 1;
                }
                else
                {
                    d[0, j] = 0;
                }
            }

            int count = 0;

            for (int k = 1; k <= a.Length; k++)
            {
                int i = k - 1;
                //if (a[i] >= 65 && a[i] <= 90 || count == 1)
                if (Char.IsUpper(a[i]) || count == 1)
                {
                    count = 1;
                    d[k, 0] = 0;
                }
                else { d[k, 0] = 1; }
            }

            for (int k = 1; k <= a.Length; k++)
            {
                int i = k - 1;
                for (int l = 1; l <= b.Length; l++)
                {
                    int j = l - 1;
                    if (a[i] == b[j])
                    {
                        d[k, l] = d[k - 1, l - 1];
                        continue;
                    }
                    else
                    {
                        if (aU[i] == b[j])
                        {
                            d[k,l] = d[k - 1,l - 1] | d[k - 1,l];
                            continue;
                        }
                    }
                    if (a[i] >= 65 && a[i] <= 90)
                    {
                        d[k,l] = 0;
                        continue;
                    }
                    else
                    {
                        d[k,l] = d[k - 1,l];
                        continue;
                    }
                }
            }

            if (d[a.Length, b.Length] > 0) return "YES";
            else return "NO";



        }



    }
}
