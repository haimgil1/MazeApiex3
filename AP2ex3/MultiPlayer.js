
var multiGame = $.connection.gameHub;

var myMazeBoard;
var otherMazeBoard;
$("nav").load("Menu.html");



$body = $("body");
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});


window.onload = function () {

    if (!localStorage.length) {
        localStorage.setItem("Rows", "10");
        localStorage.setItem("Cols", "10");
    }

    $("#rows").val(localStorage.Rows);
    $("#cols").val(localStorage.Cols);
    // document.getElementById("settingRows").value = localStorage.getItem("Rows");
    //document.getElementById("settingCols").value = localStorage.getItem("Cols");
    //document.getElementById("settingAlgo").text = localStorage.getItem("Algorithm");
}


multiGame.client.sendMaze = function (recData) {
    $body.removeClass("loading");
    myMazeBoard = $("#myMazeCanvas").mazeBoard(recData, movePlayer, "draw");
    $(document).attr("title", name);
    otherMazeBoard = $("#otherMazeCanvas").mazeBoard(recData, null, "draw");
};



multiGame.client.sendDirection = function (row, col) {
    drawPreviousPos(otherMazeBoard);
    setCurrentPos(otherMazeBoard, row, col);
    if (otherMazeBoard.exitPos.Row == row && otherMazeBoard.exitPos.Col == col) {

        var userName = sessionStorage.getItem("userName");
        $.ajax({
            type: "PUT",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/Users/" + userName + "/Looser",
            success: function (recData) {
                alert("Other player won :(");
                otherMazeBoard.gameOn = false;
                window.location.replace("MainMenu.html");

            },
            error: function (result) { alert("error " + result[0]); }
        });


    }
};

multiGame.client.closeGame = function () {
    alert("Game Over");
    window.location.replace("MainMenu.html");
};


multiGame.client.sendList = function (recData) {
    loadItems(recData);
};


$("#startGame").click(function () {

    $.connection.hub.start().done(function () {
        var name = $("#mazeName").val();    
        var rows = $("#rows").val();
        var cols = $("#cols").val();

        multiGame.server.startGame(name, rows, cols);
        $body.addClass("loading");
        alert("waiting for other player to join...");
        
    });



    //$.ajax({
    //    type: "GET",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    url: "../api/MultiPlayer/StartMultiPlayer/" + name + "/" + rows + "/" + cols + "/" + Context.ConnectionId,
    //    success: function (recData) {
    //        myMazeBoard = $("#myMazeCanvas").mazeBoard(recData, movePlayer, "draw");
    //        $(document).attr("title", name);
    //        otherMazeBoard = $("#otherMazeCanvas").mazeBoard(recData, null, "draw");

    //    },
    //    error: function (result) { alert("error " + result[0]); }
    //});
});


$("#joinGame").click(function () {
    $.connection.hub.start().done(function () {
        // join to game
        var game = $("#Games").val();
        multiGame.server.joinTo(game);
    });

    //$.ajax({
    //    type: "GET",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    url: "../api/MultiPlayer/JoinGame/" + game ,
    //    success: function (recData) {
    //        $(document).attr("title", game);
    //    },
    //    error: function (result) { alert("error " + result[0]); }
    //});
});


function loadItems(items) {
    var select = $('#Games');
    select.empty();
    $.each(items, function (index, value) {
        select.append($("<option></option>").attr("value", value).text(value))
    });
};


$("#Games").click(function () {

    $.connection.hub.start().done(function () {
        multiGame.server.getList();
    });

        //$.ajax({
        //    type: "GET",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    url: "../api/MultiPlayer/GetList",
        //    success: function (recData) {
        //        loadItems(recData);
        //    },
        //    error: function (result) { alert("error " + result[0]); }
        //});
});


function movePlayer(event) {
    moveOneStep(event.keyCode);
}

function moveOneStep(key) {


    var mazeStr = myMazeBoard.maze.Maze;
    var newRow = myMazeBoard.currentRow;
    var newCol = myMazeBoard.currentCol;

    switch (key) {
        case 37:
            newCol -= 1;
            break;
        case 38:
            newRow -= 1;
            break;
        case 39:
            newCol += 1;
            break;
        case 40:
            newRow += 1;
            break;
    }

    var currPlace = newRow * myMazeBoard.cols + newCol;

    // Check if valid point.
    if (newRow >= 0 && newRow < myMazeBoard.rows && newCol >= 0 && newCol < myMazeBoard.cols
        && mazeStr[currPlace] == 0) {
        drawPreviousPos(myMazeBoard);
        setCurrentPos(myMazeBoard,newRow, newCol);
        $.connection.hub.start().done(function () {
            multiGame.server.play(newRow, newCol);
        });
    }

    // Check if got to the end point.
    if (myMazeBoard.exitPos.Row == newRow && myMazeBoard.exitPos.Col == newCol) {
        var userName = sessionStorage.getItem("userName");
        $.ajax({
            type: "PUT",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/api/Users/" + userName + "/Win",
            success: function (recData) {
                alert("You won!!!");
                myMazeBoard.gameOn = false;
                $.connection.hub.start().done(function () {
                    multiGame.server.closeTheGame(myMazeBoard.maze.Name);
                });
                window.location.replace("MainMenu.html");

            },
            error: function (result) { alert("error " + result[0]); }
        });

        window.location.replace("MainMenu.html");
    }
}



function drawPreviousPos(currMazeBoard) {

    currMazeBoard.context.fillStyle = "White";
    currMazeBoard.context.fillRect(currMazeBoard.cellWidth * currMazeBoard.currentCol
        , currMazeBoard.cellHeight * currMazeBoard.currentRow,
        currMazeBoard.cellWidth, currMazeBoard.cellHeight);
}


function setCurrentPos(currMazeBoard,newRow, newCol) {

    currMazeBoard.currentRow = newRow;
    currMazeBoard.currentCol = newCol;

    currMazeBoard.context.drawImage(currMazeBoard.minion, currMazeBoard.cellWidth * currMazeBoard.currentCol,
        currMazeBoard.cellHeight * currMazeBoard.currentRow,
        currMazeBoard.cellWidth, currMazeBoard.cellHeight);
}
