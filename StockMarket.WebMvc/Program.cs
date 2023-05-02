using StockMarket.Core.ServiceInterfaces;
using StockMarket.Infrastructure.Services;
using StockMarket.WebMvc.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TraingOptions"));
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();



var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();


app.Run();
