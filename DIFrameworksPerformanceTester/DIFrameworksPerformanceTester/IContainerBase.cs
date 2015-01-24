using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester
{
    public interface IContainerBase : IDisposable
    {
        void ConfigureNotSingleton();
        void ConfigureSingleton();
        void ConfigureCombined();
        void ConfigureGeneric();
        void ConfigureCollection();
        void ConfigureDynamic();
		void ConfigureConditional();
        void ConfigureAll();
        void Measure();
        object Resolve(Type t);
		T Resolve<T>() where T : class;
    }
}
