(function(){
document.addEventListener("DOMContentLoaded", ready);

function ready(){

var data = JSON.parse(window.poc.jsonData);	
var body = document.body;
var mainDiv = document.createElement('div');
mainDiv.className = "mainDiv";
var search = document.createElement('input');
//search.setAttribute("placeholder", "Here u type!");
search.className="search";
mainDiv.appendChild(search);
var list = document.createElement('ul');
list.className = "list";
var dlength = data.length;
for (var i =0; i<dlength;++i)
{
	
	var li = document.createElement('li');
	var wraper = document.createElement('div');
	wraper.className="wraper";
	li.className= "li";
	var txtDiv = document.createElement('div');
	var imgDiv = document.createElement('div');
	imgDiv.className="imgDiv";
	var h = document.createElement('h5');
	h.className="name";
	h.innerHTML= data[i].name;
	var p = document.createElement('a');
	p.className="p";
	p.innerHTML=data[i].description;
	txtDiv.appendChild(h);
	txtDiv.appendChild(p);
	var img = document.createElement('img');
	img.className="img";
	img.setAttribute("src", data[i].imageUrl);
	imgDiv.appendChild(img);
	wraper.appendChild(txtDiv);
	wraper.appendChild(imgDiv);
	var hr = document.createElement("hr");
    hr.className="hr";
    li.appendChild(wraper);
    li.appendChild(hr);
	list.appendChild(li);

}
mainDiv.appendChild(list);
body.appendChild(mainDiv);
var input;
search.addEventListener("keyup", function(e){
input = search.value;
input = input.toUpperCase();
for (var i = 0; i < dlength; ++i)
{
	var elemName = data[i].name;
	var elemInfo = data[i].description;
	elemName = elemName.toUpperCase();
	elemInfo = elemInfo.toUpperCase();
	if (elemName.indexOf(input) >=0 || elemInfo.indexOf(input) >= 0) 
	{
		list.children[i].style.display = 'block';
	}
	else
	{
		list.children[i].style.display = 'none';
	}
}
	});

}})();