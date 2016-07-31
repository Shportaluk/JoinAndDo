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

$(document).ready(function () {
    var login = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    $("#info1_join #my_name").text(login);

    var creator = $("#creator_accession").text();
    if (creator == login) {
        $("#user1_invite").css("display", "block");
        $("#div_requests").css("display", "block");
        $("#user1_join").css("display", "none");
    }

    $("#info1_invite").hover(
        function () {
            $(this).css("opacity", "1");
            $(this).css("display", "block");
        }, function () {
            $(this).css("opacity", "0");
            $(this).css("display", "none");
        }
        );
    $(".user").hover(
            function () {
                var id = $(this).attr('id').slice(4, this.length);;
                $("#info" + id).css("opacity", "1");
                $("#info" + id).css("display", "block");

                $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
                $("#user" + id).css("color", "white");
            }, function () {
                var id = $(this).attr('id').slice(4, this.length);;
                $("#info" + id).css("opacity", "0");
                $("#info" + id).css("display", "none");


                $("#user" + id).css("backgroundColor", "white");
                $("#user" + id).css("color", "black");
            }
    );

    $(".information").hover(
        function () {
            $(this).css("opacity", "1");
            $(this).css("display", "block");

            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
            $("#user" + id).css("color", "white");
        }, function () {
            $(this).css("opacity", "0");
            $(this).css("display", "none");

            var id = $(this).attr('id').slice(4, this.length);
            $("#user" + id).css("backgroundColor", "white");
            $("#user" + id).css("color", "black");
        }
    );
});