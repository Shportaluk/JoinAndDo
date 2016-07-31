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