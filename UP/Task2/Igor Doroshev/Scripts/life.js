$(function(e){
   

	var canvas = $("#field")[0];
	var context = canvas.getContext('2d');

 	var filledCellColor = "#FF9900";
    var emptyCellColor = "#333333";
	var cellSize = 20;
	var canvasSize = $("#field").attr("width");
	var cellsCount = canvasSize / cellSize;
	context.beginPath();
	for (var i = 0; i < canvasSize; i += cellSize)
	{
		context.moveTo(i, 0);
		context.lineTo(i, canvasSize);
		context.moveTo(0, i);
		context.lineTo(canvasSize, i);
	}
	context.stroke();

	var cells = [];
	for (var i = 0; i < cellsCount; i++)
	{
		cells.push([]);
		for (var j = 0; j < cellsCount; j++) cells[i].push(0);
	}

	$("canvas").click(function(e)
		{ 
			var posX = e.pageX - $(this).offset().left, 
				posY = e.pageY-$(this).offset().top;
			posX = Math.floor(posX / cellSize);
			posY = Math.floor(posY / cellSize);
			cells[posX][posY] = !cells[posX][posY];
			context.fillStyle = cells[posX][posY] ? filledCellColor : emptyCellColor;
			context.fillRect(posX * cellSize + 1, posY * cellSize + 1, cellSize - 2, cellSize - 2);
		});


	function step()
	{
		var buf = [];
		for (var i = 0; i < cellsCount; i++)
		{
			buf.push([]);
			for (var j = 0; j < cellsCount; j++) buf[i].push(0);
		}	
		
		var dx = [0, 0, 1, 1, 1, -1, -1, -1];
		var	dy = [1, -1, 0, 1, -1, 0, 1, -1];
		var count = 0;
		for (var x = 0; x < cellsCount; x++)
		{
			for (var y = 0; y < cellsCount; y++)
			{
				count = 0;
				for (var i = 0; i < 8; i++) count += cells[(x + dx[i] + cellsCount ) % cellsCount][(y + dy[i] + cellsCount) % cellsCount];
				if (cells[x][y])
				{
					buf[x][y] = !(count < 2 || count > 3);
				} 
				else 
				{
					buf[x][y] = (count == 3);
				}
				context.fillStyle = buf[x][y] ?  filledCellColor : emptyCellColor;
				context.fillRect(x * cellSize + 1, y * cellSize + 1, cellSize - 2, cellSize - 2);
			}
		}
		cells = buf;
		stepNum++;
		$("#generationNum").text(stepNum);
	}



	var timer;
	var stepNum = 0;

	$("#start").click(function(){

		timer = setInterval(step, 200);
	});

	$("#start").hover(function(){
		$(this).css("cursor" , "pointer");
	});

	$("#stop").click(function(){
		clearInterval(timer);
	});

	$("#stop").hover(function(){
		$(this).css("cursor" , "pointer");
	});
});