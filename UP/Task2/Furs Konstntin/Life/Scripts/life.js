$(function(){

	var iter = 0;
	var canvas  = $("#field")[0];
    var context = canvas.getContext('2d');

 	context.beginPath();  
	for ( i = 1; i < 20; ++i ) 
	{
    	context.moveTo( i * 20, 0 );
   	 	context.lineTo( i * 20, 400 );
    	context.moveTo( 0, i * 20 );
    	context.lineTo( 400, i * 20 );
	}
	context.stroke();

	var parts = [];               
	for ( i = 0; i < 20; ++i)     
	{
		parts.push([]);
		for( j = 0; j < 20; ++j)
			parts[i].push(0);
	}

	var temp = [];
	for ( i = 0; i < 20; ++i )
	{
		temp.push([]);
		for ( j = 0; j < 20; ++j )
			temp[i].push(0);
	}

	function draw()
	{
		for ( i = 0; i < 20; ++i)
		{
			for ( j = 0; j < 20; ++j)
			{
				if(parts[i][j])
					context.fillStyle = "red";   
				else
					context.fillStyle = "#333333"; 
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
		parts[x][y] = !parts[x][y];
		draw(); 
	}

	function search ( a, b )
	{
		var counter = 0;
		for ( var i = a - 1; i <= a + 1; ++i )
		{
			for ( var j = b - 1; j <= b + 1; ++j )
			{
				if ((i != a || j != b) && parts[( i + 20 ) % 20][( j + 20 ) % 20])
					++counter;
			}
		}
		return counter;
	}


	var event = function()
	{
		for ( i = 0; i < 20; ++i )
		{
			for ( j = 0; j < 20; ++j )
			{
				temp[i][j] = parts[i][j];
				var num = search( i, j );
				if ( num < 2 || num > 3 )	
					temp[i][j] = 0;
				if ( num == 3 )
					temp[i][j] = 1;
			}
		}
		for ( i = 0; i < 20; ++i )
		{
			for ( j = 0; j < 20; ++j )
			{
				parts[i][j] = temp[i][j];
			}
		}
		draw();
		iter+= 1;
		$("#generationNum").text(iter);
	}

	$('#start').click(function(){
		time = setInterval(event, 400);
	});

	$('#stop').click(function(){
		clearInterval(time);
	});
});