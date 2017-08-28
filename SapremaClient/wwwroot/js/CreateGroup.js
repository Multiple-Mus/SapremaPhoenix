$(document).ready(function () {
    setupPage();
    $('#delete-group').hide();
});

function setupPage() {
    $('#updateGroup').hide();
    $('#delete-button').hide();
    $.ajax({
        type: "GET",
        url: 'http://localhost:5001/api/usergroups/groups/cbdf1836-1c05-40bf-8505-245fc8bcf17e',
        dataType: 'json',
        xhrFields: {
            withCredentials: true
        }
    }).done(function (data) {
        $('#group-list').html("");
        for (var i = 0; i < data.length; i++) {
            var listItem = "<div><span class='list-span' id='" + data[i].GroupId + "'>" + data[i].GroupName + "</span></div><hr /><br>";
            $('#group-list').append(listItem);
            JSON.stringify(data[i])
        }
        $('#group-name-error').hide();
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#group-list').text(jqXHR.responseText || textStatus);
    });
}

$('#group-list').on('click', '.list-span', function () {
    var groupId = $(this).attr('id');
    loadClasses(groupId);
});


$('#group-list').on('click', '.btn-danger', function () {
    var groupId = $(this).attr('data-grpremov');
    var selectorId = "#" + groupId;
    $(selectorId).nextAll().remove();
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5001/api/usergroups/groupClassRemove/' + groupId
    }).done(function () { loadClasses(groupId) });
});


function loadClasses(groupId){
    var selectorId = "#" + groupId;
    $(selectorId).nextAll().remove();
    $.ajax({
        type: "GET",
        url: 'http://localhost:5001/api/usergroups/classInfo/' + groupId,
        dataType: 'json',
        xhrFields: {
            withCredentials: true
        }
    }).done(function (data) {
        for (var i = 0; i < data.length; i++) {
            $("<div class='class-DDL'><div class='class-info-div'>Name: " + data[i].ClassName + "</div><div class='class-info-div'>Level: " + data[i].ClassLevel + "</div><div class='class-info-div'>Theme: " + data[i].ClassTheme + "</div><div class='class-info-div'>Description: " + data[i].ClassDescription + "</div><div class='class-info-div'><button data-grpremov=" + data[i].GroupId + " id=" + data[i].ClassId + " class='btn btn-danger'>Remove</button></div></div>").insertAfter($(selectorId));;
            JSON.stringify(data[i])
        }

        }).error(function (jqXHR, textStatus, errorThrown) {
            $('#group-list').text(jqXHR.responseText || textStatus);
        });
};
