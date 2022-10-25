using MortgageCalculator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:\nCalculateMortgage <data file>");
                return;
            }

            var path = args[0];

            var mortgages = ReadFile(path);
            var counter = 0;

            foreach (var mortgage in mortgages)
            {
                counter++;
                var payment = TotalPaymentCalculator.CalculateMonthlyPayment(mortgage);

                Console.WriteLine($"Prospect {counter}:\n{mortgage}\n{mortgage.Name} wants to borrow {mortgage.TotalLoan}€ for a period of {mortgage.Years} years and pay {payment:0.00}€ each month");
                Console.WriteLine();
            }
        }

        private static IEnumerable<CustomerMortgage> ReadFile(string file)
        {
            if (!File.Exists(file))
            {
                yield break;
            }

            var lines = File.ReadAllLines(file);

            foreach (var line in lines)
            {
                if (CustomerMortgage.TryParse(line, out CustomerMortgage customerMortgage))
                {
                    yield return customerMortgage;
                }
            }
        }
    }
}
