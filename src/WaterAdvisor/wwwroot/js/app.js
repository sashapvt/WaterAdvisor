var Id = document.getElementById('Id').value;
var AppViewModel;

GetData();

function GetData() {
    $.ajax({
        url: '/Api/Get/'+ Id,
        cache: false, 
        success: function (data) {
            console.log('Get request done');
            console.log(data);
            if (AppViewModel === null) {
                AppViewModel = ko.mapping.fromJS(data);
                ko.applyBindings(AppViewModel);
            }
            else {
                ko.mapping.fromJS(data, AppViewModel);
            }
        },
        contentType: 'application/json'
    });
}

function PostData() {
    var SendData = ko.mapping.toJSON(AppViewModel);
    $.ajax({
        url: '/Api/Post', type: 'post', data: SendData, contentType: 'application/json', success: function (data) {
            console.log('Post request result: ' + data);
            GetData();
        }
    });
}

$("#button_from_server").click(function () {
    GetData();
});

$("#button_to_server").click(function () {
    PostData();
});

$(':input').change(function () {
    PostData();
});
