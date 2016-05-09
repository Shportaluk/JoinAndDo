$(document).ready(function () {
    $('#top-image').mousemove(function (e) {
        var width = screen.width / 2;
        var height = screen.height / 2;
    
        var pageX = e.pageX;
        var pageY = e.pageY;
    
        var newvalueX = width + pageX * -1;
        var newvalueY = height + pageY * -1;
    
        newvalueX = newvalueX * 0.07 - width * 0.07;
        newvalueY = newvalueY * 0.05 - height * 0.05;
        $('#top-image').css("background-position", newvalueX + "px     " + newvalueY + "px");
        //alert( "end" );
    });
});