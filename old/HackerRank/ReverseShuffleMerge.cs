using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class ReverseShuffleMergeSample : TSample
    {
        public String s;
        public override string ToString()
        {
            return s.ToString();
        }
    }

    public class ReverseShuffleMergeAnswer: TAnswer
    {
        public String result;
        public override string ToString()
        {
            return result;
        }
        public override Boolean Equals(Object obj)
        {
            return (obj as ReverseShuffleMergeAnswer)?.result == this.result;

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


    class ReverseShuffleMerge : TProblem
    {
        private Random rnd = new Random();

        public override void CreateSamples(System.IO.StreamReader reader)
        {
            ReverseShuffleMergeSample sample = new ReverseShuffleMergeSample() { s = reader.ReadLine() };
            Samples.Add(sample);
        }

        public override void CreateAnswers(System.IO.StreamReader reader)
        {
            ReverseShuffleMergeAnswer ans = new ReverseShuffleMergeAnswer() { result = reader.ReadLine() };
            Answers.Add(ans);

        }

        public override void AddManualSamples()
        {
            Samples.Add(new ReverseShuffleMergeSample() { s = "caaabaddbc" });
            Answers.Add(new ReverseShuffleMergeAnswer() { result = "bdaac" });
        }

        [SolutionMethod]
        public TAnswer BF(TSample Sample)
        {
            ReverseShuffleMergeSample sample = Sample as ReverseShuffleMergeSample;
            String s = sample.s;

            char[] result = new Char[s.Length / 2];

            Int32 aPos = 'a';
            Int32 zPos = 'z';

            Int32[] cnt = new Int32[zPos - aPos + 1];
            Int32 UniqueLetters = 0;
            for (int i = 0; i < s.Length; i++)
            {
                cnt[s[i] - aPos]++;
            }

            Int32[] ap = new Int32[cnt.Length];
            for (int i = 0; i < cnt.Length; i++)
            {
                ap[i] = cnt[i] / 2;
                if (cnt[i] != 0)
                {
                    UniqueLetters++;
                }
            }


            Int32 ZeroesCount = 0;
            Int32 ptr = 0;
            for (int i = 0; i < s.Length; i++)
            {
                ap[s[i] - aPos]--;
                if (ap[s[i] - aPos] == 0)
                {
                    ZeroesCount++;
                }
                if (UniqueLetters == ZeroesCount)
                {
                    ptr = i;
                    break;
                }
            }

            Char min = 'z';

            Int32 MinPtr = ptr;
            for (int i = ptr; i < s.Length; i++)
            {
                if (min >= s[i])
                {
                    min = s[i];
                    MinPtr = i;
                }
            }

            result[0] = min;


            Int32[] ReverseCnt = new Int32[cnt.Length];
            Int32[] ShuffleCnt = new Int32[cnt.Length];


            ReverseCnt[min - aPos]++;

            for (int i = MinPtr + 1; i < s.Length; i++)
            {
                ShuffleCnt[s[i] - aPos]++;
            }


            Int32 pos = 1;
            Int32 iptr = MinPtr - 1;
            
            while (iptr >= 0 && pos != result.Length)
            {
                if (ReverseCnt[s[iptr] - aPos] == cnt[s[iptr] - aPos] / 2)
                {
                    ShuffleCnt[s[iptr] - aPos]++;
                    iptr--;
                    continue;
                }
                if (ShuffleCnt[s[iptr] - aPos] == cnt[s[iptr] - aPos] / 2)
                {
                    result[pos] = s[iptr];
                    ReverseCnt[s[iptr] - aPos]++;
                    iptr--;
                    pos++;
                    continue;
                }

                Int32[] tempCnt = new Int32[ShuffleCnt.Length];
                Array.Copy(ShuffleCnt, tempCnt, ShuffleCnt.Length);

                Int32 NewMinPtr = iptr;
                for (int j = iptr; j >= 0; j--)
                {
                    if (ReverseCnt[s[j] - aPos] < cnt[s[j] - aPos] / 2 && s[NewMinPtr] > s[j])
                    {
                        NewMinPtr = j;
                    }
                    if (tempCnt[s[j] - aPos] == cnt[s[j] - aPos] / 2)
                    {
                        break;
                    }
                    tempCnt[s[j] - aPos]++;
                }

                for (int j = iptr; j > NewMinPtr; j--)
                {
                    ShuffleCnt[s[j] - aPos]++;
                }

                ReverseCnt[s[NewMinPtr] - aPos]++;
                result[pos] = s[NewMinPtr];
                pos++;
                iptr = NewMinPtr - 1;

            }



            return new ReverseShuffleMergeAnswer() { result = new string(result) };
        }

        


    }
}
