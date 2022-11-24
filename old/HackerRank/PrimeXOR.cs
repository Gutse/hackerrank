using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{
    public class PrimeXORSample : TSample
    {
        public Int32[] a;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class PrimeXORAnswer : TAnswer
    {
        public Int32 result;
        public override string ToString()
        {
            return base.ToString();
        }

        public override Boolean Equals(Object obj)
        {
            return (obj as PrimeXORAnswer)?.result == this.result;
        }
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    class PrimeXOR : TProblem
    {

        public override void AddManualSamples()
        {
            Samples.Add(new PrimeXORSample() { a = new Int32[] { 3511, 3671, 4153 } });
            Answers.Add(new PrimeXORAnswer() { result = 4 });

            Samples.Add(new PrimeXORSample() { a = new Int32[] { 3511, 3511, 3511, 3511, 3511 } });
            Answers.Add(new PrimeXORAnswer() { result = 3 });


            Samples.Add(new PrimeXORSample() { a = new Int32[] { 3511, 3511, 3511, 3511 } });
            Answers.Add(new PrimeXORAnswer() { result = 2 });

            Samples.Add(new PrimeXORSample() { a = new Int32[] { 3,3,3,3,5,5,5,5 } });
            Answers.Add(new PrimeXORAnswer() { result = 12 });

            Samples.Add(new PrimeXORSample() { a = new Int32[] { 3517, 4186, 4156, 3822, 4102, 3696, 3528, 3800, 3803, 3931, 3850, 3734, 3788, 4069, 3873, 4082, 4146, 4004, 3862, 3544, 3634, 3948, 3747, 4204, 3635, 4142, 3713, 3988, 3733, 3625, 4194, 3742, 3741, 3713, 3812, 3757, 4170, 4192, 4006, 4037, 3754, 3633, 4160, 4146, 3824, 3509, 3884, 3545, 3956, 4134, 3943, 3598, 4031, 4170, 4020, 3697, 3918, 3521, 4015, 3607, 3652, 3987, 3947, 3601, 3987, 3518, 4031, 3919, 4134, 3991, 3602, 4076, 3543, 3987, 3824, 3564, 4124, 3669, 3706, 3854, 3736, 4027, 4064, 4169, 3605, 3568, 3999, 3670, 3892, 4085, 4110, 4125, 3708, 4054, 4076, 3910, 3755 } });
            Answers.Add(new PrimeXORAnswer() { result = 536823984 });

        }

        public static List<Int32> GenPrimeNumbers(Int32 n)
        {
            List<Int32> result = new List<int>();

            Dictionary<Int32, Int32> sqrts = new Dictionary<int, int>();

            Int32 c = 2;
            Int32 MaxSQR = (Int32)Math.Floor(Math.Sqrt(n)) + 1;
            while (c < MaxSQR)
            {
                sqrts.Add(c * c, c);
                c++;
            }
            result.Add(2);
            result.Add(3);
            for (int i = 4; i < n; i++)
            {
                Char last = i.ToString().Last();
                if (last == '0' || last == '5' || last == '2' || last == '6' || last == '8' || last == '4')
                {
                    continue;
                }

                Boolean isPrime = true;
                Int32 MaxDivider = MaxSQR;
                for (int j = i; j <= MaxSQR; j++)
                {
                    if (sqrts.ContainsKey(j))
                    {
                        MaxDivider = sqrts[j];
                        break;
                    }

                }

                foreach (var prime in result.Where(x => x <= MaxDivider))
                {
                    if (i % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    result.Add(i);
                }

            }
            return result;
        }


        public static Int32 Solution1(Int32[] sample)
        {
            Int32[] a = sample;

            var primes = GetPrimes(8191);
            Int64 count = 0;
            Dictionary<Int32, Int32> F = new Dictionary<int, int>();

            foreach (var item in a)
            {
                if (F.ContainsKey(item))
                {
                    F[item]++;
                }
                else
                {
                    F.Add(item, 1);
                }
            }

            a = a.Distinct().ToArray();

            Int32[,] M = new Int32[a.Length + 1, a.Length + 1];

            for (int i = 0; i < a.Length; i++)
            {
                M[i, i] = 0;
                M[i + 1, 0] = a[i];
                M[0, i + 1] = a[i];

                if (primes.IndexOf(a[i]) != -1)
                {
                    count++;
                }


            }

            for (int i = a.Length - 1; i > 0; i--)
            {
                for (int j = i+1; j <= a.Length; j++)
                {
                    M[i, j] = M[i, 0] ^ M[0, j];
                    if (primes.IndexOf(M[i, j]) != -1)
                    {
                        count++;
                    }
                }

                for (int k = i+1; k < a.Length; k++)
                {
                    for (int j = k+1; j <= a.Length; j++)
                    {
                        Int32 X = M[k, j] ^ M[i, 0];
                        if (primes.IndexOf(X) != -1)
                        {
                            count++;
                        }
                    }
                }

            }

            foreach (var number in F.Keys)
            {
                count *=  (Int32) Math.Ceiling((double) (F[number] ) / 2);
            }

            return (Int32)(count % (10 ^ 9 + 7));
        }


        public static List<Int32> GetPrimes(Int32 Max)
        {
            List<Int32> result = new List<int>();
            Boolean[] filtered = new Boolean[Max + 1];

            for (int i = 0; i <= Max; i++)
            {
                filtered[i] = true;
            }

            filtered[0] = false;
            filtered[1] = false;
            for (int i = 2; i * i <= Max; i++)
            {
                if (filtered[i])
                    for (int j = i * i; j <= Max; j += i)
                        filtered[j] = false;
            }
            for (int i = 0; i <= Max; i++)
            {
                if (filtered[i])
                {
                    result.Add(i);
                }
            }
            return result;
        }




        [SolutionMethod]
        public TAnswer Solution2(TSample Sample)
        {
            
            Int32[] a = (Sample as PrimeXORSample).a;
            Int32 modulus = 1000000007;


            
            Int32 m = (1 << (int)(Math.Log(a.Max(), 2) + 1)) - 1;

            var primes = GetPrimes(m);
            Int64 count = 0;


            Int32[] counts = new Int32[a.Max()+1];
            foreach (var val in a)
            {
                counts[val]++;
            }


            a = a.Distinct().ToArray();
            

            Int32 n = a.Length;



            Int64[,] dp = new Int64[n+1, m+1];

            dp[0, 0] = 1;


            for (Int32 i = 1;i<= n; i++) {
                for (int j = 0; j <= m; j++)
                {
                        dp[i, j] =
                            (((dp[i - 1, j] * (1 + counts[a[i - 1]] / 2)))
                            +
                            ((dp[i - 1, j ^ a[i - 1]] * ((counts[a[i - 1]] + 1) / 2)))) % modulus
                            ;
                }
            }


            for (int i = 0; i < primes.Count; i++)
            {
                checked
                {
                    count = (count + (dp[n, primes[i]] )) % modulus;
                }
            }

            return new PrimeXORAnswer() { result = (Int32) count};

        }


    }
}
