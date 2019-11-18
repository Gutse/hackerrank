using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class FrequencyQueriesSample: TSample
    {
        public List<List<int>> queries;

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class FrequencyQueriesAnswer: TAnswer
    {
        public List<int> answer;
        public override string ToString()
        {
            return String.Join(" ", answer);
        }
        public override Boolean Equals(Object obj)
        {
            FrequencyQueriesAnswer Answer = obj as FrequencyQueriesAnswer;
            
            if (Answer?.answer?.Count != this.answer.Count)
            {
                return false;
            }

            for (int i = 0; i < Answer.answer.Count; i++)
            {
                if (Answer.answer[i] != this.answer[i])
                {
                    return false;
                }
            }
            return true;
            
        }
    }


    class FrequencyQueries : TProblem
    {
        public override void CreateSamples(StreamReader reader)
        {
            Int64 n = Convert.ToInt32(reader.ReadLine());
            FrequencyQueriesSample sample = new FrequencyQueriesSample() { queries = new List<List<int>>() };
            while (!reader.EndOfStream)
            {
                List<int> arr = reader.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();
                sample.queries.Add(arr);
            }
            Samples.Add(sample);
        }

        public override void CreateAnswers(StreamReader reader)
        {
            FrequencyQueriesAnswer ans = new FrequencyQueriesAnswer() { answer = new List<int>() };

            while (!reader.EndOfStream)
            {
                ans.answer.Add(Convert.ToInt32(reader.ReadLine()));
            }

            Answers.Add(ans);
        }

        [SolutionMethod]
        public TAnswer BruteForce(TSample Sample)
        {
            FrequencyQueriesSample sample = Sample as FrequencyQueriesSample;
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
        public TAnswer TotalBruteForce(TSample Sample)
        {
            FrequencyQueriesSample sample = Sample as FrequencyQueriesSample;
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
