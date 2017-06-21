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
        error: function (result) { alert("error " + result[0]); }
    });
});


//(function ($) {

//    function drawMaze(mazeCanvas, currMaze) {

//            var maze = currMaze.maze;
//            currMaze.context = mazeCanvas.getContext("2d");
//            var rows = maze.Rows;
//            var cols = maze.Cols;
//            currMaze.cellWidth = mazeCanvas.width / cols;
//            currMaze.cellHeight = mazeCanvas.height / rows;
//            var cellWidth = mazeCanvas.width / cols;
//            var cellHeight = mazeCanvas.height / rows;
//            for (var i = 0; i < rows; i++) {
//                for (var j = 0; j < cols; j++) {
//                    if (i == maze.Start.Row && j == maze.Start.Col) {

//                        currMaze.context.drawImage(minion, cellWidth * j, cellHeight * i,
//                            cellWidth, cellHeight);
//                        currMaze.currentRow = i;
//                        currMaze.currentCol = j;
//                    }
//                    if (i == maze.End.Row && j == maze.End.Col) {
//                        currMaze.context.drawImage(exit, cellWidth * j, cellHeight * i,
//                            cellWidth, cellHeight);
//                    }

//                    if (maze.Maze[(i * cols) + j] == 1) {
//                        currMaze.context.fillStyle = "Black";
//                        currMaze.context.fillRect(cellWidth * j, cellHeight * i,
//                            cellWidth, cellHeight);
//                    }

//                }
//            }
//    };

//    function clearMaze(mazeCanvas, currMaze) {
//        var maze = currMaze.maze;
//        currMaze.context = mazeCanvas.getContext("2d");
//        var rows = maze.Rows;
//        var cols = maze.Cols;
//        currMaze.cellWidth = mazeCanvas.width / cols;
//        currMaze.cellHeight = mazeCanvas.height / rows;
//        var cellWidth = mazeCanvas.width / cols;
//        var cellHeight = mazeCanvas.height / rows;
//        for (var i = 0; i < rows; i++) {
//            for (var j = 0; j < cols; j++) {
//                //currMaze.context.strokeStyle = "#White";
//                //currMaze.context.strokeRect(cellWidth * j, cellHeight * i,
//                //    cellWidth, cellHeight);
//                currMaze.context.fillStyle = "White";
//                currMaze.context.fillRect(cellWidth * j, cellHeight * i,
//                    cellWidth, cellHeight);

//            }
//        }
//    };


//    $.fn.mazeBoard = function (data, callBackOnMove) {
//        var currentBoard = {
//            maze: data,
//            rows: data.Rows,
//            cols: data.Cols,
//            startPos: data.Start,
//            exitPos: data.End,
//            minion: document.getElementById("minion"),
//            exit: document.getElementById("exit"),
//            currentRow: 0,
//            currentCol: 0,
//            context: null,
//            cellWidth: 0,
//            cellHeight: 0,
//            gameOn: true
//        };
//        clearMaze(this[0], currentBoard);
//        drawMaze(this[0], currentBoard);
//        document.addEventListener("keydown", callBackOnMove);
//        return currentBoard;
//    };
//})(jQuery);



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
    var name = $("#mazeName").val();
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
            char= 39;
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

