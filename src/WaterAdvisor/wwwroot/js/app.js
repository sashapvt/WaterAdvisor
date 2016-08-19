var Id = document.getElementById('Id').value;
var AppViewModel;

GetData();

function GetData() {
    $.get('/Api/Get', { id: Id }, function (data) {
        if (AppViewModel == null) {
            AppViewModel = ko.mapping.fromJS(data);
            ko.applyBindings(AppViewModel);
        }
        else {
            ko.mapping.fromJS(data, AppViewModel);
        }
    }, 'json');
}

// TODO: Розібрати помилку сервера у прийнятті даних. Додати обробку помилок з сервера? Взяти інший приклад даних? Підняти питання безпеки.
function PostData() {
    $.post('/Api/Post', ko.mapping.toJS(AppViewModel), function (data) {
        alert(data);
    }, 'json');
}

$("#button_from_server").click(function () {
    GetData();
});

$("#button_to_server").click(function () {
    PostData();
});