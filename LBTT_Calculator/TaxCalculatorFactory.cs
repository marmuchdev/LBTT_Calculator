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

        public List<ITaxBand> taxBandsList { get; set; }

        public StandardCalculator CreateStandardResidential()
        {
            return new StandardCalculator();
        }



        //public StandardCalculator CreateFirstTimeBuyer()
        //{
        //    taxBandsList = new List<ITaxBand>();
        //    taxBandsList.Add(new TaxBandWithRange(2, 175001, 250000));
        //    taxBandsList.Add(new TaxBandWithRange(5, 250001, 325000));
        //    taxBandsList.Add(new TaxBandWithRange(10, 325001, 750000));
        //    taxBandsList.Add(new TaxBandOneLimit(12, 750001));
        //    return new StandardCalculator(taxBandsList);
        //}
       
    }
}
