const token = document.querySelector("#FinnhubToken").value;
const stockSymbol = document.querySelector("#StockSymbol").value;

const socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

// Connection opened -> Subscribe
socket.addEventListener('open', function (event) {
    console.log("Connected to Finnhub websocket")
    socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stockSymbol }))
});

// Connection error
socket.addEventListener("error", function (event) {
    console.error("Cannot connect to Finnhub Websocket")

})

// Unsubscribe
const unsubscribe = function (symbol) {
    socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': symbol }))
}

//when the page is being closed, unsubscribe from the WebSocket
window.onunload = () => unsubscribe(stockSymbol)


// Listen for messages
socket.addEventListener('message', function (event) {
    console.log(event);
    console.log('Message from server ', event.data);

    //TODO: update prices according to the data
    // I will be test it soon as they claimed market is closed saturday.



});