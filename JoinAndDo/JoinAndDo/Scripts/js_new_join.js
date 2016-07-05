function NewJoin(  )
{
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    var name = $("#name_join").val();
    var text = $("#txt_join").val();
    var category = $("#category_select").val();
    var needPeople = $("#needPeople").val();
    alert(name + ":" + text + ":" + category + ":" + needPeople);

    $.ajax({
        url: '/JoinAndDo/NewJoin',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, name: name, text: text, category: category, needPeople: needPeople }),
        success: function (res) {

        }
    });
}