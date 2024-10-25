using LBTT_Calculator.Output;
using LBTT_Calculator.TaxBand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class StandardCalculator
    {
        private LBTTTaxCalulator calc;
        private List<ITaxBand> taxBandsList = new List<ITaxBand>();
        private ITaxBand ADStaxBand;

        public StandardCalculator()
        {
            this.taxBandsList = new BandFactory().CreateStandard();
            calc = new LBTTTaxCalulator(new OutputFactory().Create(), taxBandsList);
            this.ADStaxBand = new BandFactory().CreateADS();
        }

        public double CalculateTax(TransactionDetails t)
        {
            if (t.IsFirstTimeBuyers){ taxBandsList = new BandFactory().CreateFTBRelief();}
            else { taxBandsList = new BandFactory().CreateStandard(); }
            double totalTax = 0;
            foreach (var taxBand in taxBandsList)
            {
                totalTax = totalTax + taxBand.Apply(t);
            }
            double ADSamount = t.ADSamount;
            totalTax = totalTax + ADStaxBand.Apply(t);

            return totalTax;
        }
    }
}
