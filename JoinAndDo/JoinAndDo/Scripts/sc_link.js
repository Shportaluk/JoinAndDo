$(document).ready(function () {

	$("#blocks").css( "transform", "perspective(1000px) rotateY(0deg)" );
	$("#blocks").animate({
        opacity: 1
	}, 35);


	$(".btn_Close").click(function () {
	    HideFormLoginOrRegistration();
	});

	$("#login").click(function () {
	    $("#form_registration").css("display", "none");
	    $("#form_login").css("display", "block");
	    $(".black_fon").css("display", "block");
	});

	$("#registration").click(function () {
	    $("#form_login").css("display", "none");
	    $("#form_registration").css("display", "block");
	    $(".black_fon").css("display", "block");
	});

	$("#logout").click(function () {
	    var cookieLogin = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
	    var cookieHash = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

	    //alert(cookieHash)

	    $.ajax({
	        url: 'Logout',
	        type: 'POST',
	        contentType: 'application/json;',
	        data: JSON.stringify({ login: cookieLogin, hash: cookieHash }),
	        success: function () { window.location.href = "index" }
	    });
	});

	$("#btn_Registration").click(function () {
	    var login = $("#form_registration #txt_login")
	    var pass = $("#form_registration #txt_pass")
	    Registration(login.val(), pass.val());
	})

});
function Registration(l, p) {
    $("#registration_error").text("");
    $.ajax({
        url: 'Registration',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, pass: p }),
        success: function (res) {
            if (res == "OK") {
                ShowMessage( "successfully registered" )
                HideFormLoginOrRegistration();
            }
            else {
                $("#registration_error").text(res);
            }
        }
    });
}
function HideFormLoginOrRegistration()
{
    $("#form_login").css("display", "none");
    $("#form_registration").css("display", "none");
    $(".black_fon").css("display", "none");
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
    setTimeout( HideMessage, 5000 );
    
}