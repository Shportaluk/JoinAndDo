function ShowFormWriteMsg() {
    $("#form_write_msg").css("z-index", "999");
    $("#form_write_msg").animate({ opacity: 1 }, 200);
}

function HideFormWriteMsg()
{
    $("#form_write_msg").css("z-index", "-1");
    $("#form_write_msg").animate({ opacity: 0 }, 200);

    var text = $("#form_write_msg #txt_msg").val("");
}

function SendMsg()
{
    var text = $("#form_write_msg #txt_msg").val();
    var to = $("#recipient_name").text();

    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        url: '/JoinAndDo/SendMsg',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, to: to, text: text }),
        success: function (res) {
            handleData(res);
        }
    });

    HideFormWriteMsg();
}
(function () {
    $(document).ready(function () {
        // Write Msg
        // Close Form
        // Send Msg
        var url = window.location.pathname; 
        var splitted = url.split('/');
        var id = splitted[splitted.length - 1];

        var cookieId = document.cookie.replace(/(?:(?:^|.*;\s*)id\s*\=\s*([^;]*).*$)|^.*$/, "$1");

        if (id != cookieId) {
            var button_SendMsg = document.createElement('button');
            button_SendMsg.id = "btn_WriteMsg";
            button_SendMsg.onclick = ShowFormWriteMsg;
            button_SendMsg.textContent = "Write Message";

            $("#information").append(button_SendMsg);//<button id="btn_WriteMsg" onclick="">Write Message</button>
        }


        // Like 
        $("#like_1").click(function () {
            if (getName(this) == "like.png") {
                $(this).css('background-image', 'url(' + "/Styles/images/like_.png" + ')');
            }
            else {
                $(this).css('background-image', 'url(' + "/Styles/images/like.png" + ')');
            }
            $("#dislike_1").css('background-image', 'url(' + "/Styles/images/dislike.png" + ')');
        });


        $("#like_2").click(function () {
            if (getName(this) == "like.png") {
                $(this).css('background-image', 'url(' + "/Styles/images/like_.png" + ')');
            }
            else {
                $(this).css('background-image', 'url(' + "/Styles/images/like.png" + ')');
            }
            $("#dislike_2").css('background-image', 'url(' + "/Styles/images/dislike.png" + ')');
        });



        $("#like_3").click(function () {
            if (getName(this) == "like.png") {
                $(this).css('background-image', 'url(' + "/Styles/images/like_.png" + ')');
            }
            else {
                $(this).css('background-image', 'url(' + "/Styles/images/like.png" + ')');
            }
            $("#dislike_3").css('background-image', 'url(' + "/Styles/images/dislike.png" + ')');
        });


        // Dislike

        $("#dislike_1").click(function () {
            if (getName(this) == "dislike.png") {
                $(this).css('background-image', 'url(' + "/Styles/images/dislike_.png" + ')');
            }
            else {
                $(this).css('background-image', 'url(' + "/Styles/images/dislike.png" + ')');
            }
            $("#like_1").css('background-image', 'url(' + "/Styles/images/like.png" + ')');
        });


        $("#dislike_2").click(function () {
            if (getName(this) == "dislike.png") {
                $(this).css('background-image', 'url(' + "/Styles/images/dislike_.png" + ')');
            }
            else {
                $(this).css('background-image', 'url(' + "/Styles/images/dislike.png" + ')');
            }
            $("#like_2").css('background-image', 'url(' + "/Styles/images/like.png" + ')');
        });



        $("#dislike_3").click(function () {
            if (getName(this) == "dislike.png") {
                $(this).css('background-image', 'url(' + "/Styles/images/dislike_.png" + ')');
            }
            else {
                $(this).css('background-image', 'url(' + "/Styles/images/dislike.png" + ')');
            }
            $("#like_3").css('background-image', 'url(' + "/Styles/images/like.png" + ')');
        });

        var getName = function (o) {
            var bg = $(o).css('background-image');
            bg = bg.replace('url(', '').replace(')', '');
            bg = bg.replace("\"", "");
            bg = bg.replace("\"", "");
            splitted = bg.split('/');
            return splitted[splitted.length - 1];
        }
    });
})();

