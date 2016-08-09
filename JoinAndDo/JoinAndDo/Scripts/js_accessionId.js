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
        }, function () {
            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");
            $(this).css("opacity", "0");
            $(this).css("display", "none");
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
            },
            function () {
                var id = $(this).attr('id').slice(4, this.length);
                $("#info" + id).css("opacity", "0");
                $("#info" + id).css("display", "none");


                $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");
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
            if (res == "Ok")
            {
                var div_USER = document.createElement('div');
                div_USER.className = "user";
                div_USER.id = "user1_" + $(".user").length + 1;
                div_USER.textContent = user;
                $("#users_and_information #users").append(div_USER);

                var div_INFORMATION = document.createElement('div');
                var div_SPECIALITY = document.createElement('div');
                div_INFORMATION.className = "information";
                div_SPECIALITY.className = "specialty";
                div_SPECIALITY.textContent = role;
                div_INFORMATION.id = "info1_" + div_USER.id.slice(6, this.length);

                div_INFORMATION.appendChild(div_SPECIALITY);
                $("#users_and_information #div_informations").append(div_INFORMATION);

                // REMOVE
                $("#div_requests #users div").remove(':contains("' + user + '")');
                AddHoversToAllUsers();
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
        },
        function () {
            $(this).css("opacity", "0");
            $(this).css("display", "none");

            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");
        }
    );
});