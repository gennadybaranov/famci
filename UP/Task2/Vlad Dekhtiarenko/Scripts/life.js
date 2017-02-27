var pixelSize = 20;
var numCells = 20;
var canvas = document.getElementById('field');
var ctx = canvas.getContext('2d');
var color = 'red';
var Arr = makeArr();
var interval;
var generation = 0;

function makeArr() {
        var Arr = [];
        for(var i = 0; i < numCells; i++){
                var inArr = [];
                for(var j = 0; j < numCells; j++) {
                        inArr.push(0);
                }
                Arr.push(inArr);
        }
        drawField(Arr);
        return Arr;
}
function drawField(Arr) {
        for(var x = 0 ; x < numCells; x++) {
                for(var y = 0; y < numCells; y++) {
                        ctx.beginPath();
                        ctx.rect(x*pixelSize,y*pixelSize,pixelSize,pixelSize);
                        if(Arr[x][y] == 0) {
                                ctx.fillStyle = '#333333';
                        }
                        else {
                                ctx.fillStyle = color;
                        }
                        ctx.fill();
                        ctx.stroke();
                }
        }
}
canvas.addEventListener("click", getClickPosition, false);
function getClickPosition (e) {
        var mouseX = e.clientX - canvas.offsetLeft;
        var mouseY = e.clientY - canvas.offsetTop;
        if((Math.floor(mouseX/pixelSize) <= numCells) && (Math.floor(mouseY/pixelSize) <= numCells)){
                Arr[Math.floor(mouseX/pixelSize)][Math.floor(mouseY/pixelSize)] = 1;
                drawField(Arr);
        }
}

function aliveNeighbors(Arr,x,y) {
        var amountAlive = 0;
        if(x != 0 && x != (numCells - 1)){
        var dx = [-1,-1,-1,0,0,1,1,1];
        }
        else if(x == 0){
                var dx = [numCells - 1,numCells - 1, numCells -1,0,0,1,1,1];
        }
        else if(x == (numCells - 1)){
                var dx = [-1,-1,-1,0,0,-(numCells - 1),-(numCells - 1),-(numCells - 1)];
        }
        if(y != 0 && y != (numCells - 1)){
        var dy = [-1,0,1,-1,1,-1,0,1];
        }
        else if(y == 0){
                var dy = [numCells - 1,0,1,numCells - 1,1,numCells - 1,0,1];
        }
        else if(y == (numCells - 1)){
                var dy = [-1,0,-(numCells - 1),-1,-(numCells - 1),-1,0,-(numCells - 1)];
        }
        for(var i = 0; i < 8; i++) {
                amountAlive += Arr[(x + dx[i])][(y + dy[i])];
        }
        return amountAlive;
}

function randomize(Arr) {
        for(var x = 0; x < numCells; x++)
                for(var y = 0 ; y < numCells; y++){
                                Arr[x][y]=(Math.floor(Math.random() * (1 - 0 + 1)) + 0);
                                color = "#"+((1<<24)*Math.random()|0).toString(16);
                }
                drawField(Arr);
}

function step(Arr) {
    generation++;
    var newArr = makeArr();
    for(var x = 0; x < numCells; x++)
        for(var y = 0; y < numCells; y++) {
            var cell = Arr[x][y];
            var neighbors = aliveNeighbors(Arr,x,y);
            if(cell == 0 && neighbors == 3){
                newArr[x][y] = 1;
                }
            if(cell == 1 && (neighbors == 3 || neighbors == 2)){
                newArr[x][y] = 1;
            }
            if(neighbors < 2 || neighbors > 3){
                newArr[x][y] = 0;
            }
        }
        
        return newArr;
}
var startButton = document.getElementById("start");
var stopButton = document.getElementById("stop");
var randomButton = document.getElementById("random");
randomButton.addEventListener('click',function () {randomize(Arr);});
startButton.addEventListener('click',start);
stopButton.addEventListener('click',stop);
function start() {
    interval = setInterval(function(){
        var newArr = step(Arr);
        Arr = newArr;
        drawField(newArr);
        document.getElementById('gener').innerHTML = generation;
    }, 50);

}

function stop() {
    clearInterval(interval);
}
