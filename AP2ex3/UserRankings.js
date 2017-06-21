$("nav").load("Menu.html");

function addUsersToTable(userList) {
    $(".loader").fadeOut();

    var table = document.getElementById("rankingsTable");
    var size = userList.length;
    var header = table.createTHead();
    var firstRow = header.insertRow(0);
    firstRow.insertCell(0).innerHTML = "<b>Rank </b>";
    firstRow.insertCell(1).innerHTML = "<b>User Name </b>";
    firstRow.insertCell(2).innerHTML = "<b>Wins </b>";
    firstRow.insertCell(3).innerHTML = "<b>Losses </b>";
    
    for (var i = 0; i < size; i++) {
        var row = table.insertRow(i + 1);
        var rankCell = row.insertCell(0);
        var userNameCell = row.insertCell(1);
        var winsCell = row.insertCell(2);
        var lossesCell = row.insertCell(3);
        rankCell.innerHTML = i + 1;
        userNameCell.innerHTML = userList[i].UserName;
        winsCell.innerHTML = userList[i].Wins;
        lossesCell.innerHTML = userList[i].Losses;
    }
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "/api/Users",
        success: function (userList) {
            addUsersToTable(userList);

        },
            error: function (result) { alert("error " + result[0]); }
    });
})
