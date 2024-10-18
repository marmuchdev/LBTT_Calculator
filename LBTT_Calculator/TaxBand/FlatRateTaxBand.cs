using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.TaxBand
{
    public class FlatRateTaxBand : ITaxBand
    {
        private double flatRateTax { get; set; }
        IOutput output = new ConsoleOutput();

        public double Apply(double taxableAmount)
        {
            return taxableAmount + flatRateTax;
        }
        public void Display(IOutput output)
        {
            output.Write("Flat Tax rate: " + flatRateTax);
        }
    }
}
