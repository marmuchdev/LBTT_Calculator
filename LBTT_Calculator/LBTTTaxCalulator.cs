using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class LBTTTaxCalulator : ITaxCalculator
    {
        public double purchasePrice { get; set; }
        IOutput output;
        public LBTTTaxCalulator() {
            this.output = 
        }
        public double CalculateTax()
        {
            throw new NotImplementedException();
        }

        public void GreetUser()
        {

        }
    }
}
