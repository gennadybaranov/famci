(function(){
	function ready()
	{
		var data = window.poc.xmldata;
		var parser = new DOMParser();
		var xmlDoc = parser.parseFromString(data, "text/xml");
		var body = document.body;
		var tableWidth = 360;
		var margin = 15;
		var tablesNumberOnPage = Math.floor(window.innerWidth / tableWidth);
		var pagesNumber = 0;
		var title = document.createElement("div");
		title.className = "title";
		title.textContent = "Title / Header of Lane";
		body.appendChild(title);
		var content = document.createElement("div");
		content.className = "content";
		body.appendChild(content);
		var root = xmlDoc.documentElement.childNodes;
		var tablesNumber = 0;
		var curPage = 0;
		for (var i = 0; i < root.length; i++) 
		{
			if (i % 2 == 1)
			{
				tablesNumber++;
				var dataTable = root[i];
				var columns = dataTable.getElementsByTagName("column");
				var rows = dataTable.getElementsByTagName("row");
				var wtable = document.createElement("div");
				wtable.className = "wtable";
				var table = document.createElement("table");
				table.className = "table";
				var tr = document.createElement("tr");	
				var capture = document.createElement("th");
				capture.className = "capture";
				capture.colSpan = columns.length;
				tr.appendChild(capture);
				table.appendChild(tr);
				capture.textContent = dataTable.attributes.name.textContent;
				tr = document.createElement("tr");
				for (var j = 0; j < columns.length; j++) 
				{
					var th = document.createElement("th");
					th.className = "theader";
					th.textContent = columns[j].attributes.label.textContent;
					tr.appendChild(th);
				}
				tr.className = "cols";
				table.appendChild(tr);
				for (var j = 0; j < rows.length; j++)
				{
					tr = document.createElement("tr");
					var td = document.createElement("td");
					td.textContent = rows[j].attributes.name.textContent;
					td.colSpan = columns.length;
					tr.appendChild(td);
					tr.className = "hrow";
					table.appendChild(tr);
					tr = document.createElement("tr");
					var td = document.createElement("td");
					td.textContent = rows[j].attributes.price.textContent;
					tr.appendChild(td);
					td = document.createElement("td");
					td.textContent = rows[j].attributes.change.textContent;
					if (td.textContent[0] == '-') td.className = "red"; else
						if (td.textContent != "0") td.className = "green";
					tr.appendChild(td);
					td = document.createElement("td");
					td.textContent = rows[j].attributes.percentChange.textContent;
					if (td.textContent[0] == '-') td.className = "red"; else
						if (td.textContent != "0") td.className = "green";
					tr.appendChild(td);
					tr.className = "numbers";
					table.appendChild(tr);
				}

				wtable.appendChild(table);
				content.appendChild(wtable);
			}

		}
		var btnbar = document.createElement("div");
		btnbar.className = "btnbar";
		body.appendChild(btnbar);

		function show(page)
		{
			curPage = page;
			var shift = page * tablesNumberOnPage * tableWidth;
			content.style.transform = "translateX(" + (-shift) + "px)";
			for (var i = 0; i < content.childNodes.length; i++)
			{
				var f = (tableWidth * (i + 1) <= shift + window.innerWidth);
				content.childNodes[i].style.opacity = +f;
			}
		}
		function recreateButtons()
		{
			var len = btnbar.childNodes.length;
			 for (var j = 0; j < len; j++) 
			 	btnbar.removeChild(btnbar.childNodes[0]);
			 pagesNumber = Math.ceil(tablesNumber / tablesNumberOnPage);
			 for (var j = 0; j < pagesNumber; j++) 
			 {
			 	var button = document.createElement("input");
			 	button.setAttribute("type", "radio");
			 	button.setAttribute("name", "pages");
			 	button.setAttribute("id", j);
			 	button.className = "rbutton";

			 	var label = document.createElement("label");
			 	label.setAttribute("for",j);
			 	var span = document.createElement("span");

			 	btnbar.appendChild(button);
			 	label.appendChild(span);
			 	btnbar.appendChild(label);
			 	button.addEventListener("click", function ()
			 	{
			 		show((this).getAttributeNode("id").value);
			 		(this).checked = true;
			 	});
			 }
		}
		function setInitial()
		{
			recreateButtons();
			btnbar.childNodes[0].checked = true;
			show(0);
		}
		setInitial();
		window.addEventListener("resize", function(){
			tablesNumberOnPage = Math.floor(window.innerWidth / tableWidth);
			setInitial();
		});
		var clickX = 0;
		window.addEventListener("mousedown", function(e){
			clickX = e.pageX;
			e.preventDefault();
		});
		window.addEventListener("mouseup", function(e){
			var shift = e.pageX - clickX;
			var delta = (shift == 0 ? 0 : shift / Math.abs(shift));
			var newPage = (curPage - delta + pagesNumber) % pagesNumber;
			btnbar.childNodes[newPage * 2].checked = true;
			show(newPage);
		});
	}
	document.addEventListener("DOMContentLoaded", ready);
})();