(function () {
  var tablecnt=0;
var header = document.createElement('div');
header.textContent='Title / Header of Lane';
document.body.appendChild(header);
header.className="title";
var data = window.poc.xmldata;
var parser = new window.DOMParser();
var xmlDOM = parser.parseFromString(data, 'text/xml');
var wrapper=document.createElement('div');
document.body.appendChild(wrapper);
wrapper.className="wrapper";
for(i=0;i<xmlDOM.children[0].children.length;i++)
{
  var dataTable = xmlDOM.children[0].children[i];
  var layer = document.createElement("div");
  layer.className="wrappElem";
  wrapper.appendChild(layer);
  var table = document.createElement("table");
  layer.appendChild(table);
  var rowArr = dataTable.getElementsByTagName("row");
  var columnArr = dataTable.getElementsByTagName("column");

  var initRow = document.createElement("tr");
  var initTD = document.createElement("td");
  initRow.appendChild(initTD);
  initTD.colSpan=columnArr.length;
  initRow.className="headerRow";
  initTD.textContent=dataTable.attributes.name.textContent;
  table.appendChild(initRow);

  var headerRow=document.createElement("tr");
  for(j=0;j<columnArr.length;j++)
  {
    var header = document.createElement("th");
    header.textContent=columnArr[j].attributes.label.textContent;
    header.attributes.name=columnArr[j].attributes.name;
    headerRow.appendChild(header);
  }
  table.appendChild(headerRow);

  for(j=0;j<rowArr.length;j++)
  {
    var txtname=document.createElement('td');
    txtname.className="company";
    txtname.textContent=rowArr[j].attributes.name.textContent;
    txtname.colSpan=columnArr.length;
    table.appendChild(txtname);
    var line=document.createElement('tr');
    var price =document.createElement('td');
    var change=document.createElement('td');
    var percentChange=document.createElement('td');
    price.textContent=rowArr[j].attributes.price.textContent;
    change.textContent=rowArr[j].attributes.change.textContent;
    if (change.textContent[0]=='-')
      change.className="redtext";
      else change.className="greentext";
    percentChange.textContent=rowArr[j].attributes.percentChange.textContent;
    if (percentChange.textContent[0]=='-')
      percentChange.className="redtext";
      else percentChange.className="greentext";
    line.appendChild(price);
    line.appendChild(change);
    line.appendChild(percentChange);
    table.appendChild(line);
  }
  tablecnt++;
}
var footer = document.createElement("div");
footer.className="footer";
document.body.appendChild(footer);

  var tablepx= 315;
  var tableCap = Math.floor(window.innerWidth/tablepx);

function scrollTo(num)
{
  var margin = num * tablepx +5;
  wrapper.style.transform = 'translate3D(-' +margin + 'px,0px,0px)';
  for (var j = 0; j < wrapper.children.length; j++) {
    if (wrapper.children[j].offsetLeft - margin > window.innerWidth - tablepx)
      wrapper.children[j].style.opacity = 0
     else
      wrapper.children[j].style.opacity = 1
  }
}

var curTable=0;

function activateButton(i)
{
  for(var k=0;k<footer.children.length;k++)
  {
    footer.children[k].style="background-color: #E3E3E3;";
  }
  footer.children[i].style="background-color: #3D3D3D;";
  curTable=i*tableCap;
}

function rebutton()
{
  var buttonArr=document.getElementsByClassName('footer')[0];
  while(buttonArr.children.length>0)
  {
    buttonArr.removeChild(buttonArr.children[0]);
  }
  tableCap = Math.floor(window.innerWidth/tablepx);
  for(var j=0;j<Math.ceil(tablecnt/tableCap);j++)
  {
    var scr=  document.createElement("div");
    scr.className="scroll";
    (function (j){
    scr.addEventListener('click',function (){scrollTo(j*tableCap);activateButton(j);});
  })(j);
    footer.appendChild(scr);
  }
}

var xPos=0;
addEventListener("mousedown", function(event){
  xPos=event.pageX;
  event.preventDefault();
});
addEventListener("mouseup", function(event){
  var vector=(event.pageX-xPos)/Math.abs(event.pageX-xPos);
  if(vector<0)
  {
    if(curTable<(tablecnt-tableCap))
    {
      scrollTo(curTable+tableCap);
      activateButton(Math.floor(curTable/tableCap)+1);
    }
  }
  else
  if(vector>0)
  {
    if(curTable>=tableCap)
    {
      scrollTo(curTable-tableCap);
      activateButton(Math.floor(curTable/tableCap)-1);
    }
  }
});

scrollTo(0);
rebutton();
activateButton(0);

window.addEventListener('resize', function () {
  rebutton();
  scrollTo(curTable);
})

})()
