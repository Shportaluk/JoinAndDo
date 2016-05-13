$(document).ready(function () {

	$("#blocks").css( "transform", "perspective(1000px) rotateY(0deg)" );
	$("#blocks").animate({
        opacity: 1
	}, 35);

	$("#btn_Close").click(function () {
	    $(".black_fon").css("display", "none");
	});
	$("#login").click(function () {
	    $(".black_fon").css("display", "block");
	});
});