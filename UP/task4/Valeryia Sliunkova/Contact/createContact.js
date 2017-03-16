document.addEventListener("DOMContentLoaded", function ()
{
  var data = JSON.parse(window.poc.jsonData);
  var body = document.body;
  var disp = document.createElement("div");
  disp.className = "disp";
  var list = document.createElement("ul");
  list.className = "list";


  for (var i = 0; i < data.length; ++i)
  {
    var contact = document.createElement("li");
    var textContent = document.createElement("div");
    textContent.className = "textContent";
    var image = document.createElement("div");
    image.className = "image";

    contact.className = "contacts";
    contact.setAttribute("data-id", data[i].id);

    var contName = document.createElement("h4")
    contName.className = "contTitle";
    contName.innerHTML = data[i].name;
    textContent.appendChild(contName);

    var contDescr = document.createElement("p");
    contDescr.className = "text"
    contDescr.innerHTML = data[i].description;
    textContent.appendChild(contDescr);

    contact.appendChild(textContent);

    var img = document.createElement("img");
    img.src = data[i].imageUrl;
    image.appendChild(img);
    contact.appendChild(image);

    list.appendChild(contact);
    disp.appendChild(list);
    body.appendChild(disp);
    var hr = document.createElement("hr");
    hr.style.width = "80%";
    contact.appendChild(hr);
  }

  var search = document.getElementById("search");
  search.addEventListener("keyup", function (e)
  {
  	var currentInput = search.value;
    for (var i = 0; i < data.length; i++)
    {
  	  if (data[i].name.indexOf(currentInput) >= 0 || data[i].description.indexOf(currentInput) >= 0 )
      {
        list.children[i].style.display = "";
      }
      else
      {
        list.children[i].style.display = "none";
      }
    }

  })


})
