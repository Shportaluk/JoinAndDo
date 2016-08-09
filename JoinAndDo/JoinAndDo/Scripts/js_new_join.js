function NewJoin(  )
{
    var l = document.cookie.replace(/(?:(?:^|.*;\s*)login\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var h = document.cookie.replace(/(?:(?:^|.*;\s*)hash\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    var name = $("#name_join").val();
    var text = $("#txt_join").val();
    var category = $("#category_select").val();
    var needPeople = 0;
    var listRoles = "";
    $(".name_role").each(function (i, elem) {
        needPeople++;
        listRoles += $(elem).val() + ",";
    })
    //alert(listRoles);
    //alert(name + ":" + text + ":" + category + ":" + needPeople);

    $.ajax({
        url: '/JoinAndDo/NewJoin',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ login: l, hash: h, name: name, text: text, category: category, needPeople: needPeople, listRoles: listRoles }),
        success: function (res) {
            if (res != "Error")
            {
                window.location.href = "/JoinAndDo/AccessionId/" + res;
            }
        }
    });
}
function AddFuncDeleteRole(div) {
    div.click(function () {
        $(this).parent().remove();
    })
}
function AddNewRole() {
    div_Role = document.createElement('div');
    input_NameRole = document.createElement('input');
    div_DeleteRole = document.createElement('div');
    div_Role.className = "role";
    input_NameRole.className = "name_role";
    div_DeleteRole.className = "delete_role";
    AddFuncDeleteRole($(div_DeleteRole));

    div_Role.appendChild(input_NameRole);
    div_Role.appendChild(div_DeleteRole);
    

    $("#list_roles").append(div_Role);
}

$(document).ready(function () {
    $(".delete_role").click(function () {
        $(this).parent().remove();
    })
});