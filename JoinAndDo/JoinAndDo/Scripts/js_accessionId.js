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
        success: function (res) {  }
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
    }

    AddHover($("#info1_invite"));
    AddHover($("#info1_delete_join"));
    
    $(".user").hover(
            function () {
                var id = $(this).attr('id').slice(4, this.length);

                $("#info" + id).css("opacity", "1");
                $("#info" + id).css("display", "block");

                $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
            }, function () {
                var id = $(this).attr('id').slice(4, this.length);
                $("#info" + id).css("opacity", "0");
                $("#info" + id).css("display", "none");


                $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");
            }
    );

    $(".information").hover(
        function () {
            $(this).css("opacity", "1");
            $(this).css("display", "block");

            var id = $(this).attr('id').slice(4, this.length);
            //alert(id)
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
        }, function () {
            $(this).css("opacity", "0");
            $(this).css("display", "none");

            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.5)");
        }
    );
});