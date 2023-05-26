
var connectionChat = new SignalR.HubConnectionBuilder().withUrl("/hubs/chat").build();
document.getElementById("sendMessage").disabled = true;

connectionChat.on("MessageReceived", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} - ${message}`;
});

document.getElementById("sendMessage").addEventListener("click", function (event) {
    var sender = document.getElementById("senderEmail").value;
    var message = document.getElementById("chatMessage").value;
    var receiver = document.getElementById("receiverEmail").value;

    //send message to specific user
    connectionChat.send("SendMessageToReceiver", sender, receiver, message).catch(function (err) {
        return console.error(err.toString());
    });

    //send message to all users 
    connectionChat.send("SendMessageToAll", sender, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});

connectionChat.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
});