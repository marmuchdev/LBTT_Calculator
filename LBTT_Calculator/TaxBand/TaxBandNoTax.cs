using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.TaxBand
{
    public class TaxBandNoTax : ITaxBand
    {
        private double taxRate { get; set; }
        IOutput output;
        public TaxBandNoTax()
        {
            this.taxRate = 0;
            this.output = new ConsoleOutput();

        }
        public double Apply(double taxableAmount)
        {
            return taxableAmount * (taxRate / 100);

        }

        public void Display(IOutput output)
        {
            output.Write("Tax rate: " + taxRate);
        }
    }
}
