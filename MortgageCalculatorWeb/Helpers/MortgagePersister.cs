using MortgageCalculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MortgageCalculatorWeb.Helpers
{
    public class MortgagePersister
    {
        public List<CustomerMortgage> Mortgages { get; set; }
        private string path;
        public MortgagePersister(string path)
        {
            this.path = path;
            Mortgages = System.IO.File.ReadAllLines(path)
                        .Select(x => CustomerMortgage.TryParse(x, out var c) ? c : null)
                        .Where(x => x != null)
                        .ToList();
        }

        public void Persist()
        {
            using (var file = File.CreateText(path))
            {
                foreach (var m in Mortgages)
                {
                    var line = m.Serialize();
                    file.WriteLine(line);
                }
            }
        }
    }
}