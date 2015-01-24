using SimpleInjector;
using System;

namespace DIFrameworksPerformanceTester.HelloWorld
{
	public class SimpleInjectorHelloWorld
	{
		public static Container Configure()
		{
			var container = new SimpleInjector.Container();
			container.Register<IOrder, PurchaseOrder>();
			return container;
		}
	}

	public interface IOrder
	{
		void Process();
	}

	public class PurchaseOrder : IOrder
	{
		public void Process()
		{
			Console.WriteLine("Purchase Order processed");
		}
	}

	public class ShoppingCart
	{
		private readonly IOrder _order;

		public ShoppingCart(IOrder order)
		{
			_order = order;
		}

		public void CheckOut()
		{
			_order.Process();
		}
	}
}

