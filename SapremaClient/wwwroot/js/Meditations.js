$(document).ready(function () {
    setupPage();
});

var url = "http://localhost:5002/Meditation/";
var urlAPI = "http://localhost:5001/api/";

function setupPage() {
    var apiurl = url + "GetMeditationList";

    $.ajax({
        type: "GET",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        displayMeditations(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
        //$('.meditaiton-list').text(jqXHR.responseText || textStatus);
        console.log("error");
    });
}

function displayMeditations(data) {
    var listItem = "";
    var meditationList = "";

    for (var i = 0; i < data.length; i++) {
        var buttonClick = "'" + data[i].MeditationId + "'";

        listItem = '<div class="row"><div class="col-xs-12 col-sm-12 col-md-7 col-lg-7">' + data[i].MeditationName +
            '<br />' + data[i].MeditationCreator +
            '</div><div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 store-rating">' +
            '<button class="btn btn-default button-medium" data-toggle="modal" data-target="#myModal" onclick="reviewMeditation(' + buttonClick + ')"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> Review</button>' +
            '</div><div class="col-xs-12 col-sm-12 col-md-2 col-lg-2 text-center store-details"><button class="btn btn-default button-medium" onclick="playMeditation(' + buttonClick + ')"><i class="fa fa-info" aria-hidden="true"></i> Play</button>' +
            '</div></div><hr />';
        meditationList = meditationList + listItem;
    }

    console.log(meditationList);
    $('.meditaiton-list').html(meditationList);
}

function SetVolume(val) {
    audio.volume = val / 100;
}

function reviewMeditation(data) {
    console.log("review");
}

function playMeditation(data) {
    var apiurl = url + "PlayMeditation/?itemId=" + data;

    $.ajax({
        type: "GET",
        url: apiurl,
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        console.log("success");
    }).error(function (jqXHR, textStatus, errorThrown) {
        //$('.meditaiton-list').text(jqXHR.responseText || textStatus);
        console.log("error");
        });

    //document.getElementById("meditation-display").style.backgroundColor = "rgba(121,134,203, 0.9)";
    //document.getElementById("meditation-display").innerHTML = breathText;
    //document.getElementById("meditation-display").style.width = "100%";
}

function showMeditation() {
    //document.getElementById("breath-display").style.backgroundColor = "rgba(121,134,203, 0.9)";
    //breathText = "inhale";
    //document.getElementById("breath-text").innerHTML = breathText;
    //document.getElementById("breath-display").style.width = "100%";
}