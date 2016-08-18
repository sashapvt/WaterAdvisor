// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function ___AppViewModel() {
    this.Id = ko.observable(document.getElementById('Id').value);
    this.ProjectName = ko.observable();
    this.ProjectComment = ko.observable();
    this.Date = ko.observable();
    this.Project = ko.observable();
    var data;
    $.get('/Api/Get', { id: this.Id }, function (data) { alert(data.projectName) }, 'json');
    //alert(data);
    //this.ProjectName(this.Project.ProjectName);
}

function GetData() {
    var Id = document.getElementById('Id').value;
    var result;
    $.get('/Api/Get', { id: Id }, result = function (data) { alert(data.projectName); return data; }, 'json');
    console.log(result());
    //alert(result);
    //return data;
}

var AppViewModel;
var Id = document.getElementById('Id').value;
$.get('/Api/Get', { id: Id }, function (data) { AppViewModel = ko.mapping.fromJS(data); ko.applyBindings(AppViewModel); alert(AppViewModel()); }, 'json');
//data = GetData();

var viewModel;

// result could look like this: "{ "personId": 1, "firstName": "Marco", "lastName": "Franssen", "age": 26, "webpage": "http://marcofranssen.nl", "twitter": "@marcofranssen" }"$.ajax({
$.ajax({
    url: '/Api/Get/' + Id,
    type: 'GET',
    dataType: 'JSON',
    success: function (result) {
        var data = JSON.parse(result);
        viewModel = ko.mapping.fromJS(data);
        ko.applyBindings(viewModel);
        alert(viewModel);
    },
    error: function (result) {
        //handle the error, left for brevity
    }
});

//var viewModel = ko.mapping.fromJS(data);

// Activates knockout.js
//ko.applyBindings(new AppViewModel());