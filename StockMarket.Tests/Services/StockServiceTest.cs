
using StockMarket.Core.DTO;
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
    public async Task CreateBuyOrder_NullBuyOrderRequest()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = null;

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
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
    public async Task CreateBuyOrder_ZeroQuantity()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new(){ Quantity = 0,StockName = "",StockSymbol = ""};

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
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
    public async Task CreateBuyOrder_ExceedingAllowedQuantity()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new() { Quantity = 100001, StockName = "", StockSymbol = "" };

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
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
    public async Task CreateBuyOrder_ZeroPrice()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new() { Price = 0 , StockName = "", StockSymbol = "" };

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
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
    public async Task CreateBuyOrder_ExceedingAllowedPrice()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new()
        {
            Price = 10001,
            StockName = "Microsoft",
            StockSymbol = "MSFT"
        };

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
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
    public async Task CreateBuyOrder_NullSymbol()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new() { StockSymbol = null, StockName = "" };

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
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
    public async Task CreateBuyOrder_OlderOrderDate()
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
        await Assert.ThrowsAsync<ArgumentException>(async () =>
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
    public async Task CreateBuyOrder_ValidData()
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
    public async Task CreateSellOrder_NullArgument()
    {
        //Arrange
        SellOrderRequest? sellOrderRequest = null;

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public async Task CreateSellOrder_ZeroQuantity()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new() {Quantity = 0,StockName = "",StockSymbol = ""};
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }
    
    [Fact]
    public async Task CreateSellOrder_ExceedMaxQuantity()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new() {Quantity = 100001,StockName = "",StockSymbol = ""};
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }
    [Fact]
    public async Task CreateSellOrder_ZeroPrice()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new() {Price = 0,StockName = "",StockSymbol = ""};
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }
    
    [Fact]
    public async Task CreateSellOrder_ExceedMaxPrice()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new() {Price = 10001,StockName = "",StockSymbol = ""};
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }
    
    [Fact]
    public async Task CreateSellOrder_NullStockSymbol()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new() {Price = 1,StockName = "",StockSymbol = null};
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public async Task CreateSellOrder_InvalidDateAndTime()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new() {StockName = "Microsoft",StockSymbol = "MS", DateAndTimeOfOrder = new DateTime(1999,12,31)};
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            //Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });

    }
    
    [Fact]
    public async Task CreateSellOrder_ValidData()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new()
        {
            Price = 1,
            Quantity = 1,
            StockName = "Microsoft",
            StockSymbol = "MSFT",
            DateAndTimeOfOrder = DateTime.Now
        };
        Assert.False(DateTime.Now <  new DateTime(2000,1,1));
        //Act
        var sellOrderResponse = await _stockService.CreateSellOrder(sellOrderRequest);

        //Assert
        Assert.NotEqual(sellOrderResponse.SellOrderID, Guid.Empty );
    }
    #endregion


    #region GetAllBuyOrders

    [Fact]
    public async Task GetAllBuyOrders_ReturnEmptyList()
    {
        //Act
        var list = await _stockService.GetBuyOrders();
        
        //Assert
        Assert.Empty(list);
        
    }
    
    [Fact]
    public async Task GetAllBuyOrders_CheckAddedOrders()
    {
        //Arrange
        List<BuyOrderRequest> buyOrderRequestsList = new()
        {
            new BuyOrderRequest
            {
                StockName = "Microsoft",
                StockSymbol = "MS",
                Quantity = 2,
                Price = 2,
                DateAndTimeOfOrder = DateTime.Now
            },
            new BuyOrderRequest
            {
                StockName = "Google",
                StockSymbol = "Go",
                Quantity = 2,
                Price = 2,
                DateAndTimeOfOrder = DateTime.Now
            },
        };

        List<BuyOrderResponse> buyOrderResponsesList = new();

        foreach (var order in buyOrderRequestsList)
        {
            var buyOrderResponse = await _stockService.CreateBuyOrder(order);
            buyOrderResponsesList.Add(buyOrderResponse);
        }
        
        //Act
        var list = await _stockService.GetBuyOrders();
        
        //Assert
        Assert.NotEmpty(list);
        Assert.Equal(list.Count, buyOrderRequestsList.Count);
        Assert.Equal(list.Count, buyOrderResponsesList.Count);
    }

    #endregion

    #region GetAllSellOrders

    [Fact]
    public async Task GetAllSellOrders_ReturnEmptyList()
    {
        //Act
        var list = await _stockService.GetSellOrders();
        
        //Assert
        Assert.Empty(list);
        
    }

      
    [Fact]
    public async Task GetAllSellOrders_CheckAddedOrders()
    {
        //Arrange
        List<SellOrderRequest> sellOrderRequestsList = new()
        {
            new SellOrderRequest
            {
                StockName = "Microsoft",
                StockSymbol = "MS",
                Quantity = 2,
                Price = 2,
                DateAndTimeOfOrder = DateTime.Now
            },
            new SellOrderRequest
            {
                StockName = "Google",
                StockSymbol = "Go",
                Quantity = 2,
                Price = 2,
                DateAndTimeOfOrder = DateTime.Now
            },
        };

        List<SellOrderResponse> sellOrderResponsesList = new();

        foreach (var order in sellOrderRequestsList)
        {
            var sellOrderResponse = await _stockService.CreateSellOrder(order);
            sellOrderResponsesList.Add(sellOrderResponse);
        }
        
        //Act
        var list = await _stockService.GetSellOrders();
        
        //Assert
        Assert.NotEmpty(list);
        Assert.Equal(list.Count, sellOrderResponsesList.Count);
        Assert.Equal(list.Count, sellOrderRequestsList.Count);
    }

    #endregion
}