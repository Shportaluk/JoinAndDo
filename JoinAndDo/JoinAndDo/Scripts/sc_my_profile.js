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
function EditProfileImg() {
    var formData = new FormData();
    var file = document.getElementById("fileInput").files[0];
    formData.append("fileInput", file);

    $.ajax({
        url: '/JoinAndDo/LoadProfileImg',
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            res = res.trim()
            if (res != "Error" || res != "Error Load" || res != "You are not registered or do not have entrance to the site") {
                $("#form_img_profile img").attr("src", "/Images/" + res)
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function ShowFormAddSkill(){
    $("#strong_skills").css("opacity", "0");
    $("#form_add_skill").css("display", "block")
    $("#form_add_skill").animate({
        opacity: 1
    }, 200)
}
function HideFormAddSkill() {
    $("#strong_skills").css("opacity", "1");
    $("#form_add_skill").animate({
        opacity: 0
    }, 200)

    $("#form_add_skill").css("display", "none")
}
function AddSkill() {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    
    var name = $("#select_skill").val();
    var pathImg = $("#select_skill :selected").data("target");

    $.ajax({
        url: '/JoinAndDo/AddSkillToUser',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, pathImg: pathImg, name: name }),
        success: function (res) {
            if (res == "Ok") {
                var div_SKILL = document.createElement('div');
                var div_IMG = document.createElement('img');
                var div_STRONG = document.createElement('strong');

                div_SKILL.className = "skill";

                div_IMG.src = "/Styles/images/skills/" + pathImg;
                div_STRONG.textContent = name;

                div_SKILL.appendChild(div_IMG);
                div_SKILL.appendChild(div_STRONG);

                $("#list_skills").append(div_SKILL);
                HideFormAddSkill();
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
(function () {
    $(document).ready(function () {
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

        var chat = $.connection.chatHub;
        $.connection.hub.start().done(function () {
            //alert("Open connection");

            var login = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
            var hash = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
            
            chat.server.connect(login);
            //alert();
            $('#btn_send_msg').click(function () {
                var text = $("#form_write_msg #txt_msg").val();
                var to = $("#recipient_name").text();
                chat.server.sendMsgToUser(login, hash, text, to);
                HideFormWriteMsg();
            }); 
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

