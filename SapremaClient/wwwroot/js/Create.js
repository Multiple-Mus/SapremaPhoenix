$(document).ready(function () {
    setupPage();
});

var urlMVC = "http://localhost:5002/Yoga/";
var urlAPI = "http://localhost:5001/api/";
var listItem = "";
var finalList = "";
var classList = [];
var num = 0;


function setupPage() {
    $('#pose-list').hide();
    $('#random-generate').hide();
    $('#breathOptionsUser').hide();
    $('#breathOptionsRandom').hide();
    $('#success').hide();
    var url = urlMVC + "GetUserPoses";
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        originalList = data;
        displayPoses(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#store-items').text(jqXHR.responseText || textStatus);
    });
}

function showRandom() {
    $('#pose-list').hide();
    $('#random-generate').show();
    $('#breathOptionsRandom').show();
    $('#breathOptionsUser').hide();
}

function showUser() {
    $('#pose-list').show();
    $('#random-generate').hide();
    $('#breathOptionsUser').show();
    $('#breathOptionsRandom').hide();
}

function displayPoses(data) {
    for (var i = 0; i < data.length; i++) {
        var clickOn = 'addPose("' + data[i].PoseId + '","' + data[i].PoseName + '")';
        listItem = "<div class='pose' onclick='" + clickOn + "'><div>" + data[i].PoseName + "</div></div>";
        finalList = finalList + listItem;
    }
    $('#pose-list').html(finalList);
}


function addPose(id, name) {
    //console.log(name);
    $('#success').hide();
    var clicker = 'remove("p' + num + '")';
    var cPose = "<div class='poseIn col-xs-12' id='p" + num + "'><div class='pose-name' data-myval='" + id +"'>" + name + "</div><div><input type='number' step='10' min='10' value='10' max='120' id='t" + num + "'>" +
        "</input></div><div onclick='" + clicker + "')><i class='fa fa-times' aria-hidden='true'></i></div> ";
    $('#class-pose').append(cPose);
    //console.log(classList[0]);
    num++;
}

function pranayama() {
    if ($('#pranayama').is(':checked')) {
        console.log("turned on");
        var clicker = 'remove("classPran")';
        var cPran = "<div class='poseIn col-xs-12' id='classPran'><div class='pose-name'>Pranayama</div><div><input type='number' step='10' min='10' value='10' max='600' id='timePran'>" +
            "</input></div><div onclick='" + clicker + "')><i class='fa fa-times' aria-hidden='true'></i></div> ";
        $('#classPranayama').html(cPran);
    }
    else {
        remove("classPran");
    }
}

function shavasana() {
    if ($('#shavasana').is(':checked')) {
        console.log("turned on");
        var clicker = 'remove("classShav")';
        var cShav = "<div class='poseIn col-xs-12' id='classShav'><div class='pose-name'>Shavasana</div><div><input type='number' step='10' min='10' value='10' max='600' id='timeShav'>" +
            "</input></div><div onclick='" + clicker + "')><i class='fa fa-times' aria-hidden='true'></i></div> ";
        $('#classShavasana').html(cShav);
    }
    else {
        remove("classShav");
    }
}

function remove(id) {
    $('#' + id).remove();
    if (id == "classPran") {
        $('#pranayama').prop('checked', false);
    }
    if (id == "classShav") {
        $('#shavasana').prop('checked', false);
    }
}

$(function () {
    $('#class-pose').sortable();
})


function saveClass() {
    url = urlMVC + "GetUserGroupsList";
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        saveClassModal(data);
        //displayPoses(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#store-items').text(jqXHR.responseText || textStatus);
    });
}
function saveClassModal(data) {

    $('.modal-title').html('Enter Class Details');
    var modelBody = '<div class="form-group"><label for="ClassName">Class Name:</label>' +
        '<input type="text" class="form-control" id="ClassName"></div>' +
        '<div class="form-group"><label for="ClassTheme">Class Theme:</label>' +
        '<input type="text" class="form-control" id="ClassTheme"></div>' +
        '<div class="form-group"><label for="ClassType">Class Type:</label>' +
        '<input type="text" class="form-control" id="ClassType"></div>' +
        '<div class="form-group"><label for="ClassDescription">Class Description:</label>' +
        '<input type="textbox" class="form-control" id="ClassDescription"></div>' +
        '<div class="form-group"><label for="ClassLevel">Class Level:</label>' +
        '<select class="form-control" id="ClassLevel"><option>1</option><option>2</option><option>3</option></select></div>' +
        '<div class="form-group"><label for="ClassGroupId">Class Group:</label>' +
        '<select class="form-control" id="ClassGroupId"><option val="">Select Group</option>';
    for (var i = 0; i < data.length; i++) {
        modelBody = modelBody + '<option value="' + data[i].GroupId + '">' + data[i].GroupName + '</option>';
    }
    modelBody = modelBody + '</select></div>';
    console.log(data);
    $('.modal-body').html(modelBody);
    $('.modal-footer').html("<button type='button' class='btn btn-default button-medium' onclick='classDetailsSave()'>Save</button><button type='button' class='btn btn-default button-medium' data-dismiss='modal'>Close</button>");
    $('#myModal').modal("show");
}

function getGroups() {
    
}

function classDetailsSave() {
    var childDivs = document.getElementById('class-pose').getElementsByTagName('div');
    var seq = 1;
    var className = $('#ClassName').val();
    var classLevel = $('#ClassLevel').val();
    var classTheme = $('#ClassTheme').val();
    var classDescription = $('#ClassDescription').val();
    var classGroup = $('#ClassGroupId').val();
    //var e = document.getElementById("ClassGroupId");
    //var classGroup = e.options[e.selectedIndex].value;
    var classPlan = "{'ClassName':'" + className + "', 'ClassDescription':'" + classDescription + "','ClassTheme':'" + classTheme + "','ClassLevel':" + classLevel + ",'ClassGroupId':'" + classGroup + "','Poses' : ["
    for (i = 0; i < childDivs.length; i++) {
        var childDiv = childDivs[i];
        if ($(childDivs[i]).hasClass("poseIn")) {
            var subdivs = childDivs[i].getElementsByTagName('div');
            for (p = 0; p < subdivs.length; p++) {
                if (p == 0) {
                    var id = $(subdivs[p]).data('myval');
                    classPlan = classPlan + "{'PoseId':'" + id + "',";
                }
                else if (p == 1) {
                    var timeId = $(subdivs).find('input[type="number"]')[0].id;
                    var time = document.getElementById(timeId).value;
                    classPlan = classPlan + "'PoseLength': " + time +",'PoseSequence' : " + seq + "},"
                    seq++
                }
            }
        }
    }
    if ($('#shavasana').is(':checked')) {
        var shavasanaTime = document.getElementById('timeShav').value;
        classPlan = classPlan + "{'PoseId': 999, 'PoseLength':" + shavasanaTime + ",'PoseSequence': 1000},"
    }
    if ($('#pranayama').is(':checked')) {
        var pranayamaTime = document.getElementById('timePran').value;
        classPlan = classPlan + "{'PoseId': 998, 'PoseLength':" + pranayamaTime + ",'PoseSequence': 0},"
    }
    classPlan = classPlan.substr(0, classPlan.length - 1);
    classPlan = classPlan + "]}";
    console.log(classPlan);   

    sendSaveClass(classPlan);
    
}

function sendSaveClass(value) {
    var url = urlMVC + "SaveClass";
    console.log(value);
    var newvalue = '"' + value + '"';
    $.ajax({
        type: "POST",
        url: url,
        contentType: "text/json",
        data: newvalue,
        success: function (data, txtStatus, xhr) {
            $('#class-pose').html("");
            $('#myModal').modal("hide");
            $('#success').show();
            remove("classShav");
            remove("classPran");
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });
}

function clear() {
    remove("classShav");
    remove("classPran");
    $('#shavasana').prop('checked', false);
    $('#pranayama').prop('checked', false);
}

