using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Test
{
	public interface ISingleton
	{
		int DoSomething();
	}

	/// <summary>
	/// Class that implements singleton design pattern.
	/// </summary>
	public class Singleton : ISingleton
	{
        int counter;
        public Singleton() { counter++; }

		public int DoSomething()
		{
            return counter;	
		}
	}

    public class MeasureSingleton
    {
        public static List<string> Measure(int numberOfTests)
        {
            var results = new List<string>();
            var containers = Frameworks.GetContainers();
            var sw = new Stopwatch();
            foreach (var container in containers)
            {
                sw.Start();
                container.ConfigureSingleton();
                for (int i = 0; i < numberOfTests; i++)
                {
                    //var instance = (ISingleton)container.Resolve(typeof(ISingleton));
					var instance = container.Resolve<ISingleton>();
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
