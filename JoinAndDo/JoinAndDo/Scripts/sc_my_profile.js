(function () {
    $(document).ready(function () {
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