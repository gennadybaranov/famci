/*Я пробовал в функция обернуть, но все равно не работало, поэтому в html скрипты подключаю в боди*/
 var data=window.poc.xmldata;
 var body=document.body;
 var updiv=document.createElement('div');
 updiv.textContent='Title / Header of lane';
 updiv.className="uptitle";
 body.appendChild(updiv);

 var main=document.createElement('div');
 main.className='main';
 body.appendChild(main);

 var parser = new window.DOMParser();
 var xmlData =  parser.parseFromString(data, "text/xml");
 var tcount=xmlData.children[0].children.length;
 var i;
 for (i=0;i<tcount;++i)
 {
    var tableDiv = document.createElement('div');
    tableDiv.className="tableDiv";
    var tableData=xmlData.children[0].children[i];
    var columns =tableData.getElementsByTagName('column');
    columns.className="columns"
    var rows=tableData.getElementsByTagName('row');
    var table=document.createElement('table');
    var tableName=document.createElement('tr');
    tableName.className="tableName";
    var tdTable=document.createElement('td');
    tdTable.colSpan=columns.length;
    tdTable.textContent=tableData.attributes.name.textContent;
    tableName.appendChild(tdTable);
    table.appendChild(tableName);
    var columnLength = columns.length;
    var trLabel = document.createElement('tr');
    for (var j=0;j<columnLength;++j)
    {
      var tdLabel =  document.createElement('td');
      tdLabel.className="label";
      tdLabel.textContent= columns[j].attributes.label.textContent;
      trLabel.appendChild(tdLabel);
    }
    table.appendChild(trLabel);
    var rowsLength = rows.length;
    for (var j = 0; j<rowsLength;++j)
    {
      company = document.createElement('tr');
      tdCompany = document.createElement('td');
      tdCompany.textContent=rows[j].attributes.name.textContent;
      company.className="company";
      tdCompany.colspan=rowsLength;
      company.appendChild(tdCompany);
      table.appendChild(company);
      var info = document.createElement('tr');
      info.className="info";
      var price=document.createElement('td');
      var change=document.createElement('td');
      var percentChange=document.createElement('td');
      price.className="price";
      change.className="change";
      percentChange.className="percentChange";
      price.textContent=rows[j].attributes.price.textContent;
      change.textContent=rows[j].attributes.change.textContent;
      percentChange.textContent=rows[j].attributes.percentChange.textContent;
      info.appendChild(price);
      info.appendChild(change);
      info.appendChild(percentChange);
      table.appendChild(info);

    }
    tableDiv.appendChild(table);
    main.appendChild(tableDiv);
 }

  var numcolor = document.getElementsByClassName('change');
  ncount = numcolor.length;
  for (var i = 0; i < ncount; ++i)
    {
      if (numcolor[i].textContent[0] == '-')
        numcolor[i].style.color = '#D81B60';
      else
       numcolor[i].style.color = '#4CAF50'; 
    }
  var numcolor = document.getElementsByClassName('percentChange');
  ncount = numcolor.length;
  for (var i = 0; i < ncount; ++i)
    {
      if (numcolor[i].textContent[0] == '-')
        numcolor[i].style.color = '#D81B60';
      else
       numcolor[i].style.color = '#4CAF50'; 
    }

//////////////////////////////

  var width = 300;
  var margin = 22;
  var leftTable;
  function visualNumbers (number)
  {
    var offset = number * (width+margin)  ;
   leftTable = number;
    main.style.transform = "translateX(-"+ offset + "px)";
    lngth = main.children.length;
    for (var i = 0; i < lngth; ++i)
      if (main.children[i].offsetLeft - offset - margin > window.innerWidth - width)
        main.children[i].style.opacity = 0;
      else
        main.children[i].style.opacity = 1;
  }
  visualNumbers (0);

  var panel = document.createElement('div');
  panel.className='panel';
  body.appendChild(panel);
  var lastButton = 0;
  var current;
  function createButtons()
  {
    current = Math.floor (window.innerWidth / (width+margin)); 
    var bcount = main.children.length / current; 
    while (panel.children.length)
      panel.removeChild(panel.children[0]);
    for (var i = 0; i < bcount; ++i)
    {
      var button = document.createElement("button");
      button.className = "button";
      panel.appendChild(button);
      (function (i)
      { button.addEventListener("click", function ()
        {
         visualNumbers(i*current);

         panel.children[i].style.backgroundColor = "#3D3D3D";
         panel.children[lastButton].style.backgroundColor = "#E3E3E3";
         lastButton = i;
        }) 
      }) (i)
    }
    body.appendChild(panel);
  }
  createButtons();

window.addEventListener("resize", function (e)
  {
    visualNumbers(leftTable);
    createButtons();
  });
  var curPosition;
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
        visualNumbers (leftTable - current);
    if (move > 0 && (leftTable + current) != main.children.length)
      visualNumbers (leftTable + current);
  })


 
