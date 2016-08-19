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


$("#button_from_server").click(function () {
    GetData();
});