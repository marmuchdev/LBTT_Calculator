// See https://aka.ms/new-console-template for more information
using LBTT_Calculator;
using LBTT_Calculator.Output;
using LBTT_Calculator.TaxBand;
class Application
{
    public static void Main(string[] args)
    {
        new Application().Run();
    }

    public void Run()
    {
        Console.WriteLine("LBTT CALCULATOR");

        IOutput output = new OutputFactory().Create();
        double taxableAmount = 875000;
        List<ITaxBand> taxBandsList = new List<ITaxBand>();

        ITaxBand taxBand0 = new TaxBandNoTax();
        ITaxBand taxBand2 = new TaxBandWithRange(2, 145001, 250000);
        ITaxBand taxBand5 = new TaxBandWithRange(5, 250001, 325000);
        ITaxBand taxBand10 = new TaxBandWithRange(10, 325001, 750000);
        ITaxBand tax12 = new TaxBandOneLimit(12, 750001);

        taxBandsList.Add(taxBand0);
        taxBandsList.Add(taxBand2);
        taxBandsList.Add(taxBand5);
        taxBandsList.Add(taxBand10);
        taxBandsList.Add(tax12);

        LBTTTaxCalulator calc1 = new LBTTTaxCalulator(output, taxBandsList, taxableAmount);

        calc1.CalculateTax();


    }
}