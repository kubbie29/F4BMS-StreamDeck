document.addEventListener('websocketCreate', function () {
    console.log("Websocket created!");
    showHideSettings(actionInfo.payload.settings);

    websocket.addEventListener('message', function (event) {
        console.log("Got message event!");

        // Received message from Stream Deck
        var jsonObj = JSON.parse(event.data);

        if (jsonObj.event === 'sendToPropertyInspector') {
            var payload = jsonObj.payload;
            checkShowHide();
        }
        else if (jsonObj.event === 'didReceiveSettings') {
            var payload = jsonObj.payload;
            checkShowHide();
        }
    });
});


function checkShowHide() {
    elem = document.getElementById("cmdsDataType");

    document.getElementById('chaffLowTextArea').style.display = 'none';

    if (elem.value == "cl") {
        document.getElementById('chaffLowTextArea').style.display = 'flex';
    } 

}
checkShowHide();