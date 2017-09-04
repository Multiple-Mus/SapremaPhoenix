$(document).ready(function () {
    setupPage();
});

var urlMVC = "http://localhost:5002/Meditation/";
var urlAPI = "http://localhost:5001/api/";
var meditationAudio;

function setupPage() {
    var url = urlMVC + "GetMeditationList";

    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        displayMeditations(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
    });
}

function displayMeditations(data) {
    var listItem = "";
    var meditationList = "";

    for (var i = 0; i < data.length; i++) {
        var buttonClick = "'" + data[i].MeditationId + "','" + data[i].MeditationName + "'";

        listItem = '<div class="row"><div class="col-xs-12 col-sm-12 col-md-7 col-lg-7">' + data[i].MeditationName +
            '<br />' + data[i].MeditationCreator +
            '</div><div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 store-rating">' +
            '</div><div class="col-xs-12 col-sm-12 col-md-2 col-lg-2 text-center store-details"><button class="btn btn-default button-medium" onclick="playMeditation(' + buttonClick + ')"><i class="fa fa-info" aria-hidden="true"></i> Play</button>' +
            '</div></div><hr />';
        meditationList = meditationList + listItem;
    }
    $('.meditaiton-list').html(meditationList);
}

function SetVolume(val) {
    audio.volume = val / 100;
}

function reviewMeditation(data) {
    console.log("review");
}

function playMeditation(itemId, meditationName) {
    var url = urlMVC + "PlayMeditation?itemId=" + itemId;
    var audioLink = '../Uploads/MeditationAudio/' + itemId + '.mp3';
    var imageLink = '../Uploads/MeditationImages/' + itemId + '.jpg';
    meditationAudio = new Audio(audioLink);
    //meditationAudio = new Audio("../audio/ding.mp3");
    meditationAudio.play();
    document.getElementById("meditation-display").style.backgroundColor = "rgba(121,134,203, 0.9)";
    document.getElementById("play-meditation-image").src = imageLink;
    meditationName = meditationName;
    $('#meditation-text').html(meditationName);
    document.getElementById("meditation-display").style.width = "100%"; 
    meditationAudio.addEventListener("ended", function () {
        reviewModal(itemId);
    })
}

function reviewModal(itemId) {
    var comment = "";
    var rating = 0;
    var url = urlMVC + "GetReview?itemID=" + itemId;
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
        //$('.meditaiton-list').text(jqXHR.responseText || textStatus);
        });
    //$('#reviewComment').val(data.ReviewComment);
    
    $('.modal-title').html("Review Meditation");
    $('#myModal').modal("show");
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
        $('.modal-footer').html("<button type='button' class='btn btn-default button-medium' onclick='reviewMeditation(" + buttonClick + ")'>Review</button><button type='button' class='btn btn-default button-medium' data-dismiss='modal'>Close</button>");
    }
    else {
        $('.modal-footer').html("<button type='button' class='btn btn-default button-medium' onclick='updateReview(" + buttonClick + ")'>Update</button><button type='button' class='btn btn-default button-medium' data-dismiss='modal'>Close</button>");
    }
    if (rating !== 0) {
        var roundedRating = Math.round(rating);
        var ratingStar = 'value="' + roundedRating + '"';
        var ratingShow = ratingStar + " checked";
        modelBody = modelBody.replace(ratingStar, ratingShow);
    }
    $('.modal-body').html(modelBody);
    $('.modal-title').html("Review Meditation");
    $('#myModal').modal("show");
    $('#reviewComment').val(comment);
}

function reviewMeditation(itemId) {
    var url = urlMVC + "ReviewMeditation?itemId=" + itemId;
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
        $('#myModal').modal('hide');
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
    });
}

function updateReview(itemId) {
    var url = urlMVC + "UpdateReviewMeditation?itemId=" + itemId;
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
        $('#myModal').modal('hide');
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('.meditaiton-list').text(jqXHR.responseText || textStatus);
    });
}

function SetVolume(val) {
    audio.volume = val / 100;
}

function hideDisplay() {
    meditationAudio.pause();
    document.getElementById("meditation-display").style.width = "0%";
}