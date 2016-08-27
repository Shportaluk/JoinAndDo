$(document).ready(function () {

});

function show_AccessionsManagement() {
    $("#btn_AccessionsManagement").css("backgroundColor", "white");
    $("#btn_AccessionsManagement").css("color", "black");

    $("#btn_Accessions").css("backgroundColor", "rgba(0,0,0,.7)");
    $("#btn_Accessions").css("color", "white");

    $("#list_Accessions").css("display", "none");
    $("#list_AccessionsManagement").css("display", "block");
}
function show_Accessions() {
    $("#btn_AccessionsManagement").css("backgroundColor", "rgba(0,0,0,.7)");
    $("#btn_AccessionsManagement").css("color", "white");

    $("#btn_Accessions").css("backgroundColor", "white");
    $("#btn_Accessions").css("color", "black");

    $("#list_Accessions").css("display", "block");
    $("#list_AccessionsManagement").css("display", "none");
}