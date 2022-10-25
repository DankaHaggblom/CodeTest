using NUnit.Framework;
using MortgageCalculator;
using System;

namespace MortgageCalculator_UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void PowZeroExponent()
        {
            var result = TotalPaymentCalculator.Pow(123, 0);
            Assert.That(result, Is.EqualTo((BigDecimal)1));
        }

        [Test]
        public void PowNegativeExponent()
        {
            void func()
            {
                var result = TotalPaymentCalculator.Pow(123, -1);
            }

            Assert.That(func, Throws.TypeOf<ArgumentException>());                        
        }


        [Test]
        public void PowZeroBase()
        {
            var result = TotalPaymentCalculator.Pow(0, 123);
            Assert.That(result, Is.EqualTo((BigDecimal)0));
        }

        [Test]
        public void Pow2To9()
        {
            var result = TotalPaymentCalculator.Pow(2, 9);
            Assert.That(result, Is.EqualTo((BigDecimal)512));
        }

        [Test]
        public void ZeroLoan()
        {
            CustomerMortgage testMortgage = new CustomerMortgage("Test", 0, 5, 2);
            var result = TotalPaymentCalculator.CalculateMonthlyPayment(testMortgage);

            Assert.That(result, Is.EqualTo(0));
        }

        //These three use cases have been checked using this website https://www.calculator.net/payment-calculator.html
        [Test]
        public void UseCaseOne()
        {
            CustomerMortgage testMortgage = new CustomerMortgage("Test", 1000, 5, 2);
            var result = TotalPaymentCalculator.CalculateMonthlyPayment(testMortgage);

            Assert.That(Math.Round(result, 2), Is.EqualTo(43.87m));
        }

        [Test]
        public void UseCaseTwo()
        {
            CustomerMortgage testMortgage = new CustomerMortgage("Test", 165000, 2.7m, 18);
            var result = TotalPaymentCalculator.CalculateMonthlyPayment(testMortgage);

            Assert.That(Math.Round(result, 2), Is.EqualTo(965.33m));
        }

        [Test]
        public void UseCaseThree()
        {
            CustomerMortgage testMortgage = new CustomerMortgage("Test", 175000, 1.92m, 50);
            var result = TotalPaymentCalculator.CalculateMonthlyPayment(testMortgage);

            Assert.That(Math.Round(result, 2), Is.EqualTo(453.95m));
        }
    }
}
