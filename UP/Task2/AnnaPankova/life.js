var my_canvas = $("#canvas")[0];
var context = my_canvas.getContext('2d');

var y = 20;
var x = 20;
for (var i = 1; i < 20; i++) {
  context.moveTo(0, y);
  context.lineTo(400, y);
  y += 20;
}

for (var i = 1; i < 20; i++) {
  context.moveTo(x, 0);
  context.lineTo(x, 400);
  x += 20;
}

/*
my_canvas.addEventListener("mousedown", getPosition, false);

function getPosition(event)
{
  var x = event.x;
  var y = event.y;

  var canvas = document.getElementById("canvas");

  x -= canvas.offsetLeft;
  y -= canvas.offsetTop;

  alert("x:" + x + " y:" + y);
}*.

 /*
$('.canvas').bind('click', function (ev) {

  var $div =(ev.target);
  var $display = $div.find('.display');

  var offset = $div.offset();
  var x = ev.clientX - offset.left;
  var y = ev.clientY - offset.top;

  $display.text('x: ' + x + ', y: ' + y);
});*/

context.stroke();

my_canvas.addEventListener("click", getClickPosition, false);

function getClickPosition(e) {
    var x = e.clientX;
    var y = e.clientY;

    x -= canvas.offsetLeft;
    y -= canvas.offsetTop;

    array[Math.floor(x/20)][Math.floor(y/20)] = !array[Math.floor(x/20)][Math.floor(y/20)];
    redraw();
}

var array = [];
for (var i = 0; i < 20; i++) {
  array.push([]);
  for (var j  = 0; j < 20; j++) {
    array[i].push(false);
  }
}

function redraw() {
  for (var i = 0; i < 20; i++) {
    for (var j  = 0; j < 20; j++) {
      context.fillStyle = array[i][j] ? "#FFFF00" : "#888888"
      context.fillRect(i * 20, j * 20, 19, 19);
    }
  }
}
var startButton = document.getElementById("start");
var stopButton = document.getElementById("stop");


function step() {
    function checkAround(x, y) {
      var number = 0;
      for (var i = 0; i < 8; i++) {
        var dx = [-1, 0, 1, 1, 1, 0, -1, -1][i]
        var dy = [-1, -1, -1, 0, 1, 1, 1, 0][i]
        if (array[(x + dx + 20) % 20][(y + dy + 20) % 20]) {
          number++;
        }
      }
      return number;
    }

    var next = [];
    for (var i = 0; i < 20; i++) {
      next.push([]);
      for (var j  = 0; j < 20; j++) {
        next[i].push(
          (array[i][j] && (checkAround(i, j) == 3 || checkAround(i, j) == 2)) ||
          (!array[i][j] && checkAround(i, j) == 3)
        );
      }
    }

    array = next;
    redraw();
}

var interval

function start() {
  interval = setInterval(step, 1000)
}

function stop() {
  clearInterval(interval);
}


startButton.addEventListener("click", start);
stopButton.addEventListener("click", stop);


redraw();
