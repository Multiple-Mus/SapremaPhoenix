$(document).ready(function () {
    setupPage();
    populateTheme();
    $('.modal-title').append("Class Details");
});

var originalList; // Array of class on page load
var searchList; // Array used to diplay search results
var finalList = []; // Final list of searched class to display
var themeList; // Array of themes on page load
var nameAutocomplete = []; // Array of all class names
var creatorAutocomplete = []; // Array of class creators
var url = "http://localhost:5002/Yoga/";
var urlAPI = "http://localhost:5001/api/";

// Display all Classs on page load
function setupPage() {
    var apiurl = url + "GetShopList";
    $.ajax({
        type: "GET",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        displayClass(data);
        originalList = data;
        console.log(originalList);
    }).error(function (jqXHR, textStatus, errorThrown) {
        $('#store-items').text(jqXHR.responseText || textStatus);
    });
}

// Search available meditaitons
function searchClasss() {
    searchList = originalList;
    finalList.length = 0;
    var searchResult;
    var listNum;
    var nameSearch = $("#ClassName").val();
    var creatorSearch = $("#ClassCreator").val();
    var themeSearch = $("#ClassTheme").val();
    var levelSearch = $("#ClassLevel").val();
    var searchArray = [nameSearch, creatorSearch, themeSearch, levelSearch];
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
                                console.log("test");
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

        displayClass(finalList);
    }
}

// Populate list of class's on page load or after search
function displayClass(data) {
    var listItem = "";
    var shopList = "";

    for (var i = 0; i < data.length; i++) {
        var buttonText;
        var buttonIcon;
        var buttonClick = '"' + data[i].ClassId + '"';

        nameAutocomplete[i] = data[i].ClassName;
        creatorAutocomplete[i] = data[i].ClassCreator;

        if (data[i].ClassPurchased === true) {
            buttonText = "Details";
            buttonIcon = "info";
        }

        else {
            buttonText = "Buy";
            buttonIcon = "money";
        }
        listItem = "<div class='row'><div class='col-xs-12 col-sm-12 col-md-6 col-lg-6'>" + data[i].ClassName + "<br />" + data[i].ClassCreator + "</div>" +
            "<div class='col-xs-12 col-sm-12 col-md-3 col-lg-3 store-rating'><fieldset class='rating'>" +
            "<input type='radio' id='star5' name='rating' value='5' disabled /><label class='full' for='star5' title='5 stars'></label>" +
            "<input type='radio' id='star4' name='rating' value='4' disabled /><label class='full' for='star4' title='4 stars'></label>" +
            "<input type='radio' id='star3' name='rating' value='3' disabled /><label class='full' for='star3' title='3 stars'></label>" +
            "<input type='radio' id='star2' name='rating' value='2' disabled /><label class='full' for='star2' title='2 stars'></label>" +
            "<input type='radio' id='star1' name='rating' value='1' disabled /><label class='full' for='star1' title='1 star'></label>" +
            "</fieldset></div><div class='col-xs-12 col-sm-12 col-md-1 col-lg-1 text-center review-link'>reviews</div>" +
            "<div class='col-xs-12 col-sm-12 col-md-2 col-lg-2 text-center store-details'>" +
            "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='classModal(" + buttonClick + "," + data[i].ClassPurchased + ")'><i class='fa fa-" + buttonIcon + "' aria-hidden='true'></i> " + buttonText + "</button>" +
            "</div></div><hr />";
        if (data[i].ClassRating !== 0) {
            var roundedRating = Math.round(data[i].ClassRating);
            var rating = "value='" + roundedRating + "'";
            var ratingShow = rating + " checked";
            listItem = listItem.replace(rating, ratingShow);
            //console.log(listItem);
        }
        //console.log(data[i].ClassPurchased);
        shopList = shopList + listItem;
        JSON.stringify(data[i]);
        //console.log(listItem);
    }
    //console.log(shopList);
    $('.store-items').html(shopList);
}

function populateName(data) {

    //$("#ClassName").autocomplete({
    //    source: data,
    //    select: function () {
    //        console.log('select');
    //    }
    //}).val('').data('autocomplete')._trigger('select');
}

// Populate themes dropdown
function populateTheme() {
    $.ajax({
        type: "GET",
        url: urlAPI + 'Class/themes',
        dataType: 'json'
    }).done(function (data) {
        for (var i = 0; i < data.length; i++) {
            $('#ClassTheme').append("<option value='" + data[i] + "'>" + data[i] + "</option>");
        }
    });
}

function ClassModal(id, purchased) {
    $.ajax({
        type: "GET",
        url: urlAPI + 'Class/' + id,
        dataType: 'json'
    }).done(function (data) {
        var modalBody = "<span>" + data.ClassName + "</span><br />" +
            "<span>" + data.ClassLevel + "</span><br />" +
            "<span>" + data.ClassTheme + "</span><br />" +
            "<span>Class description goes here<span>";
        var functionCall;
        var buttonIcon;
        var buttonText;

        if (purchased === true) {
            functionCall = "returnClass";
            buttonIcon = "ban";
            buttonText = "Return";
        }

        else {
            functionCall = "buyClass";
            buttonIcon = "money";
            buttonText = "Buy";
        }
        var buttonClick = '"' + id + '"';
        var modalFooter = "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='" + functionCall + "(" + buttonClick + ")'><i class='fa fa-" + buttonIcon + "' aria-hidden='true'></i> " + buttonText + "</button>";
        $('.modal-body').html(modalBody);
        $('.modal-footer').html(modalFooter);
    });
}

function buyClass(id) {
    var apiurl = url + "BuyClass/?itemId=" + id;

    $.ajax({
        type: "GET",
        url: apiurl,

        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        $('#myModal').modal('hide');
        setupPage();
    });
}

function returnClass(id) {
    var apiurl = url + "ReturnClass/?itemId=" + id;

    $.ajax({
        type: "GET",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8"
    }).done(function (data) {
        $('#myModal').modal('hide');
        setupPage();
    });
}