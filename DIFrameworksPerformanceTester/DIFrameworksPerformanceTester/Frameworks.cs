using Ninject;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester
{
    public class Frameworks
    {
        public static List<IContainerBase> GetContainers()
        {
            var containers = new List<IContainerBase>();
            containers.Add(new TestStructureMap.TestStructureMap());
            containers.Add(new TestNinject.TestNinject());
            containers.Add(new TestSimpleInjector.TestSimpleInjector());
            return containers;
        }
    }
}
