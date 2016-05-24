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

    var heightPage = page1.height();
    page2.css( "top", heightPage );
    page3.css( "top", heightPage*2 );
    page4.css("top", heightPage * 3);

    setTimeout(function () { $("#right_man_div #think").css("display", "block") }, 5000);
    setTimeout(function () { $("#right_man_div #think").css("display", "none") }, 15000);

    // if scroll win than header position = fixed
    var tmpScrollTop = -1;
    setInterval(function () {
        var scrollTop = $('body').scrollTop();
        if (scrollTop != tmpScrollTop) {
            tmpScrollTop = scrollTop;
            if( scrollTop >= page1.height() - 55 )
            {
                $( "#header" ).css( "position", "fixed" );
                $( "#header" ).css( "top", "0px" );
            }
            else
            {
                $("#header").css("position", "absolute");
                $("#header").css("top", page1.height() - 55 + "px");
            }
        }
    });


    var btn1 = $("#header button[data-target=#page1]");
    var btn2 = $("#header button[data-target=#page2]");
    var btn3 = $("#header button[data-target=#page3]");
    var btn4 = $("#header button[data-target=#page4]");
    
    var onPage;
    
    var tmpScrollTop2 = -1;
    setInterval(function () {
        var scrollTop = $('body').scrollTop();
        if (scrollTop != tmpScrollTop2) {
    
            tmpScrollTop2 = scrollTop;
            var heightPage = page1.height();
    
            if (scrollTop >= 0 && scrollTop < heightPage) { onPage = btn1; }
            else if (scrollTop >= heightPage && scrollTop < heightPage * 2) { onPage = btn2; }
            else if (scrollTop >= heightPage * 2 && scrollTop < heightPage * 3) { onPage = btn3; }
            else if (scrollTop >= heightPage * 3 && scrollTop < heightPage * 4) { onPage = btn4; }
    
            btn1.css("color", "white");
            btn2.css("color", "white");
            btn3.css("color", "white");
            btn4.css("color", "white");
            
            //btn1.hover(function () { btn1.css("backgroundColor", "red"); })
            //btn2.css("backgroundColor", "transparent");
            //btn3.css("backgroundColor", "transparent");
            //btn4.css("backgroundColor", "transparent");
            //background-color: #FFBF00;
            
            onPage.css("color", "red")
        }
    }, 200);

    $("#header button").click(function () {
        var target = $(this).attr("data-target");
        var top = $(target).offset().top;
        //alert(top);
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