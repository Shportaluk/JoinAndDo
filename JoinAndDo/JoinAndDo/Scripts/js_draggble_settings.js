$(document).ready(function () {
    var leftBoxes = $("#left_boxes");
    var rightBoxes = $("#right_boxes");
    
    
    var boxesLeft_show = $('#boxesLeft_show');
    var boxesLeft_hide = $('#boxesLeft_hide');
    var boxesRight_show = $('#boxesRight_show');
    var boxesRight_hide = $('#boxesRight_hide');
    
    
    boxesRight_show.css("top", rightBoxes.position().top);
    var cookieLogin = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var cookieHash = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");



    SetLocalStorage();
    
    
    if ( cookieLogin != "" && cookieHash != "" ) {
        // if log in
        $("#name").text( cookieLogin );
        //$( "#logout" ).css( "display", "block" );
        //$( "#registration" ).css( "display", "none" );
        //$( "#login" ).css( "display", "none" );
        //localStorage.setItem("leftBoxes_X", "20");
        //if ( GetStatus( rightBoxes ) == "left" ) {
        //    localStorage.setItem("rightBoxes_Y", "190");
        //    localStorage.setItem("rightBoxes_X", "20");
        //}
    } else {
        // if log out
        //$( "#logout" ).css( "display", "none" );
        //$( "#registration" ).css( "display", "block" );
        //$( "#login" ).css( "display", "block" );
    
        boxesLeft_show.css("display", "none");
        localStorage.setItem("leftBoxesStatus", "show");
        localStorage.setItem( "leftBoxes_X", "-500" );
        if( GetStatus( rightBoxes ) == "left" )
        {
            localStorage.setItem("rightBoxes_Y", "20");
            blocks.css("marginRight", "10px");
        }
    }
    
    SetLocalStorage();
    
    setInterval(function () {
        CheckPosition();
    
        if (IsTouchLeft(leftBoxes)) {
            boxesLeft_hide.css("display", "block");
            boxesLeft_hide.mousemove(function () {
                leftBoxes.css("left", "-500px");
                boxesLeft_show.css("display", "block");
                localStorage.setItem("leftBoxesStatus", "hide");
            });
        }
        else {
            boxesLeft_hide.css("display", "none");
            boxesLeft_show.css("display", "none");
            localStorage.setItem("leftBoxesStatus", "show");
        }
    
        if (IsTouchLeft(rightBoxes)) {
            boxesRight_hide.css("display", "block");
            boxesRight_hide.mousemove(function () {
                rightBoxes.css("left", "-500px");
                boxesRight_show.css("display", "block");
                localStorage.setItem("rightBoxesStatus", "hide");
            });
        }
        else {
            boxesRight_hide.css("display", "none");
            boxesRight_show.css("display", "none");
            localStorage.setItem("rightBoxesStatus", "show");
        }
    
    }, 50);


    boxesLeft_show.click(function () {
        ShowBoxes( leftBoxes , $(this));
        localStorage.setItem("leftBoxesStatus", "show");
    })
    boxesRight_show.click(function () {
        ShowBoxes( rightBoxes, $(this));
        localStorage.setItem("rightBoxesStatus", "show");
    })

    leftBoxes.mousemove(function () {
        localStorage.setItem( "leftBoxes_X", $(this).position().left );
        localStorage.setItem( "leftBoxes_Y", $(this).position().top );
    })
    rightBoxes.mousemove(function () {
        localStorage.setItem( "rightBoxes_X", $(this).position().left );
        localStorage.setItem( "rightBoxes_Y", $(this).position().top );
    })

});
function SetLocalStorage()
{
    var statusLeftBloxes = localStorage.getItem("leftBoxesStatus");
    var statusRightBloxes = localStorage.getItem("rightBoxesStatus");
    if (statusLeftBloxes == "hide") {
        $('#left_boxes').css("left", "-500px");
        $('#boxesLeft_show').css("display", "block");
    }
    else if (statusLeftBloxes == "show") {
        $('#left_boxes').css("left", localStorage.getItem("leftBoxes_X") + "px");
        $('#left_boxes').css("top", localStorage.getItem("leftBoxes_Y") + "px");
    }
    if (statusRightBloxes == "hide") {
        $('#right_boxes').css("left", "-500px");
        $('#boxesRight_show').css("display", "block");
    }
    else if (statusLeftBloxes == "show") {
        $('#right_boxes').css("left", localStorage.getItem("rightBoxes_X") + "px");
        $('#right_boxes').css("top", localStorage.getItem("rightBoxes_Y") + "px");
    }
}
function CheckPosition()
{
    var blocks = $('#blocks');
    var statusLeft = GetStatus( $('#left_boxes') );
    var statusRight = GetStatus( $('#right_boxes') );

    if( statusLeft == "hide" && statusRight == "hide" )
    {
        blocks.css("marginLeft", "25px");
        blocks.css("marginRight", "25px");
    }
    else if( statusLeft == "left" && statusRight == "left" )
    {
        blocks.css( "marginLeft", "260px");
        blocks.css( "marginRight", "10px");
    }
    else if (statusLeft == "right" && statusRight == "right") {
        blocks.css("marginLeft", "10px");
        blocks.css("marginRight", "260px");
    }
    else {
        if ( statusRight == "right" || statusLeft == "right" ) {
            blocks.css("marginRight", "260px");
        }
        if (statusRight == "left" || statusLeft == "left") {
            blocks.css("marginLeft", "260px");
        }
        if( statusLeft == "hide" && statusRight == "right" )
        {
            blocks.css("marginLeft", "25px");
        }
        else if(statusRight == "hide" && statusLeft == "right")
        {
            blocks.css( "marginLeft", "25px" );
        }
    }
    
}
function GetStatus(boxes) {
    var boxesPosition = boxes.position();
    if (boxes.css("display") == "none") { return "hide"; }
    if (boxesPosition.left > screen.width - 350) { return "right"; }
    else if (boxesPosition.left < 200 && boxesPosition.left <= 0) { return "left"; }
    else { return "hide" };
}
function IsTouchLeft( boxes )
{
    var boxesPosition = boxes.position();
    if( boxesPosition.left <= 0 )
    { return true; }
    return false;
}
function ShowBoxes( boxes, img )
{
    img.css("display", "none");
    boxes.animate({
        left: "20px"
    }, 275);
}