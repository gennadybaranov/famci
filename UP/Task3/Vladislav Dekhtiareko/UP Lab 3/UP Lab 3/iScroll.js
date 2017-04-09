document.addEventListener("DOMContentLoaded", page);

function page() {
    var data = window.poc.xmldata;
    var body = document.body;
    var tableWidth = 400;
    var tablesAmountOnPage = Math.floor(window.innerWidth / (tableWidth + tableWidth / 20));
    var tablesAmountTotal = 12;
    var totalButtons = Math.floor(tablesAmountTotal  / tablesAmountOnPage);
    var leftTable = 0;
    var lastButton = 0;


    parser = new window.DOMParser();
    xmlDoc = parser.parseFromString(data, 'text/xml');
    var title = document.createElement('div');
    title.textContent = 'Title / Header of Lane';
    title.className = 'title';
    var block = document.createElement('div');
    block.className = 'block';
    body.appendChild(title);
    body.appendChild(block);

    for (var i = 0; i < xmlDoc.getElementsByTagName('dataTable').length ; i++) {
        var tableBlock = document.createElement('div');
        tableBlock.className = ('tableBlock');
        var table = document.createElement('table');
        var column = xmlDoc.children[0].children[i].getElementsByTagName('column');
        var row = xmlDoc.children[0].children[i].getElementsByTagName('row');
        var tableHeader = document.createElement('th');
        tableHeader.textContent = xmlDoc.children[0].children[i].attributes.name.textContent;
        tableHeader.className = "tableHeader";
        tableBlock.appendChild(table);
        table.appendChild(tableHeader);
        table.className = ('table');
        var tr = document.createElement('tr');
        table.appendChild(tr);
        var sTHdiv = document.createElement('div');
        sTHdiv.className = "columnNameBack";
        for (var j = 0; j < column.length; j++) {

            var th = document.createElement('th');
            th.innerHTML = column[j].attributes.label.textContent;
            th.id = ('columnName');
            tr.appendChild(sTHdiv);
            sTHdiv.appendChild(th);
        }


        for (var k = 0; k < row.length; k++) {
            tr = document.createElement('hr');
            tr.className = ('row');
            table.appendChild(tr);
            var name = document.createElement('th');
            name.colSpan = 3;
            name.innerHTML = row[k].attributes.name.textContent;
            name.className = ('companyName');
            var price = document.createElement('td');
            price.className = ('data');
            var change = document.createElement('td');
            change.className = ('data');
            var percentChange = document.createElement('td');
            percentChange.className = ('data');

            var node = document.createElement('div');
            tr.appendChild(node);

            price.innerHTML = row[k].attributes.price.textContent;
            change.innerHTML = row[k].attributes.change.textContent;
            if (row[k].attributes.change.textContent[0] == '-') change.id = 'red';
            else change.id = 'green';
            percentChange.innerHTML = row[k].attributes.percentChange.textContent;
            if (row[k].attributes.percentChange.textContent[0] == '-') percentChange.id = 'red';
            else percentChange.id = 'green';
            node.appendChild(name);
            node.appendChild(price);
            node.appendChild(change);
            node.appendChild(percentChange);
        }
        
        block.appendChild(tableBlock);
        tableBlock.appendChild(table);

    }
    window.addEventListener("resize", function () {
        tablesNumberOnPage = Math.floor(window.innerWidth / tableWidth);
        location.reload();
    });

    function show(number) {
        var offset = number * (tableWidth + 10);
        
        block.style.transform = "translateX(-" + offset + "px)";
        for (var i = 0; i < block.children.length; i++) {
            if (block.children[i].offsetLeft - offset > window.innerWidth - tableWidth)
                block.children[i].style.opacity = 0;
            else
                block.children[i].style.opacity = 1;
        }
        leftTable = number;
    }
    


    var buttonPanel = document.createElement("div");
    buttonPanel.className = ("buttonPanel");


    for (var g = 0; g < totalButtons; g++) {
        var scrollButton = document.createElement("button");
        scrollButton.id = ("button" + g);
        scrollButton.className = ("scrollButton");
        (function(g) {
            scrollButton.addEventListener("click",
                function() {
                    show(g * tablesAmountOnPage);
                    document.getElementById("button" + g).style.backgroundColor = ("black");
                    for (var g1 = g + 1; g1 < totalButtons; g1++) {
                        document.getElementById("button" + g1).style.backgroundColor = ("#C1C1C1");
                    }
                    for (var g2 = g - 1; g2 >= 0; g2--) {
                        document.getElementById("button" + g2).style.backgroundColor = ("#C1C1C1");
                    }
                    lastButton = g;
                });
        })(g);
        buttonPanel.appendChild(scrollButton);
    }
    body.appendChild(buttonPanel);

    function colorButton() {
        var num = tableCounter() / tablesAmountOnPage - 1;
        document.getElementById("button" + num).style.backgroundColor = ("black");
        for (var a = num - 1; a >= 0; a--) {
            document.getElementById("button" + a).style.backgroundColor = ("#C1C1C1");
        }
        for (var a = num + 1;a < totalButtons; a++) {
            document.getElementById("button" + a).style.backgroundColor = ("#C1C1C1");
        }
    }

    var count = 0;
    var pos;

    function tableCounter() {
        count = 0;
        for (var i = 0; i < 12; i++) {
            if (block.children[i].style.opacity == 1)
                count++;
        }
        return count;
    }

    dragX();
    function dragX() {
        window.addEventListener("mousedown",
            function(e) {
                pos = e.pageX;
                e.preventDefault();
            });

        window.addEventListener("mouseup",
            function(e) {
                var tablesNow = Math.floor(window.innerWidth / (tableWidth + tableWidth / 40));
                var nowPosition = e.pageX;
                var move = pos - nowPosition;
                if ((move < 0) && (leftTable != 0)) {
                    show(leftTable - tablesNow);
                }
                else if (move > 0 && (leftTable + tablesNow) != block.children.length) {
                    show(leftTable + tablesNow);
                }
                colorButton();
            });
    }

    show(0);
    colorButton();
}