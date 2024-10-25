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
        private double ADStreshold;

        public StandardCalculator(List<ITaxBand> taxBandsList)
        {
            this.taxBandsList = new BandFactory().CreateStandard();
            calc = new LBTTTaxCalulator(new OutputFactory().Create(), taxBandsList);

            this.ADStaxBand = new TaxBandOneLimit(6, 0);
            this.ADStreshold = 40000;
        }


        public StandardCalculator()
        {
            this.taxBandsList = new BandFactory().CreateStandard();
            calc = new LBTTTaxCalulator(new OutputFactory().Create(), taxBandsList);
            this.ADStaxBand = new BandFactory().CreateADS();
            //this.ADStaxBand = new TaxBandOneLimit(6, 0);
            this.ADStreshold = 40000;
        }
        //public double CalculateTax(double purchasePrice)
        //{
        //    double totalTax = 0; 
        //    foreach (var taxBand in taxBandsList)
        //    {
        //        totalTax = totalTax + taxBand.Apply(purchasePrice);
        //    }
        //    return totalTax;
        //}

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
            if (t.ADSamount >= this.ADStreshold) return totalTax = totalTax + ADStaxBand.Apply(t);

            return totalTax;
        }
    }
}
