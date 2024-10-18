using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.TaxBand
{

    public class TaxBandOneLimit : ITaxBand

    {
        private double taxRate { get; set; }
        private double lowerLimit { get; set; }

        IOutput output;
        public TaxBandOneLimit(double taxRate, double lowerLimit)
        {
            this.taxRate = taxRate;
            this.output = new ConsoleOutput();
            this.lowerLimit = lowerLimit;   
        }
        public double Apply(double taxableAmount)
        {
            if (taxableAmount < lowerLimit) return 0;
            taxableAmount-=lowerLimit;
            return Math.Round(taxableAmount * (taxRate / 100));

        }

        public void Display(IOutput output)
        {
            output.Write("Tax rate: "+ taxRate);
        }
    }
}
