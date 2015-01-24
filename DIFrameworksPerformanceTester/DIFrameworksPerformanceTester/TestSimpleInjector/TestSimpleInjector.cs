using DIFrameworksPerformanceTester.Test;
using SimpleInjector;
using SimpleInjector.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DIFrameworksPerformanceTester.TestSimpleInjector
{
    /// <summary>
    /// This class is used for testing SimpleInjector DI framework.
	/// SimpleInjector does not support child containers directly
	/// You can enable it with some custom code, but here it is considered as not supported
    /// </summary>
	public class TestSimpleInjector : IContainerBase
	{
		static Container container;
        public TestSimpleInjector()
        {
            container = new SimpleInjector.Container();
        }
        public void ConfigureNotSingleton()
		{
			container.Register<INotSingleton, NotSingleton>();
		}

        public void ConfigureSingleton()
		{
			container.RegisterSingle<ISingleton, Singleton>();
		}

        public void ConfigureCombined()
		{
            container.Register<INotSingleton, NotSingleton>();
            container.RegisterSingle<ISingleton, Singleton>();
			container.Register<ICombined, Combined>();
		}

		// This shouuld be advanced SimpleInjector
        public void ConfigureGeneric()
		{
            container.RegisterOpenGeneric(typeof(IGeneric<>), typeof(Generic<>));
        }

        public void ConfigureCollection()
		{
			container.RegisterAll<ICollector>(
				typeof(FirstCollector),
				typeof(SecondCollector),
				typeof(ThirdCollector));
            container.Register<Collection, Collection>();
		}

        public void ConfigureDynamic()
        {
			//var assemblyClasses = Dynamic.GetClassTypes();
			//foreach (var type in assemblyClasses)
			//{
			//	//container.Register<IDynamic, type>().Named(type.FullName);
			//}
        }

		public void ConfigureConditional()
		{
			//container.Register<ConditionalConstructorFirst>();
			//container.Register<ConditionalConstructorSecond>();

			//container.RegisterWithContext<IConditional>(context => context.ImplementationType == typeof(ConditionalConstructorFirst)
			//		? container.GetInstance<ConditionalImplementationFirst>()
			//		: container.GetInstance<ConditionalImplementationSecond>()
			//		as IConditional);
		}

        public void ConfigureAll()
        {
            container.Register<INotSingleton, NotSingleton>();
            container.RegisterSingle<ISingleton, Singleton>();
            container.Register<ICombined, Combined>();
            container.RegisterOpenGeneric(typeof(IGeneric<>), typeof(Generic<>));
            container.RegisterAll<ICollector>(
                typeof(FirstCollector),
                typeof(SecondCollector),
                typeof(ThirdCollector));
            container.Register<Collection, Collection>();
        }

        public void Measure()
        {
            //var sw = new Stopwatch();

            // Not singleton class.
            //sw.Start();
            var notSingleton = container.GetInstance<INotSingleton>();
            notSingleton.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Singleton class.
            //sw.Start();
            var singleton = container.GetInstance<ISingleton>();
            singleton.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Combination singleton and not singleton class.
            //sw.Start();
            var combined = container.GetInstance<ICombined>();
            combined.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Generic class.
            //sw.Start();
            var generic = container.GetInstance<IGeneric<int>>();
            generic.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Collection class.
            //sw.Start();
            var collection = container.GetInstance<Collection>();
            collection.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

			// Conditional class.
			//sw.Start();
			//var conditional = container.GetInstance<IConditional>();
			//conditional.DoSomething();
			//sw.Stop();
			//Console.WriteLine("Elapsed={0}", sw.Elapsed);
			//sw.Reset();

            // Dynamic class.
			//sw.Start();
			//var dynamic = container.GetInstance<IDynamic>();
			//sw.Stop();
			//Console.WriteLine("Elapsed={0}", sw.Elapsed);
			//sw.Reset();
        }        

        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

		public object Resolve(Type t)
		{
			return container.GetInstance(t);
		}

		public T Resolve<T>() where T : class
		{
			return container.GetInstance<T>();
		}
	}
}
