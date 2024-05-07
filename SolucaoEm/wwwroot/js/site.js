
function updateSearchType() {
    var selectedOption = document.querySelector('input[name="searchOptions"]:checked').value;
    document.getElementById('searchType').value = selectedOption;
}
function handleInput(e) {
    var ss = e.target.selectionStart;
    var se = e.target.selectionEnd;
    e.target.value = e.target.value.toUpperCase();
    e.target.selectionStart = ss;
    e.target.selectionEnd = se;
}


