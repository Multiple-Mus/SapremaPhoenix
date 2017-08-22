
//Commented code are differenc attempts to call PUT and POST api calls

//$('#clcikme').on('submit', function (e) {

//    e.preventDefault();

//    var gName = document.getElementById('groupName').value;
//    var gDescription = document.getElementById('geoupDescription').value;
//    var access = document.getElementById('privateSetting').value;
//    var gLevel = document.getElementById('groupLevel').value;
//    var gID = "f2d919ca-d527-44de-84af-a8e2b2813d48";
//    var value = { "GroupName": gName, "GroupDescription": gDescription, "GroupLevel": gLevel, "GroupStatus": access };
//    console.log(value);
//    $.ajax({
//        type: "PUT",
//        url: "http://localhost:5001/api/user/groups/f2d919ca-d527-44de-84af-a8e2b2813d48",
//        data: value,
//    });
//});


//function updateGroup() {
//    var gName = document.getElementById('groupName').value;
//    var gDescription = document.getElementById('groupDescription').value;
//    var access = document.getElementById('privateSetting').value;
//    var gLevel = document.getElementById('groupLevel').value;
//    var gID = "f2d919ca-d527-44de-84af-a8e2b2813d48";
//    var value = "{ GroupName: 'new', GroupDescription: 'new', GroupLevel: 1, GroupStatus: false}";
//    console.log(value);
//    $.ajax({
//        type: "PUT",
//        url: "http://localhost:5001/api/user/groups/f2d919ca-d527-44de-84af-a8e2b2813d48",
//        data: value,
//        dataType: "text",
//        success: function (data, txtStatus, xhr) {
//            console.log(data);
//        },
//        error: function (xhr, txtStatus, errorThrown) {
//            console.log('error');
//        }
//    });
//}

//didnt work at all
//function updateGroup() {
//    var options = {};
//    options.url = "http://localhost:5001/api/user/groups";
//    options.type = "POST";

//    var obj = {};
//    obj.GroupName = "newNEWNEW";
//    obj.GroupDescription = "newNEWNEW";
//    obj.GroupLevel = 1;
//    obj.GroupStatus = false;
//    obj.GroupAdmin = "cbdf1836-1c05-40bf-8505-245fc8bcf17e";

//    options.data = JSON.stringify(obj);
//    options.contentType = "application/json";
//    options.dataType = "html";
//    options.success = function (msg) {
//        $("#msg").html(msg);
//    };
//    $.ajax(options);
//}




//function updateGroup() {
//    var value = "{ GroupName: 'new', GroupDescription: 'new', GroupLevel: 1, GroupStatus: false, GroupAdmin: 'cbdf1836-1c05-40bf-8505-245fc8bcf17e'}";
//    $.ajax({
//        type: "POST",
//        url: '@Url.Action("groups","UserController")',
//        data: JSON.stringify(value),
//        contentType: "application/json;charset=utf-8",
//        success: function (data, txtStatus, xhr) {
//            console.log(data);
//        },
//        error: function (xhr, txtStatus, errorThrown) {
//            console.log('error');
//        }
//    });
//}



// *************** WORKING ****************

$('#createGroup').on('click', function () {
    var validation = checkInput();
    if (validation == true) {
        createGroup();
    }
    else {
        $('#group-name-error').show();
    }
    //console.log(validation)
});

function updateGroup(id) {
    var validation = checkInput();
    if (validation == true) {
        update(id);
    }
    else {
        $('#group-name-error').show();
    }
}

function checkInput() {
    var test = $('#GroupName').val();
    if (test == "")
    {
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
    $('.modal-title').append("Delete Group");
    $('.modal-body').append("Are you sure you want to delete group:<br>" + $('#GroupName').val());
    $('.modal-footer').append('<button type="button" class="btn btn-default" id="deleteConfermation" data-dismiss="modal">Delete</button><button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>');
    $('#deleteConfermation').attr("onclick", "deleteGroup('" + id + "')");
}

function createGroup() {
    var GroupDescription = "'" + $('#GroupDescription').val() + "'";
    var GroupName = "'" + $('#GroupName').val() + "'";
    var GroupLevel = $('#GroupLevel').val();
    var GroupAdmin = "'cbdf1836-1c05-40bf-8505-245fc8bcf17e'";
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
        ',GroupAdmin:' + GroupAdmin + '}"';
    var apiurl = "http://localhost:5001/api/user/groups/";
    //console.log(value);
    $.ajax({
        type: "POST",
        url: apiurl,
        contentType: "text/json",
        data: value,
        success: function (data, txtStatus, xhr) {
            //console.log(data);
            clearForm();
            //location.reload();
            setupPage();
            $('#success-message').append("<span class='success'>Group Created</span>");
            
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });
}

function deleteGroup(id) {
    var apiurl = "http://localhost:5001/api/user/groups/" + id;
    console.log("this is some text");
    $.ajax({
        type: "DELETE",
        url: apiurl,
        contentType: "text/json",
        success: function (data, txtStatus, xhr) {
            //console.log(data);
            clearForm();
            //location.reload();
            setupPage();
            $('#success-message').append("<span class='success'>Group Deleted</span>");
            $('.modal-body').html("");
            $('.modal-footer').html("");
            setupPage();
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });
}

function update(id) {
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
        ',GroupLevel:' + GroupLevel + '}"';
    var apiurl = "http://localhost:5001/api/user/groups/" + id;
    //console.log(value);
    $.ajax({
        type: "PUT",
        url: apiurl,
        contentType: "text/json",
        data: value,
        success: function (data, txtStatus, xhr) {
            //console.log(data);
            clearForm();
            $('#success-message').val("Group Updated");
            //location.reload();
        },
        error: function (xhr, txtStatus, errorThrown) {
            console.log('error');
        }
    });
}

function editGroup(id) {
    //console.log(id);
    $('#createGroup').hide();
    $('#updateGroup').show();
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
        $('success-message').val("");
        deleteModal(data['GroupId']);
        $('#delete-group').show();
    })
}

$(document).ready(function () {
    setupPage();
    $('#delete-group').hide();
});

function setupPage() {
    $('#updateGroup').hide();
    $.ajax({
        type: "GET",
        url: 'http://localhost:5001/api/user/groups/cbdf1836-1c05-40bf-8505-245fc8bcf17e',
        dataType: 'json',
    }).done(function (data) {
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
