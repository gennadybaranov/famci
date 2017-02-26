$(function(){
	var my_canvas = $("#canvas")[0];
    var context = my_canvas.getContext('2d');

var x=20,y=20;
for (var i=0;i<19;++i,x+=20)
{
	context.beginPath();
	context.moveTo(x,0);
	context.lineTo(x,400);
	context.stroke();
}
for(i=0;i<19;++i,y+=20)
{
	context.beginPath();
	context.moveTo(0,y);
	context.lineTo(400,y);
	context.stroke();
}

var mass=[];
for (i=0;i<20;++i)
{
	mass[i]=[];
	for (j=0;j<20;++j)
	{
		mass[i][j]=0;
	}
}

function paint()
{
	for (i=0;i<20;++i)
	{
		for (j=0;j<20;++j)
		{
			if (mass[i][j])
				context.fillStyle="green";
			else
				context.fillStyle="#333333";
			context.fillRect(i*20,j*20,19,19);
		}
	}
}
paint();
my_canvas.addEventListener("click",position,false);
function position(e)
{
	var x=Math.floor(e.clientX/20)-1;
	var y=Math.floor(e.clientY/20)-1;
	mass[x][y]=!mass[x][y];
	paint();
	
}


function check(x0,y0)
{
	var num=0;
	for (var i = x0-1;i<=x0+1;++i)
	{
		for (var j =y0-1;j<=y0+1;++j)
			if(mass[(i+20)%20][(j+20)%20]&&(i!=x0||j!=y0))
				++num;
	}
	return num;
}

var tmp=[];
for (i=0;i<20;++i)
{
	tmp[i]=[];
	for (j=0;j<20;++j)
	{
		tmp[i][j]=0;
	}
}

var geniration=function()
{
	for (i=0;i<20;++i)
	{
		for (j=0;j<20;++j)
			tmp[i][j]=mass[i][j];
	}
	for (i=0;i<20;++i)
	{
		for (j=0;j<20;++j)
		{
			var numbers = check(i,j);
			if (numbers<2||numbers>3)
				tmp[i][j]=0;
			else if(numbers==3)
				tmp[i][j]=1;
		}
	}
	for (i=0;i<20;++i)
	{
		for (j=0;j<20;++j)
			mass[i][j]=tmp[i][j];
	}
	paint();
	var cell=document.getElementById('generationNum');
	var count=Number(cell.innerHTML);
	cell.innerHTML=count+=1;
}
var start = function()
{
	myinterval = setInterval(geniration,200);
}

var stop = function()
{
	clearInterval(myinterval);
}
$('#start').click(start);
$('#stop').click(stop); 
});