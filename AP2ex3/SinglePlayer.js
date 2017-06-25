$("nav").load("Menu.html");


window.onload = function () {

    if (!localStorage.length) {
        localStorage.setItem("Rows", "10");
        localStorage.setItem("Cols", "10");
        localStorage.setItem("Algorithm", "1");
    }

    $("#rows").val(localStorage.Rows);
    $("#cols").val(localStorage.Cols);
    $("#searchAlgorithem").val(localStorage.Algorithm);
}


var myMazeBoard;
var timer;
var counter = 0;



document.getElementById("searchAlgorithem").style.display = "inline - block";


$body = $("body");
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});


$("#startGame").click(function () {
    var name = $("#mazeName").val();
    if (name === "") {
        alert("please write a name for the game");
        return;
    }
    var rows = $("#rows").val();
    var cols = $("#cols").val();
    var operation = "draw";
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "../api/SingleGame/GenerateMaze/" + name + "/" + rows + "/" + cols,
        success: function (recData) {
            myMazeBoard = $("#mazeCanvas").mazeBoard(recData, movePlayer, operation);
            $(document).attr("title", name);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //            alert("error " + result[0]);
            if (jqXHR.status === 409) {
                alert("this game is already exist, please choose another name");
            } else {
                alert("sorry, we have some errors connecting the server, please try again later..");
            }
        }
    });
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
        drawPreviousPos();
        setCurrentPos(newRow, newCol);
    }

    // Check if got to the end point.
    if (myMazeBoard.exitPos.Row == newRow && myMazeBoard.exitPos.Col == newCol) {
        alert("You won!!!");
        myMazeBoard.gameOn = false;
        window.location.replace("MainMenu.html");
    }
}



function drawPreviousPos() {

    myMazeBoard.context.fillStyle = "White";
    myMazeBoard.context.fillRect(myMazeBoard.cellWidth * myMazeBoard.currentCol
        , myMazeBoard.cellHeight * myMazeBoard.currentRow,
        myMazeBoard.cellWidth, myMazeBoard.cellHeight);
}


function setCurrentPos(newRow, newCol) {

    myMazeBoard.currentRow = newRow;
    myMazeBoard.currentCol = newCol;

    myMazeBoard.context.drawImage(myMazeBoard.minion, myMazeBoard.cellWidth * myMazeBoard.currentCol,
        myMazeBoard.cellHeight * myMazeBoard.currentRow,
        myMazeBoard.cellWidth, myMazeBoard.cellHeight);
}

$("#solveGame").click(function () {
    var name = myMazeBoard.maze.Name;
    var algo = $("#searchAlgorithem").val();

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "../api/SingleGame/SolveMaze/" + name + "/" + algo,
        success: function (recData) {
            document.removeEventListener("keydown", movePlayer);
            solveMaze(recData);
        },
        error: function (result) { alert("error " + result[0]); }
    });
});

function solveMaze(solution) {
    timer = setInterval(moveSolution, 100, solution);
}

function moveSolution(solution) {
    if (myMazeBoard.gameOn) {
        moveOneStep(directionToKey(solution));
    }
    else {
        clearInterval(timer);
        counter = 0;
    }
}

function directionToKey(solution) {
    var char;
    switch (solution.charAt(counter)) {
        case '0':
            char = 39;
            break;
        case '1':
            char = 37;
            break;
        case '2':
            char = 40;
            break;
        case '3':
            char = 38;
            break;
    }
    counter++;
    return char;
}

