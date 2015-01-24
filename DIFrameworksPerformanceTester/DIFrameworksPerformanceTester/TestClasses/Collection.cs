using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Test
{
	public interface ICollector
	{
		int DoSomething();
	}

	public class FirstCollector : ICollector
	{
        int counter;
        public FirstCollector() { counter++; }
		public int DoSomething()
		{
            return counter;
		}
	}

	public class SecondCollector : ICollector
	{
        int counter;
        public SecondCollector() { counter++; }
        public int DoSomething()
        {
            return counter;
        }
	}

	public class ThirdCollector : ICollector
	{
        int counter;
        public ThirdCollector() { counter++; }
        public int DoSomething()
        {
            return counter;
        }
	}

	public class Collection : ICollector
	{
        int counter;
        public IEnumerable<ICollector> Collectors;
        public Collection(IEnumerable<ICollector> collectors) 
		{
			if (collectors == null)
				throw new ArgumentNullException("Collectors can't be null");
			Collectors = collectors;
            counter++;
		}
		public int DoSomething()
		{
            return counter;
		}
	}

    public class MeasureCollection
    {
        public static List<string> Measure(int numberOfTests)
        {
            var results = new List<string>();
            var containers = Frameworks.GetContainers();
            var sw = new Stopwatch();
            foreach (var container in containers)
            {
                sw.Start();
                container.ConfigureCollection();
                for (int i = 0; i < numberOfTests; i++)
                {
                    //var instance = (Collection)container.Resolve(typeof(Collection));
					var instance = container.Resolve<Collection>();
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
