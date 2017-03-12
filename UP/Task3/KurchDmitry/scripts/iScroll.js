(function() {

	document.addEventListener("DOMContentLoaded", ready);

	function ready() {

		var data = window.poc.xmldata;// через window.poc получаем доступ к данным и записываем всё в одну строку в data
		var body = document.body;//переменная, имеющая доступ к УЖЕ СУЩЕСТВУЮЩЕМУ тегу <body> в html файле
		var Head = document.createElement("div");//переменная, хранящая тег div
		var body_div = document.createElement("div");
		const tableWidth = 320;
		const tableMargin = 10;
		var lastTableNumber = 0;
		var numberOfTablesNow = Math.floor(window.innerWidth / tableWidth);
		body_div.className = "body_div";
		Head.textContent = "Title/header of Lane";// через переменную мы имеем доступ к самому тегу, и записываем туда текст
		Head.className = "Head";//устанавливаем стили для div
		body.appendChild(Head);//присоединяем заголовочный div к последнему узлу body
		body.appendChild(body_div);

		var parser = new window.DOMParser();//создаём переменную, типа:считывающая информацию
		var xmlData = parser.parseFromString(data, "text/xml");//считываем информацию и структурузируем её
		var Table = xmlData.getElementsByTagName("dataTable");//создаём переменную, содержащую элементы доступа к названиям таблиц и далее
		var Table_count = Table.length;//записываем в переменную table_count количество таблиц
		xmlData.childNotes;//пока хз чё это

		for (var i = 0; i<Table_count; i++) {
			var table_div = document.createElement("div");
			var column = Table[i].getElementsByTagName("column");//определяем переменные, содержащие в себес труктуризированную информацию о
			var row = Table[i].getElementsByTagName("row");//столбцах и строках соответсвенно
			var table = document.createElement("table");//i-ой таблицы в цикле
			table_div.className = "table_div";

			var head = document.createElement("tr");//занимаемся заголовком таблицы
			head.className = "head";
			var tHhead = document.createElement("th");
			tHhead.textContent = Table[i].attributes.name.textContent;
			tHhead.setAttribute("colspan", "3");
			head.appendChild(tHhead);
			table.appendChild(head);

			var tRheader = document.createElement("tr");//занимаеся подзаголовком таблицы(названия столбцов)
			tRheader.className = "tRheader";
			for (var n = 0; n<3; n++) {
				var tHheader = document.createElement("th");
				tHheader.textContent = column[n].attributes.label.textContent;
				tRheader.appendChild(tHheader);
			}
			table.appendChild(tRheader);
			//здесь создаем цикл для вывода всей информации
			for (var j = 0; j<row.length; j++) {
				var tRcompany = document.createElement("tr");//названия компаний
				tRcompany.className = "tRcompany";
				var tDcompany = document.createElement("td");
				tDcompany.textContent = row[j].attributes.name.textContent;
				tDcompany.setAttribute("colspan", "3");
				tRcompany.appendChild(tDcompany);
				table.appendChild(tRcompany);

				var tRnumbers = document.createElement("tr");//информация о процентах, изменениях, изменениях в процентах
				tRnumbers.className = "tRnumbers";
				var tDnumber = document.createElement("td");
				tDnumber.textContent = row[j].attributes.price.textContent;
				tRnumbers.appendChild(tDnumber);
				table.appendChild(tRnumbers);
				document.body.appendChild(document.body.appendChild(table));
				var tDnumber = document.createElement("td");
				tDnumber.textContent = row[j].attributes.change.textContent;
				if (parseFloat(tDnumber.textContent, 10)>0)//Изменяем цвет в зависимости от знака переменной для 2 стобца
					tDnumber.className = "green";
				else
					tDnumber.className = "red";
				tRnumbers.appendChild(tDnumber);
				table.appendChild(tRnumbers);
				document.body.appendChild(document.body.appendChild(table));
				var tDnumber = document.createElement("td");
				tDnumber.textContent = row[j].attributes.percentChange.textContent;
				if (parseFloat(tDnumber.textContent, 10)>0)//Изменяем цвет в зависимости от знака переменной для 3 столбца
					tDnumber.className = "green";
				else
					tDnumber.className = "red";
				tRnumbers.appendChild(tDnumber);
				table.appendChild(tRnumbers);

				var trhr = document.createElement("tr");//Создаём линию после каждой строки. Пытался tr style="border-bottom:1px solid #CCCCCC"
				var tdhr = document.createElement("td");//Не отображалось. Пришлось делать так(
				tdhr.setAttribute("colspan", "3");
				var hr = document.createElement("hr");
				hr.className = "hr";
				tdhr.appendChild(hr);
				trhr.appendChild(tdhr);
				table.appendChild(trhr);
			}
			table_div.appendChild(table);
			body_div.appendChild(table_div);
		}

		function hidTable(number) {//скрываем таблицы, которые не помещаются на экран
			var offset = number * tableWidth;
			lastTableNumber = number;
			body_div.style.transform = 'translateX(-' + offset + 'px)';
			for (var g = 0; g < body_div.children.length; g++) {
				if (body_div.children[g].offsetLeft - offset - tableMargin > window.innerWidth - tableWidth) {
					body_div.children[g].style.opacity = 0
				}
				else {
					body_div.children[g].style.opacity = 1
				}
			}
		};

		/////////////////////////////////////////////////////////////////////////////////////////////
		var buttons = document.createElement("div");//начинаем делать кнопки
		buttons.className = "buttons";
		body.appendChild(buttons)

			function createBut() {//делаем кнопки
			var buttons = document.getElementsByClassName("buttons")[0];
			var body_div = document.getElementsByClassName("body_div")[0];
			var numberOfButtons = Math.ceil(body_div.children.length / numberOfTablesNow);
			while (buttons.children.length > 0) {
				buttons.removeChild(buttons.children[0]);
			}
			for (var i = 0; i < numberOfButtons; i++) {
				var button = document.createElement("div");
				buttons.appendChild(button);
				(function(i) {
					button.addEventListener("click", function() {
						hidTable(i * numberOfTablesNow);
						colorButtons(i);
					})
				})(i)
			}
		}

		function colorButtons(number) {//выделяем отдельную кнопку по номеру
			for (var i = 0; i < buttons.children.length; i++) {
				buttons.children[i].className = "";
			}
			buttons.children[number].className = "active";
		}

		window.addEventListener("resize", function() {
			createBut();
			hidTable(lastTableNumber);
		})

			createBut();
		hidTable(0);
		colorButtons(0);
		////////////////////////////////////////////////////////////
		var XPos = 0;
		var move = 0;
		window.addEventListener('mousedown', function(e) {
			XPos = e.pageX;
			e.preventDefault();
		})
			//делаем обработчики событий для нажатия, нажатия и сдвига влево/вправо мышки
			window.addEventListener("mouseup", function(e) {
			var numberOfTablesOnScreen = Math.floor(window.innerWidth / tableWidth);
			move = XPos - e.pageX;
			if (move < 0)
				if (lastTableNumber < numberOfTablesOnScreen)
					hidTable(0);
				else
					hidTable(lastTableNumber - numberOfTablesOnScreen);
			else
				if (move > 0)
					if (lastTableNumber + numberOfTablesOnScreen < body_div.children.length)
						hidTable(lastTableNumber + numberOfTablesOnScreen);
			colorButtons(lastTableNumber / numberOfTablesOnScreen);
		})
	};
}());
