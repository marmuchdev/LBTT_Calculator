using LBTT_Calculator.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.TaxBand
{
    public class DefaultTaxBands : ITaxBands
    {
        private List<ITaxBand> taxBandList = new List<ITaxBand>();
        IOutput output;

        public DefaultTaxBands(List<ITaxBand> taxBandList, IOutput output)
        {
            this.taxBandList = taxBandList;
            this.output = output;
        }

        public void Add(ITaxBand band) { 
            this.taxBandList.Add(band);
        }

        public void DisplayList()
        {
            foreach (var taxBand in taxBandList)
            {
                taxBand.Display(output);
            }
        }
       
    }
}
