using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;


namespace HackerRank
{

    public class FraudulentActivityNotificationsSample
    {
        public Int32[] ex;
        public Int32 d;
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class FraudulentActivityNotificationsAnswer
    {
        public Int32 result;
        public override string ToString()
        {
            return result.ToString();
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


    class FraudulentActivityNotifications : TProblem<FraudulentActivityNotificationsSample, FraudulentActivityNotificationsAnswer>
    {
        private Random rnd = new Random();

        public override bool CheckAnswer(int SampleID, FraudulentActivityNotificationsAnswer Answer)
        {
            //return base.CheckAnswer(SampleID, Answer);
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
                    string[] nm = reader.ReadLine().Split(' ');
                    Int32 n = Convert.ToInt32(nm[0]);
                    Int32 r = Convert.ToInt32(nm[1]);

                    //List<long> arr = reader.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt64(arrTemp)).ToList();


                    FraudulentActivityNotificationsSample sample = new FraudulentActivityNotificationsSample() { };
                    sample.d = r;
                    sample.ex = Array.ConvertAll(reader.ReadLine().Split(' '), sTemp => Convert.ToInt32(sTemp));

                    Samples.Add(sample);
                }


                using (System.IO.StreamReader reader = new System.IO.StreamReader(OutputFile))
                {
                    FraudulentActivityNotificationsAnswer ans = new FraudulentActivityNotificationsAnswer() { };
                    Int32.TryParse(reader.ReadLine(), out ans.result);
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
            Samples.Add(new FraudulentActivityNotificationsSample() { d = 5, ex = new Int32[] { 6, 4, 5, 3, 7, 3, 9, 10, 11 } });
            Answers.Add(new FraudulentActivityNotificationsAnswer() { result = 2 });
            LoadSamples("All");
        }

        //[SolutionMethod]
        public FraudulentActivityNotificationsAnswer BruteForce(FraudulentActivityNotificationsSample sample)
        {
            Int32 d = sample.d;
            int[] ex = sample.ex;

            Int32[] LastD = new Int32[d];
            Array.Copy(ex, LastD, d);
            Array.Sort(LastD);
            Int32 med;
            Boolean odd = d % 2 == 0;

            if (odd)
            {
                med = (LastD[d / 2] + LastD[d / 2 - 1]) /2;
            }
            else
            {
                med = LastD [d/ 2];
            }

            Int32 r = 0;
            for (int i = d; i < ex.Length; i++)
            {
                if (ex[i] >= 2 * med)
                {
                    r++;
                }
                
                Array.Copy(ex, i - d + 1, LastD, 0, d);
                Array.Sort(LastD);
                if (odd)
                {
                    med = (LastD[d / 2] + LastD[d / 2 - 1]) / 2;
                }
                else
                {
                    med = LastD[d / 2 ];
                }


            }

            return new FraudulentActivityNotificationsAnswer() { result = r };
        }

        //[SolutionMethod]
        public FraudulentActivityNotificationsAnswer Indexes(FraudulentActivityNotificationsSample sample)
        {
            Int32 d = sample.d;
            int[] ex = sample.ex;

            Int32[] LastD = new Int32[d];
            Array.Copy(ex, 0, LastD, 0, d);
            //Array.Sort(LastD);
            Int32[] idx = new Int32[ex.Length];
            for (int i = 0; i < d; i++)
            {
                idx[i] = i;
            }
            Boolean sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < d - 1; i++)
                {
                    if (LastD[i] > LastD[i + 1])
                    {
                        //swap
                        Int32 temp = LastD[i];
                        LastD[i] = LastD[i + 1];
                        LastD[i + 1] = temp;
                        temp = idx[i];
                        idx[i] = idx[i + 1];
                        idx[i + 1] = temp;
                        sorted = false;
                    }
                }
            }

            List<Int32> LD = LastD.ToList();
            List<Int32> LI = idx.ToList();


            Int32 med = LD[d / 2];
            Int32 r = 0;
            for (int i = d; i < ex.Length; i++)
            {
                if (ex[i] >= 2 * med)
                {
                    r++;
                }

                Int32 Removed = LI[i - d];


                LD.RemoveAt(Removed);


                Int32 next = ex[i];

                Int32 InsertTo = LD.Count / 2;
                Int32 way = 1;

                if (LD[InsertTo] > next)
                {
                    way = -1;
                }

                Boolean done = LD[InsertTo] >= next && LD[InsertTo - 1] <= next;

                while (!done)
                {

                    InsertTo += way;
                    if (InsertTo == 0 || InsertTo == d - 1)
                    {
                        done = true;
                    }
                    else
                    {
                        done = LD[InsertTo] >= next && LD[InsertTo - 1] <= next;
                    }
                }

                LD.Insert(InsertTo, next);
                LI[i] = InsertTo;



                med = LD[d / 2];

            }

            return new FraudulentActivityNotificationsAnswer() { result = r };
        }


        private static void QSI(Int32[] array, Int32 start, Int32 end, Int32[] indexes)
        {

            if (start >= end)
            {
                return;
            }
            Int32 temp;

            int marker = start;//divides left and right subarrays
            for (int i = start; i < end; i++)
            {
                if (array[i] < array[end]) //array[end] is pivot
                {
                    temp = array[marker]; // swap
                    array[marker] = array[i];
                    array[i] = temp;

                    temp = indexes[i];
                    indexes[i] = indexes[marker];
                    indexes[marker] = temp;
                    marker++;
                }
            }
            //put pivot(array[end]) between left and right subarrays
            temp = array[marker];
            array[marker] = array[end];
            array[end] = temp;


            QSI(array, start, marker - 1, indexes);
            QSI(array, marker + 1, end, indexes);

        }


        void swap(Int32[] arr, Int32 index1, Int32 index2, Int32[] idx, Int32[] pos)
        {
            Int32 temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;

            Int32 pos1 = pos[index1];
            Int32 pos2 = pos[index2];

            temp = idx[pos1];
            idx[pos1] = idx[pos2];
            idx[pos2] = temp;

            temp = pos[index1];
            pos[index1] = pos[index2];
            pos[index2] = temp;

        }

        void QS(Int32[] arr, Int32 left, Int32 right, Int32[] idx, Int32[] pos)
        {

            do
            {
                int index1 = left;
                int index2 = right;
                int index3 = index1 + (index2 - index1 >> 1);
                
                
                if (arr[index1] > arr[index3])
                {

                    swap(arr, index1, index3, idx, pos);
                }
                if (arr[index1] > arr[index2])
                {
                    swap(arr, index1, index2, idx, pos);
                }
                if (arr[index3] > arr[index2])
                {
                    swap(arr, index2, index3, idx, pos);
                }
                int p = arr[index3];
                do
                {
                    while (arr[index1] < p)
                        ++index1;
                    while (arr[index2] > p)
                        --index2;
                    if (index1 <= index2)
                    {
                        if (index1 < index2)
                        {
                            swap(arr, index1, index2, idx, pos);
                        }
                        ++index1;
                        --index2;
                    }
                    else
                        break;
                }
                while (index1 <= index2);

                if (index2 - left <= right - index1)
                {
                    if (left < index2)
                        QS(arr, left, index2, idx, pos);
                    left = index1;
                }
                else
                {
                    if (index1 < right)
                        QS(arr, index1, right, idx, pos);
                    right = index2;
                }
            }
            while (left < right);
        }
       //[SolutionMethod]
        public FraudulentActivityNotificationsAnswer Simple(FraudulentActivityNotificationsSample sample)
        {
            Int32 d = sample.d;
            int[] ex = sample.ex;




            Int32[] idx = new Int32[ex.Length];
            Int32[] pos = new Int32[d];
            for (int i = 0; i < ex.Length; i++)
            {
                idx[i] = i;
            }

            Int32[] LD = new int[d];
            Array.Copy(ex, LD, d);
            for (int i = 0; i < d; i++)
            {
                pos[i] = idx[i];
            }

            QS(LD, 0, d - 1, idx, pos);
            //QSI(ex, 0, d-1, idx);



            Int32 med;
            Boolean odd = d % 2 == 0;

            if (odd)
            {
                med = (LD[d / 2] + LD[d / 2 - 1]) /2;
            }
            else
            {
                med = LD[d / 2];
            }
            //Int32 med = LD[d / 2];
            Int32 r = 0;

            Stopwatch sw = new Stopwatch();
            for (int i = d; i < ex.Length; i++)
            {
                if (ex[i] >= 2 * med)
                {
                    r++;
                }

                Int32 InsertTo = idx[i - d];

                LD[InsertTo] = ex[i];

                idx[i - d] = -1;

                idx[i] = InsertTo;
                pos[InsertTo] = i;

                Boolean done = false;
                Int32 way = 1;


                while (!done)
                {
                    if (InsertTo == 0)
                    {
                        done = LD[0] <= LD[1];
                        way = 1;
                    }
                    else
                    {
                        if (InsertTo == d - 1)
                        {
                            done = LD[d - 1] >= LD[d - 2];
                            way = -1;
                        }
                        else
                        {
                            way = 1;
                            done = LD[InsertTo] >= LD[InsertTo - 1] && LD[InsertTo] <= LD[InsertTo + 1];
                            if (LD[InsertTo] < LD[InsertTo - 1])
                            {
                                way = -1;
                            }
                        }
                    }

                    //swap
                    if (!done)
                    {

                        //Int32 swi = idx[idx[i] + way];
                        Int32 swi = pos[idx[i] + way];
                        Int32 temp = LD[InsertTo + way];
                        LD[InsertTo + way] = LD[InsertTo];
                        LD[InsertTo] = temp;


                        temp = pos[InsertTo];
                        pos[InsertTo] = pos[InsertTo + way];
                        pos[InsertTo + way] = temp;




                        idx[i] = idx[i] + way;

                        idx[swi] = idx[i] - way;



                        //InsertTo = pos[idx[InsertTo] + way];
                        InsertTo = idx[i];
                    }

                }
                if (odd)
                {
                    med = (LD[d / 2] + LD[d / 2 - 1]) / 2;
                }
                else
                {
                    med = LD[d / 2];
                }


            }

            return new FraudulentActivityNotificationsAnswer() { result = r };
        }


       //[SolutionMethod]
        public FraudulentActivityNotificationsAnswer BS(FraudulentActivityNotificationsSample sample)
        {
            Int32 d = sample.d;
            int[] ex = sample.ex;

            Int32[] arr = new int[d];
            Int32[] idx = new Int32[d];
            Int32[] pos = new Int32[d];
            Queue<MyList> q = new Queue<MyList>();
            MyList[] ml = new MyList[d];
            for (int i = 0; i < d; i++)
            {
                idx[i] = i;
                pos[i] = i;
                ml[i] = new MyList();
                ml[i].data = ex[i];
                q.Enqueue(ml[i]);
                ml[i].index = i;
            }


            Array.Copy(ex, arr, d);

            QS(arr, 0, d - 1, idx, pos);



            MyList root = ml[pos[0]];
            MyList med = null;

            root.next = ml[pos[1]];
            MyList curr = root.next;



            for (int i = 1; i < d -1; i++)
            {
                curr.prev = ml[pos[i - 1]];
                curr.next = ml[pos[i+1]];
                curr = curr.next;
            }

            med = ml[pos[d/2]];
            ml[pos[d - 1]].prev = ml[pos[d - 2]];

            Int32 r = 0;

            for (int i = d; i < ex.Length; i++)
            {
                if (ex[i] >= 2 * med.data)
                {
                    r++;
                }

                MyList throwed = q.Dequeue();
                MyList s = null;
                if (throwed.prev != null)
                {
                    s = throwed.prev;
                    throwed.prev.next = throwed.next;
                }

                if (throwed.next != null)
                {
                    s = throwed.next;
                    throwed.next.prev = throwed.prev;
                }

                if (throwed == root)
                {
                    root = throwed.next;
                }



                MyList n = new MyList();
                n.data = ex[i];
                q.Enqueue(n);

                if (s.data < n.data)
                {
                    while (s.next != null && s.data < ex[i]) { s = s.next; };

                    if (s.next == null)
                    {
                        s.next = n;
                        n.prev = s;
                    }
                    else
                    {
                        if (s.prev != null)
                        {
                            s.prev.next = n;
                            n.next = s;
                            s.prev = n;
                        }
                        else
                        {
                            s.prev = n;
                            n.next = s;
                        }
                    }

                }
                else {
                    while (s.prev != null && s.data > ex[i]) { s = s.prev; };
                    if (s.next == null)
                    {
                        s.next = n;
                        n.prev = s;
                    }
                    else
                    {
                        s.next.prev = n;
                        n.next = s.next;
                        s.next = n;
                    }
                }

                /*
                if (throwed == med)
                {
                    if (med.prev.data <= ex[i] && med.next.data >= ex[i])
                    {
                        med = n;
                    }
                    else
                    {
                        if (med.data > ex[i])
                        {
                            med = med.prev;
                        }
                        else
                        {
                            med = med.next;
                        }
                    }
                }
                else
                {
                    if (throwed.data < med.data)
                    {
                        if (n.data > med.data)
                        {
                            med = med.next;
                        }
                    }
                    else
                    {
                        if (n.data < med.data)
                        {
                            med = med.prev;
                        }
                    }
                }
                */
                med = root;
                for (int j = 0; j < d/2; j++)
                {
                    med = med.next;
                }




            }

            return new FraudulentActivityNotificationsAnswer() { result = r };

        }


       // [SolutionMethod]
        public FraudulentActivityNotificationsAnswer BS2(FraudulentActivityNotificationsSample sample)
        {
            Int32 d = sample.d;
            int[] ex = sample.ex;

            Int32[] arr = new int[d];
            Int32[] idx = Enumerable.Range(0, ex.Length).ToArray();
            Int32[] pos = Enumerable.Range(0, d).ToArray();

            Array.Copy(ex, arr, d);
            QS(arr, 0, d - 1, idx, pos);
            
            Int32 r = 0;

            List<Int32> LD = arr.ToList();
            Double med;
            Boolean odd = d % 2 == 0;

            if (odd)
            {
                med = (LD[d / 2] + LD[d  / 2 + 1]) / 2;
            }
            else {
                med = LD[d / 2];
            }

            for (int i = d; i < ex.Length; i++)
            {
                if (ex[i] >= 2 * med)
                {
                    r++;
                }

                Int32 index = LD.BinarySearch(ex[i]);
                if (index < 0)
                {
                    index = ~index;
                }

                Int32 PosOfItemToDelete = idx[i - d];

                if (PosOfItemToDelete == index)
                {
                    LD[index] = ex[i];
                    idx[i] = index;
                    pos[index] = i;
                }
                else {
                    if (PosOfItemToDelete > index)
                    {
                        LD.RemoveAt(PosOfItemToDelete);
                        LD.Insert(index, ex[i]);
                        for (int j = PosOfItemToDelete; j >index ; j--)
                        {
                            pos[j] = pos[j-1];
                            idx[pos[j]]++;
                        }
                        idx[i] = index;
                        pos[index] = i;
                    }
                    else {
                        LD.Insert(index, ex[i]);
                        LD.RemoveAt(PosOfItemToDelete);
                        if (index == d)
                        {
                            for (int j = PosOfItemToDelete; j < d-1; j++)
                            {
                                pos[j] = pos[j + 1];
                                idx[pos[j]]--;
                                
                            }
                            pos[d - 1] = i;
                            idx[i] = d - 1;
                        }
                        else {
                            for (int j = PosOfItemToDelete; j < index-1; j++)
                            {
                                
                                pos[j] = pos[j + 1];
                                idx[pos[j]]--;
                            }
                            idx[i] = index-1;
                            pos[index - 1] = i;
                        }
                    }
                }
                idx[i - d] = -1;
                if (odd)
                {
                    med = (LD[d / 2] + LD[d  / 2 + 1]) / 2;
                }
                else
                {
                    med = LD[d / 2];
                }
            }

            return new FraudulentActivityNotificationsAnswer() { result = r };

        }

       
       [SolutionMethod]
        public FraudulentActivityNotificationsAnswer csort(FraudulentActivityNotificationsSample sample)
        {
            Int32 d = sample.d;
            int[] ex = sample.ex;

            

            Int32 mi1 = d / 2;
            Int32 mi2 = mi1;

            if (d % 2 == 0)
            {
                mi2--;
            }


            Int32 r = 0;

            Int32[] cs = new Int32[ex.Max()+1];
            
            for (int i = 0; i < d; i++)
            {
                cs[ex[i]]++;
            
            }

           

            

            for (int i = d; i < ex.Length; i++)
            {
                

                Int32 med1 = -1;
                Int32 med2 = -1;

                Int32 cnt = 0;

                for (int j = 0; j < cs.Length; j++)
                {
                    cnt += cs[j];

                    if (med1 == -1 && cnt >= mi1 + 1)
                    {
                        med1 = j;
                    }

                    if (med2 == -1 && cnt >= mi2 + 1)
                    {
                        med2 = j;
                    }

                    if (med1!= -1 && med2!= -1)
                    {
                        break;
                    }
                }
                double med = (med1 + med2 ) / 2.0;


                if (ex[i] >= 2 * med)
                {
                    r++;
                }

                cs[ex[i - d]]--;
                cs[ex[i]]++;
            }

            return new FraudulentActivityNotificationsAnswer() { result = r };

        }

    }
    public class MyList
    {
        public MyList prev = null;
        public MyList next = null;
        public Int32 data;
        public Int32 index;

    }
}
