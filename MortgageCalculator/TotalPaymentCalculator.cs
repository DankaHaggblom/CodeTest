using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator
{
    /// <summary>
    /// Contains methods for calculating mortgage payments
    /// </summary>
    public static class TotalPaymentCalculator
    {
        /// <summary>
        /// Calculates the fix monthly payment for a customer mortgage
        /// </summary>
        /// <param name="mortgage">Customer mortgage with loan terms.</param>
        /// <returns>The montly payment for each month.</returns>
        public static decimal CalculateMonthlyPayment(CustomerMortgage mortgage)
        {
            var pow = Pow(1 + mortgage.MonthlyInterestFractionary, mortgage.Months);
            var division = pow / (pow - 1);
            var result = mortgage.TotalLoan * mortgage.MonthlyInterestFractionary * division;

            var truncated = result.Truncate(27);

            return (decimal)truncated;
        }

        /// <summary>
        /// Calculates a power of a decimal base with a natural exponent.
        /// </summary>
        /// <returns>
        /// The power as a BigDecimal.
        /// </returns>
        public static BigDecimal Pow(BigDecimal @base, int exponent)
        {
            if (exponent < 0)
            {
                throw new ArgumentException("Exponent cannot be negative");
            }

            BigDecimal result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * @base;
            }

            return result;
        }
    }
}
