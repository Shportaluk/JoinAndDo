function OpenUser(id) {
    window.location.href = "/JoinAndDo/peopleId/" + id;
}
function SendRequestToAccession() {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    var category = $( "#info1_join select" ).val();
    var text = $( "#info1_join #text" ).val();
    var idAccession = $("#id_accession").text();

    //alert( l + ":" + category + ":" + text + ":" + idAccession )
    $.ajax({
        url: '/JoinAndDo/SendRequestToAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, text: text, category: category, idAccession: idAccession }),
        success: function (res) {
            if( res != "Ok" ) {
                $("#info1_join #error").css( "opacity", "1" );
                $("#info1_join #error").text( res );
            }
            else {
                ShowMessage("successfully sent");
                $("#info1_join #error").css("opacity", "0");
                $("#info1_join #error").text("");
            }
        }
    });
}
function AddHover( div ) {
    div.hover(
        function () {
            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
            $(this).css("opacity", "1");
            $(this).css("display", "block");
            $("#messages").css("opacity", "0.2")
        }, function () {
            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");
            $(this).css("opacity", "0");
            $(this).css("display", "none");
            $("#messages").css("opacity", "1")
        }
    );
}
function AddHoversToAllUsers() {
    $(".user").hover(
            function () {
                var id = $(this).attr('id').slice(4, this.length);
                var top = $(this).offset().top;
                $("#info" + id).css("top", top - 60 + "px" );
                $("#info" + id).css("opacity", "1");
                $("#info" + id).css("display", "block");

                $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
                $("#messages").css("opacity", "0.2")
            },
            function () {
                var id = $(this).attr('id').slice(4, this.length);
                $("#info" + id).css("opacity", "0");
                $("#info" + id).css("display", "none");


                $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");

                $("#messages").css("opacity", "1")
            }
    );
}
function DeleteJoin() {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var idAccession = $("#id_accession").text();

    $.ajax({
        url: '/JoinAndDo/DeleteJoin',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, idAccession: idAccession }),
        success: function (res) {
            if (res == "Ok") {
                window.location.href = "/JoinAndDo/search_accession/";
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function AcceptUser(id) {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var idAccession = $("#id_accession").text();
    var role = $("#info2_" + id + " .specialty").text().replace(/^\s+/, "").replace(/\s+$/, "");
    var user = $("#info2_" + id + " #name").text().replace(/^\s+/, "").replace(/\s+$/, "");

    $.ajax({
        url: '/JoinAndDo/AcceptRequestOfUserToAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, user: user, role: role, idAccession: idAccession }),
        success: function (res) {
            if (res == "Ok") {
                $("#users_and_information #users div").remove(':contains("' + role + '")');

                var div_RemoveUser = document.createElement('div');

                div_RemoveUser.className = "remove_user";
                div_RemoveUser.onclick = function () { RemoveUser(user); };


                $("#info2_" + id).remove();
                $("#user2_" + id).append(div_RemoveUser);
                $("#user2_" + id).appendTo("#users_and_information #users");


                if ($(".need_people").length <= 0) {
                    $("#users_and_information #users p").remove(':contains("Need people")');
                }
                if ($("#div_requests .user").length <= 0) {
                    $("#div_requests p").remove();
                }
               
                AddHoversToAllUsers();
                ShowMessage( "User " + user + " was added" );
            }
            else {
                ShowMessage( res );
            }
        }
    });
}
function RejectUser(id) {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var idAccession = $("#id_accession").text();
    var user = $("#info2_" + id + " #name").text().replace(/^\s+/, "").replace(/\s+$/, "");

    $.ajax({
        url: '/JoinAndDo/RejectRequestOfUserToAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, user: user, idAccession: idAccession }),
        success: function (res) {
            if (res == "Ok") {
                $("#div_requests .user").remove(':contains("'+user+'")');

                if ($(".need_people").length <= 0) {
                    $("#users_and_information #users p").remove(':contains("Need people")');
                }
                if ($("#div_requests .user").length <= 0) {
                    $("#div_requests p").remove();
                }
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function SendMessageToAccession() {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var text = $("#txt_msg").val();
    var idAccession = $("#id_accession").text();

    $.ajax({
        url: '/JoinAndDo/SendMsgToAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, idAccession: idAccession, text: text }),
        success: function (res) {
            if (res == "Ok") {
                alert("Ok");
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function SendRequestCompleteAccession() {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var idAccession = $("#id_accession").text();

    $.ajax({
        url: '/JoinAndDo/SendRequestCompleteAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, idAccession: idAccession }),
        success: function (res) {
            if (res == "Ok") {
                window.location.href = "/JoinAndDo/my_accession/";
            }
            else if (res == "Complated") {
                window.location.href = "/JoinAndDo/my_accession/";
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function EditDescription(text) {
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var idAccession = $("#id_accession").text();

    $.ajax({
        url: '/JoinAndDo/EditDescriptionOfAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, idAccession: idAccession, description: text }),
        success: function (res) {
            if (res == "Ok") {
                $("#description strong").text(text);
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function EditTitle(text)
{
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var idAccession = $("#id_accession").text();

    $.ajax({
        url: '/JoinAndDo/EditTitleOfAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, idAccession: idAccession, title: text }),
        success: function (res) {
            if (res == "Ok") {
                $("#title strong").text(text);
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function RemoveUser(user)
{
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var idAccession = $("#id_accession").text();

    $.ajax({
        url: '/JoinAndDo/RemoveUserFromAccession',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, idAccession: idAccession, user: user }),
        success: function (res) {
            if (res == "Ok") {
                $("#users_and_information #users div").remove(':contains("'+user+'")');
            }
            else {
                ShowMessage(res);
            }
        }
    });
}
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
function AddMsgToAccession(name, text)
{
    var div_MSG = document.createElement('div');
    var MSG = document.createElement('div');
    var p = document.createElement('p');
    var strong_Name = document.createElement('strong');
    var strong_Text = document.createElement('strong');

    div_MSG.className = "div_message";
    MSG.className = "message";
    strong_Name.className = "name_sender";
    strong_Text.className = "text";

    strong_Name.textContent = name;
    strong_Text.textContent = text;

    p.appendChild(strong_Name);
    p.appendChild(strong_Text);

    MSG.appendChild(p);
    div_MSG.appendChild(MSG);
    $("#list_messages").append(div_MSG);

    ScrollDown();
}
function ScrollDown() {
    var wtf = $('#list_messages');
    var height = wtf[0].scrollHeight;
    wtf.scrollTop(height);
}
$(document).ready(function () {
    var login = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var hash = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    $("#info1_join #my_name").text(login);

    var creator = $("#creator_accession").text();
    if (login == "") {
        $("#user1_join").css("display", "none");
        $("#users_and_information #users p").remove(':contains("Management")'); 
        $("#users_and_information #users p").remove(':contains("Customers")');
    }
    else if (creator == login) {
        $("#user1_delete_join").css("display", "block");
        $("#div_requests").css("display", "block");
        $("#user1_join").css("display", "none");
        $("#user1_exit_from_the_accession").css("display", "none");

        $(".edit").css("display", "block");
        $("#title .edit").click(function () {
            var title = $("#title strong").text();
            $("#title_edit").text(title);
            $("#title_edit").css("display", "block");
            $("#title strong").css("opacity", "0");
            $(this).css("display", "none");
            $("#title .edit_ok").css("display", "block");
        });
        $("#title .edit_ok").click(function () {
            $("#title_edit").css("display", "none");
            $("#title strong").css("opacity", "1");
            $(this).css("display", "none");
            $("#title .edit").css("display", "block");
            var title = $("#title_edit").val();
            EditTitle(title);
        });
        $("#description .edit").click(function () {
            var description = $("#description strong").text();
            $("#description_edit").text(description);
            $("#description strong").css("opacity", "0");
            $("#description_edit").css("display", "block");
            $(this).css("display", "none");
            $("#description .edit_ok").css("display", "block");
        });
        $("#description .edit_ok").click(function () {
            var text = $("#description_edit").val();
            $("#description strong").css("opacity", "1");
            $("#description_edit").css("display", "none");
            $(this).css("display", "none");
            $("#description .edit").css("display", "block");

            EditDescription( text );
        });
    }
    else {
        $("#user1_delete_join").css("display", "block");
        $("#user1_exit_from_the_accession").click(function() {
            var idAccession = $("#id_accession").text();

            $.ajax({
                url: '/JoinAndDo/ExitWithAccession',
                type: 'POST',
                contentType: 'application/json;',
                data: JSON.stringify({ login: login, hash: hash, idAccession: idAccession }),
                success: function (res) {
                    if (res == "Ok") {
                        location.reload();
                    }
                    else {
                        ShowMessage(res);
                    }
                }
            });
        })
    }

    AddHover($("#info1_invite"));
    AddHover($("#info1_delete_join"));
    
    
    AddHoversToAllUsers();

    $(".information").hover(
        function () {
            $(this).css("opacity", "1");
            $(this).css("display", "block");

            var id = $(this).attr('id').slice(4, this.length);
            //alert(id)
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
            $("#messages").css("opacity", "0.2")
        },
        function () {
            $(this).css("opacity", "0");
            $(this).css("display", "none");

            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");
            $("#messages").css("opacity", "1")
        }
    );




    var chat = $.connection.chatHub;
    chat.client.addMessageToAccession = function (name, message) {
        AddMsgToAccession(name, message);
    };
    $.connection.hub.start().done(function () {
        //alert("Open connection");

        var login = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
        var hash = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");
        var idAccession = $("#id_accession").text();
        chat.server.connectToAccession(login, idAccession);
        //alert();
        $('#btn_send_msg_to_accession').click(function () {
            chat.server.sendMsgToAccession(login, hash, $('#txt_msg').val(), idAccession);
            $("#txt_msg").val("");
        });
    });

});
