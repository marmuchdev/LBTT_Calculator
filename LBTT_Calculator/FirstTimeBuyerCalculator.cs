using LBTT_Calculator.Output;
using LBTT_Calculator.TaxBand;

namespace LBTT_Calculator
{
    public class FirstTimeBuyerCalculator
    {
        private LBTTTaxCalulator calc;
        private List<ITaxBand> taxBandsList = new List<ITaxBand>();


        public FirstTimeBuyerCalculator()
        {
            taxBandsList = new List<ITaxBand>();
            taxBandsList.Add(new TaxBandWithRange(2, 175001, 250000));
            taxBandsList.Add(new TaxBandWithRange(5, 250001, 325000));
            taxBandsList.Add(new TaxBandWithRange(10, 325001, 750000));
            taxBandsList.Add(new TaxBandOneLimit(12, 750001));
            calc = new LBTTTaxCalulator(new OutputFactory().Create(), taxBandsList);
        }

        public double CalculateTax(double purchasePrice)
        {
            double totalTax = 0;
            foreach (var taxBand in taxBandsList)
            {
                totalTax = totalTax + taxBand.Apply(purchasePrice);
            }
            return totalTax;
        }

    }
}