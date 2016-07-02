function CheckSms(handleData) {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        url: 'CheckSms',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h }),
        success: function (res) {
            handleData(res);
        }
    });
};

function ShowMsg( res )
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

    var div_SMS = document.createElement('div');
    var div_NameSender = document.createElement('p');
    var div_Text = document.createElement('p');

    div_SMS.className = "sms";
    div_NameSender.className = "name_sender";
    div_Text.className = "text";

    div_NameSender.textContent = name;
    div_Text.textContent = text;

    div_SMS.appendChild(div_NameSender);
    div_SMS.appendChild(div_Text);

    $("#array_sms").append(div_SMS);
    document.getElementById('new_msg_sound').play()

    setTimeout(function () {
        $(div_SMS).animate({ opacity: 0 }, 1500);
    }, 2500)
}

function GetLastMessages(handleData)
{
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        url: 'GetLastMessages',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h }),
        success: function (res) {
            handleData(res)
        }
    });
}
$(document).ready(function () {
    //ShowMsg("SDS:SD1111");
    //setTimeout(function () {
    //    ShowMsg("SDS:SD2222");
    //}, 1500)

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
            if( n != count )
            {
                //alert("New Msg");
                // NEW MESSAGES !!!
                n = count;
                GetLastMessages(function (output) {
                    ShowMsg(output);
                });
            }
        });
    }, 100);

});
