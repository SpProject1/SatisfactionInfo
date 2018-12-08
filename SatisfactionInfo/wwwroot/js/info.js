$('document').ready(() => {
    $('#closeMsg').on('click', toggleInfo);     
});
function toggleInfo() {
    $('.info').toggleClass('notVisible');  
   
}
function addInfo(type, message) {
    let messageType = ''
    switch (type) {
        case 1:
            messageType = 'info_msg'
            break;
        case 2:
            messageType = 'success_msg'
            break;
        case 3:
            messageType = 'danger_msg'
            break;
        default:
    }
    $('.info').html('');    
    $('.info').html('<div class="msg ' + messageType + '"><div id="closeMsgJs" class="right fa fa-times-circle"></div>' + message + '</div>');
    $('.info').removeClass('notVisible');
    $('#closeMsgJs').on('click', toggleInfo);
}