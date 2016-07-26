function OpenUser(id) {
    window.location.href = "/JoinAndDo/peopleId/" + id;
}

$(document).ready(function () {
    $(".user").hover(
            function () {
                var id = $(this).attr('id').slice(5, this.length);;
                $("#info_" + id).css("opacity", "1");
                $("#info_" + id).css("display", "block");


                $("#user_" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
                $("#user_" + id).css("color", "white");
            }, function () {
                var id = $(this).attr('id').slice(5, this.length);;
                $("#info_" + id).css("opacity", "0");
                $("#info_" + id).css("display", "none");


                $("#user_" + id).css("backgroundColor", "white");
                $("#user_" + id).css("color", "black");
            }
    );

    $(".information").hover(
        function () {
            $(this).css("opacity", "1");
            $(this).css("display", "block");

            var id = $(this).attr('id').slice(5, this.length);
            $("#user_" + id).css("backgroundColor", "rgba(0,0,0,0.8)");
            $("#user_" + id).css("color", "white");
        }, function () {
            $(this).css("opacity", "0");
            $(this).css("display", "none");

            var id = $(this).attr('id').slice(5, this.length);
            $("#user_" + id).css("backgroundColor", "white");
            $("#user_" + id).css("color", "black");
        }
    );
});