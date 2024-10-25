using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class LBTTTaxCalulator 
    {
        private double purchasePrice { get; set; }
        IOutput output;
        private List<ITaxBand> taxBandsList  = new List<ITaxBand>();
        private double totalTax { get; set; }
        public LBTTTaxCalulator(IOutput output, List<ITaxBand> taxBandList, double purchasePrice) {
            this.output = output;
            this.taxBandsList = taxBandList;
            this.purchasePrice = purchasePrice;
            this.totalTax = 0;
        }

        public LBTTTaxCalulator(IOutput output, List<ITaxBand> taxBandList)
        {
            this.output = output;
            this.taxBandsList = taxBandList;
            this.totalTax = 0;
        }




        public double CalculateTax(TransactionDetails t)
        {
            foreach (var taxBand in taxBandsList)
            {
                totalTax = totalTax + taxBand.Apply(t);
            }
            DisplayCalculatedTax();
            return totalTax;
        }
        public void DisplayList()
        {
            foreach (var taxBand in taxBandsList)
            {
                taxBand.Display(output);
            }
        }
        public void DisplayCalculatedTax()
        {
            output.Write("For "+purchasePrice + " LBTTax = "+totalTax);
        }
    }
}
