$(function(){
	var canvas  = $("#field")[0];
    var context = canvas.getContext('2d');


context.beginPath();  
for ( i = 1; i < 20; ++i ) 
{
    context.moveTo( i * 20, 0 );
    context.lineTo( i * 20, 400 );
    context.lineWidth = 1;
}
for ( i = 1; i < 20; ++i ) 
{
    context.moveTo( 0, i * 20 );
    context.lineTo( 400, i * 20 );
    context.lineWidth = 1;
}
context.stroke();

var array = [];               
for ( i = 0; i < 20; ++i)     
{
	array.push([]);
	for( j = 0; j < 20; ++j)
	{
		array[i].push(0);
	}
}

function draw()
{
	for ( i = 0; i < 20; ++i)
	{
		for ( j = 0; j < 20; ++j)
		{
			if(array[i][j])
			{ 
				context.fillStyle = "green";   
			}
			else
			{ 
				context.fillStyle = "#333333"; 
			}
			context.fillRect( i * 20, j * 20, 19, 19 )
		}
	}
}
draw();


canvas.addEventListener( "click", position, false )
function position(a)
{
	var x = Math.floor ( a.clientX / 20 ) - 1;
	var y = Math.floor ( a.clientY / 20 ) - 1;
	array[x][y] = !array[x][y];
	draw(); 
}

function searchNeighbors ( x, y )
{
	var count = 0;
	for ( var i = x - 1; i <= x + 1; ++i )
	{
		for ( var j = y - 1; j <= y + 1; ++j )
		{
			if (array[(i + 20) % 20 ][(j + 20) % 20] && (i != x || j != y))
			{
				++count;
			}
		}
	}
	return count;
}

var temp = [];
for ( i = 0; i < 20; ++i )
{
	temp.push([]);
	for ( j = 0; j < 20; ++j )
	{
		temp[i].push(0);
	}
}

var generation = function()
{
	for ( i = 0; i < 20; ++i )
	{
		for ( j = 0; j < 20; ++j )
		{
			temp[i][j] = array[i][j];
		}
	}

	for ( i = 0; i < 20; ++i )
	{
		for ( j=0; j < 20; ++j )
		{
			var num = searchNeighbors( i, j );
			if ( num < 2 || num > 3 )
			{	
				temp[i][j] = 0;
			}
			else if ( num == 3 )
			{
				temp[i][j] = 1;
			}
		}
	}

	for ( i = 0; i < 20; ++i )
	{
		for ( j = 0; j < 20; ++j )
		{
			array[i][j] = temp[i][j];
		}
	}

	draw();
	var cell = document.getElementById('generationNum');
	var count = Number(cell.innerHTML);
	cell.innerHTML = count += 1;
}

var start = function()
{
	interval = setInterval(generation, 200);
}

var stop = function()
{
	clearInterval(interval);
}

$('#start').click(start);
$('#stop').click(stop);
});