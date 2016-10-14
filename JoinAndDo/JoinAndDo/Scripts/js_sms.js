$(document).ready(function () {
    //var countNewMsg = document.cookie.replace(/(?:(?:^|.*;\s*)new_msg\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    //$("#count_msg").text("+" + countNewMsg);

    var chat = $.connection.chatHub;
    chat.client.addMessage = function (name, message) {
        var div_SMS = document.createElement('div');
        var div_NameSender = document.createElement('p');
        var div_Text = document.createElement('p');

        div_SMS.className = "sms";
        div_SMS.onclick = function () { window.location.href = '/JoinAndDo/my_message'; }
        div_NameSender.className = "name_sender";
        div_Text.className = "text";

        div_NameSender.textContent = name;
        div_Text.textContent = message;

        div_SMS.appendChild(div_NameSender);
        div_SMS.appendChild(div_Text);

        $("#array_sms").append(div_SMS);
        document.getElementById('new_msg_sound').play()


        //var count_msg = document.cookie.replace(/(?:(?:^|.*;\s*)new_msg\s*\=\s*([^;]*).*$)|^.*$/, "$1");
        //count_msg = count_msg.replace(/[^-0-9]/gim, '');
        //count_msg++;
        //$("#count_msg").text("+" + count_msg);
        //document.cookie = "new_msg=" + count_msg + ";";

        setTimeout(function () {
            $(div_SMS).animate({ opacity: 0 }, 1500);
        }, 2500)
    };
    $.connection.hub.start().done(function () {
        //alert("Open connection");

        var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
        chat.server.connect(l);
    });
});