using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class TransactionDetails
    {
        private double purchasePrice;
        public double PurchasePrice { get; }
        private double aDSamount { get; }
        public double ADSamount { get; }
        private bool isFirstTimeBuyers;
        public bool IsFirstTimeBuyers { get; }

        public TransactionDetails(double purchasePrice, double ADSamount, bool isFirstTimeBuyers)
        {
            this.PurchasePrice = purchasePrice;
            this.ADSamount = ADSamount;
            this.IsFirstTimeBuyers = isFirstTimeBuyers;
        }
    }
}
