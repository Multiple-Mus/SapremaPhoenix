$(document).ready(function () {
    setupPage();
});

var originalList; // Array of meditations on page load
var urlMVC = "http://localhost:5002/Social/";
var urlAPI = "http://localhost:5001/api/";

// Display all meditations on page load
function setupPage() {
    var url = urlMVC + "GetStoreList";
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        displayGroups(data);
        //originalList = data;
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#store-items').text(jqXHR.responseText || textStatus);
    });
}

// Populate list of meditations on page load or after search
function displayGroups(data) {
    var listItem = "";
    var shopList = "";

    for (var i = 0; i < data.length; i++) {
        var buttonText;
        var buttonIcon;
        var buttonClick = '"' + data[i].GroupId + '"';

        if (data[i].GroupJoined === true) {
            buttonText = "Details";
        }

        else {
            buttonText = "Join";
        }
        listItem = "<div class='row'><div class='col-xs-12 col-sm-12 col-md-6 col-lg-6'>" + data[i].GroupName + "<br />" + data[i].GroupAdmin + "<br />Level:" + data[i].GroupLevel + "</div>" +
        "<div class='col-xs-12 col-sm-12 col-md-2 col-lg-2 text-center store-details'>" +
            "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='groupModal(" + buttonClick + "," + data[i].GroupJoined + ")'></i> " + buttonText + "</button>" +
            "</div></div><hr />";
        shopList = shopList + listItem;
        //JSON.stringify(data[i]);
    }
    $('.search-items').html(shopList);
}

function groupModal(id, purchased) {
    var url = urlAPI + "groups/" + id;
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json'
    }).done(function (data) {
        var modalBody = "<span><strong>Name:</strong> " + data.GroupName + "</span><br />" +
            "<span><strong>Description:</strong> " + data.GroupDescription + "</span><br />";
        var functionCall;
        var buttonIcon;
        var buttonText;

        if (purchased === true) {
            functionCall = "returnGroup";
            buttonIcon = "ban";
            buttonText = "Leave";
        }

        else {
            functionCall = "buyGroup";
            buttonText = "Join";
        }
        var buttonClick = '"' + id + '"';
        var modalFooter = "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='" + functionCall + "(" + buttonClick + ")'><i class='fa fa-" + buttonIcon + "' aria-hidden='true'></i> " + buttonText + "</button>";
        $('.modal-body').html(modalBody);
        $('.modal-footer').html(modalFooter);
        $('.modal-title').html("Meditation Details");
    });
}

function buyGroup(id) {
    var url = urlMVC + "JoinGroup?itemId=" + id;

    $.ajax({
        type: "POST",
        url: url,

        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        $('#myModal').modal('hide');
        setupPage();
    });
}

function returnGroup(id) {
    var url = urlMVC + "LeaveGroup?itemId=" + id;

    $.ajax({
        type: "DELETE",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        $('#myModal').modal('hide');
        setupPage();
    });
}

