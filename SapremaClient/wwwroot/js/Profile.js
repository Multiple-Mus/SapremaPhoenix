﻿$(document).ready(function () {
    setupPage();
});

var url = "http://localhost:5002/Social/";

function setupPage() {
    var apiurl = url + "GetUserPoses";
    $.ajax({
        type: "GET",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        displayPoses(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#store-items').text(jqXHR.responseText || textStatus);
    });
}

function displayPoses(data) {
    listItem = "";
    finalList = "";

    for (var i = 0; i < data.length; i++) {
        var checked;
        if (data[i].PoseOmit === true) {
            checked = "";
        }

        else {
            checked = "checked";
        }

        var switchclick = 'updatePoseStatus("' + data[i].PoseId + '")';

        listItem = "<div class='col-xs-12 col-sm-6 col-md-4 col-lg-4 pose-list'><div class='pose'>" + data[i].PoseName +
            "</div><label class='switch'><input id='p" + data[i].PoseId + "' type='checkbox' " + checked + " onclick='" + switchclick + "'>" +
            "<div class='slider round'></div></label></div>";
        console.log(listItem);

        finalList = finalList + listItem;
    }
    $('#poseList').html(finalList);
}

function updatePoseStatus(id) {
    var apiurl;
    var methodCall;
    var itemId = "#p" + id;

    if ($("#p" + id).prop('checked') != true) {
         console.log('turned on');
        apiurl = url + "OmitPose/?itemId=" + id;
    }

    else {
        //console.log('turned off')
        apiurl = url + "IncludePose/?itemId=" + id;
    }

    $.ajax({
        type: "GET",
        url: apiurl,
        data: id,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        setupPage();
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#store-items').text(jqXHR.responseText || textStatus);
    });
}