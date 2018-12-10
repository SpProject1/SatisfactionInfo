function loader() {
    $('.loader').toggleClass('hidden')
}
function log(what) {
    console.log(what)
}
$(document).ready(function () {
    let questionCount = parseInt($('#questionCount').val())     
    if (!isNaN(questionCount)) {
        let code = $('#questionnarieCode').val();
        let qArray = $('#questions').val().split(';').filter(Boolean).sort()           
        for (var i = 1; i <= qArray[qArray.length - 1]; i++) {
            let questionNumber = i
            let code = $('#questionnarieCode').val();
            let addWhyBody = $('#addWhyBody_' + String(questionNumber)).val();
            if (addWhyBody === undefined) {
                addWhyBody = null
            }
            let item = {
                Code: code,
                QuestionNumber: questionNumber,
                Answered: '',
                AddWhyBody: ''
            }
            model[questionNumber - 1] = item;             
        }
    }
});

let model = [] 

function updateAddWhyBody(questionNumber) {     
    model[questionNumber - 1].AddWhyBody = $('#addWhyBody_' + String(questionNumber)).val();   
}
function updateSimlpeAnswer(questionNumber, id) {     
    model[questionNumber - 1].Answered = $(id).val();    
    if (model[questionNumber - 1].Answered.length > 0) {
        removeRequired(questionNumber);
    }
} 
function updateAnswer(questionNumber, answerType, answered, obj) {
    model[questionNumber - 1].Answered = calculateAnswered(model[questionNumber - 1].Answered, answered, answerType, obj); 
    if (model[questionNumber - 1].Answered.length > 0) {
        removeRequired(questionNumber);
    }
} 
function calculateAnswered(currentVal, newVal, answerType, obj) { 
    if (answerType === "jednokrotny" || answerType === "numeryczny") {
        currentVal = newVal
    }
    else if (answerType === "wielokrotny") {
        if (obj != null && obj.checked) {
            currentVal += newVal + ';'
        }
        else {
            currentVal = String(currentVal).replace(newVal + ';', '')
        }
    }     
    return currentVal;
}
function validInput() {
    let result = 'Brak odpowiedzi w pytaniach: '  
    let qArray = $('#questions').val().split(';').filter(Boolean).sort()
    for (var i = 0; i < model.length; i++) {
        if (qArray.includes(String(model[i].QuestionNumber)) && (model[i].Answered == null || model[i].Answered == '')) {
            result += model[i].QuestionNumber + '; '
            addRequired(String(model[i].QuestionNumber));
        }
        else {
            removeRequired(String(model[i].QuestionNumber));
        }
    }
    return result = result == 'Brak odpowiedzi w pytaniach: ' ? true : result;        
}
function addRequired(questionNumber) {
    $('#required_' + questionNumber).addClass('required')
}
function removeRequired(questionNumber) {
    $('#required_' + questionNumber).removeClass('required')
}

function sendQuestionnarie() {
    if (validInput() !== true) {
        addInfo(3, validInput());
        return;
    }
    else
        toggleInfo();  
    $('#sendBtn').attr('disabled', 'disabled')
    $('#questionnarieContent').toggleClass('hidden')
    loader()
    $.ajax({
        type: "POST",
        url: '/Home/AddUserQuestionnarie',
        data: {
            model: $.extend({}, model)
        },
        success: function (result) {
            location.href = '/Home/?Type=' + result.info.type + '&Message=' + result.info.message;
            $('#sendBtn').removeAttr('disabled')
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać ankiety. Przepraszamy.');
            $('#sendBtn').removeAttr('disabled')
            loader()
            $('#questionnarieContent').toggleClass('hidden')
        }
    });
} 
