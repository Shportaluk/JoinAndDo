$(document).ready(function () {
    var blocks = $('#blocks');
    $('#left_boxes').mousemove(function () {
        if( IsEmptyRight() ) {
            blocks.css("marginRight", "0")
            blocks.css("marginLeft", "260px")
        } else if (IsEmptyLeft()) {
            blocks.css("marginRight", "260px")
            blocks.css("marginLeft", "0")
        }
        else { blocks.css("margin", "0 260px 0 260px") }
    })
    $('#right_boxes').mousemove(function () {
        if (IsEmptyRight()) {
            blocks.css("marginRight", "0")
            blocks.css("marginLeft", "260px")
        } else if (IsEmptyLeft()) {
            blocks.css("marginRight", "260px")
            blocks.css("marginLeft", "0")
        }
        else { blocks.css("margin", "0 260px 0 260px") }
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
