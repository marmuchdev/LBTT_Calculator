using LBTT_Calculator;
using LBTT_Calculator.TaxBand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
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
        public void Apply12PCTaxBandForTaxBandOneLimit()
        {
            //Arrange
            double taxableAmount = 875000;
            double ADSAmount = 0;
            double expected = 15000;
            bool FirstTimeBuyers = true;
            ITaxBand taxBandNoRange = new TaxBandOneLimit(12, 750000);
            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);


            //Act
            double result = taxBandNoRange.Apply(t);

            //Assign
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
        [Test]
        public void Apply2PCTaxBandForTaxBandNWithRangeBetween145001And250000WithTaxableAmount145000()
        {
            //Arrange
            double taxableAmount = 235000;
            double expected = 1800;
            double ADSAmount = 0;
            bool FirstTimeBuyers = true;

            ITaxBand taxBandWithRange = new TaxBandWithRange(2, 145001, 250000);
            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

            //Act
            double result = taxBandWithRange.Apply(t);

            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTaxFor235000UsingStandardCalculator()
        {
            //Arrange
            double ADSAmount = 0;
            bool FirstTimeBuyers = true;

            double taxableAmount = 235000;
            double expected = 1800;

            var stdCalc = new TaxCalculatorFactory().CreateStandardResidential();
            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

            //Act
            var result = stdCalc.CalculateTax(t);

            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void CalculateTaxFor235000WithADSOf45000UsingStandardResidentialCalculator()
        {
            //Arrange
            double ADSAmount = 45000;
            bool FirstTimeBuyers = true;

            double taxableAmount = 235000;
            double expected = 4500;

            var stdCalc = new TaxCalculatorFactory().CreateStandardResidential();
            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

            //Act
            var result = stdCalc.CalculateTax(t);

            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void CalculateTaxFor875000WithADSOf45000UsingStandardResidentialCalculator()
        {
            //Arrange
            double ADSAmount = 45000;
            bool FirstTimeBuyers = true;

            double taxableAmount = 875000;
            double expected = 66050;

            var stdCalc = new TaxCalculatorFactory().CreateStandardResidential();
            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

            //Act
            var result = stdCalc.CalculateTax(t);

            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }
        [Test]
        public void CalculateTaxFor875000WithADSOf40000UsingStandardResidentialCalculator()
        {
            //Arrange
            double ADSAmount = 40000;
            bool FirstTimeBuyers = true;

            double taxableAmount = 875000;
            double expected = 65750;

            var stdCalc = new TaxCalculatorFactory().CreateStandardResidential();
            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

            //Act
            var result = stdCalc.CalculateTax(t);

            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void CalculateTaxFor875000WithADSOf39999UsingStandardResidentialCalculator()
        {
            //Arrange
            double ADSAmount = 39999;
            bool FirstTimeBuyers = true;

            double taxableAmount = 875000;
            double expected = 63350;

            var stdCalc = new TaxCalculatorFactory().CreateStandardResidential();
            TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

            //Act
            var result = stdCalc.CalculateTax(t);

            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }
    }
}
