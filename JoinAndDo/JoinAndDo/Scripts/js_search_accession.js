function SearchAccession() {
    var text = $('input[name="name"]').val();
    window.location.href = "/JoinAndDo/search_accession?text=" + text;
}

function OpenAccession(id)
{
    window.location.href = "/JoinAndDo/AccessionId/" + id;
}