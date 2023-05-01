using StockMarket.WebMvc.Config;
using StockMarket.WebMvc.ServiceInterfaces;
using StockMarket.WebMvc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TraingOptions"));
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();



var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();


app.Run();
