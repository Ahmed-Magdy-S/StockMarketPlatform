
using StockMarket.Infrastructure.DTO;
using StockMarket.WebMvc.Services;

namespace Tests.Services;

public class StockServiceTest
{
    private readonly StockService _stockService;

    public StockServiceTest()
    {
        _stockService = new StockService();
    }

    #region CreateBuyOrder

    /// <summary>
    ///  When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
    /// </summary>
    [Fact]
    public void CreateBuyOrder_NullBuyOrderRequest()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = null;

        //Assert
        Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            //Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });

    }

    /// <summary>
    /// When you supply buyOrderQuantity as 0
    /// (as per the specification, minimum is 1), it should throw ArgumentException.
    /// </summary>
    [Fact]
    public void CreateBuyOrder_ZeroQuantity()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new(){ Quantity = 0,StockName = "",StockSymbol = ""};

        //Assert
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
             await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    /// <summary>
    /// When you supply buyOrderQuantity as 100001
    /// (as per the specification, maximum is 100000), it should throw ArgumentException.
    /// </summary>

    [Fact]
    public void CreateBuyOrder_ExceedingAllowedQuantity()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new() { Quantity = 100001, StockName = "", StockSymbol = "" };

        //Assert
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    /// <summary>
    /// When you supply buyOrderPrice as 0
    /// (as per the specification, minimum is 1), it should throw ArgumentException.
    /// </summary>
    [Fact]
    public void CreateBuyOrder_ZeroPrice()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new() { Price = 0 , StockName = "", StockSymbol = "" };

        //Assert
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    /// <summary>
    /// When you supply buyOrderPrice as 10001 (as per the specification,
    /// maximum is 10000), it should throw ArgumentException.
    /// </summary>
    [Fact]
    public void CreateBuyOrder_ExceedingAllowedPrice()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new()
        {
            Price = 10001,
            StockName = "Microsoft",
            StockSymbol = "MSFT"
        };

        //Assert
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    /// <summary>
    /// When you supply stock symbol=null (as per the specification,
    /// stock symbol can't be null), it should throw ArgumentException.
    /// </summary>
    [Fact]
    public void CreateBuyOrder_NullSymbol()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new() { StockSymbol = null, StockName = "" };

        //Assert
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    /// <summary>
    /// When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) -
    /// (as per the specification, it should be equal or
    /// newer date than 2000-01-01), it should throw ArgumentException
    /// </summary>
    [Fact]
    public void CreateBuyOrder_OlderOrderDate()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new()
        {
            DateAndTimeOfOrder = new DateTime(1999,12,31),
            StockName = "",
            StockSymbol = ""
        };

        DateTime testDateTime = new(2000,1,1);
        
        Assert.False(buyOrderRequest.DateAndTimeOfOrder > testDateTime);
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _stockService.CreateBuyOrder(buyOrderRequest);

        });
    }

    /// <summary>
    /// 8. If you supply all valid values, it should be successful
    /// and return an object of BuyOrderResponse type with
    /// auto-generated BuyOrderID (guid).
    /// </summary>
    [Fact]
    public async void CreateBuyOrder_ValidData()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new()
        {
            Price = 1,
            Quantity = 1,
            StockName = "Microsoft",
            StockSymbol = "MSFT",
            DateAndTimeOfOrder = DateTime.Now
        };
        Assert.False(DateTime.Now <  new DateTime(2000,1,1));
        //Act
        BuyOrderResponse buyOrderResponse = await _stockService.CreateBuyOrder(buyOrderRequest);

        Assert.NotEqual(buyOrderResponse.BuyOrderID, Guid.Empty );
    }
    #endregion

    #region CreateSellOrder

    [Fact]
    public void CreateSellOrder_NullArgument()
    {
        //Arrange
        SellOrderRequest? sellOrderRequest = null;

        Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    #endregion
}

