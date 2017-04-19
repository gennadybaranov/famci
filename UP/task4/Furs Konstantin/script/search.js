document.addEventListener('DOMContentLoaded', function(){
	var info = JSON.parse(window.poc.JSON);
	var data = document.getElementById('info');
	var base = document.createElement('ul');
		base.classList.add('base');
		data.appendChild(base);
	Get();

	function Get() 
	{
		for ( var i = 0; i < info.length; ++i ) {
			var current = document.createElement('li');
			var text = document.createElement('div');
			var image = document.createElement('div');
			var name = document.createElement('h2');
			var description = document.createElement('p');

			current.classList.add('indent');
			text.classList.add('text');
			image.classList.add('image');
			name.classList.add('name');
			description.classList.add('description');

			base.appendChild(current);
			current.appendChild(text);
			current.appendChild(image);
			text.appendChild(name);
			text.appendChild(description);

			description.innerHTML = info[i].description;
			image.style.backgroundImage = 'url("' + info[i].imageURL + '")';
			name.innerHTML = info[i].name;
	}

	var search = document.getElementById('input');
	search.addEventListener('keyup', function(e) 
	{
		for ( var i = 0; i < info.length; ++i ) {

			var value = search.value.toLowerCase();
			var name = info[i].name.toLowerCase();
			var description = info[i].description.toLowerCase();

			if ( name.indexOf(value) >= 0 || description.indexOf(value) >= 0 ) {
				base.children[i].style.display = "";
			}
			else {
				base.children[i].style.display = "none";
			}
		}
	});
}});