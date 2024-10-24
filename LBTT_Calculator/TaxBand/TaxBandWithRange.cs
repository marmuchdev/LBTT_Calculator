using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.TaxBand
{
    public class TaxBandWithRange : ITaxBand
    {
        private double taxRate { get; set; }
        private double lowerLimit { get; set; }
        private double upperLimit { get; set; }

        private IOutput output;

        public TaxBandWithRange(double taxRate, double lowerLimit, double upperLimit)
        {
            this.taxRate = taxRate;
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
            this.output = new ConsoleOutput();

        }

        public double Apply(double taxableAmount)
        {
            if (taxableAmount < lowerLimit) taxableAmount = 0;
            else if (taxableAmount > upperLimit) taxableAmount = upperLimit - lowerLimit;
            else taxableAmount = taxableAmount - lowerLimit;
            return Math.Round(taxableAmount * (taxRate / 100));
        }

        public void Display(IOutput output)
        {
            output.Write("Tax rate: " + taxRate + " Range: " + lowerLimit + "-" + upperLimit);
        }

        public double Apply(TransactionDetails t)
        {
            double taxableAmount = t.PurchasePrice;
            if (taxableAmount < lowerLimit) taxableAmount = 0;
            else if (taxableAmount > upperLimit) taxableAmount = upperLimit - lowerLimit;
            else taxableAmount = taxableAmount - lowerLimit;
            return Math.Round(taxableAmount * (taxRate / 100));
        }
    }
}
