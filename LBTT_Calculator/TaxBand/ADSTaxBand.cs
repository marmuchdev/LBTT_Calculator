using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.TaxBand
{
    public class ADSTaxBand : ITaxBand
    {
        private double taxRate { get; set; }
        private double treshold;
        public ADSTaxBand(double taxRate, double treshold)
        {
            this.taxRate = taxRate;
            this.treshold = treshold;
        }

        public double Apply(TransactionDetails t)
        {
            double taxableAmount = t.ADSamount;
            if (taxableAmount < treshold) return 0;
            return Math.Round(taxableAmount * (taxRate / 100));
        }

        public void Display(IOutput output)
        {
            throw new NotImplementedException();
        }

    }
}
