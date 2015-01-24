using DIFrameworksPerformanceTester.Test;
using Ninject;
using Ninject.Modules;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

namespace DIFrameworksPerformanceTester.TestNinject
{
    /// <summary>
    /// This class is used for testing Ninject DI framework.
	/// Important to recognize that the transient scope is the
	/// default scope set.
    /// </summary>
    public class TestNinject : IContainerBase
	{
        static IKernel container;
        public TestNinject()
        {
            container = new StandardKernel();
        }

		public void ConfigureNotSingleton()
		{
            container.Bind<INotSingleton>().To<NotSingleton>();
        }

        public void ConfigureSingleton()
		{
            container.Bind<ISingleton>().To<Singleton>().InSingletonScope();
        }

        public void ConfigureCombined()
		{
            container.Bind<INotSingleton>().To<NotSingleton>();
            container.Bind<ISingleton>().To<Singleton>().InSingletonScope();
            container.Bind<ICombined>().To<Combined>();
		}

        public void ConfigureGeneric()
		{
			container.Bind(typeof(IGeneric<>)).To(typeof(Generic<>)).InTransientScope();
		}

		/// <summary>
		/// Here we have a problem when we want to configure a collection
		/// at once we have to do the mappings one at a time.
        /// SimpleInjector is ahead here with much cleaner way.
		/// </summary>
        public void ConfigureCollection()
		{
            container.Bind<ICollector>().To<FirstCollector>();
            container.Bind<ICollector>().To<SecondCollector>();
            container.Bind<ICollector>().To<ThirdCollector>();
            container.Bind<Collection>().To<Collection>();
		}

        public void ConfigureDynamic()
		{
			//var assemblyClasses = Dynamic.GetClassTypes();
			//foreach (var type in assemblyClasses)
			//{
			//	//container.Bind<IDynamic>().To<type>().Named(type.FullName);
			//}
		}

		public void ConfigureConditional()
		{
			container.Bind<ConditionalConstructorFirst>().To<ConditionalConstructorFirst>().InTransientScope();
			container.Bind<ConditionalConstructorSecond>().To<ConditionalConstructorSecond>().InTransientScope();
			container.Bind<IConditional>()
						.To<ConditionalImplementationFirst>()
						.WhenInjectedInto<ConditionalConstructorFirst>()
						.InTransientScope();
			container.Bind<IConditional>()
						.To<ConditionalImplementationSecond>()
						.WhenInjectedInto<ConditionalConstructorSecond>()
						.InTransientScope();
		}

        public void ConfigureAll()
        {
            container.Bind<INotSingleton>().To<NotSingleton>();
            container.Bind<ISingleton>().To<Singleton>().InSingletonScope();
            container.Bind<ICombined>().To<Combined>();
            container.Bind<Collection>().To<Collection>();
            container.Bind<ICollector>().To<FirstCollector>();
            container.Bind<ICollector>().To<SecondCollector>();
            container.Bind<ICollector>().To<ThirdCollector>();
            container.Bind(typeof(IGeneric<>)).To(typeof(Generic<>));
        }

        public void Measure()
        {
            //var sw = new Stopwatch();

            // Not singleton class.
            //sw.Start();
			var notSingleton = container.Get<INotSingleton>();
            notSingleton.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Singleton class.
            //sw.Start();
			var singleton = container.Get<ISingleton>();
            singleton.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Combined singleton and not singleton class.
            //sw.Start();
			var combined = container.Get<ICombined>();
            combined.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Generic class.
            //sw.Start();
            var generic = container.Get<IGeneric<int>>();
            generic.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            // Collection class.
            //sw.Start();
            var collection = container.Get<Collection>();
            collection.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

			// Conditional class.
			//sw.Start();
			//var conditional = container.Get<IConditional>();
			//conditional.DoSomething();
			//sw.Stop();
			//Console.WriteLine("Elapsed={0}", sw.Elapsed);
			//sw.Reset();

            // Dynamic class.
            //sw.Start();
			//var dynamic = container.Get<IDynamic>();
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
			return container.Get(t);
		}

		public T Resolve<T>() where T : class
		{
			return container.Get<T>();
		}
	}
}
