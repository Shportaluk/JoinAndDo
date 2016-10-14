function CheckSms(handleData) {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        url: '/JoinAndDo/CheckSms',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h }),
        success: function (res) {
            handleData(res);
        }
    });
};

function GetLastMessages(handleData) {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        url: '/JoinAndDo/GetLastMessages',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h }),
        success: function (res) {
            handleData(res)
        }
    });
}

function AddMsgToDialog( res )
{
    var name;
    var text;
    for (var i = 1; i < res.length; i++) {
        if (res[i] == ":") {
            name = res.substr(0, i);
            text = res.substr(i + 1);
    
            i = res.length;
        }
    }
    $('.dialog .sender_name:contains("' + name + '")').parent().children(".messages").append("<div id='message'><div class='list_message' id='him_message'>" + text + "</div></div>");
    ScrollDown();
    document.getElementById('new_msg_in_dialog_sound').play()
}

function ScrollDown() {
    var wtf = $('.messages');
    var height = wtf[0].scrollHeight;
    wtf.scrollTop(height);
}

$(document).ready(function () {
    var count;
    var n;
    CheckSms(function (output) {
        n = output;
    });
    setInterval(function () {
        //alert("1- " + n + " " + count);
        CheckSms(function (output) {
            count = output;
            //alert("2- " + n + " " + count);
            if (n != count) {
                //alert("New Msg");
                // NEW MESSAGES !!!
                n = count;
                GetLastMessages(function (output) {
                    AddMsgToDialog(output);
                });
            }
        });
    }, 100);
});
