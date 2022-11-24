using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;


namespace HackerRank
{
    /*
6
a b c aa d b
1 2 3 4 5 6
3
1 5 caaab
0 4 xyz
2 4 bcdybc     
     */
    public class Strand
    {
        public long Start { get; set; }
        public long End { get; set; }
        public string StrandString { get; set; }
    }

    public class DnaHealthSample : TSample
    {
        public List<string> Genes { get; set; }
        public List<int> Health { get; set; }
        public List<Strand> Strands { get; set; }

    }

    public class DnaHealthAnswer : TAnswer
    {
        public long min;
        public long max;

        public override string ToString()
        {
            return base.ToString();
        }

        public override Boolean Equals(Object obj)
        {
            if (obj is DnaHealthAnswer a)
            {
                return a.min == min && a.max == max;
            }

            return false;
        }
    }

    class DnaHealth : TProblem
    {
        public override void AddManualSamples()
        {
            Samples.Add(new DnaHealthSample
            {
                Genes = new List<string>
                {
                    "a", "b", "c", "aa", "d", "b"
                },
                Health = new List<int> { 1, 2, 3, 4, 5, 6 },
                Strands = new List<Strand>
                {
                    new Strand
                    {
                        Start = 1, End = 5, StrandString = "caaab"
                    },
                    new Strand
                    {
                        Start = 0, End = 4, StrandString = "xyz"
                    },
                    new Strand
                    {
                        Start = 2, End = 4, StrandString = "bcdybc"
                    },
                }
            });
            Answers.Add(new DnaHealthAnswer()
            {
                min = 0,
                max = 19
            });
        }

        public override void CreateSamples(StreamReader reader)
        {
            int n = Convert.ToInt32(reader.ReadLine());
            var genes = reader.ReadLine().Split(' ');
            int[] arr = Array.ConvertAll(reader.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));

            var strands = new List<Strand>();
            var strandsCount = Convert.ToInt32(reader.ReadLine());
            for (int i = 0; i < strandsCount; i++)
            {
                var line = reader.ReadLine().Split(' ');
                strands.Add(new Strand()
                {
                    Start = Convert.ToInt32(line[0]),
                    End = Convert.ToInt32(line[1]),
                    StrandString = line[2]
                });
            }

            Samples.Add(new DnaHealthSample()
            {
                Genes = genes.ToList(),
                Health = arr.ToList(),
                Strands = strands
            });
        }

        public override void CreateAnswers(StreamReader reader)
        {
            var vals = reader.ReadLine().Split(' ');

            Answers.Add(new DnaHealthAnswer() { min = Convert.ToInt64(vals[0]), max = Convert.ToInt64(vals[1]) });
        }

        [SolutionMethod]
        public TAnswer BF(TSample Sample)
        {
            var sample = Sample as DnaHealthSample;
            var min = long.MaxValue;
            var max = long.MinValue;

            var genes = new Dictionary<string, List<(long index, long value)>>();
            var minGeneLength = int.MaxValue;
            var maxGeneLength = int.MinValue;

            for (var i = 0; i < sample.Genes.Count; i++)
            {
                var gene = sample.Genes[i];
                if (genes.ContainsKey(gene))
                {
                    genes[gene].Add((i, sample.Health[i]));
                }
                else
                {
                    genes.Add(gene, new List<(long index, long value)> { (i, sample.Health[i]) });
                }

                if (gene.Length > maxGeneLength)
                {
                    maxGeneLength = gene.Length;
                }

                if (gene.Length < minGeneLength)
                {
                    minGeneLength = gene.Length;
                }
            }

            for (int i = 0; i < sample.Strands.Count; i++)
            {
                var health = GetStrandHealth(genes, sample.Strands[i].Start, sample.Strands[i].End, sample.Strands[i].StrandString, minGeneLength, maxGeneLength);
                if (health >= max)
                {
                    max = health;
                }

                if (health <= min)
                {
                    min = health;
                }
            }
            return new DnaHealthAnswer() { max = max, min = min };
        }

        public long GetStrandHealth(Dictionary<string, List<(long index, long value)>> genes, long start, long end, string strand, int minGeneLength, int maxGeneLength)
        {
            long result = 0;
            for (var i = minGeneLength; i <= maxGeneLength; i++)
            {
                for (int j = 0; j < strand.Length - i + 1; j++)
                {
                    var ss = strand.Substring(j, i);
                    if (genes.ContainsKey(ss))
                    {
                        foreach (var (index, value) in genes[ss])
                        {
                            if (index >= start && index <= end)
                            {
                                result += value;
                            }
                        }
                    }
                }
            }

            return result;
        }

        [SolutionMethod]
        public TAnswer DP(TSample Sample)
        {
            var sample = Sample as DnaHealthSample;
            
        }
    }
}
