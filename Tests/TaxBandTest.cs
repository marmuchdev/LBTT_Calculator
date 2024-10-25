using LBTT_Calculator;
using LBTT_Calculator.Output;
using LBTT_Calculator.TaxBand;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    internal class MockOutput : IOutput
    {
        public string actual { get; private set; } = "";
        public void Write(string text)
        {
            actual += text;
        }
    }
    internal class MockListOutput : IOutput
    {
        private List<string> actual = new List<string>();
        public void Write(string text)
        {
            actual.Add(text);
        }

        public string AtLine(int lineNumber)
        {
            return actual[lineNumber - 1];
        }
    }
    internal class MockTaxBands
    {
        private List<ITaxBand> taxBandList = new List<ITaxBand>();
        IOutput output;

        public class Tests
        {


            [SetUp]
            public void Setup()
            {

            }

            [Test]
            public void Apply8PCTaxBandForTaxBandNoRange()
            {
                //Arrange
                double taxableAmount = 875000;
                double expected = 15000;
                double ADSAmount = 45000;
                bool FirstTimeBuyers = true;
                TaxBandOneLimit taxBandNoRange = new TaxBandOneLimit(12, 750000);
                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, FirstTimeBuyers);

                //Act
                double result = taxBandNoRange.Apply(t);

                //Assert
                Assert.That(result, Is.EqualTo(expected));
            }


            [Test]
            public void Apply2PCTaxBandForTaxBandNWithRangeBetween145001And250000WithTaxableAmount145000()
            {
                //Arrange
                double taxableAmount = 145000;
                double expected = 0;
                double ADSAmount = 45000;
                bool FirstTimeBuyers = true;
                ITaxBand taxBandWithRange = new TaxBandWithRange(2, 145001, 250000);
                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, FirstTimeBuyers);


                //Act
                double result = taxBandWithRange.Apply(t);

                //Assert
                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void ApplyADSTaxBandFor45000()
            {
                //Arrange
                double taxableAmount = 275000;
                double expected = 2700;
                double ADSAmount = 45000;
                double ADStaxRate = 6;
                double ADStreshold = 40000;
                bool FirstTimeBuyers = true;
                ITaxBand ADStaxband = new ADSTaxBand(ADStaxRate, ADStreshold);
                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, FirstTimeBuyers);

                //Act
                double result = ADStaxband.Apply(t);

                //Assert
                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void CalculateTaxFor235000()
            {
                //Arrange
                IOutput output = new ConsoleOutput();
                double taxableAmount = 235000;
                double expected = 1800;
                List<ITaxBand> taxBandsList = new List<ITaxBand>();

                ITaxBand taxBand2 = new TaxBandWithRange(2, 145001, 250000);
                ITaxBand taxBand5 = new TaxBandWithRange(5, 250001, 325000);
                ITaxBand taxBand10 = new TaxBandWithRange(10, 325001, 750000);
                ITaxBand tax12 = new TaxBandOneLimit(12, 750001);

                taxBandsList.Add(taxBand2);
                taxBandsList.Add(taxBand5);
                taxBandsList.Add(taxBand10);
                taxBandsList.Add(tax12);
                double ADSAmount = 0;
                bool FirstTimeBuyers = true;

                LBTTTaxCalulator calc1 = new LBTTTaxCalulator(output, taxBandsList, taxableAmount);
                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, FirstTimeBuyers);


                //Act
                double result = calc1.CalculateTax(t);


                //Assert
                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void CalculateTaxFor235000UsingStandardCalculator()
            {
                //Arrange
                MockOutput output = new MockOutput();
                double taxableAmount = 235000;
                double expected = 1800;
                double ADSAmount = 0;
                bool FirstTimeBuyers = true;


                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

                var stdCalc = new TaxCalculatorFactory().CreateResidential();


                //Act
                var result = stdCalc.CalculateTax(t);


                //Assert
                Assert.That(result, Is.EqualTo(expected));

            }


            [Test]
            public void CalculateTaxFor875000WithADSamountUnderTresholdOf40000()
            {
                //Arrange
                MockOutput output = new MockOutput();
                double taxableAmount = 875000;
                double ADSAmount = 30000;
                double expected = 63350;
                bool FirstTimeBuyers = true;


                var ADSCalcResult = new TaxCalculatorFactory().CreateResidential();
                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);

                //Act
                var result = ADSCalcResult.CalculateTax(t);


                //Assert
                Assert.That(result, Is.EqualTo(expected));

            }

            [Test]
            public void CalculateTaxFor875000WithADSamountAboveTresholdOf40000()
            {
                //Arrange
                MockOutput output = new MockOutput();
                double taxableAmount = 875000;
                double ADSAmount = 45000;
                double expected = 66050;
                bool FirstTimeBuyers = true;

                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);
                var ADSCalcResult = new TaxCalculatorFactory().CreateResidential();

                //Act
                var result = ADSCalcResult.CalculateTax(t);


                //Assert
                Assert.That(result, Is.EqualTo(expected));

            }

            [Test]
            public void CalculateTaxFor135000()
            {
                //Arrange
                IOutput output = new ConsoleOutput();
                double taxableAmount = 135000;
                double expected = 0;
                List<ITaxBand> taxBandsList = new List<ITaxBand>();

                ITaxBand taxBand2 = new TaxBandWithRange(2, 145001, 250000);
                ITaxBand taxBand5 = new TaxBandWithRange(5, 250001, 325000);
                ITaxBand taxBand10 = new TaxBandWithRange(10, 325001, 750000);
                ITaxBand tax12 = new TaxBandOneLimit(12, 750001);

                taxBandsList.Add(taxBand2);
                taxBandsList.Add(taxBand5);
                taxBandsList.Add(taxBand10);
                taxBandsList.Add(tax12);

                LBTTTaxCalulator calc1 = new LBTTTaxCalulator(output, taxBandsList, taxableAmount);

                double ADSAmount = 0;
                bool FirstTimeBuyers = true;
                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);


                //Act
                double result = calc1.CalculateTax(t);


                //Assert
                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void CalculateTaxFor875000()
            {
                //Arrange
                MockOutput output = new MockOutput();
                double taxableAmount = 875000;
                double expected = 63350;
                string expected_output = "For 875000 LBTTax = 63350";
                List<ITaxBand> taxBandsList = new List<ITaxBand>();

                ITaxBand taxBand2 = new TaxBandWithRange(2, 145001, 250000);
                ITaxBand taxBand5 = new TaxBandWithRange(5, 250001, 325000);
                ITaxBand taxBand10 = new TaxBandWithRange(10, 325001, 750000);
                ITaxBand tax12 = new TaxBandOneLimit(12, 750001);

                taxBandsList.Add(taxBand2);
                taxBandsList.Add(taxBand5);
                taxBandsList.Add(taxBand10);
                taxBandsList.Add(tax12);

                double ADSAmount = 0;
                bool FirstTimeBuyers = true;
                TransactionDetails t = new TransactionDetails(taxableAmount, ADSAmount, !FirstTimeBuyers);
                LBTTTaxCalulator calc1 = new LBTTTaxCalulator(output, taxBandsList, taxableAmount);


                //Act
                double result = calc1.CalculateTax(t);
                string result_standard_output = output.actual;


                //Assert
                Assert.That(result, Is.EqualTo(expected));
                Assert.That(result_standard_output, Is.EqualTo(expected_output));

            }


        }

    }
}