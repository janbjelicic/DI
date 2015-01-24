using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.HelloWorld
{
	public class NinjectHelloWorld : NinjectModule
	{
		public override void Load()
		{
			Bind<IMailSender>().To<MockMailSender>();
		}

		public static IKernel Configure()
		{
			var kernel = new StandardKernel();
			return kernel;
		}
	}

	public interface IMailSender
	{
		void Send(string toAddress, string subject);
	}

	public class MailSender : IMailSender
	{
		public void Send(string toAddress, string subject)
		{
			Console.WriteLine("Sending mail to [{0}] with subject [{1}]", toAddress, subject);
		}
	}

	public class MockMailSender : IMailSender
	{
		public void Send(string toAddress, string subject)
		{
			Console.WriteLine("Mocking mail to [{0}] with subject [{1}]", toAddress, subject);
		}
	}

	public class FormHandler
	{
		private readonly IMailSender mailSender;

		public FormHandler(IMailSender mailSender)
		{
			this.mailSender = mailSender;
		}

		public void Handle(string toAddress)
		{
			mailSender.Send(toAddress, "This is non-Ninject example");
		}
	}
}
