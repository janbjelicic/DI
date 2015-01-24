using DIFrameworksPerformanceTester.Test;
using StructureMap;
using System;
using System.Diagnostics;
using System.Reflection;

namespace DIFrameworksPerformanceTester.TestStructureMap
{
    /// <summary>
    /// This class is used for testing StructureMap DI framework.
    /// </summary>
    public class TestStructureMap : IContainerBase
    {
        static Container container;
        public TestStructureMap()
        {
            container = new Container();
        }
        public void ConfigureNotSingleton()
        {
            container.Configure(x => x.For<INotSingleton>().Use<NotSingleton>());
        }

        public void ConfigureSingleton()
        {
            container.Configure(x => x.For<ISingleton>().Singleton().Use<Singleton>());
        }

        /// <summary>
        /// We have to have here configurations for INotSingleton and ISingleton
        /// because otherwise StructureMap wouldn't know how to instantiate
        /// a Combined class which in it's constructor combines INotSingleton and
        /// ISingleton.
        /// </summary>
        public void ConfigureCombined()
        {
            container.Configure(x => x.For<INotSingleton>().Use<NotSingleton>());
            container.Configure(x => x.For<ISingleton>().Singleton().Use<Singleton>());
            container.Configure(x => x.For<ICombined>().Use<Combined>());          
        }

        public void ConfigureGeneric()
        {
            container.Configure(x => x.For(typeof(IGeneric<>)).Use(typeof(Generic<>)));
        }

        /// <summary>
        /// Here we have a problem when we want to configure a collection
        /// at once we have to do the mappings one at a time.
        /// SimpleInjector is ahead here with much cleaner way.
        /// </summary>
        public void ConfigureCollection()
        {
            container.Configure(x => x.For<Collection>().Use<Collection>());
            container.Configure(x => x.For<ICollector>().Use<FirstCollector>());
            container.Configure(x => x.For<ICollector>().Use<SecondCollector>());
            container.Configure(x => x.For<ICollector>().Use<ThirdCollector>());         
        }

        public void ConfigureDynamic()
        {
			//var assemblyClasses = Dynamic.GetClassTypes();
			//foreach (var type in assemblyClasses)
			//{
			//	container.Inject(Dynamic.GetFirstInterface(), type);
			//}
        }

		public void ConfigureConditional()
		{
		}

        public void ConfigureAll()
        {
            container.Configure(x => x.For<INotSingleton>().Use<NotSingleton>());
            container.Configure(x => x.For<ISingleton>().Singleton().Use<Singleton>());
            container.Configure(x => x.For<ICombined>().Use<Combined>());
            container.Configure(x => x.For(typeof(IGeneric<>)).Use(typeof(Generic<>)));
            container.Configure(x => x.For<Collection>().Use<Collection>());
            container.Configure(x => x.For<ICollector>().Use<FirstCollector>());
            container.Configure(x => x.For<ICollector>().Use<SecondCollector>());
            container.Configure(x => x.For<ICollector>().Use<ThirdCollector>());
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

            //Singleton class.
            //sw.Start();
            var singleton = container.GetInstance<ISingleton>();
            singleton.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            //Combination singleton and not singleton class.
            //sw.Start();
            var combined = container.GetInstance<ICombined>();
            combined.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            ////Generic class.
            //sw.Start();
            var generic = container.GetInstance<IGeneric<int>>();
            generic.DoSomething();
            //sw.Stop();
            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //sw.Reset();

            //Collection class.
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
			
            //sw.Start();
			//var dynamic = container.GetInstance(type, Dynamic.GetClassNames()[0]);
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
