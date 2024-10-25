using LBTT_Calculator.Output;
using LBTT_Calculator.TaxBand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class ResidentialCalculator
    {
        private LBTTTaxCalulator calc;
        private List<ITaxBand> taxBandsList = new List<ITaxBand>();
        public ResidentialCalculator()
        {
            this.taxBandsList = new TaxBandFactory().CreateStandard();
            calc = new LBTTTaxCalulator(new OutputFactory().Create(), taxBandsList);
        }

        public double CalculateTax(TransactionDetails t)
        {
            Console.WriteLine($"Calculating tax for: PurchasePrice = {t.PurchasePrice}, ADSamount = {t.ADSamount}, IsFirstTimeBuyers = {t.IsFirstTimeBuyers}");
            if (t.IsFirstTimeBuyers){ taxBandsList = new TaxBandFactory().CreateFTBRelief();}
            else { taxBandsList = new TaxBandFactory().CreateStandard(); }
            double totalTax = 0;
            foreach (var taxBand in taxBandsList)
            {
                Console.WriteLine($"Tax from band: {taxBand.Apply(t)}");
                totalTax = totalTax + taxBand.Apply(t);
            }
            Console.WriteLine($"Total Tax after all bands: {totalTax}");

            return totalTax;
        }
    }
}
