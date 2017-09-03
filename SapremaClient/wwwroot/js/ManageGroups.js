var urlMVC = "http://localhost:5002/Social/"
var urlAPI = "http://localhost:5001/api/"

$(document).ready(function () {
    setupPage();
});

$('#createGroup').on('click', function () {
    var validation = checkInput();
    if (validation === true) {
        createGroup();
    }
    else {
        $('#group-name-error').show();
    }
});

function updateGroup(id) {
    var validation = checkInput();
    if (validation === true) {
        update(id);
    }
    else {
        $('#group-name-error').show();
    }
}

function checkInput() {
    var check = $('#GroupName').val();
    if (check === "") {
        return false;
    }

    else {
        return true;
    }
}

function clearForm() {
    $('#GroupName').val("");
    $('#GroupDescription').val("");
}

function deleteModal(id) {
    $('.modal-title').html("Delete Group");
    $('.modal-body').html("Are you sure you want to delete group:<br>" + $('#GroupName').val());
    $('.modal-footer').html('<button type="button" class="btn btn-default" id="deleteConfermation" data-dismiss="modal">Delete</button><button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>');
    $('#deleteConfermation').attr("onclick", "deleteGroup('" + id + "')");
}

function createGroup() {
    var GroupDescription = "'" + $('#GroupDescription').val() + "'";
    var GroupName = "'" + $('#GroupName').val() + "'";
    var GroupLevel = $('#GroupLevel').val();
    var GroupStatus;
    if ($('#GroupStatus').is(':checked')) {
        GroupStatus = true;
    }
    else {
        GroupStatus = false;
    }
    var value = '"{GroupStatus:' + GroupStatus +
        ',GroupName:' + GroupName +
        ',GroupDescription:' + GroupDescription +
        ',GroupLevel:' + GroupLevel +'}"';
    var url = urlMVC + "CreateGroup";
    $.ajax({
        type: "POST",
        url: url,
        contentType: "text/json",
        data: value,
        success: function (data, txtStatus, xhr) {
            clearForm();
            setupPage();
            $('#success-message').html("<span class='success'>Group Created</span>");
            
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });
}

function deleteGroup(id) {
    var apiurl = urlMVC + "DeleteGroup?itemid=" + id;
    $.ajax({
        type: "DELETE",
        url: apiurl,
        contentType: "text/json",
        success: function (data, txtStatus, xhr) {
            clearForm();
            setupPage();
            $('#success-message').html("<span class='success'>Group Deleted</span>");
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

function update(id) {
    var GroupDescription = "'" + $('#GroupDescription').val() + "'";
    var GroupName = "'" + $('#GroupName').val() + "'";
    var GroupLevel = $('#GroupLevel').val();
    var GroupId = "'" + id + "'";
    var GroupStatus;
    if ($('#GroupStatus').is(':checked')) {
        GroupStatus = true;
    }
    else {
        GroupStatus = false;
    }
    var value = '"{GroupStatus:' + GroupStatus +
        ',GroupName:' + GroupName +
        ',GroupDescription:' + GroupDescription +
        ',GroupLevel:' + GroupLevel +
        ',GroupId:' + GroupId + '}"';
    var url = urlMVC + "UpdateGroup";
    $.ajax({
        type: "PUT",
        url: url,
        contentType: "text/json",
        data: value,
        success: function (data, txtStatus, xhr) {
            clearForm();
            $('#success-message').html("Group Updated");
            $('#createGroup').show();
            $('#updateGroup').hide();
            setupPage();
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });
}

function editGroup(id) {
    $('#createGroup').hide();
    $('#updateGroup').show();
    $('success-message').html("");
    var apiurl = "http://localhost:5001/api/groups/" + id;
    $.ajax({
        type: "GET",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        console.log(data);
        $('#GroupName').val(data["GroupName"]);
        $('#GroupDescription').val(data["GroupDescription"]);
        var status = data["GroupStatus"];
        if (status === "True") {
            $('#GroupStatus').attr("checked", true);
        }
        else {
            $('#GroupStatus').attr("checked", false);
        }
        $('#GroupLevel').val(data["GroupLevel"]);
        $('#updateGroup').attr("onclick", "updateGroup('" + data['GroupId'] + "')");
        $('#deleteGroup').attr("onclick", "deleteModal('" + data['GroupId'] + "')");
        $('#success-message').html("");
        deleteModal(data['GroupId']);
        $('#delete-button').show();
    })
}

function setupPage() {
    $('#updateGroup').hide();
    $('#delete-button').hide();
    var url = urlMVC + "GetUserGroups";
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
    }).done(function (data) {
        $('#group-list').html("");
        for (var i = 0; i < data.length; i++) {
            var listItem = "<span class='list-span'>" + data[i].GroupName + "<button onclick=\"editGroup('" + data[i].GroupId + "')\" class=\"btn btn-default\" type=\"submit\">Submit</button></span><hr /><br>";
            $('#group-list').append(listItem);
            JSON.stringify(data[i])
        }
        $('#group-name-error').hide();
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#group-list').text(jqXHR.responseText || textStatus);
    });
}
