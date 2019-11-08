using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class ReverseShuffleMergeSample
    {
        public String s;
        public override string ToString()
        {
            return s.ToString();
        }
    }

    public class ReverseShuffleMergeAnswer
    {
        public String result;
        public override string ToString()
        {
            return result;
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


    class ReverseShuffleMerge : TProblem<ReverseShuffleMergeSample, ReverseShuffleMergeAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, ReverseShuffleMergeAnswer Answer)
        {

            return Answer?.result == Answers?[SampleID]?.result;
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
        private void LoadSamples(String opt, String opt2 = "")
        {
            void LoadFile(String InputFile, String OutputFile)
            {
                if (!System.IO.File.Exists(InputFile) || !System.IO.File.Exists(OutputFile))
                {
                    Console.WriteLine($"files {InputFile} or {OutputFile} not found");
                    return;
                }

                using (System.IO.StreamReader reader = new System.IO.StreamReader(InputFile))
                {
                    ReverseShuffleMergeSample sample = new ReverseShuffleMergeSample() { s = reader.ReadLine() };
                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    ReverseShuffleMergeAnswer ans = new ReverseShuffleMergeAnswer() { result = reader.ReadLine() };
                    Answers.Add(ans);
                }
            }



            //String SamplesDir = $"{AppDomain.CurrentDomain.BaseDirectory}\\Samples\\{this.GetType().Name}\\";
            String SamplesDir = $"{Properties.Settings.Default["SamplesBaseDir"]}\\{this.GetType().Name}\\";
            if (!System.IO.Directory.Exists(SamplesDir))
            {
                Console.WriteLine($"Directory {SamplesDir} not exists");
                return;
            }

            if (opt == "All")
            {
                foreach (var file in System.IO.Directory.GetFiles(SamplesDir, "*input*"))
                {
                    LoadFile(file, file.Replace("input", "output"));
                }
            }
            else
            {
                if (Int32.TryParse(opt, out Int32 sampleNumber))
                {
                    LoadFile($"{SamplesDir}input{opt}.txt", $"{SamplesDir}output{opt}.txt");
                }

                else
                {
                    LoadFile(opt, opt2);
                }
            }
        }

        public override void GenSamples()
        {
            LoadSamples("All");
            Samples.Add(new ReverseShuffleMergeSample() { s = "caaabaddbc" });
            Answers.Add(new ReverseShuffleMergeAnswer() { result = "bdaac" });
        }

        [SolutionMethod]
        public ReverseShuffleMergeAnswer BF(ReverseShuffleMergeSample sample)
        {
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

        //[SolutionMethod]
        public ReverseShuffleMergeAnswer stub(ReverseShuffleMergeSample sample)
        {
            String s = sample.s;
            String result = "";

            return new ReverseShuffleMergeAnswer() { result = result };
        }


    }
}
