(function(){
	document.addEventListener("DOMContentLoaded", runScript);
	function runScript(){
		var XMLString = window.poc.xmldata;
		var parser = new DOMParser();
		var xml = parser.parseFromString(XMLString, "application/xml");

		var wrap = document.createElement('div');
		wrap.classList.add('wrap');
		document.body.appendChild(wrap);

		var pageHeader = document.createElement('div');
		pageHeader.classList.add('pageHeader');
		pageHeader.innerHTML = "Title / Header of a Lane";
		wrap.appendChild(pageHeader);

		var tableContainer = document.createElement('div');
		tableContainer.classList.add('tableContainer');
		wrap.appendChild(tableContainer);

		var scrollBar = document.createElement('div');
		scrollBar.classList.add('scrollBar');
		wrap.appendChild(scrollBar);
		
		var data = xml.getElementsByTagName('data')[0];
		var tables = data.getElementsByTagName('dataTable');
		
		for (var i = 0; i < tables.length; i++)
			addTable(tables[i]);

		var buttonsWrap = document.createElement('div');
		buttonsWrap.classList.add('buttonWrap');

		var tableWidth = tableContainer.children[0].offsetWidth;;
		var shownTablesNum;
		var pagesNum;
		var tableSideMargin = 20;
		var curPage = undefined;
		
		renderPage();

		function renderPage(){
			setDisplayedNum();
			makeButtons();
			goToPage(0);		
			window.addEventListener('resize', resize);
		}	

		tableContainer.onmousedown = function(e){
			var startX = e.clientX;

			tableContainer.onmousemove = function(e){
				var dx = e.clientX - startX;

				tableContainer.onmouseup = function(){
					if (dx > 0 && curPage > 0){
						buttonsWrap.children[curPage].classList.remove('buttonActive');
						goToPage (curPage - 1); // function changes curPage value
						buttonsWrap.children[curPage].classList.add('buttonActive');
					}
					if (dx < 0 && curPage !== (pagesNum - 1)){
						buttonsWrap.children[curPage].classList.remove('buttonActive');
						goToPage(curPage + 1);
						buttonsWrap.children[curPage].classList.add('buttonActive');
					}
					tableContainer.onmousemove = null;
				}
			}
			e.preventDefault();
		}

		function addTable(xmlTable){
			var tableWrap = document.createElement('div');
			tableWrap.classList.add("tableWrap");
			tableContainer.appendChild(tableWrap);

			var curTable = document.createElement('table');
			curTable.classList.add('table');
			tableWrap.appendChild(curTable);	

			var columns = xmlTable.getElementsByTagName('column');

			// adding table header
			var tableHeaderRow = curTable.insertRow();
			var tableHeaderCell = tableHeaderRow.insertCell();
			tableHeaderCell.innerHTML = xmlTable.getAttribute('name');
			tableHeaderCell.classList.add('tableHeader');
			tableHeaderCell.colSpan = columns.length;

			// adding column names
			var columnsNames = curTable.insertRow();
			columnsNames.classList.add('columnsNames');
			var rowAttributes = [];
			for (var i = 0; i < columns.length; i++){
				columnsNames.insertCell().innerHTML = columns[i].getAttribute('label');
				rowAttributes[i] = columns[i].getAttribute('name');
			}

			// adding rows with data
			var rows = xmlTable.getElementsByTagName('row');
			for (var i = 0; i < rows.length; i++){
				var rowHeader = curTable.insertRow();	// adding row name
				var headerCell = rowHeader.insertCell(); // try without cell
				headerCell.innerHTML = rows[i].getAttribute('name');
				headerCell.colSpan = columns.length;
				headerCell.classList.add('rowHeader');

				var curRow = curTable.insertRow();
				curRow.classList.add('info');
				for (var j = 0; j < columns.length; j++){
					var cell = curRow.insertCell();
					cell.innerHTML = rows[i].getAttribute(rowAttributes[j]);	
					if (rowAttributes[j] === 'change' || rowAttributes[j] === 'percentChange'){
						if (parseFloat(rows[i].getAttribute(rowAttributes[j])) < 0)
							cell.classList.add('red');
						if (parseFloat(rows[i].getAttribute(rowAttributes[j])) > 0)
							cell.classList.add('green');
					}
				}
			}
		}

		function makeButtons(){		
			if (!scrollBar.hasChildNodes())
				scrollBar.appendChild(buttonsWrap);
			while (buttonsWrap.firstChild)
				buttonsWrap.removeChild(buttonsWrap.firstChild);

			for (var i = 0; i < pagesNum; i++){
				var cButton = document.createElement('div');
				cButton.classList.add('button');
				buttonsWrap.appendChild(cButton);

				(function(i){ // immediately invoked function expression. no need to pass 'i', it is visible. but still
					var b = buttonsWrap.children[i];
					b.onmouseover = function(){
						if (!b.classList.contains('buttonActive'))
						b.classList.add('buttonHover');
					}
					b.onmouseleave = function(){
						b.classList.remove('buttonHover');
					}
					b.addEventListener('click', function(){
						b.classList.remove('buttonHover');
						for (var j = 0; j < pagesNum; j++){
							buttonsWrap.children[j].classList.remove('buttonActive');
						}
						b.classList.add('buttonActive');
						goToPage(i);
					});
				})(i);

				buttonsWrap.children[0].classList.add('buttonActive');
			}
		}

		function setDisplayedNum(){
			shownTablesNum = Math.floor(tableContainer.offsetWidth / (tableWidth + tableSideMargin*2));
			pagesNum = Math.ceil(tables.length / shownTablesNum);
		}

		function goToPage (n){
			if (curPage !== undefined){
				tableContainer.style.transition = "transform 1s ease-out";
				for (var i = 0; i < tables.length; i++)
					tableContainer.children[i].style.transition = "opacity 1s ease";
				}
			else
				curPage = n;

			var first = (n)*shownTablesNum;
			var last = first + shownTablesNum - 1;
			if (last >= tables.length)
				last = tables.length - 1;		
			var shift = ((tableWidth + tableSideMargin*2) * shownTablesNum) * (-n);

			for (var i = 0; i < tables.length; i++){
				if (i > last || i < first)				
					tableContainer.children[i].style.opacity = "0";
				else				
					tableContainer.children[i].style.opacity = "1";
			}	

			if (n > curPage)
				for (var i = 0; i <= last; i++)
					tableContainer.children[i].style.opacity = "1";

			tableContainer.style.transform = "translateX(" + shift + "px)";

			curPage = n;	
		}

		function resize(){
			var curFirst = curPage*shownTablesNum;
			setDisplayedNum();
			var newPage = Math.ceil((curFirst+1)/(shownTablesNum)) - 1;
			makeButtons();
			for (var i = 0; i < pagesNum; i++){
				buttonsWrap.children[i].classList.remove('buttonActive');
			}
			buttonsWrap.children[newPage].classList.add('buttonActive');
			goToPage(newPage);
		}
	}
})();