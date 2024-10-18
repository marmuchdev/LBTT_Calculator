using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.TaxBand
{
    public interface ITaxBand
    {
        //public void Apply(TaxableAmount t);
        public double Apply(double taxableAmount);
        public void Display(IOutput output);

    }
}
