// Write your Javascript code.
function getTest() {
    var data = "this is some test data";
    var apiurl = "http://localhost:5002/home/GetData";
    $.ajax({
        type: "GET",
        url: apiurl,
        data: data,
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
    }).done(function (data) {
        console.log(data);
    })
}