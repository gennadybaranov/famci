$(function() {
    var canvas = document.getElementById("field");
    var Context = canvas.getContext('2d');

    Context.beginPath();
    for (i = 1; i < 20; i++) {
        Context.moveTo(i * 20, 0);
        Context.lineTo(i * 20, 400);
        Context.lineWidth = 1;
    }
    for (i = 1; i < 20; i++) {
        Context.moveTo(0, i * 20);
        Context.lineTo(400, i * 20);
        Context.lineWidth = 1;
    }
    Context.stroke();

    var A = [];
    for (i = 0; i < 20; i++) {
        A.push([]);
        for (j = 0; j < 20; j++)
            A[i].push(false);
    }

    function draw() {
        for (i = 0; i < 20; i++)
            for (j = 0; j < 20; j++) {
                if (A[i][j])
                    Context.fillStyle = "Purple";
                else
                    Context.fillStyle = "#FFF0F5";
                Context.fillRect(i * 20, j * 20, 19, 19);
            }
    }


    function get_click(e) {
        var x = e.clientX;
        var y = e.clientY;
        x = x - canvas.offsetLeft;
        y = y - canvas.offsetTop;
        A[Math.floor(x / 20)][Math.floor(y / 20)] = !A[Math.floor(x / 20)][Math.floor(y / 20)];
        draw();
    }

    canvas.addEventListener("click", get_click, false);

    function position(e) {
        var x = Math.floor(e.clientX / 20) - 1;
        var y = Math.floor(e.clientY / 20) - 1;
        mass[x][y] = !mass[x][y];
        draw();
    }

    function find_neighbors(x, y) {
        var k = 0;
        for (var i = x - 1; i <= x + 1; ++i) {
            for (var j = y - 1; j <= y + 1; ++j)
                if (A[(i + 20) % 20][(j + 20) % 20] && (i != x || j != y))
                    ++k;
        }
        return k;
    }

    var templa = [];
    for (i = 0; i < 20; i++) {
        templa.push([]);
        for (j = 0; j < 20; j++)
            templa[i].push(false);
    }

    var gen_grow = function() {
        for (i = 0; i < 20; ++i) {
            for (j = 0; j < 20; ++j)
                templa[i].push(A[i][j]);
        }
        for (i = 0; i < 20; ++i) {
            for (j = 0; j < 20; ++j) {
                var numb = find_neighbors(i, j);
                if (numb < 2 || numb > 3)
                    templa[i][j] = false;
                else if (numb == 3)
                    templa[i][j] = true;
            }
        }
        for (i = 0; i < 20; ++i) {
            for (j = 0; j < 20; ++j)
                A[i][j] = templa[i][j];
        }
        draw();
        var cell = document.getElementById('generationNum');
        var count = Number(cell.innerHTML);
        cell.innerHTML = count += 1;
    }
    var begin = function() {
        myinterval = setInterval(gen_grow, 200);
    }

    var end = function() {
        clearInterval(myinterval);
    }
    $('#start').click(begin);
    $('#stop').click(end);
});
