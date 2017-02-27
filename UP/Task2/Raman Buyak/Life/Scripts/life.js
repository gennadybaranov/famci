var LIFECLR="#008800"
var EMPTYCLR = "#333333";
var gener=0;
var cellSize = 20;
var canvas = $("#field")[0];
var context = canvas.getContext('2d');
var fieldWidth = $("#field").attr("width");
var fieldHeight = $("#field").attr("height");
var columns=Math.floor(fieldWidth/cellSize);
var rows=Math.floor(fieldHeight/cellSize);
context.beginPath();
for (var i = 0; i < fieldWidth; i += cellSize)
	{
		context.moveTo(i, 0);
		context.lineTo(i, fieldHeight);
	}
  for (var i = 0; i < fieldHeight; i +=cellSize)
  {
    context.moveTo(0, i);
    context.lineTo(fieldWidth, i);
  }
context.stroke();
var cellMatrix = [];
for (var i = 0; i < columns; ++i)
{
  cellMatrix.push([]);
  for (var j = 0; j < rows; ++j)
	{
		cellMatrix[i].push(false);
	}
}

$("canvas").click(function () {
	var canoffset = $(canvas).offset();
	var cellX = Math.floor((event.clientX + document.body.scrollLeft + document.documentElement.scrollLeft - Math.floor(canoffset.left))/cellSize);
	var cellY = Math.floor((event.clientY + document.body.scrollTop + document.documentElement.scrollTop - Math.floor(canoffset.top) + 1)/cellSize);
	context.fillStyle = (cellMatrix[cellX][cellY] ? EMPTYCLR : LIFECLR);
	cellMatrix[cellX][cellY]= !cellMatrix[cellX][cellY];
	context.fillRect(cellX * cellSize + 1, cellY * cellSize + 1, cellSize - 2, cellSize - 2);
});

var interval;

$("#start").click(function()
{
	interval=setInterval(genesis, 250);
});

$("#stop").click( function()
{
	clearInterval(interval);
});

function countCellPotential(x,y)
{
	var count=0;
	var chX = [-1,0,1,-1,1,-1,0,1];
	var chY = [-1,-1,-1,0,0,1,1,1];
	for(var i=0;i<8;i++)
			{
				count+=cellMatrix[(x+chX[i]+columns)%columns][(y+chY[i]+rows)%rows];
			}
	return count;
}

function genesis()
{
	var buf = [];
	for(var i=0;i<columns;i++)
	{
		buf.push([]);
		for (var j = 0; j < rows; j++)
		{
			buf[i].push(false);
		}
	}
	for(var i=0;i<columns;i++)
	{
		for(var j=0;j<rows;j++)
		{
			var pot = countCellPotential(i,j);
			if(cellMatrix[i][j])
			{
				if((pot==2)||(pot==3))
				{
					buf[i][j]=true;
				}
				else
					{
						buf[i][j]=false;
					}
				}
				else {
					if(pot==3)
					buf[i][j]=true;
				}
				context.fillStyle = (buf[i][j] ? LIFECLR : EMPTYCLR);
				context.fillRect(i*cellSize + 1 , j*cellSize + 1, cellSize - 2, cellSize - 2);
			}
		}
	cellMatrix=buf;
	gener++;
	$("#generationNum").text(gener);
}
