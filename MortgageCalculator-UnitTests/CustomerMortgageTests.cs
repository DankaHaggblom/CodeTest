using MortgageCalculator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator_UnitTests
{
    [TestFixture]
    public class CustomerMortgageTests
    {
        [Test]
        public void ParseEmpty()
        {
            var success = CustomerMortgage.TryParse(string.Empty, out _);
            Assert.That(success, Is.False);
        }

        [Test]
        public void ParseNull()
        {
            var success = CustomerMortgage.TryParse(null, out _);
            Assert.That(success, Is.False);
        }


        [Test]
        public void ParseCommasInNameColumn()
        {
            var line = @"""Bond, James"",1138.11,12.3,12";
            var success = CustomerMortgage.TryParse(line, out var mort);
            Assert.That(success, Is.True);
            Assert.That(mort.Name, Is.EqualTo("Bond, James"));
            Assert.That(mort.TotalLoan, Is.EqualTo(1138.11m));
            Assert.That(mort.Interest, Is.EqualTo(12.3m));
            Assert.That(mort.Years, Is.EqualTo(12));
            Assert.That(mort.Months, Is.EqualTo(12 * 12));
        }

        [Test]
        public void ParseNameWithCommasShouldBeQuoted()
        {
            var line = @"Bond, James,1138.11,12.3,12";
            var success = CustomerMortgage.TryParse(line, out _);
            Assert.That(success, Is.False);
        }

        [Test]
        public void ParseSpacesShouldBeFine()
        {
            var line = @"James Bond    , 1138.11   , 12.3   , 12";
            var success = CustomerMortgage.TryParse(line, out var mort);
            Assert.That(success, Is.True);
            Assert.That(mort.Name, Is.EqualTo("James Bond"));
            Assert.That(mort.TotalLoan, Is.EqualTo(1138.11m));
            Assert.That(mort.Interest, Is.EqualTo(12.3m));
            Assert.That(mort.Years, Is.EqualTo(12));
            Assert.That(mort.Months, Is.EqualTo(12 * 12));
        }


    }
}
