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
    var SendData = ko.mapping.toJS(AppViewModel);
    console.log(SendData);
    $.post('/Api/Post', SendData, function (data) {
        console.log(data);
    }, 'json');
}

$("#button_from_server").click(function () {
    GetData();
});

$("#button_to_server").click(function () {
    PostData();
});