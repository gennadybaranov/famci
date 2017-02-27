"use strict" // latest javaScript standart

$(function(){
	// document elements
	var canvas = document.getElementById('field');
	var context = canvas.getContext('2d');
	var startBtn = document.getElementById('start');
	var stopBtn = document.getElementById('stop');
	var generation = document.getElementById('generationNum');
	var info = document.getElementsByClassName('info');
	var interval;

	// other variables
	var num = 30; // field dimendion
	var height = canvas.clientHeight;
	var width = canvas.clientWidth;
	var cellHeight = height/num;
	var cellWidth = width/num;
	var lineWidth = 1; // widht of border between cells

	var curField = [];
	var nextField = [];

	var Game = {};

	// methods definitions
	Game.getStartPosition = function() {	
		Game.renderField();
		Game.initFields();
		canvas.addEventListener('click', Game.fieldClick);
		stopBtn.addEventListener('click', Game.stopGame);
		startBtn.value = "Start";
		stopBtn.value = "Stop";
		generation.innerHTML = '0';
		
		info[0].style.visibility = "visible";

		stopBtn.disabled = true;
		stopBtn.classList.remove('activeBtn');
		stopBtn.classList.add('disabledBtn');
	}

	Game.gaming = function() {
		interval = setInterval(Game.update, 1000);
		canvas.removeEventListener('click', Game.fieldClick);

		info[0].style.visibility = "hidden";

		stopBtn.value = "Stop";
		stopBtn.removeEventListener('click', Game.getStartPosition);
		stopBtn.addEventListener('click', Game.stopGame);

		startBtn.disabled = true;
		startBtn.classList.remove('activeBtn');
		startBtn.classList.add('disabledBtn');
		stopBtn.disabled = false;
		stopBtn.classList.remove('disabledBtn');
		stopBtn.classList.add('activeBtn');
	}

	Game.stopGame = function() {
		clearInterval (interval);

		startBtn.disabled = false;
		startBtn.classList.remove('disabledBtn');
		startBtn.classList.add('activeBtn');
		startBtn.value = "Continue";
		stopBtn.value = "New Game";

		stopBtn.removeEventListener('click', Game.stopGame);
		stopBtn.addEventListener('click', Game.getStartPosition);
	}

	Game.update = function() {
		generation.innerHTML = Number(generation.innerHTML) + 1;

		for (var i = 0; i < num; i++){
			for (var j = 0; j < num; j++){
				var n = Game.aliveNeighbors(i, j);
				if (curField[i][j] === 1){
					if (n < 2 || n > 3)
						nextField[i][j] = 0;
					else
						nextField[i][j] = 1;
				}
				else{
					if (n === 3)
						nextField[i][j] = 1;
					else
						nextField[i][j] = 0;
				}
			}
		}

		for (var i = 0; i < num; i++){
			for (var j = 0; j < num; j++){
				if (nextField[i][j] !== curField[i][j]){
					var sx = j*cellWidth;
					var sy = i*cellHeight;
					Game.drawRect (sx+lineWidth, sy+lineWidth, cellWidth-2*lineWidth, cellHeight-2*lineWidth, nextField[i][j]);	
				}
			}
		}

		for (var i = 0; i < nextField.length; i++)
			curField[i] = nextField[i].slice();
	}

	Game.aliveNeighbors = function(r, c) {
		var cnt = 0;

		if (r === 0){
			if (curField[num-1][c] === 1)
				cnt++;
			if (c === 0){
				if (curField[num-1][num-1] === 1)
					cnt++;
			}
			else
				if (curField[num-1][c-1] === 1)
					cnt++;
			if (c === num-1){
				if (curField[num-1][0] === 1)
					cnt++;
			}
			else
				if (curField[num-1][c+1] === 1)
					cnt++;
		}
		else{
			if (curField[r-1][c] === 1)
				cnt++;
			if (c === 0){
				if (curField[r-1][num-1] === 1)
					cnt++;
			}
			else
				if (curField[r-1][c-1] === 1)
					cnt++;
			if (c === num-1){
				if (curField[r-1][0] === 1)
					cnt++;
			}
			else
				if (curField[r-1][c+1] === 1)
					cnt++;	
		}

		if (c === 0){
			if (curField[r][num-1] === 1)
				cnt++;
		}
		else		
			if (curField[r][c-1] === 1)
				cnt++;
		if (c === num-1){
			if (curField[r][0] === 1)
				cnt++;
		}
		else
			if (curField[r][c+1] === 1)
				cnt++;

		if (r === num-1){
			if (curField[0][c] === 1)
				cnt++;
			if (c === 0){
				if (curField[0][num-1] === 1)
					cnt++;
			}
			else
				if (curField[0][c-1] === 1)
					cnt++;
			if (c === num-1){
				if (curField[0][0] === 1)
					cnt++;
			}
			else
				if (curField[0][c+1] === 1)
					cnt++;
		}
		else{
			if (curField[r+1][c] === 1)
				cnt++;
			if (c === 0){
				if (curField[r+1][num-1] === 1)
					cnt++;
			}
			else
				if (curField[r+1][c-1] === 1)
					cnt++;
			if (c === num-1){
				if (curField[r+1][0] === 1)
					cnt++;
			}
			else
				if (curField[r+1][c+1] === 1)
					cnt++;
		}		

		return cnt;	
	}

	Game.renderField = function() {
		context.clearRect(0,0,width,height);
		context.strokeStyle="#000";
		for (var i = 0; i < num - 1; i++){
			var dx = width/num*(i+1);
			var dy = height/num*(i+1);
			context.beginPath();
			context.lineWidth=lineWidth;
			context.moveTo(0, dy);
			context.lineTo(width, dy);
			context.stroke();
			context.beginPath();
			context.moveTo(dx, 0);
			context.lineTo(dx, height);
			context.stroke();
		}
	}

	Game.initFields = function() {
		for (var i = 0; i < num; i++){
			curField[i] = [];
			nextField[i] = [];
			for (var j = 0; j < num; j++){
				curField[i][j] = 0;
				nextField[i][j] = 0;
			}
		}			
	}		

	Game.fieldClick = function(e) {
		var rect = canvas.getBoundingClientRect();
		var x = e.clientX - Math.floor(rect.left);
		var y = e.clientY - Math.floor(rect.top);
		var row = Math.floor(y/cellHeight);
		var column = Math.floor(x/cellWidth);

		var sx = (column)*cellWidth;
		var sy = (row)*cellHeight;
		if (curField[row][column] === 0){
			curField[row][column] = 1;
			Game.drawRect(sx+lineWidth, sy+lineWidth, cellWidth-2*lineWidth, cellHeight-2*lineWidth, 1);
		}
		else if (curField[row][column] === 1){
			curField[row][column] = 0;
			Game.drawRect(sx+lineWidth,sy+lineWidth,cellWidth-2*lineWidth,cellHeight-2*lineWidth,0);
		}
		else{
			alert('clickProcess: the value of chosen cell: ' + curField[row][column]);
		}
	}

	Game.drawRect = function(sx, sy, width, height, val) {
		if (val === 1){
			context.fillStyle = "#4aa54d";
		}
		if (val === 0){
			context.fillStyle = "#7c7c7c";
		}
		context.fillRect(sx, sy, width, height);
	}

	// first call of game
	Game.getStartPosition();
	startBtn.addEventListener('click', Game.gaming);

});