var canvas = document.getElementById("field");
var context = canvas.getContext('2d');

context.beginPath();
for (i=1;i<20;i++)
{
context.moveTo(i*20,0);
context.lineTo(i*20,400);
}
for (i=1;i<20;i++)
{
  context.moveTo(0,i*20);
  context.lineTo(400,i*20);
}
context.stroke();

var array=[];
for (i=0;i<20;i++)
{
  array.push([]);
  for (j=0;j<20;j++)
     array[i].push(false);
}


function draw()
{
  for (i=0;i<20;i++)
    for (j=0;j<20;j++)
      if (array[i][j])
      {
        context.fillStyle="Purple";
        context.fillRect(i*20,j*20,19,19);
      }
}

function getClickPosition(e)
{
  var x=e.clientX;
  var y=e.clientY;
  x=x-canvas.offsetLeft;
  y=y-canvas.offsetTop;
  array[Math.floor(x/20)][Math.floor(y/20)]=!array[Math.floor(x/20)][Math.floor(y/20)];
  draw();
}
canvas.addEventListener("click",getClickPosition,false);
