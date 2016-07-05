function SendMsg( text, to ) {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        url: '/JoinAndDo/SendMsg',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, to: to, text: text }),
        success: function (res) {
            
        }
    });
}

(function () {
    $(document).ready(function () {

        ScrollDown();

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

        $(".btn_send_msg").click(function () {
            var text = $(this).parent().children(".txt_msg").val();
            var to = $(this).parent().parent().children(".sender_name").text();
            to = to.replace(/\s+/g, '');
            
            SendMsg(text, to);
            
            
            $(this).parent().children(".txt_msg").val("");
            $(this).parent().parent().children(".messages").append("<div class='list_message' id='my_message'>" + text + "</div>")
            ScrollDown();

        })
    });
})();

function ScrollDown()
{
    var wtf = $('.messages');
    var height = wtf[0].scrollHeight;
    wtf.scrollTop(height);
}