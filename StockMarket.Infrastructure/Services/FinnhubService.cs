using System.Text.Json;
using Microsoft.Extensions.Configuration;
using StockMarket.Core.DTO;
using StockMarket.Core.ServiceInterfaces;

namespace StockMarket.Infrastructure.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly string _baseURL = "https://finnhub.io/api/v1";

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<CompanyProfile> GetCompanyProfile(string stockSymbol)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequest = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/stock/profile2?symbol={stockSymbol}&token={_configuration["x-finnhub-token"]}"),
            };

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            httpResponse.EnsureSuccessStatusCode();

            string responseData = await httpResponse.Content.ReadAsStringAsync();

            Dictionary<string, object>? companyProfileDic = JsonSerializer.Deserialize<Dictionary<string,object>>(responseData);

            if (companyProfileDic == null) throw new Exception("Invalid json deserilization operation");

            var test = Convert.ToDouble(companyProfileDic["marketCapitalization"].ToString());

            var test2 = companyProfileDic["marketCapitalization"].ToString();
            var test3 = companyProfileDic["marketCapitalization"];


            try
            {
                CompanyProfile companyProfile = new()
                {
                    Country = Convert.ToString(companyProfileDic["country"]),
                    Currency = Convert.ToString(companyProfileDic["currency"]),
                    Exchange = Convert.ToString(companyProfileDic["exchange"]),
                    FinnhubIndustry = Convert.ToString(companyProfileDic["finnhubIndustry"]),
                    Ipo = Convert.ToString(companyProfileDic["ipo"]),
                    Logo = Convert.ToString(companyProfileDic["logo"]),
                    MarketCapitalization = Convert.ToDouble(companyProfileDic["marketCapitalization"].ToString()),
                    Name = Convert.ToString(companyProfileDic["name"]),
                    Phone = Convert.ToString(companyProfileDic["phone"]),
                    ShareOutstanding = Convert.ToDouble(companyProfileDic["shareOutstanding"].ToString()),
                    Ticker = Convert.ToString(companyProfileDic["ticker"]),
                    Weburl = Convert.ToString(companyProfileDic["weburl"])
                };
                return companyProfile;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid conversion of JSON Response to CompanyProfile model");
            }

        }

        public async Task<StockPriceQuote> GetStockPriceQuote(string stockSymbol)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequest = new() {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseURL}/quote?symbol={stockSymbol}&token={_configuration["x-finnhub-token"]}")
            };

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            httpResponse.EnsureSuccessStatusCode();

            string responseData = await httpResponse.Content.ReadAsStringAsync();

            Dictionary<string, object>? stockPriceQuoteDic = JsonSerializer.Deserialize<Dictionary<string, object>>(responseData);

            if (stockPriceQuoteDic == null) throw new Exception("Invalid json deserilization operation");

            try
            {
                StockPriceQuote stockPriceQuote = new()
                {
                    CurrentPrice = Convert.ToDouble(stockPriceQuoteDic["c"].ToString()),
                    Change = Convert.ToDouble(stockPriceQuoteDic["d"].ToString()),
                    PercentChange = Convert.ToDouble(stockPriceQuoteDic["dp"].ToString()),
                    HighPriceOfTheDay = Convert.ToDouble(stockPriceQuoteDic["h"].ToString()),
                    LowPriceOfTheDay = Convert.ToDouble(stockPriceQuoteDic["l"].ToString()),
                    OpenPriceOfTheDay = Convert.ToDouble(stockPriceQuoteDic["o"].ToString()),
                    PreviousClosePrice = Convert.ToDouble(stockPriceQuoteDic["pc"].ToString()),
                    TimeStamp = Convert.ToUInt64(stockPriceQuoteDic["t"].ToString())
                };

                return stockPriceQuote;
            }

            catch (Exception ex)
            {
                throw new Exception("Invalid conversion of JSON Response to stockPriceQuote model");
            }


        }
    }
}
