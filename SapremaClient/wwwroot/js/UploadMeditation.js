var urlMVC = "http://localhost:5002/Meditation/";
var urlapi = "http://localhost:5001/api/admin/"

$('#uploadMeditation').on('click', function () {

    var validation = checkInput();

    if (validation === true) {
        uploadMeditation();
    }
    else {
        //do nothing
        // $('#group-name-error').show();
    }
    //console.log(validation)
});


//shorthand needs to be change
function checkInput() {

    for (i = 1; i <= 4; i++)
    {
        $('#meditation-error' + i).hide();
    }

    var error1 = $('#MeditationName').val();
    var error2 = $('#MeditationDescription').val();
    var error3 = $('#MeditationTheme').val();
    var error4 = $('#MeditationCreator').val();
    var error5 = $('#MeditationType').val();

    var validationArray = [error1, error2, error3, error4, error5];
    //console.log(validationArray);
    //console.log(error1);
    //var validationArray = new Array($('#MeditationDescription').val(), $('#MeditationTheme').val(), $('#MeditationCreator').val(), $('#MeditationAudio').val());

    for (i = 0; i <= 4; i++) {
        if (validationArray[i] === "") {
            console.log(validationArray[i]);
            $('#meditation-error' + (i + 1)).show();
            return false;
        }
        else {
            console.log("woohoo");
            return true;
        }

    }
}

function uploadMeditation() {

    var MeditationName = "'" + $('#MeditationName').val() + "'";
    var MeditationDescription = "'" + $('#MeditationDescription').val() + "'";
    var MeditationTheme = "'" + $('#MeditationTheme').val() + "'";
    var MeditationCreator = "'" + $('#MeditationCreator').val() + "'";
    var MeditationType = "'" + $('#MeditationType').val() + "'";
    var value = '"{MeditationName:' + MeditationName +
        ',MeditationDescription:' + MeditationDescription +
        ',MeditationTheme:' + MeditationTheme +
        ',MeditationCreator:' + MeditationCreator +
        ',MeditationType:' + MeditationType + '}"';


    var apiurl = urlapi + "meditation";
    //console.log(value);
    $.ajax({
        type: "POST",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        data: value,
        success: function (data, txtStatus, xhr) {
            //uploadMedia()
            clearForm();
            //location.reload();
            setupPage();
            $('#success-message').html("<span class='success'>Meditation Uploaded</span>");
            //setupModal()

        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });
}


function uploadMedia()
{

    var url = urlMVC + "UploadFiles?itemId=dhfjkshdfksdhfksdjhfskjdfhsk";

    var fileUpload = $("#files").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: data,
        success: function (message) {
            alert(message);
        },
        error: function () {
            alert("There was error uploading files!");
        }
    });
}

function clearForm() {
    $('#MeditationName').val("");
    $('#MeditationDescription').val("");
    $('#MeditationTheme').val("");
    $('#MeditationCreator').val("");
    $('#MeditationType').val("");
}

function deleteModal(id) {
    $('.modal-title').html("Delete Meditation");
    $('.modal-body').html("Are you sure you want to delete meditation:<br>" + $('#MeditationName').val());
    $('.modal-footer').html('<button type="button" class="btn btn-default" id="deleteConfermation" data-dismiss="modal">Delete</button><button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>');
    $('#deleteConfermation').attr("onclick", "deleteGroup('" + id + "')");
}


function setupPage() {
    //$('#updateGroup').hide();
    //$('#delete-button').hide();
    url = urlMVC + "GetMeditationAdmin"
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
    }).done(function (data) {
        $('#group-list').html("");
        for (var i = 0; i < data.length; i++) {
            var listItem = "<span class='list-span'>" + data[i].MeditationName + "<button onclick=\"editGroup('" + data[i].MeditationId + "')\" class=\"btn btn-default\" type=\"submit\">Submit</button></span><hr /><br>";
            $('#group-list').append(listItem);
            JSON.stringify(data[i])
        }
        $('#group-name-error').hide();
        $('#updateGroup').hide();
        $('#delete-button').hide();
        $('#uploadMeditation').show();
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#group-list').text(jqXHR.responseText || textStatus);
    });
}

function deleteGroup(id) {
    var url = urlMVC + "DeleteMeditationAdmin?itemId=" + id;
    console.log("this is some text");
    $.ajax({
        type: "DELETE",
        url: url,
        contentType: "text/json",
        success: function (data, txtStatus, xhr) {
            //console.log(data);
            clearForm();
            //location.reload();
            setupPage();
            $('#success-message').html("<span class='success'>Meditation Deleted</span>");
            $('.modal-body').html("");
            $('.modal-footer').html("");
            setupPage();
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }

    });

    $('#createGroup').show();
}


function editGroup(id) {
    var url = urlMVC + "GetMeditationDetailsAdmin?ItemId=" + id;
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        console.log(data);
        $('#MeditationName').val(data["MeditationName"]);
        $('#MeditationDescription').val(data["MeditationDescription"]);
        $('#MeditationTheme').val(data["MeditationTheme"]);
        $('#MeditationCreator').val(data["MeditationCreator"]);
        $('#MeditationType').val(data["MeditationType"]);
        $('#updateGroup').attr("onclick", "updateGroup('" + data['MeditationId'] + "')");
        $('#deleteGroup').attr("onclick", "deleteModal('" + data['MeditationId'] + "')");
        $('#success-message').html("");
        deleteModal(data['MeditationId']);
        $('#delete-button').show();
        $('#updateGroup').show();
        $('#uploadMeditation').hide();
    })
}


function updateGroup(id) {
    var validation = checkInput();
    if (validation === true) {
        update(id);
    }
    else {
        //$('#group-name-error').show();
    }
}

function update(id) {

    var MeditationName = "'" + $('#MeditationName').val() + "'";
    var MeditationDescription = "'" + $('#MeditationDescription').val() + "'";
    var MeditationTheme = "'" + $('#MeditationTheme').val() + "'";
    var MeditationCreator = "'" + $('#MeditationCreator').val() + "'";
    var MeditationType = "'" + $('#MeditationType').val() + "'";

    var value = '"{MeditationName:' + MeditationName +
        ',MeditationDescription:' + MeditationDescription +
        ',MeditationTheme:' + MeditationTheme +
        ',MeditationCreator:' + MeditationCreator +
        ',MeditationType:' + MeditationType + '}"';

    console.log(id);
    var url = urlMVC + "UpdateMeditationAdmin?itemId=" + id;
    //console.log(apiurl);
    $.ajax({
        type: "PUT",
        url: url,
        contentType: "text/json",
        data: value,
        success: function (data, txtStatus, xhr) {
            //console.log(data);
            clearForm();
            $('#success-message').html("Meditation Updated");
            $('#createGroup').show();
            $('#updateGroup').hide();
            setupPage();
            //location.reload();
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });



}

$(document).ready(function () {

    $('#meditation-error1').hide();
    $('#meditation-error2').hide();
    $('#meditation-error3').hide();
    $('#meditation-error4').hide();
    $('#meditation-error5').hide(); 

    setupPage();

});