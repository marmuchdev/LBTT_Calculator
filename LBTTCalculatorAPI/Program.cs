using LBTT_Calculator;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
TransactionDetails t1 = new TransactionDetails(12,45,false);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World");
app.MapGet("/residential/{purchasePrice}", (double purchasePrice) => {
    TransactionDetails t = new TransactionDetails(purchasePrice, 0, false);

    return Results.Ok(t); 
});

//app.MapPost("/residential", (TransactionDetails t) =>
//{
//    var stdCalc = new TaxCalculatorFactory().CreateResidential().CalculateTax(t);

//    return stdCalc;
//});

app.MapPost("/residential", async (TransactionDetails t) =>
{
    var stdCalc = new TaxCalculatorFactory().CreateResidential();
    double result =stdCalc.CalculateTax(t);

    return Results.Ok(result);
});

app.Run();

