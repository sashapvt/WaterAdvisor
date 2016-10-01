var Id = document.getElementById('Id').value;
var AppViewModel;

GetData();

function GetData() {
    $.ajax({
        url: '/Api/Get/' + Id,
        cache: false,
        success: function (data) {
            console.log('Get request done');
            if (AppViewModel == null) {
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
        url: '/Api/Post', type: 'post', data: SendData, contentType: 'application/json; charset=utf-8', success: function (data) {
            console.log('Post request done');
            GetData();
        }
    });
}

function PostDataPartial(changedValueObject) {
    $.ajax({
        url: '/Api/PostPartial', type: 'post', data: JSON.stringify(changedValueObject), contentType: 'application/json; charset=utf-8', success: function (data) {
            console.log('Post request done');
            GetData();
        }
    });
}

$(':input').change(function (eventObject) {
    var changedValueObject = new Object();
    changedValueObject.ProjectId = Id;
    changedValueObject.Name = eventObject.target.name;
    changedValueObject.Value = eventObject.target.value;
    if (changedValueObject.Name == "PasteROSA") return;
    if (changedValueObject.Name == "P.pHCorrection" && changedValueObject.Value == AppViewModel.P.pHCorrection()) return;
    PostDataPartial(changedValueObject);
});

$('button#buttonBalanceAnalysis').click(function () {
    var SumIonsBalance = AppViewModel.WaterIn.SumIonsBalance();
    var Na = AppViewModel.WaterIn.Na.ValueMEq();
    var Cl = AppViewModel.WaterIn.Cl.ValueMEq();
    if (SumIonsBalance != 0) {
        if (SumIonsBalance > 0) {
            AppViewModel.WaterIn.Cl.ValueMEq(Cl + SumIonsBalance);
            $("input#WaterIn_Cl_ValueMEq").trigger("change");
        }
        else {
            AppViewModel.WaterIn.Na.ValueMEq(Na + Math.abs(SumIonsBalance));
            $("input#WaterIn_Na_ValueMEq").trigger("change");
        }
    }
});

$('button#buttonPasteROSA').click(function () {
    var textROSA = $('textarea#PasteROSA').val();
    if (textROSA.length > 0) {
        var changedValueObject = new Object();
        changedValueObject.ProjectId = Id;
        changedValueObject.Name = "PasteROSA";
        changedValueObject.Value = textROSA;
        PostDataPartial(changedValueObject);
        $('input#PasteROSA').val('');
    }
});
