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

        public MockTaxBands(List<ITaxBand> taxBandList, IOutput output)
        {
            this.taxBandList = taxBandList;
            this.output = output;
        }

        public void Add(ITaxBand band)
        {
            this.taxBandList.Add(band);
        }

        public void DisplayList()
        {
            foreach (var item in taxBandList)
            {
                output.Write(item.ToString());
            }
        }

    }
    internal class MockTaxBandNoRange : ITaxBand
    {
        public double taxRate { get; set; }
        public MockTaxBandNoRange(double taxRate)
        {
            this.taxRate = taxRate;
        }

        public double Apply(double taxableAmount)
        {
            return taxableAmount * (taxRate/100);
        }

        public void Display(IOutput output)
        {
            throw new NotImplementedException();
        }

        public double Apply(TransactionDetails t)
        {
            throw new NotImplementedException();
        }
    }

    internal class MockTaxBandWithRange : ITaxBand
    {
        public double taxRate { get; set; }
        public double lowerLimit { get; set; }
        public double upperLimit { get; set; }
        public MockTaxBandWithRange(double taxRate, double lowerLimit, double upperLimit)
        {
            this.taxRate = taxRate;
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
        }

        public double Apply(double taxableAmount)
        {
            if (taxableAmount < lowerLimit) taxableAmount = 0;
            else if (taxableAmount > upperLimit) taxableAmount = upperLimit - lowerLimit;
            else taxableAmount = taxableAmount - lowerLimit;
            return Math.Round(taxableAmount * (taxRate / 100));
        }

        public void Display(IOutput output)
        {
            throw new NotImplementedException();
        }

        public double Apply(TransactionDetails t)
        {
            throw new NotImplementedException();
        }
    }

    internal class MockFlatRateTaxBand : ITaxBand
    {
        public double flatRateTax { get; set; }
        public double Apply(double taxableAmount)
        {
            return taxableAmount + flatRateTax;
        }

        public double Apply(TransactionDetails t)
        {
            throw new NotImplementedException();
        }

        public void Display(IOutput output)
        {
            output.Write("Flat Tax rate: " + flatRateTax);
        }

    }
    public class Tests
    {
        private MockFlatRateTaxBand mockFlatRateTaxBand;
        private MockTaxBandNoRange mockTaxBandNoRange;

        [SetUp]
        public void Setup()
        {
            mockFlatRateTaxBand = new MockFlatRateTaxBand();
            mockTaxBandNoRange = new MockTaxBandNoRange(10);
        }

       

        [Test]
        public void ApplyNockFlatRateTaxBandOf5000()
        {
            //Arrange
            double taxableAmount = 15000;
            mockFlatRateTaxBand.flatRateTax = 5000;
            double expected = 20000;
            
            //Act
            double result = mockFlatRateTaxBand.Apply(taxableAmount);
            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Apply10PCTaxBandForMockTaxBandNoRange()
        {
            //Arrange
            double taxableAmount = 15000;
            double expected = 1500;

            //Act
            double result = mockTaxBandNoRange.Apply(taxableAmount);
            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }



        [Test]
        public void Apply8PCTaxBandForMockTaxBandNoRange()
        {
            //Arrange
            double taxableAmount = 875000;
            double expected = 15000;
            TaxBandOneLimit taxBandNoRange = new TaxBandOneLimit(12,750000);

            //Act
            double result = taxBandNoRange.Apply(taxableAmount);

            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Apply2PCTaxBandForMockTaxBandNWithRangeBetween145001And250000WithTaxableAmount235000()
        {
            //Arrange
            double taxableAmount = 235000;
            double expected = 1800;
            MockTaxBandWithRange mockTaxBandWithRange = new MockTaxBandWithRange(2, 145001,250000);

            //Act
            double result = mockTaxBandWithRange.Apply(taxableAmount);

            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Apply2PCTaxBandForMockTaxBandNWithRangeBetween145001And250000WithTaxableAmount275000()
        {
            //Arrange
            double taxableAmount = 275000;
            double expected = 2100;
            MockTaxBandWithRange mockTaxBandWithRange = new MockTaxBandWithRange(2, 145001, 250000);

            //Act
            double result = mockTaxBandWithRange.Apply(taxableAmount);

            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Apply2PCTaxBandForMockTaxBandNWithRangeBetween145001And250000WithTaxableAmount145000()
        {
            //Arrange
            double taxableAmount = 145000;
            double expected = 0;
            MockTaxBandWithRange mockTaxBandWithRange = new MockTaxBandWithRange(2, 145001, 250000);

            //Act
            double result = mockTaxBandWithRange.Apply(taxableAmount);

            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Apply2PCTaxBandForTaxBandNWithRangeBetween145001And250000WithTaxableAmount145000()
        {
            //Arrange
            double taxableAmount = 145000;
            double expected = 0;
            TaxBandWithRange taxBandWithRange = new TaxBandWithRange(2, 145001, 250000);

            //Act
            double result = taxBandWithRange.Apply(taxableAmount);

            //Assign
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

            LBTTTaxCalulator calc1 = new LBTTTaxCalulator(output, taxBandsList, taxableAmount);


            //Act
            double result = calc1.CalculateTax();


            //Assign
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTaxFor235000UsingStandardCalculator()
        {
            //Arrange
            MockOutput output = new MockOutput();
            double taxableAmount = 235000;
            double expected = 1800;


            var stdCalc = new TaxCalculatorFactory().CreateStandardResidential();


            //Act
            var result = stdCalc.CalculateTax(taxableAmount);


            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void CalculateTaxFor235000UsingFirstTimeBuyerCalculator()
        {
            //Arrange
            MockOutput output = new MockOutput();
            double taxableAmount = 235000;
            double expected = 1200;


            var stdCalc = new TaxCalculatorFactory().CreateFirstTimeBuyer();


            //Act
            var result = stdCalc.CalculateTax(taxableAmount);


            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void CalculateTaxFor875000UsingADSSupplementCalculatorWithADSamountUnderTresholdOf40000()
        {
            //Arrange
            MockOutput output = new MockOutput();
            double taxableAmount = 875000;
            double ADSAmount = 30000;
            double expected = 63350;


            var ADSCalcResult = new TaxCalculatorFactory().CreateAdditionalDwellingSupplementCalculator();



            //Act
            var result = ADSCalcResult.CalculateTax(taxableAmount, ADSAmount);


            //Assign
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void CalculateTaxFor875000UsingADSSupplementCalculatorWithADSamountAboveTresholdOf40000()
        {
            //Arrange
            MockOutput output = new MockOutput();
            double taxableAmount = 875000;
            double ADSAmount = 45000;
            double expected = 66050;


            var ADSCalcResult = new TaxCalculatorFactory().CreateAdditionalDwellingSupplementCalculator();



            //Act
            var result = ADSCalcResult.CalculateTax(taxableAmount, ADSAmount);


            //Assign
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


            //Act
            double result = calc1.CalculateTax();


            //Assign
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

            LBTTTaxCalulator calc1 = new LBTTTaxCalulator(output, taxBandsList, taxableAmount);


            //Act
            double result = calc1.CalculateTax();
            string result_standard_output = output.actual;


            //Assign
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result_standard_output, Is.EqualTo(expected_output));

        }


    }



 

    
}