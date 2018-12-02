function loader() {
    $('.loader').toggleClass('hidden')
}
$('document').ready(() => {
    $('#closeMsg').on('click', toggleInfo);     
});
function toggleInfo() {
    $('.msg').toggleClass('notVisible');     
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

