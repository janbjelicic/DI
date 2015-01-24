using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using Ninject;
using DIFrameworksPerformanceTester.HelloWorld;
using DIFrameworksPerformanceTester.TestStructureMap;
using DIFrameworksPerformanceTester.Test;
using DIFrameworksPerformanceTester.TestSimpleInjector;
using DIFrameworksPerformanceTester.Log;

namespace DIFrameworksPerformanceTester
{
	/// <summary>
	/// Class that will be used for testing DI framework performances.
	/// Cases that will be tested:
	///		1. Simple not singleton class
	///		2. Simple singleton class
	///		3. Combination class with singleton and not singleton interface implementors
	///		4. Complex case where we will generate multiple class and interfaces dynamically
	///		5. Class with generic type
	///		6. Class that has a collection of objects injected that are implementing the same interface
	///		7. Maybe conditional mapppings between abstractions and concrete implementations if we would have time.
	/// </summary>
	public class TestService
	{
        public static void MeasureAll(int numberOfTests)
        {
            var results = new List<string>();
            var sw = new Stopwatch();

            // StructureMap testing.
            Console.WriteLine("StructureMap");
            sw.Start();
            for (int i = 0; i < numberOfTests; i++)
            {
                var testStructureMap = new TestStructureMap.TestStructureMap();
                testStructureMap.ConfigureAll();
                testStructureMap.Measure();
                testStructureMap.Dispose();
            }
            results.Add((new TimeSpan(sw.Elapsed.Ticks / numberOfTests)).ToString());
            Console.WriteLine("StructureMap Elapsed={0}", sw.Elapsed);
            sw.Reset();

            // Ninject testing.
            Console.WriteLine("Ninject");
            sw.Start();
            for (int i = 0; i < numberOfTests; i++)
            {
                var testNinject = new TestNinject.TestNinject();
                testNinject.ConfigureAll();
                testNinject.Measure();
                testNinject.Dispose();
            }
            results.Add((new TimeSpan(sw.Elapsed.Ticks / numberOfTests)).ToString());
            Console.WriteLine("Ninject Elapsed={0}", sw.Elapsed);
            sw.Reset();

            // SimpleInjector testing.
            Console.WriteLine("SimpleInjector");
            sw.Start();
            for (int i = 0; i < numberOfTests; i++)
            {
                var simpleInjector = new TestSimpleInjector.TestSimpleInjector();
                simpleInjector.ConfigureAll();
                simpleInjector.Measure();
                simpleInjector.Dispose();
            }
            results.Add((new TimeSpan(sw.Elapsed.Ticks / numberOfTests)).ToString());
            Console.WriteLine("SimpleInjector Elapsed={0}", sw.Elapsed);            
            sw.Reset();

            // Logging the results.
            Data.LogToAllResults(results);
        }

        public static void MeasureCategories(int numberOfTests)
        {
            var results = new List<List<string>>();
            results.Add(MeasureNotSingleton.Measure(numberOfTests));
            results.Add(MeasureSingleton.Measure(numberOfTests));
            results.Add(MeasureCombined.Measure(numberOfTests));
            results.Add(MeasureGeneric.Measure(numberOfTests));
			results.Add(MeasureCollection.Measure(numberOfTests));
			//results.Add(MeasureConditional.Measure(numberOfTests));
            results.Add(MeasureDynamic.Measure(numberOfTests));
            // Logging the results.
            Data.LogToCategoryResults(results);
        }

		public static void RunStructureMap()
		{
			var container = StructureMapHelloWorld.ConfigureDependencies();

			var appEngine = container.GetInstance<IAppEngine>();
			appEngine.Run();
		}


		public static void RunNinject()
		{
			var kernel = NinjectHelloWorld.Configure();
			var mailSender = kernel.Get<IMailSender>();

			FormHandler formHandler = new FormHandler(mailSender);
			formHandler.Handle("test@test.com");
		}

		public static void RunSimpleInjector()
		{
			var container = SimpleInjectorHelloWorld.Configure();
			var shoppingCart = container.GetInstance<ShoppingCart>();
			shoppingCart.CheckOut();
		}
	}
}
