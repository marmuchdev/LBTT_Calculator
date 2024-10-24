using LBTT_Calculator;
using LBTT_Calculator.TaxBand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    //internal struct TransactionDetails
    //{
    //    private double purchasePrice { get; set; }
    //    private double ADSamount { get; set; }
    //    private bool isFirstTimeBuyers;
    //    public TransactionDetails(double purchasePrice, double ADSamount, bool isFirstTimeBuyers) {
    //        this.purchasePrice = purchasePrice;
    //        this.ADSamount = ADSamount;
    //        this.isFirstTimeBuyers = isFirstTimeBuyers;
    //    }

    //}
    public class TransactionStructTest
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CalculateFlatRateTaxOf1000() {
            //Arrange
            double taxableAmount = 875000;
            double ADSAmount = 0;
            double flatRateTax = 1000;
            double expected = 876000;
            bool FirstTimeBuyers = true;

            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);
            ITaxBand flatRateTaxBand = new FlatRateTaxBand(flatRateTax);

            //Act
            var result = flatRateTaxBand.Apply(t);

            //Assert
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void CalculateTaxFor875000UsingADSSupplementCalculatorWithADSamountAboveTresholdOf40000()
        {
            //Arrange
            double taxableAmount = 875000;
            double ADSAmount = 45000;
            double expected = 66050;
            bool FirstTimeBuyers = true;

            TransactionDetails t = new TransactionDetails(taxableAmount,ADSAmount,!FirstTimeBuyers);
            var ADSCalcResult = new TaxCalculatorFactory().CreateAdditionalDwellingSupplementCalculator();



            //Act
            var result = ADSCalcResult.CalculateTax(taxableAmount, ADSAmount);


            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }
    }
}
