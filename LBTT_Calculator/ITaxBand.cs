using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public interface ITaxBand
    {
        public void Apply(TaxableAmount t);
    }
}
