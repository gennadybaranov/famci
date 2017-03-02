$(function(e){
   

	var canvas = $("#field")[0];
	var context = canvas.getContext('2d');

	var canvasSize = $("#field").attr("width");
	var cellScale = 20;
	var cellsNum = canvasSize / cellScale;
	var timer = 0;
	var stepNum = 0;

	$("#start").click(function(){
		timer = setInterval(step, 500);
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

	context.beginPath();
	for (var i = 0; i < canvasSize; i += cellScale)
	{
		context.moveTo(i, 0);
		context.lineTo(i, canvasSize);
		context.moveTo(0, i);
		context.lineTo(canvasSize, i);
	}
	context.stroke();

	var mas = [];
	for (var i = 0; i < 20; i++) {
		mas[i]=[]
		for (var j = 0; j < 20; j++) {
			mas[i][j]=0;
		}
	}

	$('canvas').click(function(e){

		var positionX = e.clientX, positionY = e.clientY;
		positionX -= canvas.offsetLeft;
		positionY -= canvas.offsetTop;
		positionX = Math.floor(positionX/cellScale);
		positionY = Math.floor(positionY/cellScale);
		mas[positionX][positionY] = !mas[positionX][positionY];
		context.fillStyle = mas[positionX][positionY] ? "#DE5246" : "#333333";
		context.fillRect(positionX * cellScale + 1, positionY * cellScale + 1, cellScale - 2, cellScale - 2);

	});

	function step(){
		var buffer = [];
		for (var i = 0; i < cellsNum; i++)
		{
			buffer[i]=[]
		for (var j = 0; j < 20; j++) {
			buffer[i][j]=0;
		}
		}	
		
		var matrX = [0, 0, 1, 1, 1, -1, -1, -1];
		var	matrY = [1, -1, 0, 1, -1, 0, 1, -1];
		var counter = 0;
		for (var x = 0; x < cellsNum; x++)
		{
			for (var y = 0; y < cellsNum; y++)
			{
				counter = 0;
				for (var i = 0; i < 8; i++){
					counter += mas[(x + matrX[i] + cellsNum ) % cellsNum][(y + matrY[i] + cellsNum) % cellsNum];
				}
				if (mas[x][y])
					buffer[x][y] = !(counter > 3 || counter < 2);
				else 
					buffer[x][y] = (counter == 3);

				context.fillStyle = buffer[x][y] ? "#DE5246" : "#333333";
				context.fillRect(x * cellScale + 1, y * cellScale + 1, cellScale - 2, cellScale - 2);
			}
		}
		mas = buffer;
		stepNum++;
		$("#generationNum").text(stepNum);
	}
});