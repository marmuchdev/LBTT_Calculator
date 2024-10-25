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
        TransactionDetails t = new TransactionDetails(875000, 0, true);

        var stdCalc = new TaxCalculatorFactory().CreateResidential();
        var result = stdCalc.CalculateTax(t);
        Console.WriteLine("Standard Calculator LBTT = "+ result);

    }
}