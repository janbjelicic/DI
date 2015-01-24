using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Test
{
	public interface IConditional
	{
		int DoSomething();
	}

	public class ConditionalImplementationFirst : IConditional
	{
		int counter = 1;

		public int DoSomething()
		{
			return counter;
		}
	}

	public class ConditionalImplementationSecond : IConditional
	{
		int counter = 1;

		public int DoSomething()
		{
			return counter;
		}
	}

	public class ConditionalConstructorFirst : IConditional
	{		
		IConditional conditional;

		public ConditionalConstructorFirst(IConditional conditional)
		{
			if (conditional.GetType() != typeof(ConditionalImplementationFirst))
				throw new Exception("Wrong conditional parameter!");

			this.conditional = conditional;
		}

		public int DoSomething()
		{
			return conditional.DoSomething();
		}
	}

	public class ConditionalConstructorSecond : IConditional
	{
		IConditional conditional;

		public ConditionalConstructorSecond(IConditional conditional)
		{
			if (conditional.GetType() != typeof(ConditionalImplementationSecond))
				throw new Exception("Wrong conditional parameter!");

			this.conditional = conditional;
		}

		public int DoSomething()
		{
			return conditional.DoSomething();
		}
	}

	public class MeasureConditional
	{
		public static List<string> Measure(int numberOfTests)
		{
			var results = new List<string>();
			var containers = Frameworks.GetContainers();
			var sw = new Stopwatch();
			foreach (var container in containers)
			{
				sw.Start();
				container.ConfigureConditional();
				for (int i = 0; i < numberOfTests; i++)
				{
					//var instance = (IConditional)container.Resolve(typeof(IConditional));
					var instance = container.Resolve<IConditional>();
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
