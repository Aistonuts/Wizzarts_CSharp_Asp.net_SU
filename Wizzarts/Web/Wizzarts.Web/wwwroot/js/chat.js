
    var connection =
        new signalR.HubConnectionBuilder()
            .withUrl("/chat")
            .build();

    connection.on("NewMessage",
        function (message) {
            var chatInfo1 = `<div>[${message.user}] ${escapeHtml(message.text)}</div>`;
            var chatInfo = `<div class="message"><header style="color:black">[${message.user}]:</header><p style="color:black">${escapeHtml(message.text)}</p></div >`
            $("#messagesList").append(chatInfo);
        });

    $("#sendButton").click(function () {
        var message = $("#messageInput").val();
        connection.invoke("Send", message);
        /*$("#messageInput").val("");*/
    });

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    function escapeHtml(unsafe) {
        return unsafe
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    }

