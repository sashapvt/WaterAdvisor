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

function PostData() {
    var SendData = ko.mapping.toJSON(AppViewModel);
    $.ajax({ url: '/Api/Post', type: 'post', data: SendData, contentType: 'application/json', success: function (data) { console.log('Post request result: ' + data); } });
}

$("#button_from_server").click(function () {
    GetData();
});

$("#button_to_server").click(function () {
    PostData();
});