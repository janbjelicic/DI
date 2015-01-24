using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Test
{
	public interface IGeneric<T>
	{
		int DoSomething();
	}

	public class Generic<T> : IGeneric<T>
	{
        public T Variable;
        int counter;

		public int DoSomething()
		{
            return counter++;
		}
	}

    public class MeasureGeneric
    {
        public static List<string> Measure(int numberOfTests)
        {
            var results = new List<string>();
            var containers = Frameworks.GetContainers();
            var sw = new Stopwatch();
            foreach (var container in containers)
            {
                sw.Start();
                container.ConfigureGeneric();
                for (int i = 0; i < numberOfTests; i++)
                {
                    //var instance = (IGeneric<int>)container.Resolve(typeof(IGeneric<int>));
					var instance = container.Resolve<IGeneric<int>>();
                    instance.DoSomething();
                }
                results.Add((new TimeSpan(sw.Elapsed.Ticks / numberOfTests)).ToString());
                sw.Reset();
            }
            containers.ForEach(x => x.Dispose());
            return results;
        }
    }
}
