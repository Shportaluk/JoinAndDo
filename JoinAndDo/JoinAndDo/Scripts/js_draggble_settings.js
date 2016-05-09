$(document).ready(function () {
    var blocks = $('#blocks');
    var isShowLeftBoxes = true;
    var isShowRightBoxes = true;

    setInterval(function ()
    {
        if (isShowLeftBoxes == false) {
            $('#boxesLeft_show').css("display", "block");
        }
        else {
            $('#boxesLeft_hide').css("display", "none");
            if (IsEmptyRight()) {
                blocks.css("margin", "0 10px 0 260px");
            }
        }

        if (isShowRightBoxes == false) {
            $('#boxesRight_show').css("display", "block");
        }
        else {
            $('#boxesRight_hide').css("display", "none");
            if (IsEmptyRight()) {
                blocks.css("margin", "0 10px 0 260px");
            }
        }

        if (isShowLeftBoxes == false && isShowRightBoxes == false)
        { blocks.css("margin", "0 10px 0 10px"); }
    }, 750);

    $('#boxesLeft_show').click(function () {
        isShowLeftBoxes = true;
        $('#boxesLeft_show').css("display", "none");
        $('#left_boxes').css("left", "0px");
    })

    $('#boxesRight_show').click(function () {
        isShowRightBoxes = true;
        $('#boxesRight_show').css("display", "none");
        $('#right_boxes').css("left", "0px");
    })

    $('#left_boxes').mousemove(function () {
        if( IsEmptyRight() ) {
            blocks.css("marginRight", "10px")
            blocks.css("marginLeft", "260px")
        } else if (IsEmptyLeft()) {
            blocks.css("marginRight", "260px")
            blocks.css("marginLeft", "10px")
        }
        else { blocks.css("margin", "0 260px 0 260px") }


        if (IsTouchLeft($(this))) {
            $('#boxesLeft_hide').css("display", "block");
            $('#boxesLeft_hide').mousemove(function () {
                $('#left_boxes').css("left", "-500px");
                $('#left_boxes').css("z-index", "0");
                isShowLeftBoxes = false;
            });
        }
        else {
            $('#boxesLeft_hide').css("display", "none");
        }
    })
    $('#right_boxes').mousemove(function () {
        if (IsEmptyRight()) {
            blocks.css("marginRight", "10px")
            blocks.css("marginLeft", "260px")
        } else if (IsEmptyLeft()) {
            blocks.css("marginRight", "260px")
            blocks.css("marginLeft", "10px")
        } else { blocks.css("margin", "0 260px 0 260px") }



        if (IsTouchLeft($(this))) {
            $('#boxesRight_hide').css("display", "block");
            $('#boxesRight_hide').mouseup(function () {
                $('#right_boxes').css("left", "-500px");
                $('#right_boxes').css("z-index", "0");
                isShowRightBoxes = false;
            });
        }
        else {
            $('#boxesRight_hide').css("display", "none");
        }
    })


});

function IsEmptyRight() {
    var leftBoxes = $('#left_boxes');
    var rightBoxes = $('#right_boxes');

    var leftBoxesPosition = leftBoxes.position();
    var rightBoxesPosition = rightBoxes.position();

    if (leftBoxesPosition.left < 100 && rightBoxesPosition.left < 100 ) { return true; }
    return false;
}
function IsEmptyLeft() {
    var leftBoxes = $('#left_boxes');
    var rightBoxes = $('#right_boxes');

    var leftBoxesPosition = leftBoxes.position();
    var rightBoxesPosition = rightBoxes.position();

    if (leftBoxesPosition.left > screen.width - 350 && rightBoxesPosition.left > screen.width - 350) { return true; }
    return false;
}
function IsTouchLeft( boxes )
{
    var boxesPosition = boxes.position();
    if( boxesPosition.left <= -10 )
    { return true; }
    return false;
}
