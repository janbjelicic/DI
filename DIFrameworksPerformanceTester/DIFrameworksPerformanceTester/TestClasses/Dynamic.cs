using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Test
{
	public class Dynamic
	{
		public static Type[] GetClassTypes()
		{
			var mscorlib = typeof(string).Assembly;
			return mscorlib.GetTypes().Where(x => x.IsAssignableFrom(GetFirstInterface())).ToArray();
		}

		public static Type GetFirstInterface()
		{
			return typeof(string).GetInterfaces()[0];
		}

		public static List<string> GetClassNames()
		{
			var mscorlib = typeof(string).Assembly;
			var i = GetFirstInterface();
			var listClassNames = new List<string>();
			foreach (var type in GetClassTypes())
			{
				listClassNames.Add(type.FullName);
			}
			return listClassNames;
		}
	}	

    public class MeasureDynamic
    {
        public static List<string> Measure(int numberOfTests)
        {
			var results = new List<string>();
			var containers = Frameworks.GetContainers();
			var sw = new Stopwatch();
			foreach (var container in containers)
			{
				sw.Start();
				container.ConfigureDynamic();
				for (int i = 0; i < numberOfTests; i++)
				{
					//var instance = (IDynamic)container.Resolve(typeof(IDynamic));
					//var type = Dynamic.GetFirstInterface();
					//container.Resolve<>(type);
				}
				results.Add((new TimeSpan(sw.Elapsed.Ticks / numberOfTests)).ToString());
				sw.Reset();
			}
			containers.ForEach(x => x.Dispose());
			return results;
        }
    }
}
