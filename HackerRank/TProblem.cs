using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SolutionMethodAttribute : Attribute {
    }

    public class TProblem<T, U> 
    {
        public TProblem() {
            var solMethods = this.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(SolutionMethodAttribute),true).Length > 0);
            foreach (var method in solMethods)
            {
                var s = method.CreateDelegate(typeof(Solution), this);
                Solutions.Add(s as Solution);
            }
        }

        public delegate U Solution(T sample);
        public readonly List<Solution> Solutions = new List<Solution>();

        public virtual void GenSamples() {
        }

        public virtual Boolean CheckAnswer(Int32 SampleID, U Answer)
        {
            return Answer.Equals(Answers[SampleID]);
        }


        public List<T> Samples = new List<T>();
        public List<U> Answers = new List<U>();
       
    }

}
