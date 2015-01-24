using System;
namespace DIFrameworksPerformanceTester
{
    public class Program
    {
        static void Main(string[] args)
        {
            // This flag will be used to determine which
            // category the user wants to see tested. 
            // The all in one category(1), or split by classes category(2).
            int flag = 0;
            int numberOfTests = 0;            
            while (flag == 0)
            {
                Console.WriteLine("Choose 1 for all category time or\nchoose 2 for times over categories.");
                if (!int.TryParse(Console.ReadLine(), out flag))
                    continue;
                Console.WriteLine("How much tests would you want to run:");                
                if (!int.TryParse(Console.ReadLine(), out numberOfTests))
                    continue;
                switch (flag)
                {
                    case 1:
                        TestService.MeasureAll(numberOfTests);
                        break;
                    case 2:
                        TestService.MeasureCategories(numberOfTests);
                        break;
                    default:
                        flag = 0;
                        break;
                }
            }
        }
    }
}
