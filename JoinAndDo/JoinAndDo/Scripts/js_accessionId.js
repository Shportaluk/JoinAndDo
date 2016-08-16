﻿function OpenUser(id) {
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
            if (res == "Ok")  {
                window.location.href = "/JoinAndDo/my_accession/";
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
                $("#user2_" + id).appendTo("#users_and_information #users");
                if ($(".need_people").length <= 0) {
                    $("#users_and_information #users p").remove(':contains("Need people")');
                }
                if ($("#div_requests .user").length <= 0) {
                    $("#div_requests p").remove();
                }
               
                AddHoversToAllUsers();
            }
            else {
                ShowMessage( res );
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
$(document).ready(function () {
    
    var login = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    $("#info1_join #my_name").text(login);

    var creator = $("#creator_accession").text();
    if (creator == login) {
        $("#user1_invite").css("display", "block");
        $("#user1_delete_join").css("display", "block");
        $("#div_requests").css("display", "block");
        $("#user1_join").css("display", "none");

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
});