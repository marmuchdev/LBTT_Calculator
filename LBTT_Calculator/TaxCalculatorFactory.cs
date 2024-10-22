using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class TaxCalculatorFactory
    {
        public StandardCalculator CreateStandardResidential() { return new StandardCalculator(); }
        public FirstTimeBuyerCalculator CreateFirstTimeBuyer() { return new FirstTimeBuyerCalculator(); }

    }
}
