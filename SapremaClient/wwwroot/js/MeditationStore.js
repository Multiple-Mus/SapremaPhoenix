$(document).ready(function() {
    setupPage();
    populateTheme();
    populateType();
    $('.modal-title').append("Meditation Details");
});

var originalList; // Array of meditations on page load
var searchList; // Array used to diplay search results
var finalList = []; // Final list of searched meditations to display
var themeList; // Array of themes on page load
var typeList; // Array of types on page load
var nameAutocomplete = []; // Array of all meditation names
var creatorAutocomplete = []; // Array of meditation creators
var url = "http://localhost:5002/Meditation/";
var urlAPI = "http://localhost:5001/api/";

// Display all meditations on page load
function setupPage() {
    var apiurl = url + "GetShopList";
    $.ajax({
        type: "GET",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        displayMeditations(data);
        originalList = data;
        console.log(originalList);
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
                            for (var x = 0; x < searchList.length; x++) {
                                if (searchList[x].MeditationCreator === creatorSearch) {
                                    finalList.push(searchList[x]);
                                }
                            }
                        }

                        else {
                            for (var x = 0; x < finalList.length; x++) {
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
                            for (var x = 0; x < searchList.length; x++) {
                                if (searchList[x].MeditationTheme === themeSearch) {
                                    finalList.push(searchList[x]);
                                }
                            }
                        }

                        else {
                            for (var x = 0; x < finalList.length; x++) {
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
                            for (var x = 0; x < searchList.length; x++) {
                                if (searchList[x].MeditationType === typeSearch) {
                                    finalList.push(searchList[x]);
                                }
                            }
                        }

                        else {
                            for (var x = 0; x < finalList.length; x++) {
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
            "<input type='radio' id='star5' name='rating' value='5' disabled /><label class='full' for='star5' title='5 stars'></label>" +
            "<input type='radio' id='star4' name='rating' value='4' disabled /><label class='full' for='star4' title='4 stars'></label>" +
            "<input type='radio' id='star3' name='rating' value='3' disabled /><label class='full' for='star3' title='3 stars'></label>" +
            "<input type='radio' id='star2' name='rating' value='2' disabled /><label class='full' for='star2' title='2 stars'></label>" +
            "<input type='radio' id='star1' name='rating' value='1' disabled /><label class='full' for='star1' title='1 star'></label>" +
            "</fieldset></div><div class='col-xs-12 col-sm-12 col-md-1 col-lg-1 text-center review-link'>reviews</div>" +
            "<div class='col-xs-12 col-sm-12 col-md-2 col-lg-2 text-center store-details'>" +
            "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='meditationModal(" + buttonClick + "," + data[i].MeditationPurchased + ")'><i class='fa fa-" + buttonIcon + "' aria-hidden='true'></i> " + buttonText + "</button>" +
            "</div></div><hr />";
        if (data[i].MeditationRating !== 0)
        {
            var roundedRating = Math.round(data[i].MeditationRating);
            var rating = "value='" + roundedRating + "'";
            var ratingShow = rating + " checked";
            listItem = listItem.replace(rating, ratingShow);
            //console.log(listItem);
        }
        //console.log(data[i].MeditationPurchased);
        shopList = shopList + listItem;
        JSON.stringify(data[i])
        //console.log(listItem);
    }
    //console.log(shopList);
    $('.store-items').html(shopList);

    // AUTOCOMPLETE code
    //populateName(nameAutocomplete);
    //$("#MeditationName").autocomplete({
    //    source: nameAutocomplete,
    //    minLength: 3,
    //    select: function () {
    //        //console.log("select");
    //    }
    //}).val("").data("autocomplete")._trigger("select");
    //$('#MeditationName').autocomplete({
    //    source: nameAutocomplete,
    //    select: function () {
    //        console.log('select');
    //    }
    //}).val("").data()._trigger('select');
    //$("#MeditationName").autocomplete(nameAutocomplete);
    //$("#MeditationCreator").autocomplete(creatorAutocomplete);
}

function populateName(data) {

    //$("#MeditationName").autocomplete({
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
        url: urlAPI + 'meditation/themes',
        dataType: 'json',
    }).done(function (data) {
        for (var i = 0; i < data.length; i++) {
            $('#MeditationTheme').append("<option value='" + data[i] + "'>" + data[i] + "</option>");
        }
    })
}

// Populate types dropdown
function populateType() {
    $.ajax({
        type: "GET",
        url: urlAPI + 'meditation/types',
        dataType: 'json',
    }).done(function (data) {
        for (var i = 0; i < data.length; i++) {
            $('#MeditationType').append("<option value='" + data[i] + "'>" + data[i] + "</option>");
        }
    });
}

function meditationModal(id, purchased) {
    $.ajax({
        type: "GET",
        url: urlAPI + 'meditation/' + id,
        dataType: 'json',
    }).done(function (data) {
        var modalBody = "<span>" + data.MeditationName + "</span><br />" +
            "<span>" + data.MeditationType + "</span><br />" +
            "<span>" + data.MeditationTheme + "</span><br />" +
            "<span>Meditation description goes here<span>";
        var functionCall;
        var buttonIcon;
        var buttonText;

        if (purchased == true) {
            functionCall = "returnMeditation";
            buttonIcon = "ban";
            buttonText = "Return";
        }

        else {
            functionCall = "buyMeditation";
            buttonIcon = "money";
            buttonText = "Buy"
        }
        var buttonClick = '"' + id + '"';
        var modalFooter = "<button class='btn btn-default button-medium' data-toggle='modal' data-target='#myModal' onclick='" + functionCall + "(" + buttonClick + ")'><i class='fa fa-" + buttonIcon + "' aria-hidden='true'></i> " + buttonText + "</button>";
        $('.modal-body').html(modalBody);
        $('.modal-footer').html(modalFooter);
    }) 
}

function buyMeditation(id) {
    var apiurl = url + "BuyMeditation/?itemId=" + id;

    $.ajax({
        type: "GET",
        url: apiurl,

        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        $('#myModal').modal('hide');
        setupPage();
    });
}

function returnMeditation(id) {
    var apiurl = url + "ReturnMeditation/?itemId=" + id;

    $.ajax({
        type: "GET",
        url: apiurl,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        $('#myModal').modal('hide');
        setupPage();
    });

    //$.ajax({
    //    type: "DELETE",
    //    url: 'http://localhost:5001/api/usermeditations/shopdelete/cbdf1836-1c05-40bf-8505-245fc8bcf17e/' + id,
    //    dataType: 'json'
    //}).done(function (data) {
    //    $('#myModal').modal('hide');
    //    setupPage();
    //}) ;
}

// ******************************** Attempted code at search functionality
                //for (var x = 0; x < searchList.length; x++) {
            //    while (searchList[x].MeditationName.includes(nameSearch) != true) {
            //        searchList.splice(x, 1);
            //        console.log("test");
            //    }
            //}

                //searchResult = searchList[listNum].MeditationName.includes(nameSearch);
            ////console.log("not empyt");
            //if (i == 0 && searchResult == false) {
            //    delete searchList[listNum];
            //    listnum++;
            //    console.log()
            //}
            //else {
            //    listnum++;
            //}

            //switch (i) {
            //    case 0:
            //        searchResult = searchlist[listNum].MeditationName.includes(nameSearch);
            //        if (searchResult == true) {
            //            //console.log(searchlist[listNum]);
            //            delete searchResult[listNum];
            //            listNum++;
            //        }
            //}
            //for (car x = 0; x < searchList.length)
            //switch (i) {
            //    case 0:
            //        searchResult = searchList[x].MeditationName.includes(nameSearch);

            //}
    //for (var i = 0; i < searchList.length; i++) {
    //    for (var x = 0; x < searchArray.length; x++) {
    //        //console.log(searchArray[x]);
    //        if (searchArray[x] != "") {
    //            switch (x) {
    //                case 0:
    //                    var searchResult = searchList[i].MeditationName.includes(searchArray[x]);
    //                    console.log(searchResult);
    //                    if (searchResult == true) {

    //                    }

    //                    else {

    //                    }
    //            }
    //        }
    //    }
    //}

    //for (var i = 0; i < searchArray.length; i++) {
    //    if (searchArray[i] != "") {
    //        for (var x = 0; x < searchList.length; x++) {
    //            //console.log(x);
    //            var searchTest;
    //            var searchResult;
    //            switch (i) {
    //                case 0:
    //                    searchTest = searchList[x].MeditationName.includes(searchArray[i]);   //searchTest.includes(JSON.stringify(originalList[i]));
    //                    //console.log(searchTest);
    //                    if (nameSearch != "" && searchTest == true) {
    //                        //searchList.append(originalList[x]);
    //                        //console.log(originalList[x]);
    //                    }
    //            }
    //        }
    //    }
    //}

    //for (var x = 0; x < finalList.Lenght; x++) {
                        //    //searchResult = finalList[x].MeditationTheme.equals(themeSearch);
                        //    //while (finalList[x].Meditationtheme !== themeSearch) {
                        //    //    finalList.splice(x, 1);
                        //    //}

                        //    if (finalList[x].MeditationTheme == themeSearch) {
                        //        console.log("fhhfdfhd");
                        //    }

                        //    else {
                        //        console.log("made it");
                        //        delete finalList[i];
                        //    }
                        //}