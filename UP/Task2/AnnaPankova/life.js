var myCanvas = $("#canvas")[0];
var context = myCanvas.getContext('2d');

var canvasWidth = myCanvas.width;
var canvasHeight = myCanvas.height;
var horizontalSize = 20;
var verticalSize = 20;
var cellHeight = canvasHeight / verticalSize;
var cellWidth = canvasWidth / horizontalSize;

var array = [];
var interval;

var startButton = document.getElementById("start");
var stopButton = document.getElementById("stop");

var generation = document.getElementById("generationNum");
var generationNum = 1;

startButton.addEventListener("click", start);
stopButton.addEventListener("click", stop);

(function () {
  for (var i = 1; i < verticalSize; i++) {
    var horizontalLine = cellHeight;
    context.moveTo(0, horizontalLine);
    context.lineTo(canvasWidth, horizontalLine);
    horizontalLine += cellHeight;
  }

   for (var i = 1; i < horizontalSize; i++) {
   var verticalLine = cellWidth;
   context.moveTo(verticalLine, 0);
   context.lineTo(verticalLine, canvasHeight);
   verticalLine += cellWidth;
  }
})();

context.stroke();

myCanvas.addEventListener("click", getClickPosition, false);

function getClickPosition(e) {
    var xMouse = e.clientX;
    var yMouse = e.clientY;

    xMouse -= canvas.offsetLeft;
    yMouse -= canvas.offsetTop;

    array[Math.floor(xMouse/cellWidth)][Math.floor(yMouse/cellHeight)] = !array[Math.floor(xMouse/cellWidth)][Math.floor(yMouse/cellHeight)];
    redraw();
}

for (var i = 0; i < horizontalSize; i++) {
  array.push([]);
  for (var j  = 0; j < verticalSize; j++) {
    array[i].push(false);
  }
}

function redraw() {
  for (var i = 0; i < horizontalSize; i++) {
    for (var j  = 0; j < verticalSize; j++) {
      context.fillStyle = array[i][j] ? "#FFFF00" : "#888888";
      context.fillRect(i * cellWidth, j * cellHeight, cellWidth - 1, cellHeight - 1);
    }
  }
}

function step() {
    function checkAround(x, y) {
      var number = 0;
      for (var i = 0; i < 8; i++) {
        var nearestX = [-1, 0, 1, 1, 1, 0, -1, -1][i];
        var nearestY = [-1, -1, -1, 0, 1, 1, 1, 0][i];
        if (array[(x + nearestX + cellWidth) % cellWidth][(y + nearestY + cellHeight) % cellHeight]) {
          number++;
        }
      }
      return number;
    }

    var next = [];
    for (var i = 0; i < horizontalSize; i++) {
      next.push([]);
      for (var j  = 0; j < verticalSize; j++) {
        next[i].push(
          (array[i][j] && (checkAround(i, j) == 3 || checkAround(i, j) == 2)) ||
          (!array[i][j] && checkAround(i, j) == 3)
        );
      }
    }
    array = next;
    generation.innerHTML = generationNum++;
    redraw();
}

function start() {
  interval = setInterval(step, 1000);
}

function stop() {
  clearInterval(interval);
}

redraw();
