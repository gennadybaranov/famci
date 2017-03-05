(function ()
{
	var data = window.poc.xmldata;
	var body = document.body;
	const width = 255;
	const margin = 10;
	var leftTable = 0;
	var lastButton = 0;
	var curPosition = 0;
	var disp = document.createElement('div');

	(function ()
	{
		var parser = new window.DOMParser();
		var doc = parser.parseFromString(data, 'text/xml');
		var header = document.createElement('div')
		header.className = 'header';
		header.textContent = "Header of Lane";
		body.appendChild(header);
		disp.className = 'disp';
		body.appendChild(disp);

		for (var i = 0; i < doc.children[0].children.length; i++)
		{
			var dispTable = document.createElement('div');
			var dataTable = doc.children[0].children[i];
			var columns = dataTable.getElementsByTagName('column');
			var rows = dataTable.getElementsByTagName('row');
			var table = document.createElement('table');

			var tr = document.createElement('tr');
			var title = document.createElement('th');
			title.setAttribute('id', 'title');
			title.colSpan = columns.length;
			title.textContent = dataTable.attributes.name.textContent;
			tr.appendChild(title);
			table.appendChild(tr);

			tr = document.createElement('tr');
			tr.setAttribute('id', 'columns');
		  for (var c = 0; c < columns.length; c++)
		  {
				var label = columns[c].attributes.label.textContent;
				var th = document.createElement('th');
				th.innerHTML = label;
				th.setAttribute('class', 'section');
				tr.appendChild(th);
		  }
			table.appendChild(tr);

		  for (var r = 0; r < rows.length; r++)
		  {
				tr = document.createElement('tr');
				td = document.createElement('td');
				td.className = 'firmName';
				td.innerHTML = rows[r].attributes.name.textContent;
				tr.appendChild(td);
				td.colSpan = rows.length;
				table.appendChild(tr);
				tr = document.createElement('tr');
				tr.className = 'value';
				var price = document.createElement('td');
				var change = document.createElement('td');
				var percentChange = document.createElement('td');
				price.className = 'price';
				change.className = 'change';
				percentChange.className = 'change';
				price.innerHTML = rows[r].attributes.price.textContent;
				change.innerHTML = rows[r].attributes.change.textContent;
				percentChange.innerHTML = rows[r].attributes.percentChange.textContent;
				tr.appendChild(price);
				tr.appendChild(change);
				tr.appendChild(percentChange);
				table.appendChild(tr);

				var hr = document.createElement('div');
				hr.className = 'hr';
				table.appendChild(hr);
				tr = document.createElement('tr');
				var td = document.createElement('td');
				td.colSpan = 3;
				td.appendChild(hr);
				tr.appendChild(td);
				table.appendChild(tr);
		  }
		  disp.appendChild(dispTable);
		  dispTable.appendChild(table);
		  dispTable.className = 'dispTable';
		}

		var feature = document.getElementsByClassName('change');
		for (var i = 0; i < feature.length; i++)
		{
			if (feature[i].textContent.substring(0, 1) == '-')
			{ feature[i].style.color = '#D81B60'; }
			else
			{ feature[i].style.color = '#4CAF50'; }
		}
	} () )

	function show (number)
	{
		var offset = number * (width + margin) ;
		leftTable = number;
		disp.style.transform = "translateX(-"+ offset + "px)";
		for (var i = 0; i < disp.children.length; i++)
			if (disp.children[i].offsetLeft - offset - margin > window.innerWidth - width)
				disp.children[i].style.opacity = 0;
			else
				disp.children[i].style.opacity = 1;
	}
	show (0);

	var buttons = document.createElement("div");
	buttons.className = "buttons";
	body.appendChild(buttons);

	function makeButton ()
	{
		var tablesNow = Math.floor (window.innerWidth / (width + margin));
		var buttonsAmount = disp.children.length / tablesNow;
		while (buttons.children.length)
			buttons.removeChild(buttons.children[0]);
		for (var i = 0; i < buttonsAmount; i++)
		{
			var button = document.createElement("button");
			button.className = "button";
			buttons.appendChild(button);
			(function (i)
			{ button.addEventListener("click", function ()
			 	{
				 show(i*tablesNow);
				 buttons.children[i].style.backgroundColor = "#3E2723";
				 buttons.children[lastButton].style.backgroundColor = "#A1887F";
				 lastButton = i;
			  }) }) (i)
		}
	}

	makeButton ();

	window.addEventListener("resize", function (e)
	{
		show(leftTable);
		makeButton();
	});

	window.addEventListener("mousedown", function (e)
	{
		curPosition = e.pageX;
		e.preventDefault();
	});

	window.addEventListener("mouseup", function (e)
	{
		var tablesNow = Math.floor (window.innerWidth / (width + margin));
		var nowPosition = e.pageX;
		var move = curPosition - nowPosition;
		if (move < 0 && leftTable != 0)
			show (leftTable - tablesNow);
		if (move > 0 && (leftTable + tablesNow) != disp.children.length)
			show (leftTable + tablesNow);
	})

}) ()
