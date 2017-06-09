(function()
{
	function ready()
	{
		console.log(window.poc.jsonData);
		var data = JSON.parse(window.poc.jsonData);
		var wrapper = document.createElement("div");
		var items = document.getElementById("items");
		var ul = document.createElement("ul");
		ul.className = "items";
		for (var i = 0; i < data.length; i++)
		{
			var li = document.createElement("li");
			li.className = "item";
			li.setAttribute("layout", "row");
			var divLeft = document.createElement("div");
			divLeft.className = "divText";
			var info = document.createElement("p");
			divLeft.appendChild(info);
			info.className = "info";
			var link = document.createElement("a");
			link.className = "link";
			link.setAttribute("href", data[i].link);
			link.setAttribute("target", "_blank");
			info.appendChild(link);
			link.textContent = data[i].info;
			var divRight = document.createElement("div");
			var img = document.createElement("img");
			divRight.appendChild(img);
			divRight.className = "divImage";
			img.className = "image";
			img.setAttribute("src", data[i].imageUrl);
			var description = document.createElement("a");
			description.className = "description";
			description.textContent = data[i].description;
			divLeft.appendChild(description);

			li.appendChild(divRight);
			li.appendChild(divLeft);
			ul.appendChild(li);
		}
		items.appendChild(ul);

		var search = document.getElementById("searchbar");
		search.addEventListener("keyup", function(e){
			var curStr = search.value;
			curStr = curStr.toUpperCase();
			var lists = document.getElementsByTagName("li");
			for (var i = 0; i < data.length; i++)
			{
				var strInfo = data[i].info;
				var strDesc = data[i].description;
				strInfo = strInfo.toUpperCase();
				strDesc = strDesc.toUpperCase();
				if (strInfo.indexOf(curStr) == -1 && strDesc.indexOf(curStr) == -1) 
				{
					lists[i].style.display = "none";
				}
				else
				{
					lists[i].style.display = "block";
				}

			}
		});
	}
	document.addEventListener("DOMContentLoaded", ready);
})();