using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Test
{
	public interface ICalculatorFirst
	{
		double Sum(double a, double b);
		double Difference(double a, double b);
		double Multiply(double a, double b);
		double Divide(double a, double b);
	}

    public interface ICalculatorSecond
    {
        double Sum(double a, double b);
        double Difference(double a, double b);
        double Multiply(double a, double b);
        double Divide(double a, double b);
    }

    public class PrimitiveCalculatorFirst : ICalculatorFirst
	{
        public PrimitiveCalculatorFirst() { }


		public double Sum(double a, double b)
		{
            return a + b;
		}

		public double Difference(double a, double b)
		{
            return a - b;
		}

		public double Multiply(double a, double b)
		{
            return a * b;
		}

		public double Divide(double a, double b)
		{
            return a / b;
		}
	}

    public class PrimitiveCalculatorSecond : ICalculatorSecond
    {
        public PrimitiveCalculatorSecond() { }


        public double Sum(double a, double b)
        {
            return a + b;
        }

        public double Difference(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            return a / b;
        }
    }
}
