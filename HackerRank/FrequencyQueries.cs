using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class FrequencyQueriesSample
    {
        public List<List<int>> queries;

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class FrequencyQueriesAnswer
    {
        public List<int> answer;
        public override string ToString()
        {
            return String.Join(" ", answer);
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


    class FrequencyQueries : TProblem<FrequencyQueriesSample, FrequencyQueriesAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, FrequencyQueriesAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
            //return Answer?.result == Answers?[SampleID]?.result;

            if (Answer?.answer?.Count != Answers?[SampleID]?.answer?.Count)
            {
                return false;
            }

            for (int i = 0; i < Answer.answer.Count; i++)
            {
                if (Answer.answer[i] != Answers[SampleID].answer[i])
                {
                    return false;
                }
            }

            return true;

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
                    Int64 n = Convert.ToInt32(reader.ReadLine());
                    FrequencyQueriesSample sample = new FrequencyQueriesSample() { queries = new List<List<int>>() };
                    //reader.ReadToEnd().TrimEnd().Split('\n')
                    while (!reader.EndOfStream)
                    //for (int i = 0; i < n; i++)
                    {
                        List<int> arr = reader.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();
                        sample.queries.Add(arr);
                    }
                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    FrequencyQueriesAnswer ans = new FrequencyQueriesAnswer() { answer = new List<int>() };

                    while (!reader.EndOfStream)
                    {
                        ans.answer.Add(Convert.ToInt32(reader.ReadLine()));
                    }

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
            LoadSamples("07");
            //Samples.Add(new FrequencyQueriesSample() {queries = new List<List<int>>() });
            //Answers.Add(new FrequencyQueriesAnswer() {answer = new List<int>()});
        }

        [SolutionMethod]
        public FrequencyQueriesAnswer BruteForce(FrequencyQueriesSample sample)
        {
            List<List<int>> queries = sample.queries;

            List<Int32> result = new List<int>();
            Dictionary<Int32, Int32> data = new Dictionary<int, int>();
            Dictionary<Int32, Int32> fq = new Dictionary<int, int>();


            for (int i = 0; i < queries.Count; i++)
            {
                Int32 OpValue = queries[i][1];

                if (queries[i][0] == 1)
                {
                    if (data.ContainsKey(OpValue))
                    {
                        data[OpValue]++;
                    }
                    else
                    {
                        data.Add(OpValue, 1);
                    }

                    if (fq.ContainsKey(data[OpValue]))
                    {
                        fq[data[OpValue]]++;
                    }
                    else
                    {
                        fq.Add(data[OpValue], 1);
                    }

                    if (data[OpValue]-1 > 0)
                    {
                        fq[data[OpValue] - 1]--;
                    }


                }

                if (queries[i][0] == 2)
                {

                    if (data.ContainsKey(OpValue) && data[OpValue] > 0)
                    {
                        fq[data[OpValue]]--;
                        data[OpValue]--;
                        if (data[OpValue] > 0)
                        {
                            fq[data[OpValue]]++;
                        }
                    }
                }

                Int32[] a  =new Int32[3];
                
            
                if (queries[i][0] == 3)
                {

                    if (fq.ContainsKey(OpValue) && fq[OpValue] > 0)
                    {
                        result.Add(1);
                    }
                    else
                    {
                        result.Add(0);
                    }
                }
            }

            return new FrequencyQueriesAnswer() { answer = result };
        }

        [SolutionMethod]
        public FrequencyQueriesAnswer TotalBruteForce(FrequencyQueriesSample sample)
        {
            List<List<int>> queries = sample.queries;

            List<Int32> result = new List<int>();
            Dictionary<Int32, Int32> data = new Dictionary<int, int>();



            for (int i = 0; i < queries.Count; i++)
            {
                if (queries[i][0] == 1)
                {
                    Int32 AddedValue = queries[i][1];
                    if (data.ContainsKey(AddedValue))
                    {
                        data[AddedValue]++;
                    }
                    else
                    {
                        data.Add(AddedValue, 1);
                    }
                }

                if (queries[i][0] == 2)
                {
                    Int32 DeletedValue = queries[i][1];
                    if (data.ContainsKey(DeletedValue))
                    {
                        if (data[queries[i][1]] > 0)
                        {
                            data[queries[i][1]]--;
                        }
                    }
                }


                if (queries[i][0] == 3)
                {
                    if (data.ContainsValue(queries[i][1]))
                    {
                        result.Add(1);
                    }
                    else
                    {
                        result.Add(0);
                    }

                }
            }

            return new FrequencyQueriesAnswer() { answer = result };
        }


    }
}
