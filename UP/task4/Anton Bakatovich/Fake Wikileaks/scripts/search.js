document.addEventListener('DOMContentLoaded', function(){
	var data = JSON.parse(window.poc.JSONdata);
	var list = document.createElement('ul');
	list.classList.add('list');
	var postCover = document.getElementById('postCover');
	postCover.appendChild(list);
	Data();

	var search = document.getElementById('input');
	search.addEventListener('keyup', function(e) 
	{
		for ( var i = 0; i < data.length; ++i ) {
			var currentInput = search.value.toLowerCase();
			var nameText     = data[i].name.toLowerCase();
			var desText      = data[i].description.toLowerCase();
			list.children[i].style.display = "";

			if ( nameText.indexOf(currentInput) != -1 || desText.indexOf(currentInput) != -1 ) {
				;
			}
			else {
				list.children[i].style.display = "none";
			}
		}
	});

	function Data() 
	{
	for ( var i = 0; i < data.length; ++i ) {
		var current = document.createElement('li');
		current.classList.add('post');
		list.appendChild(current);
		
		var text = document.createElement('div');
		text.classList.add('text');
		current.appendChild(text);

		var image = document.createElement('div');
		image.classList.add('image');
		current.appendChild(image);
		image.style.backgroundImage = 'url("' + data[i].imageURL + '")';

		var name = document.createElement('h2');
		name.classList.add('name');
		name.innerHTML = data[i].name;
		text.appendChild(name);

		var description = document.createElement('p');
		description.classList.add('description');
		description.innerHTML = data[i].description;
		text.appendChild(description);
	}
}
});