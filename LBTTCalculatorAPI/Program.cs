using LBTT_Calculator;
using System.Text.Json;

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

//Handlers
var handlePostResidential = (TransactionDetails t) =>
{
    if (t == null)
    {
        return Results.BadRequest("Invalid data.");
    }
    var stdCalc = new TaxCalculatorFactory().CreateResidential();
    TransactionDetails temp = t;
    double result = stdCalc.CalculateTax(t);

    return Results.Ok(result);
};

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World");

//Routes
app.MapPost("/residential", handlePostResidential);

app.Run();

