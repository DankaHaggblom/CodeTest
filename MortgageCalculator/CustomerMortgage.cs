using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MortgageCalculator
{
    /// <summary>
    /// Represents a customer mortgage
    /// </summary>
    public class CustomerMortgage
    {
        string _name;
        decimal _totalLoan;
        decimal _interest;
        decimal _years;

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        [RegularExpression(@"\d+([\.]\d+)?", ErrorMessage = "Must be a number. Use . as decimal separator")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Loan can't be zero")]
        /// <summary>
        /// The amount of money loaned
        /// </summary>
        public decimal TotalLoan { get => _totalLoan; set => _totalLoan = value; }

        [RegularExpression(@"\d+([\.]\d+)?", ErrorMessage = "Must be a number. Use . as decimal separator")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Interest can't be zero")]
        /// <summary>
        /// The yearly interest expressed as percentage (that is, a value of 1 means 1%)
        /// </summary>
        public decimal Interest { get => _interest; set => _interest = value; }

        [RegularExpression(@"\d+([\.]\d+)?", ErrorMessage = "Must be a number. Use . as decimal separator")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Years can't be zero")]
        /// <summary>
        /// The duration of the loan in years
        /// </summary>
        public decimal Years { get => _years; set => _years = value; }

        /// <summary>
        /// The duration of the loan in months.
        /// </summary>
        public int Months => (int)(_years * 12);

        /// <summary>
        /// The monthly interest expressed as a fraction (that is, 0.01 means 1% monthly interest)
        /// </summary>
        public decimal MonthlyInterestFractionary => Interest / 1200 ;

        public CustomerMortgage(string name, decimal totalLoan, decimal interest, decimal years)
        {
            Name = name;
            TotalLoan = totalLoan;
            Interest = interest;
            Years = years;
        }

        public string Serialize()
        {
            string name = Name.Contains(",") ? $"\"{Name}\"" : Name;

            FormattableString result = $"{name},{TotalLoan},{Interest},{Years}";
            return result.ToString(CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Loan: {TotalLoan}, Interest: {Interest}%, Years: {Years}";
        }

        #region Parsing
        static readonly Regex regex = new Regex(@"\s*((""(?'Name'[^""]+)"")|(?'Name'[^,]+)),\s*(?'Loan'[^\s,]+)\s*,\s*(?'Interest'[^\s,]+)\s*,\s*(?'Years'[^\s]+)\s*");

        /// <summary>
        /// Attempts to parse a customer mortgage from a line of text
        /// </summary>
        /// <param name="line">Input text line</param>
        /// <param name="mortgage">Resulting mortgage object</param>
        /// <returns>True if the parsing was successful, false otherwise.</returns>
        public static bool TryParse(string line, out CustomerMortgage mortgage)
        {
            if (line == null)
            {
                mortgage = null;
                return false;
            }

            var match = regex.Match(line);

            if (match.Success)
            {
                try
                {
                    // Since we accept spaces before and after the values in the columns,
                    // we will remove the leading and trailing spaces here.
                    var name = match.Groups["Name"].Value?.Trim();
                    var loan = decimal.Parse(match.Groups["Loan"].Value, CultureInfo.InvariantCulture);
                    var interest = decimal.Parse(match.Groups["Interest"].Value, CultureInfo.InvariantCulture);
                    var years = decimal.Parse(match.Groups["Years"].Value, CultureInfo.InvariantCulture);
                    mortgage = new CustomerMortgage(name, loan, interest, years);
                    return true;
                }
                catch
                {
                    // Any exception means a failure at parsing.
                }
            }
            // No match means a failure at parsing
            mortgage = null;
            return false;

        }
        #endregion Parsing
    }
}
