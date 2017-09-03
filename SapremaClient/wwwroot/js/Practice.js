$(document).ready(function () {
    setupPageUser();
    //setupPageGroup()
});

var urlMVC = "http://localhost:5002/Yoga/";
var urlAPI = "http://localhost:5001/api/";
var classAudio;

function setupPageUser() {
    var url = urlMVC + "GetUserClasses";

    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        displayUserClasses(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
    });
}

//function setupPageGroup() {
//    var url = urlMVC + "GetSubbedClasses";

//    $.ajax({
//        type: "GET",
//        url: url,
//        dataType: 'json',
//        contentType: "application/json;charset=utf-8",
//    }).done(function (data) {
//        displayUserClasses(data);
//    }).error(function (jqXHR, textStatus, errorThrown) {
//        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
//    });
//}

function displayUserClasses(data) {
    var listItem = "";
    var classList = "";

    for (var i = 0; i < data.length; i++) {
        var buttonClick = "'" + data[i].ClassId + "'";

        listItem = '<div class="row"><div class="col-xs-12 col-sm-12 col-md-7 col-lg-7">' + data[i].ClassName +
            '<br />Theme:' + data[i].ClassTheme + '<br />Level:' + data[i].ClassLevel +
            '</div><div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 store-rating">' +
            '</div><div class="col-xs-12 col-sm-12 col-md-2 col-lg-2 text-center store-details"><button class="btn btn-default button-medium" onclick="playClass(' + buttonClick + ')"><i class="fa fa-info" aria-hidden="true"></i> Play</button>' +
            '</div></div><hr />';
        classList = classList + listItem;
    }
    $('#userclasses').html(classList);
}

function playClass(data) {
    var url = urlMVC + "GetClassPoses";
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        startClass(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
    });
}