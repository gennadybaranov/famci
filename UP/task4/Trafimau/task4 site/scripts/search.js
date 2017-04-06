"use strict"
document.addEventListener('DOMContentLoaded', function(){
	var data = JSON.parse(window.poc.JSONdata);

	var list = document.createElement('ul');
	list.classList.add('list');
	var postsWrapper = document.getElementById('postsWrapper');
	postsWrapper.appendChild(list);

	appendData();

	var inputForm = document.getElementById('input');
	inputForm.addEventListener('keyup', function(e){
		for (var i = 0; i < data.length; i++){
			var curInput = inputForm.value.toLowerCase();
			list.children[i].style.display = "";

			var nameText = data[i].name.toLowerCase();
			var descriptionText = data[i].description.toLowerCase();
			var pos = -1;
			if ( (pos = nameText.indexOf(curInput)) != -1){

			}
			else{
				if ( (pos = descriptionText.indexOf(curInput)) != -1){
				
				}
				else{
					list.children[i].style.display = "none";
				}
			}
		}
	});

	function appendData (){
	for (var i = 0; i < data.length; i++){
		var curPost = document.createElement('li');
		curPost.classList.add('post');
		list.appendChild(curPost);
		
		var text = document.createElement('div');
		text.classList.add('postText');
		curPost.appendChild(text);

		var image = document.createElement('div');
		image.classList.add('postImage');
		curPost.appendChild(image);
		image.style.backgroundImage = 'url("' + data[i].imageURL + '")';

		var title = document.createElement('h2');
		title.classList.add('postTitle');
		title.innerHTML = data[i].name;
		text.appendChild(title);

		var description = document.createElement('p');
		description.classList.add('postDescription');
		description.innerHTML = data[i].description;
		text.appendChild(description);

		var date = document.createElement('p');
		date.classList.add('postDate');
		date.innerHTML = "posted: " + data[i].date;
		text.appendChild(date);
	}
}
});