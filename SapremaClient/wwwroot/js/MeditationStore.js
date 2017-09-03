$(document).ready(function() {
    setupPage();
    populateTheme();
    populateType();
});

var originalList; // Array of meditations on page load
var searchList; // Array used to diplay search results
var finalList = []; // Final list of searched meditations to display
var themeList; // Array of themes on page load
var typeList; // Array of types on page load
var nameAutocomplete = []; // Array of all meditation names
var creatorAutocomplete = []; // Array of meditation creators
var urlMVC = "http://localhost:5002/Meditation/";
var urlAPI = "http://localhost:5001/api/";

// Display all meditations on page load
function setupPage() {
    var url = urlMVC + "GetShopList";
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        displayMeditations(data);
        originalList = data;
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#store-items').text(jqXHR.responseText || textStatus);
    });
}

// Search available meditaitons
function searchMeditations() {
    searchList = originalList;
    finalList.length = 0;
    var searchResult;
    var listNum;
    var nameSearch = $("#MeditationName").val();
    var creatorSearch = $("#MediationCreator").val();
    var themeSearch = $("#MeditationTheme").val();
    var typeSearch = $("#MeditationType").val();
    var searchArray = [nameSearch, creatorSearch, themeSearch, typeSearch];
    var testArray = ["", "", "", ""];
    if (JSON.stringify(testArray) !== JSON.stringify(searchArray)) {
        for (var i = 0; i < searchArray.length; i++) {
            if (searchArray[i] !== "") {
                switch (i) {
                    case 0:
                        for (var x = 0; x < searchList.length; x++) {
                            searchResult = searchList[x].MeditationName.includes(nameSearch);
                            if (searchResult === true) {
                                finalList.push(searchList[x]);
                            }
                        }
                        break;
                    case 1:
                        if (finalList.length === 0) {
                            for (x = 0; x < searchList.length; x++) {
                                if (searchList[x].MeditationCreator === creatorSearch) {
                                    finalList.push(searchList[x]);
                                }
                            }
                        }

                        else {
                            for (x = 0; x < finalList.length; x++) {
                                if (creatorSearch !== finalList[x].MeditationCreator) {
                                    delete finalList[x];
                                }
                            }

                            finalList = finalList.filter(function (e) {
                                return e !== undefined;
                            }); // Function cleans undefined elements of the array 
                        }
                        break;
                    case 2:
                        if (finalList.length === 0) {
                            for (x = 0; x < searchList.length; x++) {
                                if (searchList[x].MeditationTheme === themeSearch) {
                                    finalList.push(searchList[x]);
                                }
                            }
                        }

                        else {
                            for (x = 0; x < finalList.length; x++) {
                                if (themeSearch !== finalList[x].MeditationTheme) {
                                    delete finalList[x];
                                }
                            }

                            finalList = finalList.filter(function (e) {
                                return e !== undefined;
                            }); // Function cleans undefined elements of the array 
                        }
                        break;
                    case 3:
                        if (finalList.length === 0) {
                            for (x = 0; x < searchList.length; x++) {
                                if (searchList[x].MeditationType === typeSearch) {
                                    finalList.push(searchList[x]);
                                }
                            }
                        }

                        else {
                            for (x = 0; x < finalList.length; x++) {
                                if (typeSearch !== finalList[x].MeditationType) {
                                    delete finalList[x];
                                }
                            }

                            finalList = finalList.filter(function (e) {
                                return e !== undefined;
                            }); // Function cleans undefined elements of the array 
                        }
                        break;
                }
            }
        }

        displayMeditations(finalList);
    }
}

// Populate list of meditations on page load or after search
function displayMeditations(data) {
    var listItem = "";
    var shopList = "";

    for (var i = 0; i < data.length; i++) {
        var buttonText;
        var buttonIcon;
        var buttonClick = '"' + data[i].MeditationId + '"';

        nameAutocomplete[i] = data[i].MeditationName;
        creatorAutocomplete[i] = data[i].MeditationCreator;

        if (data[i].MeditationPurchased === true) {
            buttonText = "Details";
            buttonIcon = "info";
        }

        else {
            buttonText = "Buy";
            buttonIcon = "money";
        }
        listItem = "<div class='row'><div class='col-xs-12 col-sm-12 col-md-6 col-lg-6'>" + data[i].MeditationName + "<br />" + data[i].MeditationCreator + "</div>" +
            "<div class='col-xs-12 col-sm-12 col-md-3 col-lg-3 store-rating'><fieldset class='rating'>" +
            "<input type='radio' id='star5_" + i + "' name='rating_" + i + "' disabled value='5' /><label class='full' for='star5_" + i + "' title='5 stars'></label>" +
            "<input type='radio' id='star4_" + i + "' name='rating_" + i + "' disabled value='4' /><label class='full' for='star4_" + i + "' title='4 stars'></label>" +
            "<input type='radio' id='star3_" + i + "' name='rating_" + i + "' disabled value='3' /><label class='full' for='star3_" + i + "' title='3 stars'></label>" +
            "<input type='radio' id='star2_" + i + "' name='rating_" + i + "' disabled value='2' /><label class='full' for='star2_" + i + "' title='2 stars'></label>" +
            "<input type='radio' id='star1_" + i + "' name='rating_" + i + "' disabled value='1' /><label class='full' for='star1_" + i + "' title='1 star'></label>" +
            "</fieldset></div><div class='col-xs-12 col-sm-12 col-md-1 col-lg-1 text-center review-link' data-toggle='modal' data-target='#myModal' onclick='getReviews(" + buttonClick + ")'>reviews</div>" +
            "<div class='col-xs-12 col-sm-12 col-md-2 col-lg-2 text-center store-details'>" +
            "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='meditationModal(" + buttonClick + "," + data[i].MeditationPurchased + ")'><i class='fa fa-" + buttonIcon + "' aria-hidden='true'></i> " + buttonText + "</button>" +
            "</div></div><hr />";
        if (data[i].MeditationRating !== 0)
        {
            var roundedRating = Math.round(data[i].MeditationRating);
            var rating = "value='" + roundedRating + "'";
            var ratingShow = rating + " checked";
            listItem = listItem.replace(rating, ratingShow);
        }
        shopList = shopList + listItem;
        JSON.stringify(data[i]);
    }
    $('.store-items').html(shopList);
}

// Populate themes dropdown
function populateTheme() {
    $.ajax({
        type: "GET",
        url: urlAPI + 'meditation/themes',
        dataType: 'json'
    }).done(function (data) {
        for (var i = 0; i < data.length; i++) {
            $('#MeditationTheme').append("<option value='" + data[i] + "'>" + data[i] + "</option>");
        }
    });
}

// Populate types dropdown
function populateType() {
    $.ajax({
        type: "GET",
        url: urlAPI + 'meditation/types',
        dataType: 'json'
    }).done(function (data) {
        for (var i = 0; i < data.length; i++) {
            $('#MeditationType').append("<option value='" + data[i] + "'>" + data[i] + "</option>");
        }
    });
}

function getReviews(id) {
    var url = urlAPI + "meditation/reviews/" + id;
    var reviewList = "";
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        //console.log(data);
        $('.modal-footer').html("<button type='button' class='btn btn-default button-medium' data-dismiss='modal'>Close</button>");
        $('.modal-title').html("Meditation Reviews");
        var modalBody = "";
        if (data.length === 0) {
            $('.modal-body').html("<div class='text-center'><strong>No reviews yet</strong></div>");
        }
        else {
            for (var i = 0; i < data.length; i++) {
                var reviewItem = "<div class='text-center'>" + data[i].Username + "</div><br />" +
                    "<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12 store-rating'><fieldset class='rating'>" +
                    "<input type='radio' id='star5__" + i + "' name='rating__" + i + "' disabled value='5' /><label class='full' for='star5__" + i + "' title='5 stars'></label>" +
                    "<input type='radio' id='star4__" + i + "' name='rating__" + i + "' disabled value='4' /><label class='full' for='star4__" + i + "' title='4 stars'></label>" +
                    "<input type='radio' id='star3__" + i + "' name='rating__" + i + "' disabled value='3' /><label class='full' for='star3__" + i + "' title='3 stars'></label>" +
                    "<input type='radio' id='star2__" + i + "' name='rating__" + i + "' disabled value='2' /><label class='full' for='star2__" + i + "' title='2 stars'></label>" +
                    "<input type='radio' id='star1__" + i + "' name='rating__" + i + "' disabled value='1' /><label class='full' for='star1__" + i + "' title='1 star'></label>" +
                    "</fieldset></div><br /><div class='text-center'>" + data[i].ReviewComment + "</div><hr />";
                var rating = "value='" + data[i].ReviewStars + "'";
                var ratingShow = rating + " checked";
                reviewItem = reviewItem.replace(rating, ratingShow);
                reviewList = reviewList + reviewItem;
            }
            $('.modal-body').html(reviewList);
        }
    });
}

function meditationModal(id, purchased) {
    $.ajax({
        type: "GET",
        url: urlAPI + 'meditation/' + id,
        dataType: 'json'
    }).done(function (data) {
        var modalBody = "<span><strong>Name:</strong> " + data.MeditationName + "</span><br />" +
            "<span><strong>Type:</strong> " + data.MeditationType + "</span><br />" +
            "<span><strong>Theme:</strong> " + data.MeditationTheme + "</span><br />" +
            "<span><strong>Description:</strong> " + data.MeditationDescription + "<span>";
        var functionCall;
        var buttonIcon;
        var buttonText;

        if (purchased === true) {
            functionCall = "returnMeditation";
            buttonIcon = "ban";
            buttonText = "Return";
        }

        else {
            functionCall = "buyMeditation";
            buttonIcon = "money";
            buttonText = "Buy";
        }
        var buttonClick = '"' + id + '"';
        var modalFooter = "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='" + functionCall + "(" + buttonClick + ")'><i class='fa fa-" + buttonIcon + "' aria-hidden='true'></i> " + buttonText + "</button>";
        $('.modal-body').html(modalBody);
        $('.modal-footer').html(modalFooter);
        $('.modal-title').html("Meditation Details");
    }); 
}

function buyMeditation(id) {
    var url = urlMVC + "BuyMeditation?itemId=" + id;

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

function returnMeditation(id) {
    var url = urlMVC + "ReturnMeditation?itemId=" + id;

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

