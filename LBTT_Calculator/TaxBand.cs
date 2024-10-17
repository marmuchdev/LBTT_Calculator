using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class TaxBand : ITaxBand
    {
        public double lowerLimit { get; private set; }
        public double upperLimit { get; private set; }

        public double taxRate { get; private set; }

        public TaxBand(double taxRate, double lowerLimit, double upperLimit)
        {
            this.taxRate = taxRate;
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
        }

        public double GetTaxRate() {
            return taxRate;
        }
    }
}
