;
$(function(e){

	var canvas = $("#field")[0];
	var startButton = $("#start")[0];
	var stopButton = $("#stop")[0];
	var ctx = canvas.getContext('2d');
	var filledCellColor = "#4EFC4E";
	var emptyCellColor = "#333333";
	var cellSize = 20;
	var canvasWidth = $("#field").attr("width");
	var canvasHeight = $("#field").attr("height");
	var cellsCountWidth = canvasWidth / cellSize;
	var cellsCountHeight = canvasHeight / cellSize;

	ctx.beginPath();
	for(var i = 0; i < canvasWidth; i += cellSize){
			ctx.moveTo(i, 0);
			ctx.lineTo(i, canvasHeight);
	}
	for(var i = 0; i < canvasHeight; i += cellSize){
			ctx.moveTo(0, i);
			ctx.lineTo(canvasWidth, i);
	}
	ctx.stroke();

	var cells = [];
	for(var i = 0; i < cellsCountWidth; ++i){
		cells.push([]);
		for(var j = 0; j < cellsCountHeight; ++j){
			cells[i].push(0);
		}
	}

	$("canvas").click(function(e){
		var X = Math.floor((e.pageX - $(this).offset().left) / cellSize);
		var Y = Math.floor((e.pageY - $(this).offset().top) / cellSize);
		cells[X][Y] = !cells[X][Y];
		ctx.fillStyle = cells[X][Y] ? filledCellColor : emptyCellColor;
		ctx.fillRect(X * cellSize + 1, Y * cellSize + 1, cellSize - 2, cellSize - 2);
	});

	var timer;
	var stepNum = 0;

	function step(){
		var temp = [];
		for(var i = 0; i < cellsCountWidth; ++i){
			temp.push([]);
			for(var j = 0; j < cellsCountHeight; ++j){
				temp[i].push(0);
			}
		}

		var dx = [-1, -1, -1, 0, 0, 1, 1, 1];
		var dy = [-1, 0, 1, -1, 1, -1, 0, 1];
		var count;
		for(var i = 0; i < cellsCountWidth; ++i){
			for(var j = 0; j < cellsCountHeight; ++j){
				count = 0;
				for (var k = 0; k < 8; ++k){
					count += cells[(i + dx[k] + cellsCountWidth) % cellsCountWidth][(j + dy[k] + cellsCountHeight) % cellsCountHeight];
				}
				if (cells[i][j])
				{
					temp[i][j] = !(count < 2 || count > 3);
				}
				else
				{
					temp[i][j] = (count == 3);
				}
				ctx.fillStyle = temp[i][j] ?  filledCellColor : emptyCellColor;
				ctx.fillRect(i * cellSize + 1, j * cellSize + 1, cellSize - 2, cellSize - 2);
			}
		}

		++stepNum;
		$("#generationNum").text(stepNum);
		cells = temp;
	}

	$("#start").click(function(){

		timer = setInterval(step, 100);
	});

	$("#start").hover(function(){
		$(this).css("cursor" ,"pointer");
	});

	$("#stop").click(function(){
		clearInterval(timer);
	});

	$("#stop").hover(function(){
		$(this).css("cursor" ,"pointer");
	});
});