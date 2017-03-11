document.addEventListener("DOMContentLoaded", function(){
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

	var curPage = 1;
	makeButtons();

	// tableContainer.onmousedown = onMouseDown;
	// tableContainer.onmouseup = onMouseUp;

	// function onMouseDown(event){
	// 	tableContainer.onmousemove = onMouseMove;
	// 	var startX = event.clientX;
	// }

	// function onMouseMove(event){
	// 	tableContainer.style.left = (tableContainer.offsetLeft + event.clientX - startX) + 'px';
	// }

	// function onMouseUp(event){
	// 	tableContainer.onmousemove = null;
	// }

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
		var tw = document.getElementsByClassName('tableWrap');
		var width = tw[1].offsetWidth + 40; // + margin left
		var displayingNum = Math.floor(tableContainer.offsetWidth / width);
		var pagesNum = Math.ceil(tables.length / displayingNum);
		
		var buttonWrap = document.createElement('div');
		buttonWrap.classList.add('buttonWrap');
		scrollBar.appendChild(buttonWrap);
		for (var i = 0; i < pagesNum; i++){
			var cButton = document.createElement('div');
			cButton.classList.add('button');
			buttonWrap.appendChild(cButton);	
			cButton.id = "btn" + i;

			(function(i){ // immediately invoked function expression. no need to pass 'i', it is visible. but still
				var b = document.getElementById('btn' + i);
				b.onmouseover = function(){
					if (!b.classList.contains('buttonActive'))
					b.classList.add('buttonHover');
					// buttonWrap.style.transform = "translateX(20px)";
				}
				b.onmouseleave = function(){
					b.classList.remove('buttonHover');
				}
				b.addEventListener('click', function(){
					b.classList.add('buttonActive');
					b.classList.remove('buttonHover');
					// remove activeButton from other buttons
					for (var j = 0; j < pagesNum; j++){ // pagesNum is also visible
						var tmpB = document.getElementById('btn' + j);
						if (tmpB != b)
							tmpB.classList.remove('buttonActive');
					}
					// shift tables
					goToPage(i+1);
				});
			})(i);
		}
	}

	function goToPage (n){
		var tw = document.getElementsByClassName('tableWrap');
		var width = tw[1].offsetWidth + 40; // + margin left
		var displayingNum = Math.floor(tableContainer.offsetWidth / width);
		var pagesNum = Math.ceil(tables.length / displayingNum);

		var first = (n-1)*displayingNum;
		var last = first + displayingNum - 1;
		if (last >= tables.length)
			last = tables.length - 1;
		var shift = width*displayingNum;
		// shifting tables
		for (var j = 0; j < tables.length; j++){
			var cTable = tw[j];
			if (j < first)
				cTable.style.transform = "translateX(-" + shift + "px)";
			if (j > last)
				cTable.style.transform = "translateX(+" + shift + "px)";
		}
		for (var j = first; j <= last; j++){
			var cTable = tw[j];
			tw.style.left = j*width;
		}

		curPage = n;	
	}
});