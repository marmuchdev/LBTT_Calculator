using LBTT_Calculator;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    public class Tests
    {
        public TaxBand taxBand { get; set; } = null!;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetTaxBandRateBasedOnLowerAndUpperLimit_Test()
        {
            //Arrange
            double taxRate = 2;
            double lowerLimit = 145001;
            double upperLimit = 250000;
            double expected = 2;
            //Act
            TaxBand taxBand = new TaxBand(taxRate, lowerLimit, upperLimit);
            double result = taxBand.GetTaxRate();
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

    }



 

    
}