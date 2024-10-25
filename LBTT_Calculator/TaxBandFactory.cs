using LBTT_Calculator.TaxBand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator
{
    public class TaxBandFactory
    {
        public List<ITaxBand> taxBandsList { get; set; }
        public List<ITaxBand> CreateStandard()
        {

            taxBandsList = new List<ITaxBand>();
            taxBandsList.Add(new TaxBandWithRange(2, 145001, 250000));
            taxBandsList.Add(new TaxBandWithRange(5, 250001, 325000));
            taxBandsList.Add(new TaxBandWithRange(10, 325001, 750000));
            taxBandsList.Add(new TaxBandOneLimit(12, 750001));
            taxBandsList.Add(new ADSTaxBand(6, 40000));

            return taxBandsList;
        }

        public List<ITaxBand> CreateFTBRelief()
        {

            taxBandsList = new List<ITaxBand>();
            taxBandsList.Add(new TaxBandWithRange(2, 175001, 250000));
            taxBandsList.Add(new TaxBandWithRange(5, 250001, 325000));
            taxBandsList.Add(new TaxBandWithRange(10, 325001, 750000));
            taxBandsList.Add(new TaxBandOneLimit(12, 750001));

            return taxBandsList;
        }

        public ITaxBand CreateADS()
        {
            ITaxBand taxBand = new ADSTaxBand(6, 40000);
            return taxBand;
        }
    }
}
