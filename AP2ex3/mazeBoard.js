(function ($) {

    function drawMaze(mazeCanvas,currMaze) {
        //var mazeCanvas = $(this)[0];
        var maze = currMaze.maze;
        currMaze.context = mazeCanvas.getContext("2d");
        // Clear canvas.
        currMaze.context.clearRect(0, 0, mazeCanvas.width, mazeCanvas.height);

        var rows = maze.Rows;
        var cols = maze.Cols;
        currMaze.cellWidth = mazeCanvas.width / cols;
        currMaze.cellHeight = mazeCanvas.height / rows;
        var cellWidth = mazeCanvas.width / cols;
        var cellHeight = mazeCanvas.height / rows;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (i == maze.Start.Row && j == maze.Start.Col) {

                    currMaze.context.drawImage(minion, cellWidth * j, cellHeight * i,
                        cellWidth, cellHeight);
                    currMaze.currentRow = i;
                    currMaze.currentCol = j;
                }
                if (i == maze.End.Row && j == maze.End.Col) {
                    currMaze.context.drawImage(exit, cellWidth * j, cellHeight * i,
                        cellWidth, cellHeight);
                }

                if (maze.Maze[(i * cols) + j] == 1) {
                    currMaze.context.fillStyle = "Black";
                    currMaze.context.fillRect(cellWidth * j, cellHeight * i,
                        cellWidth, cellHeight);
                }

            }
        }
    };



    $.fn.mazeBoard = function (data, callBackOnMove,operation) {
        var currentBoard = {
            maze: data,
            rows: data.Rows,
            cols: data.Cols,
            startPos: data.Start,
            exitPos: data.End,
            minion: document.getElementById("minion"),
            exit: document.getElementById("exit"),
            currentRow: 0,
            currentCol: 0,
            context: null,
            cellWidth: 0,
            cellHeight: 0,
            gameOn: true
        };

        if (operation == "draw")
        {
            drawMaze(this[0], currentBoard);
        }

        if (callBackOnMove != null)
        {
            document.addEventListener("keydown", callBackOnMove);
        }
        return currentBoard;
    };
})(jQuery);
