var canvas = document.getElementById("field");
var ctx = canvas.getContext("2d");
var height = 20;
var width = 20;
var size = document.getElementById("field").width;
var aliveCell = "#00E676";
var deadCell = "#B9F6CA";
var num = 1;
var generNum = document.getElementById("generationNum");

function drawCanvas(ctx)
{
	for (i = 1; i < height; i++)
	{
		ctx.beginPath();
		ctx.moveTo(height*i, 0);
		ctx.lineTo(height*i, size);
		ctx.stroke();
	}
	for (i = 1; i < width; i++)
	{
		ctx.beginPath();
		ctx.moveTo(0, width*i);
		ctx.lineTo(size, width*i);
		ctx.stroke();
	}
}

drawCanvas(ctx);

var arr = [];
for (var i = 0; i < height; i++) 
{
  arr.push([]);
  for (var j  = 0; j < width; j++) 
  {
    arr[i].push(false);
  }
}

function draw() {
  for (var i = 0; i < height; i++) {
    for (var j  = 0; j < width; j++) {
      ctx.fillStyle = arr[i][j] ? aliveCell : deadCell;
      ctx.fillRect(i * height, j * width, height - 1, width - 1);
    }
  }
}

canvas.addEventListener("click", getClickPosition, false);

function getClickPosition(e) {
    var x = e.clientX;
    var y = e.clientY;

    x -= canvas.offsetLeft;
    y -= canvas.offsetTop;

    arr[Math.floor(x/height)][Math.floor(y/width)] = !arr[Math.floor(x/height)][Math.floor(y/width)];
    draw();
}

var startButton = document.getElementById("start");
var stopButton = document.getElementById("stop");

function countAliveNeighbours(x, y)
{
	var count = 0;
		var dx = [-1, 0, 1, 1, 1, 0, -1, -1];
		var dy = [-1, -1, -1, 0, 1, 1, 1, 0];
			for (var i = 0; i < 8; i++) 
			{
				if (arr[(x + dx[i] + height) % height][(y + dy[i] + width) % width]) 
				{ count++; }
			}
	return count;
}

function nextGeneration() {
	generNum.innerHTML = num++;
	var sarr = [];
	for (var i = 0; i < height; i++) 
	{
		sarr.push([]);
	  for (var j  = 0; j < width; j++) 
	  {
		  
		  if (countAliveNeighbours(i, j) == 3)
			  sarr[i].push(true);
		  if (countAliveNeighbours(i, j) == 2)
			  sarr[i].push(arr[i][j]);
		  if (countAliveNeighbours(i, j) != 3 && countAliveNeighbours(i, j) != 2)
			  sarr[i].push(false);
		  
	  }
	}
	arr = sarr;
	draw ();
}

var interval;

function start() {
  interval = setInterval(nextGeneration, 250)
}

function stop() {
  clearInterval(interval);
}

startButton.addEventListener("click", start);
stopButton.addEventListener("click", stop);

draw();
