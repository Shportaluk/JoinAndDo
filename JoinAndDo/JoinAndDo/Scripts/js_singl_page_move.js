$(document).ready(function () {
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

    //var btn1 = $("#header button[data-target=#page1]");
    //var btn2 = $("#header button[data-target=#page2]");
    //var btn3 = $("#header button[data-target=#page3]");
    //var btn4 = $("#header button[data-target=#page4]");
    //
    //var onPage;
    //
    //var tmpScrollTop = -1;
    //setInterval(function () {
    //    var scrollTop = $('body').scrollTop();
    //    if (scrollTop != tmpScrollTop) {
    //
    //        //alert(scrollTop + " " + tmpScrollTop);
    //        tmpScrollTop = scrollTop;
    //        var heightPage = page1.height();
    //
    //        if (scrollTop >= 0 && scrollTop < heightPage) { onPage = btn1; }
    //        else if (scrollTop >= heightPage && scrollTop < heightPage * 2) { onPage = btn2; }
    //        else if (scrollTop >= heightPage * 2 && scrollTop < heightPage * 3) { onPage = btn3; }
    //        else if (scrollTop >= heightPage * 3 && scrollTop < heightPage * 4) { onPage = btn4; }
    //
    //        //btn1.css("backgroundColor", "transparent");
    //        //btn2.css("backgroundColor", "transparent");
    //        //btn3.css("backgroundColor", "transparent");
    //        //btn4.css("backgroundColor", "transparent");
    //        //
    //        //btn1.hover(function () { btn1.css("backgroundColor", "red"); })
    //        ////btn2.css("backgroundColor", "transparent");
    //        ////btn3.css("backgroundColor", "transparent");
    //        ////btn4.css("backgroundColor", "transparent");
    //        ////background-color: #FFBF00;
    //        //
    //        //onPage.css("backgroundColor", "#A4A4A4")
    //    }
    //}, 200);

    //alert(heightPage);
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