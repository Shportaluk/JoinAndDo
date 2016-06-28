$(document).ready(function () {
    var P1_go_to = -100;
    var P2_go_to = -0;
    var P3_go_to = 100;
    setInterval(function () {
        var p1 = $("#running_text p:nth-child(1)");
        var p2 = $("#running_text p:nth-child(2)");
        var p3 = $("#running_text p:nth-child(3)");

        p1.animate({
            top: P1_go_to
        }, 500);
        p2.animate({
            top: P2_go_to
        }, 500);
        p3.animate({
            top: P3_go_to
        }, 500);


        P1_go_to -= 100;
        P2_go_to -= 100;
        P3_go_to -= 100;


        if (P1_go_to == -200) {
            p1.animate({
                top: 200
            }, 0);
            P1_go_to = 100;
        }
        else if (P2_go_to == -200) {
            //alert("s");
            p2.animate({
                top: 200
            }, 0);
            P2_go_to = 100;
        }
        else if (P3_go_to == -200) {
            p3.animate({
                top: 200
            }, 0);
            P3_go_to = 100;
        }
    }, 2500)

    var page1 = $("#page1");
    var page2 = $("#page2");
    var page3 = $("#page3");
    var page4 = $("#page4");

    var btn1 = $("#header button[data-target=#page1]");
    var btn2 = $("#header button[data-target=#page2]");
    var btn3 = $("#header button[data-target=#page3]");
    var btn4 = $("#header button[data-target=#page4]");

    var heightPage = page1.height();
    page2.css("top", heightPage);
    page3.css("top", heightPage * 2);
    page4.css("top", heightPage * 3);

    setTimeout(function () { $("#right_man_div #think").css("display", "block") }, 5000);
    setTimeout(function () { $("#right_man_div #think").css("display", "none") }, 15000);

    // Scroll page
    btn1.css("border", "1px solid white");
    document.addEventListener('wheel', function (e) {
        btn1.css("border", "1px solid transparent");
        btn2.css("border", "1px solid transparent");
        btn3.css("border", "1px solid transparent");
        btn4.css("border", "1px solid transparent");

        var st = $(this).scrollTop();
        //alert(st + " " + heightPage)
        if (e.deltaY > 0) {
            //alert("Down");
            if (st == 0) {//&& st < heightPage - 1) {
                //alert("About");
                $("#header").css("position", "fixed");
                $("#header").css("top", "5px");
                btn2.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage }, 100);
            }
            else if (st >= heightPage - 1 && st <= heightPage + 1) {//&& st < heightPage*2 - 1 ) {
                //alert("Test");
                btn3.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage * 2 }, 100);
            }
            else if (st >= heightPage * 2 - 1 && st < heightPage * 2 + 1) {
                btn4.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage * 3 }, 100);
            }
        }
        else {
            //alert("Up");
            if (st >= heightPage * 3 - 1 && st < heightPage * 3 + 1) {
                btn3.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage * 2 }, 100);
            }
            else if (st >= heightPage * 2 - 1 && st < heightPage * 2 + 1) {
                btn2.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage }, 100);
            }
            else if (st >= heightPage - 1 && st < heightPage + 1) {
                $("#header").css("position", "absolute");
                $("#header").css("top", heightPage - 53);
                btn1.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: 0 }, 100);
            }
        }
    });
    $("#header button").click(function () {
        var target = $(this).attr("data-target");
        var top = $(target).offset().top;
        //alert(top);

        btn1.css("border", "1px solid transparent");
        btn2.css("border", "1px solid transparent");
        btn3.css("border", "1px solid transparent");
        btn4.css("border", "1px solid transparent");

        if (top > -1 && top < 1) {
            $("#header").css("position", "absolute");
            $("#header").css("top", heightPage - 53);

            btn1.css("border", "1px solid white");
        } else {
            switch (target) {
                case "#page2":
                    btn2.css("border", "1px solid white");
                    break;
                case "#page3":
                    btn3.css("border", "1px solid white");
                    break;
                case "#page4":
                    btn4.css("border", "1px solid white");
                    break;
            }
            $("#header").css("position", "fixed");
            $("#header").css("top", "5px");
        }

        $('body,html').animate({ scrollTop: top }, 500);
    })
    $(window).resize(function () {
        var page1 = $("#page1");
        var page2 = $("#page2");
        var page3 = $("#page3");
        var page4 = $("#page4");

        var heightPage = page1.height();
        page2.css("top", heightPage);
        page3.css("top", heightPage * 2);
        page4.css("top", heightPage * 3);
    });

    // Join NOW
    $("#join_center_div").click(function () {
        $("#black_fon").css("z-index", "997");
        $("#black_fon").animate({
            opacity: 1
        }, 300)

        $("#login_or_registration").css("display", "block");
        $("#login_or_registration").animate({
            opacity: 1
        }, 300);
    });

    // Swap
    var logOrRegistr = $("#login_or_registration");
    var spanRegistr = $("#login_or_registration span[data-target='registration']");
    var spanLogin = $("#login_or_registration span[data-target='login']");

    spanRegistr.click(function () {
        logOrRegistr.animate({
            height: 330
        }, 150);
        Swap(spanRegistr, spanLogin);
        $("#form_registration").css("opacity", "1")
        $("#form_login").css("opacity", "0")
    })
    spanLogin.click(function () {
        logOrRegistr.animate({
            height: 230
        }, 150);
        Swap(spanLogin, spanRegistr);
        $("#form_registration").css("opacity", "0")
        $("#form_login").css("opacity", "1")
    })

    // CLose
    $(".btn_Close").click(function () {
        Close();
    })

    // Click
    $("#btn_Login").click(function () {
        var login = $("#form_login #txt_login")
        var pass = $("#form_login #txt_pass")
        Login(login.val(), pass.val());

    })

    $("#btn_Registration").click(function () {
        var login = $("#form_registration #txt_login")
        var pass = $("#form_registration #txt_pass")
        var firstName = $("#form_registration #txt_first_name")
        var lastName = $("#form_registration #txt_last_name")
        Registration(login.val(), pass.val(), firstName.val(), lastName.val());
    })

});

function Swap(spanClick, span) {
    spanClick.css("border", "1px solid black");
    spanClick.css("box-shadow", "0 0 10px black");

    span.css("border", "1px solid transparent");
    span.css("box-shadow", "0 0 10px transparent");
};
function Login(l, p) {
    $("#login_error").text("");
    $.ajax({
        url: 'Login',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, pass: p }),
        success: function (res) {
            if (res == "OK") {
                var NestId = $(this).data('id');
                var url = "/JoinAndDo/my_profile?NestId=" + NestId;
                window.location.href = url;
            }
            else {
                $("#login_error").text(res);
            }
        }
    });
};
function Registration(l, p, fn, ln) {
    $("#registration_error").text("");
    $.ajax({
        url: 'Registration',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, pass: p, firstName: fn, lastName: ln }),
        success: function (res) {
            if (res == "OK") {
                ShowMessage("successfully registered")
                Close();
            }
            else {
                $("#registration_error").text(res);
            }
        }
    });
};
function Close()
{
    $("#black_fon").css("z-index", "0");
    $("#black_fon").animate({
        opacity: 0
    }, 300)
    $("#login_or_registration").css("display", "none");
    $("#login_or_registration").animate({
        opacity: 0
    }, 300);
}
function HideMessage() {
    var top_message = $(".top_message");
    top_message.animate({
        opacity: 0,
    }, 1750);
    setTimeout(function () {
        top_message.css("top", "-150px");
        top_message.css("width", "100px");
        top_message.css("fontSize", "0px");
    }, 1750);
}
function ShowMessage(msg) {
    $(".top_message #text").text(msg);
    $(".top_message").animate({
        opacity: 1,
        top: "10px",
        width: "400px",
        marginLeft: "-200px",
        fontSize: "25px"
    }, 750);
    setTimeout(HideMessage, 3000);
}