(function()
{
	document.addEventListener("DOMContentLoaded", script);

	function script()
	{
		var cover = document.createElement('div');
		cover.classList.add('cover');
		document.body.appendChild(cover);

		var header = document.createElement('div');
		header.classList.add('header');
		header.innerHTML = "Title / Header of a Lane";
		cover.appendChild(header);

		var xmlStr = window.poc.xmldata;
		var parser = new DOMParser();
		var xml = parser.parseFromString(xmlStr, "application/xml");
		var data = xml.getElementsByTagName('data')[0];
		var tables = data.getElementsByTagName('dataTable');

		var container = document.createElement('div');
		container.classList.add('container');
		cover.appendChild(container);

		var scrollBar = document.createElement('div');
		scrollBar.classList.add('scrollBar');
		cover.appendChild(scrollBar);
		
		for (var i = 0; i < tables.length; i++)
		{
			addTable(tables[i]);
		}

		var buttonCover = document.createElement('div');
		buttonCover.classList.add('buttonCover');

		container.onmousedown = function(e)
		{
			var begin = e.clientX;
			container.onmousemove = function(e)
			{
				var x = e.clientX - begin;

				container.onmouseup = function()
				{
					if (currentPage > 0 && x > 0)
					{
						buttonCover.children[currentPage].classList.remove('active');
						page(currentPage - 1); 
						buttonCover.children[currentPage].classList.add('active');
					}
					if (x < 0 && currentPage != (numOfPages - 1))
					{
						buttonCover.children[currentPage].classList.remove('active');
						page(currentPage + 1);
						buttonCover.children[currentPage].classList.add('active');
					}
					container.onmousemove = null;
				}
			}
			e.preventDefault();
		}

		var showNumber;
		var currentPage = 0;
		var numOfPages;
		var margin = 20;
		var widht = container.children[0].offsetWidth;

		render();

		function render()
		{
			setNumber();
			buttons();
			page(0);		
			window.addEventListener('resize', resize);
		}	

		function addTable(xmlTable)
		{
			var tableCover = document.createElement('div');
			tableCover.classList.add("tableCover");
			container.appendChild(tableCover);

			var currentTable = document.createElement('table');
			currentTable.classList.add('table');
			tableCover.appendChild(currentTable);	

			var columns = xmlTable.getElementsByTagName('column');
			var tableHeaderRow  = currentTable.insertRow();
			var tableHeaderCell = tableHeaderRow.insertCell();
			tableHeaderCell.innerHTML = xmlTable.getAttribute('name');
			tableHeaderCell.colSpan   = columns.length;

			tableHeaderCell.classList.add('tableHeader');

			var columnName = currentTable.insertRow();
			columnName.classList.add('columnName');
			var rowAttributes = [];
			for (var i = 0; i < columns.length; i++)
			{
				columnName.insertCell().innerHTML = columns[i].getAttribute('label');
				rowAttributes[i] = columns[i].getAttribute('name');
			}

			var rows = xmlTable.getElementsByTagName('row');
			for (var i = 0; i < rows.length; i++)
			{
				var rowHeader = currentTable.insertRow();	
				var headerCage = rowHeader.insertCell(); 
				headerCage.innerHTML = rows[i].getAttribute('name');
				headerCage.colSpan = columns.length;
				headerCage.classList.add('rowHeader');
				var currentRow = currentTable.insertRow();
				currentRow.classList.add('information');

				for (var j = 0; j < columns.length; j++)
				{
					var cage = currentRow.insertCell();
					cage.innerHTML = rows[i].getAttribute(rowAttributes[j]);	
					if (rowAttributes[j] === 'change' || rowAttributes[j] === 'percentChange')
					{
						if (parseFloat(rows[i].getAttribute(rowAttributes[j])) < 0)
						{
							cage.classList.add('red');
						}
						if (parseFloat(rows[i].getAttribute(rowAttributes[j])) > 0)
						{
							cage.classList.add('green');
						}
					}
				}
			}
		}

		function buttons()
		{		
			if ( !scrollBar.hasChildNodes() )
			{
				scrollBar.appendChild(buttonCover);
			}
			while (buttonCover.firstChild)
			{
				buttonCover.removeChild(buttonCover.firstChild);
			}

			for (var i = 0; i < numOfPages; i++)
			{
				var button = document.createElement('div');
				button.classList.add('button');
				buttonCover.appendChild(button);

				(function(i)
				{ 
					var b = buttonCover.children[i];
					b.onmouseover = function()
					{
						if ( !b.classList.contains('active') )
						b.classList.add('wait');
					}
					b.onmouseleave = function()
					{
						b.classList.remove('wait');
					}
					b.addEventListener('click', function()
					{
						b.classList.remove('wait');
						for (var j = 0; j < numOfPages; j++)
						{
							buttonCover.children[j].classList.remove('active');
						}
						b.classList.add('active');
						page(i);
					});
				})(i);

				buttonCover.children[0].classList.add('active');
			}
		}

		function resize()
		{
			var curFirst = currentPage * showNumber;
			setNumber();
			var newPage = Math.ceil( (curFirst+1) / (showNumber) ) - 1;
			buttons();
			for (var i = 0; i < numOfPages; i++)
			{
				buttonCover.children[i].classList.remove('active');
			}
			buttonCover.children[newPage].classList.add('active');
			page(newPage);
		}

		function page(n)
		{
			if (currentPage !== 0)
			{
				container.style.transition = "transform 1s ease-out";
				for (var i = 0; i < tables.length; i++)
				{
					container.children[i].style.transition = "opacity 1s ease";
				}
			}
			else
			{
				currentPage = n;
			}

			var first = n * showNumber;
			var last  = first + showNumber - 1;

			if (last >= tables.length)
			{
				last = tables.length - 1;		
			}
			var transfer = ( ( widht + margin * 2 ) * showNumber ) * ( -n );

			for (var i = 0; i < tables.length; ++i)
			{
				if (i > last || i < first)	
				{			
					container.children[i].style.opacity = "0";
				}
				else				
				{
					container.children[i].style.opacity = "1";
				}
			}	

			if (n > currentPage)
			{
				for (var i = 0; i <= last; i++)
				{
					container.children[i].style.opacity = "1";
				}
			}

			container.style.transform = "translateX(" + transfer + "px)";
			currentPage = n;	
		}

		function setNumber()
		{
			showNumber = Math.floor(container.offsetWidth / (widht + margin * 2) );
			numOfPages = Math.ceil(tables.length / showNumber);
		}
	}
})();