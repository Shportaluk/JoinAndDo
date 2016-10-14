function AddMsgToDialog(user, text) {
    $(".sender_name").each(function (i, elem) {
        if ($(elem).text() == user) {
            $(elem).parent().children(".messages").append("<div id='message'><div class='list_message' id='him_message'>" + text + "</div></div>");
            ScrollDown();
        }
    });
}

(function () {
    $(document).ready(function () {
        ScrollDown();

        var chat = $.connection.chatHub;
        chat.client.addMessage = function (name, message) {
            AddMsgToDialog(name, message);
        };
        $.connection.hub.start().done(function () {
            //alert("Open connection");

            var login = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
            var hash = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

            $(".sender_name").each(function (i, elem) {
                chat.server.connectToDialog( login, $(elem).text() );
            });
            
            $(".btn_send_msg").click(function () {
                var text = $(this).parent().children(".txt_msg").val();
                var toUser = $(this).parent().parent().children(".sender_name").text();
                toUser = toUser.replace(/\s+/g, '');

                chat.server.sendMsgToUser(login, hash, text, toUser);


                $(this).parent().children(".txt_msg").val("");
                $(this).parent().parent().children(".messages").append("<div id='message'><div class='list_message' id='my_message'>" + text + "</div></div>")

                ScrollDown();

            })
        });

        $(".messages").click(function () {
            if ($(this).parent().width() != $("#blocks").width()) {
                $(this).parent().animate({
                    width: 100 + "%"
                }, 200);
            }
            else {
                $(this).parent().animate({
                    width: $("#blocks").width()/4 - 6
                }, 200);
            }
            
        })
       
    });
})();

function ScrollDown()
{
    var wtf = $('.messages');
    var height = wtf[0].scrollHeight;
    wtf.scrollTop(height);
}