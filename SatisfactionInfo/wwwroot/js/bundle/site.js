let toDelete = -1; //Jakieś Id do usunięcia .

function clearFilter() {
    $('#filterName').val(null)
    $('#filterCode').val(null)
    $('#filterDate').val(null)
    $('#filterDescription').val(null)
    hide('#buttonUpdateFilter') 
    updateFilter();
}
function showUpdateFilter() {
    let name = $('#filterName').val()
    let code = $('#filterCode').val()
    let date = $('#filterDate').val()
    let description = $('#filterDescription').val()
    if (name.length > 0 || code.length > 0 || date.length > 0 || description.length > 0) {       
        show('#buttonUpdateFilter')
    }
}
function togglechecked(id) {
    if ($(id).is(':checked')) {
        $(id).prop('checked', false);
    } else {
        $(id).prop('checked', true);
    }
}
function loader() {
    $('.loader').toggleClass('hidden')
}
function log(what) {
    console.log(what)
}
function hide(id) {
    let element = $(id)
    if (!element.hasClass('hidden')) {
        element.addClass('hidden')
    }
}
function show(id) {
    let element = $(id)
    if (element.hasClass('hidden')) {
        element.removeClass('hidden')
    }
}
function change(from, to) {
    hide(from);
    show(to);
}
function showPopup(id, popupId) {
    toDelete = id;
    show(popupId)
}
function hidePopup() {
    $('.confirmation').each(function () {
        if (!$(this).hasClass('hidden')) {
            $(this).addClass("hidden");
        }
    });
    toDelete = -1;
}
function toggleRow(id, senderId) {

    let element = $(id)
    let sender = $(senderId)
    if (sender.hasClass('fa-angle-down')) {
        sender.removeClass('fa-angle-down')
        sender.addClass('fa-angle-up')
        element.removeClass('hidden')
    }
    else {
        sender.removeClass('fa-angle-up')
        sender.addClass('fa-angle-down')
        element.addClass('hidden')
    }
    // element.toggleClass('hidden')
}
$(document).ready(function () {
    let questionCount = parseInt($('#questionCount').val())
    if (!isNaN(questionCount)) {
        let code = $('#questionnarieCode').val();
        let qArray = $('#questions').val().split(';').filter(Boolean)
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
function updateTempAnswer(questionNumber, answerType, answered, obj) {
    model[questionNumber - 1].Answered = calculateAnswered(model[questionNumber - 1].Answered, answered, answerType, obj);
    if (model[questionNumber - 1].Answered.length > 0) {
        removeRequired(questionNumber);
    }    
}
function calculateAnswered(currentVal, newVal, answerType, obj) {
    if (answerType.toLowerCase() === "jednokrotny" || answerType.toLowerCase() === "numeryczny") {
        currentVal = newVal
    }
    else if (answerType.toLowerCase() === "wielokrotny") {
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
        window.scrollTo(0, 0);
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
            item: $.extend({}, model)
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

function addQuestion() {
    $('#addBtn').attr('disabled', 'disabled')
    let data = {
        Question: $('#newQuestionQuestion').val(),
        AddWhyName: $('#newQuestionAddWhyName').val(),
        AddWhy: $('#newQuestionAddWhy').is(':checked'),
        AnswerType: $('#newQuestionAnswerType').val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Questions/AddOrUpdate',
        data: {
            item: data
        },
        success: function (result) {
            $('#questionsTable').html(result)
            loader()
            addInfo(2, 'Dodano.');
            $('#addBtn').removeAttr('disabled')
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#addBtn').removeAttr('disabled')
            loader()
            $('#addNewQuestion').toggleClass('hidden')
        }
    });
}
function updateQuestion(id) {
    $('#editBtn').attr('disabled', 'disabled')
    let data = {
        ID: id,
        Question: $('#editQuestionQuestion_' + String(id)).val(),
        AddWhyName: $('#editQuestionAddWhyName_' + String(id)).val(),
        AddWhy: $('#editQuestionAddWhy_' + String(id)).is(':checked'),
        AnswerType: $('#editQuestionAnswerType_' + String(id)).val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Questions/AddOrUpdate',
        data: {
            item: data
        },
        success: function (result) {
            $('#questionsTable').html(result)
            loader()
            addInfo(2, 'Zaktualizowano.');
            $('#editBtn').removeAttr('disabled')
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#editBtn').removeAttr('disabled')
            loader()
            $('#addNewQuestion').toggleClass('hidden')
        }
    });
}
function deleteQuestion() {
    loader()
    $.ajax({
        type: "Delete",
        url: '/Questions/Delete',
        data: {
            id: toDelete
        },
        success: function (result) {
            if (result == 'success') {
                $('#row_' + String(toDelete)).remove();
                hidePopup()
                loader()
                addInfo(2, 'Usunięto.');
            }
            else {
                hidePopup()
                loader()
                addInfo(3, result);
            }

        },
        error: function (e) {
            addInfo(3, 'Nie udało się usunąć.');
            loader()
            hidePopup()
        }
    });

}

//Answers

function addAnswer() {
    $('#addBtn').attr('disabled', 'disabled')
    let data = {
        Answer: $('#newAnswerAnswer').val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Answers/AddOrUpdate',
        data: {
            item: data
        },
        success: function (result) {
            $('#answersTable').html(result)
            loader()
            addInfo(2, 'Dodano.');
            $('#addBtn').removeAttr('disabled')
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#addBtn').removeAttr('disabled')
            loader()
            $('#addNewAnswer').toggleClass('hidden')
        }
    });
}
function updateAnswer(id) {
    $('#editBtn').attr('disabled', 'disabled')
    let data = {
        ID: id,
        Answer: $('#editAnswerAnswer_' + String(id)).val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Answers/AddOrUpdate',
        data: {
            item: data
        },
        success: function (result) {
            $('#answersTable').html(result)
            loader()
            addInfo(2, 'Zaktualizowano.');
            $('#editBtn').removeAttr('disabled')
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#editBtn').removeAttr('disabled')
            loader()
            $('#addNewAnswer').toggleClass('hidden')
        }
    });
}
function deleteAnswer() {
    loader()
    $.ajax({
        type: "Delete",
        url: '/Answers/Delete',
        data: {
            id: toDelete
        },
        success: function (result) {
            if (result == 'success') {
                $('#row_' + String(toDelete)).remove();
                hidePopup()
                loader()
                addInfo(2, 'Usunięto.');
            }
            else {
                hidePopup()
                loader()
                addInfo(3, 'Nie uało sie usunąć.');
            }

        },
        error: function (e) {
            addInfo(3, 'Nie udało się usunąć.');
            loader()
            hidePopup()
        }
    });
}
function addQuestionAnswer(id) {
    $('#addQABtn_' + String(id)).attr('disabled', 'disabled')
    let data = {
        questionId: id,
        answerId: $('#questionAnswer_' + String(id)).val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Questions/AddQuestionAnswer',
        data: {
            item: data
        },
        success: function (result) {
            if (result == 'exists') {
                addInfo(1, 'Taka odpowiedź już istnieje w pytaniu.');
            }
            else {
                $('#qaTable_' + String(id)).html(result)
                addInfo(2, 'Dodano.');
            }
            loader()
            $('#addQABtn_' + String(id)).removeAttr('disabled')
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#addQABtn_' + String(id)).removeAttr('disabled')
            loader()
            $('#addNewQuestionAnswer_' + String(id)).toggleClass('hidden')
        }
    });
}
function deleteQuestionAnswer() {
    loader()
    let row = $('#rowDetail_' + String(toDelete[0]) + '_' + String(toDelete[1]));
    $.ajax({
        type: "Delete",
        url: '/Questions/DeleteQuestionAnswer',
        data: {
            QuestionId: toDelete[0],
            AnswerId: toDelete[1]
        },
        success: function (result) {
            if (result == 'success') {
                row.remove();
                hidePopup()
                loader()
                addInfo(2, 'Usunięto.');
            }
            else {
                loader()
                addInfo(3, 'Nie uało sie usunąć.');
            }

        },
        error: function (e) {
            addInfo(3, 'Nie udało się usunąć.');
            loader()
            hidePopup()
        }
    });
}
function addQuestionnarie() {
    $('#addBtn').attr('disabled', 'disabled')
    let data = {
        Name: $('#newQuestionnarieName').val(),
        MaxAnswers: $('#newQuestionnarieMaxAnswers').val(),
        Description: $('#newQuestionnarieDescription').val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Questionnaries/AddOrUpdate',
        data: {
            item: data
        },
        success: function (result) {
            if (result != 'Wypełnij wszystkie wymagane pola') {
                $('#questionnariesTable').html(result)
                addInfo(2, 'Dodano.');
            } 
            else {
                addInfo(3, result);
            }
            loader()             
            $('#addBtn').removeAttr('disabled')
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#addBtn').removeAttr('disabled')
            loader()
            $('#addNewQuestionnarie').toggleClass('hidden')
        }
    });
}
function cloneQuestionnarie(id) {  
    loader()
    $.ajax({
        type: "POST",
        url: '/Questionnaries/Clone',
        data: {
            id: id
        },
        success: function (result) {             
            if (result != 'Wypełnij wszystkie wymagane pola') {
                $('#questionnariesTable').html(result)               
                addInfo(2, 'Kopia wykonana.');
            }
            else {
                addInfo(3, result);
            }
            loader() 
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');        
            loader()            
        }
    });
}
function updateQuestionnarie(id) {
    $('#editBtn').attr('disabled', 'disabled')
    let data = {
        ID: id,
        Name: $('#editQuestionnarieName_' + String(id)).val(),
        Code: $('#editQuestionnarieCode_' + String(id)).val(),
        Active: $('#editQuestionnarieActive_' + String(id)).is(':checked'),
        MaxAnswers: $('#editQuestionnarieMaxAnswers_' + String(id)).val(),
        Description: $('#editQuestionnarieDescription_' + String(id)).val(),
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Questionnaries/AddOrUpdate',
        data: {
            item: data
        },
        success: function (result) {
           
            if (result != 'Wypełnij wszystkie wymagane pola') {
                $('#questionnariesTable').html(result)               
                addInfo(2, 'Zaktualizowano.');
                $('#editBtn').removeAttr('disabled')
            }
            else {
                addInfo(3, result);
            }
            loader() 
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#editBtn').removeAttr('disabled')
            loader()            
        }
    });
}
function deleteQuestionnarie() {
    loader()
    $.ajax({
        type: "Delete",
        url: '/Questionnaries/Delete',
        data: {
            id: toDelete
        },
        success: function (result) {
            if (result == 'success') {
                $('#row_' + String(toDelete)).remove();
                $('#row_questions_' + String(toDelete)).remove();;
                hidePopup()
                loader()
                addInfo(2, 'Usunięto.');
            }
            else {
                hidePopup()
                loader()
                addInfo(3, result);
            }
        },
        error: function (e) {
            addInfo(3, 'Nie udało się usunąć.');
            loader()
            hidePopup()
        }
    });
}

function addQuestionnarieQuestion(id) {
    $('#addQQBtn' + String(id)).attr('disabled', 'disabled')
    let data = {
        questionnarieId: id,
        questionId: $('#questionnarieQuestionQuestionId_' + String(id)).val(),
        questionNumber: $('#questionnarieQuestionQuestionNumber_' + String(id)).val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Questionnaries/AddOrUpdateQuestionnarieQuestion',
        data: {
            item: data
        },
        success: function (result) {     
           
            if (result != 'Wypełnij wszystkie wymagane pola' && result != 'Pytanie istnieje w ankiecie') {
                $('#qqTable_' + String(id)).html(result)
                addInfo(2, 'Dodano.');              
                $('#addQQBtn' + String(id)).removeAttr('disabled')
            }
            else {
                addInfo(3, result);
            }
            loader() 
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#addQQBtn' + String(id)).removeAttr('disabled')
            loader()
            $('#addNewQuestion_' + String(id)).toggleClass('hidden')
        }
    });
}
function updateQuestionnarieQuestion(id, questionId) {
    $('#editBtn' + String(id)).attr('disabled', 'disabled')
    let data = {
        questionnarieId: id,
        questionId: $('#editQuestionQuestionId_' + String(id) + '_' + String(questionId)).val(),
        questionNumber: $('#editQuestionQuestionNumber_' + String(id) + '_'+ String(questionId)).val()
    }
    loader()
    $.ajax({
        type: "POST",
        url: '/Questionnaries/AddOrUpdateQuestionnarieQuestion',
        data: {
            item: data
        },
        success: function (result) {
          
            if (result != 'Wypełnij wszystkie wymagane pola') {
                $('#qqTable_' + String(id)).html(result)
                addInfo(2, 'Dodano.');            
                $('#editBtn' + String(id)).removeAttr('disabled')
            }
            else {
                addInfo(3, result);
            }
            loader() 
        },
        error: function (e) {
            addInfo(3, 'Nie udało się zapisać.');
            $('#editBtn' + String(id)).removeAttr('disabled')
            loader()
            $('#addNewQuestion_' + String(id)).toggleClass('hidden')
        }
    });
}
function deleteQuestionnarieQuestion() {
    loader()
    let row = $('#rowDetail_' + String(toDelete[0]) + '_' + String(toDelete[1]));
    $.ajax({
        type: "Delete",
        url: '/Questionnaries/DeleteQuestionnarieQuestion',
        data: {
            questionnarieId: toDelete[0],
            questionId: toDelete[1]
        },
        success: function (result) {
            if (result == 'success') {
                row.remove();
                hidePopup()
                loader()
                addInfo(2, 'Usunięto.');
            }
            else {
                loader()
                addInfo(3, 'Nie uało sie usunąć.');
            }

        },
        error: function (e) {
            addInfo(3, 'Nie udało się usunąć.');
            loader()
            hidePopup()
        }
    });
}
function updateFilter() {
    let name = $('#filterName').val();
    let code = $('#filterCode').val();
    let date = $('#filterDate').val();
    let description = $('#filterDescription').val();
    loader()
    $.ajax({
        type: "GET",
        url: '/UserQuestionnaries/GetFiltered',
        data: {
            name: name,
            code: code,
            date: date,
            description: description
        },
        success: function (result) {
            if (result != 'Problem z pobraniem danych') {
                $('#questionnaries').html(result)                
            }
            else {
                addInfo(3, result);
            }
            loader()
        },
        error: function (e) {
            addInfo(3, 'Nie udało się pobrać danych.');           
            loader()           
        }
    });  
}
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
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
