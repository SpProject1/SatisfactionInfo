$('document').ready(() => {
    $('#closeMsg').on('click', toggleInfo);     
});
function toggleInfo() {
    $('.msg').toggleClass('notVisible');     
}