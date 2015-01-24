using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Test
{
	public interface ICombined
	{
		int DoSomething();
	}

	/// <summary>
	/// Class that accepts singleton and not singleton object through constructor.
	/// </summary>
	public class Combined : ICombined
	{
		ISingleton Singleton;
		INotSingleton NotSingleton;
        int counter;

		public Combined(ISingleton singleton, INotSingleton notSingleton)
		{
			if (singleton == null || notSingleton == null)
			{
				throw new ArgumentNullException("Parameters are null, this is not allowed.");
			}
			Singleton = singleton;
			NotSingleton = notSingleton;
            counter++;
		}

		public int DoSomething()
		{
            return counter;
		}
	}

    public class MeasureCombined
    {
        public static List<string> Measure(int numberOfTests)
        {
            var results = new List<string>();
            var containers = Frameworks.GetContainers();
            var sw = new Stopwatch();
            foreach (var container in containers)
            {
                sw.Start();
                container.ConfigureCombined();
                for (int i = 0; i < numberOfTests; i++)
                {
                    //var instance = (ICombined)container.Resolve(typeof(ICombined));
					var instance = container.Resolve<ICombined>();
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
