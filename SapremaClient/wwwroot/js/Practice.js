$(document).ready(function () {
    setupPageUser();
    setupPageGroup()
});

var urlMVC = "http://localhost:5002/Yoga/";
var urlAPI = "http://localhost:5001/api/";
var classAudio;
var p = 0;
var poseTime = 0;
var audioLink = "";
var imageLink = "";
var dataLength = 0;
var classPlan = [];
var stopPlay = 0;
var classId = "";

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

function setupPageGroup() {
    var url = urlMVC + "GetSubbedClasses";

    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        displayubbedClasses(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
    });
}

function SetVolume(val) {
    classAudio.volume = val / 100;
}

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

function displayubbedClasses(data) {
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
    $('#groupclasses').html(classList);
}

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
    var url = urlMVC + "GetClassPoses?itemId=" + data;
    classId = data;
    stopPlay = 0;
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        dataLength = data.length;
        classPlan = data;
        startClass(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
    });
}

function startClass(data) {
    poseDisplay(data);
    document.getElementById("class-display").style.backgroundColor = "rgba(121,134,203, 0.9)";
    document.getElementById("class-display").style.width = "100%";
}

function hideDisplay() {
    location.reload();
}

function poseDisplay() {
    if (classPlan[p].PoseId == 999 || classPlan[p].PoseId == 998) {
        document.getElementById("play-class-image").src = "../images/logo.svg";
    }
    else {
        
        audioLink = "../audio/pose/" + classPlan[p].PoseId + ".wav";
        imageLink = "../images/poses/" + classPlan[p].PoseId + ".svg";
        classAudio = new Audio(audioLink);
        var tempVol = document.getElementById('vol-control').value;
        classAudio.volume = tempVol / 100;
        classAudio.play();
        document.getElementById("play-class-image").src = imageLink;
    }
    
    $('#class-text').html(classPlan[p].PoseName);
    poseTime = classPlan[p].PoseLength;
    setTimeout(function () {
        p++
        if (p == dataLength) {
            classComplete(classId)
        }
        else {
            poseDisplay();
        }
    }, 1000 * poseTime);
}

function classComplete() {
    var url = urlMVC + "CheckClass?itemId=" + classId;
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        if (data == "true") {
            hideDisplay();
        }
        else {
            setupReview(classId);
        }
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
    });
}

function setupReview(itemId) {
    var url = urlMVC + "GetReview?itemid=" + itemId;

    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        if (data === null) {
            comment = "";
            rating = 0;
            reviewModelContent(comment, rating, itemId);
        }
        else {
            rating = data.ReviewStars;
            comment = data.ReviewComment;
            reviewModelContent(comment, rating, itemId);
        }
    }).error(function (jqXHR, textStatus, errorThrown) {
    });
}

function reviewModelContent(comment, rating, itemId) {
    var buttonClick = '"' + itemId + '"';
    var modelBody = '<div class="row">Rating:<fieldset class="rating" id="reviewStars">' +
        '<input type="radio" id="star5" name="rating" value="5" /><label class="full" for="star5" title="5 stars"></label>' +
        '<input type="radio" id="star4" name="rating" value="4" /><label class="full" for="star4" title="4 stars"></label>' +
        '<input type="radio" id="star3" name="rating" value="3" /><label class="full" for="star3" title="3 stars"></label>' +
        '<input type="radio" id="star2" name="rating" value="2" /><label class="full" for="star2" title="2 stars"></label>' +
        '<input type="radio" id="star1" name="rating" value="1" /><label class="full" for="star1" title="1 star"></label>' +
        '</fieldset></div ><div class="row"><textarea rows="4" cols="50" id="reviewComment" placeholder="comment"></textarea></div>';

    if (comment == "" && rating == 0) {
        $('.modal-footer').html("<button type='button' class='btn btn-default button-medium' onclick='reviewClass(" + buttonClick + ")'>Review</button><button type='button' class='btn btn-default button-medium' onclick'hideDisplay()' data-dismiss='modal'>Close</button>");
    }
    else {
        $('.modal-footer').html("<button type='button' class='btn btn-default button-medium' onclick='updateReview(" + buttonClick + ")'>Update</button><button type='button' class='btn btn-default button-medium' onclick'hideDisplay()' data-dismiss='modal'>Close</button>");
    }
    if (rating !== 0) {
        var roundedRating = Math.round(rating);
        var ratingStar = 'value="' + roundedRating + '"';
        var ratingShow = ratingStar + " checked";
        modelBody = modelBody.replace(ratingStar, ratingShow);
    }
    $('.modal-body').html(modelBody);
    $('.modal-title').html("Review Class");
    $('#myModal').modal("show");
    $('#reviewComment').val(comment);
}

function reviewClass(itemId) {
    var url = urlMVC + "ReviewClass?itemId=" + itemId;
    var ReviewStars = $('input[name=rating]:checked').val();
    var ReviewComment = "'" + $('#reviewComment').val() + "'";
    var data = '"{ReviewStars:' + ReviewStars +
        ',ReviewComment:' + ReviewComment + '}"';
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        hideDisplay();
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
    });
}

function updateReview(itemId) {
    var url = urlMVC + "UpdateReviewClass?itemId=" + itemId;
    var ReviewStars = $('input[name=rating]:checked').val();
    var ReviewComment = "'" + $('#reviewComment').val() + "'";
    var data = '"{ReviewStars:' + ReviewStars +
        ',ReviewComment:' + ReviewComment + '}"';
    $.ajax({
        type: "PUT",
        url: url,
        data: data,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        hideDisplay();
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
    });
}