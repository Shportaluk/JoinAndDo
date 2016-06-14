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
    page2.css( "top", heightPage );
    page3.css( "top", heightPage*2 );
    page4.css( "top", heightPage * 3);

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
        if( e.deltaY > 0 )
        {
            //alert("Down");
            if (st >= 0 && st < heightPage - 1) {
                $("#header").css( "position", "fixed" );
                $("#header").css( "top", "5px" );
                btn2.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage }, 100); }
            else if (st >= heightPage - 1 && st < heightPage) {
                btn3.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage*2 }, 100); }
            else if (st >= heightPage * 2 - 1 && st < heightPage * 3) {
                btn4.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage * 3 }, 100); }
        }
        else
        {
            //alert("Up");
            if (st >= heightPage * 2 && st < heightPage * 3) {
                btn3.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage*2 - 1 }, 100); }
            else if (st >= heightPage && st < heightPage * 2) {
                btn2.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: heightPage-1 }, 100); }
            else if (st >= 0 && st < heightPage) {
                $("#header").css("position", "absolute");
                $("#header").css("top", heightPage - 53);
                btn1.css("border", "1px solid white");
                $('body,html').animate({ scrollTop: 0 }, 100); }
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

        if ( top > -1 && top < 1 ) {
            $("#header").css("position", "absolute");
            $("#header").css("top", heightPage - 53);

            btn1.css("border", "1px solid white");
        } else {
            switch (target)
            {
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
});