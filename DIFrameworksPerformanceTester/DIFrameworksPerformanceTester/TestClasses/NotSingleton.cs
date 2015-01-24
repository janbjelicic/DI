using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DIFrameworksPerformanceTester.Test
{
	public interface INotSingleton
	{
		int DoSomething();
	}

	/// <summary>
    /// Class that implements an interface INotSingleton.
	/// </summary>
	public class NotSingleton : INotSingleton
	{
        int counter;
        public NotSingleton() { counter++; }

		public int DoSomething()
		{
            return counter;
		}
	}

    public class MeasureNotSingleton
    {
        public static List<string> Measure(int numberOfTests)
        {
            var results = new List<string>();
            var containers = Frameworks.GetContainers();
            var sw = new Stopwatch();
            foreach (var container in containers)
            {
                sw.Start();
                container.ConfigureNotSingleton();
                for (int i = 0; i < numberOfTests; i++)
                {
                    //var instance = (INotSingleton)container.Resolve(typeof(INotSingleton));
					var instance = container.Resolve<INotSingleton>();
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
