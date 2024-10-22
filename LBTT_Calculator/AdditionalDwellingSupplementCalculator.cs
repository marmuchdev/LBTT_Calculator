using LBTT_Calculator.Output;
using LBTT_Calculator.TaxBand;

namespace LBTT_Calculator
{
    public class AdditionalDwellingSupplementCalculator
    {
        private LBTTTaxCalulator calc;
        private List<ITaxBand> taxBandsList = new List<ITaxBand>();
        private ITaxBand ADStaxBand;
        private double ADStreshold;


        public AdditionalDwellingSupplementCalculator()
        {
            taxBandsList = new List<ITaxBand>();
            taxBandsList.Add(new TaxBandWithRange(2, 145001, 250000));
            taxBandsList.Add(new TaxBandWithRange(5, 250001, 325000));
            taxBandsList.Add(new TaxBandWithRange(10, 325001, 750000));
            taxBandsList.Add(new TaxBandOneLimit(12, 750001));

            calc = new LBTTTaxCalulator(new OutputFactory().Create(), taxBandsList);
            this.ADStaxBand = new TaxBandOneLimit(6, 0);
            this.ADStreshold = 40000;
        }

        public double CalculateTax(double purchasePrice ,double ADSAmount)
        {
            double totalTax = 0;
            foreach (var taxBand in taxBandsList)
            {
                totalTax = totalTax + taxBand.Apply(purchasePrice);
            }
            if (ADSAmount >= this.ADStreshold) return totalTax = totalTax + ADStaxBand.Apply(ADSAmount);
            return totalTax;
        }
    }
}