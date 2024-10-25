using LBTT_Calculator.Output;
using LBTT_Calculator.TaxBand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class TaxCalculatorFactory
    {

        public ResidentialCalculator CreateResidential()
        {
            return new ResidentialCalculator();
        }       
    }
}
