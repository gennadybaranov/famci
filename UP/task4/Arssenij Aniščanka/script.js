;/*data*/
window.info = {};
window.info.news =
' [ \
  { \
    "image": "images/news1.jpg", \
    "title": "Lorem ipsum dolor.", \
    "description": "Cras eget enim eget tortor pharetra convallis. Aenean non venenatis erat. Nam porta justo mauris. Curabitur porta odio a convallis condimentum. Vivamus mi metus, lobortis sagittis commodo et, rhoncus in ex. Vestibulum mollis ante a mollis dapibus. Nulla ullamcorper facilisis leo, vitae dignissim arcu porttitor mollis. Donec vel ornare sem. " \
  }, \
  { \
    "image": "images/news2.jpg", \
    "title": "Maecenas molestie sollicitudin.", \
    "description": "Phasellus ullamcorper risus in dolor ultricies, volutpat vulputate ligula laoreet. Cras dignissim leo sit amet urna auctor, vel tristique odio ornare. Cras id suscipit neque, ac aliquet mauris. Duis bibendum convallis mauris, id finibus tellus volutpat vel. Etiam pharetra magna vel enim efficitur vestibulum. Vivamus ac purus mauris. Sed blandit." \
  }, \
  { \
    "image": "images/news3.jpg", \
    "title": "Donec auctor ut.", \
    "description": "Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam convallis interdum lacus, quis ultrices tellus molestie a. In faucibus nec dui in feugiat. Nam eros quam, maximus ut ipsum a, vulputate euismod leo. Morbi ligula magna, euismod nec tincidunt eu, accumsan vel sapien. Nullam molestie nibh eu rutrum. " \
  }, \
  { \
    "image": "images/news4.jpg", \
    "title": "Phasellus ac sapien.", \
    "description": "Aenean ac massa ac augue dapibus vulputate id et lectus. Nullam eu rutrum turpis. Etiam facilisis a urna non mollis. Nunc quis nibh dui. Morbi tempus suscipit odio a venenatis. Proin nunc leo, placerat ut tristique at, vulputate ac metus. Sed tempus iaculis sagittis. Morbi congue consectetur rhoncus. Pellentesque blandit. " \
  }, \
  { \
    "image": "images/news5.jpg", \
    "title": "Pellentesque sit amet.", \
    "description": "Sed auctor euismod dolor sed cursus. Proin metus ipsum, porta eu dolor pretium, malesuada sollicitudin dui. Duis eget magna ut felis eleifend consequat. Nulla eget ante id mauris egestas congue. Vivamus a suscipit lacus, non mattis leo. Mauris dictum sagittis mattis. Aliquam vestibulum feugiat libero, ac fermentum leo sodales vel. " \
  }, \
  { \
    "image": "images/news6.jpg", \
    "title": "Sed ac ante.", \
    "description": "Quisque pellentesque, metus a molestie consequat, massa arcu vulputate felis, consequat vulputate mauris nibh a mi. Ut malesuada condimentum fringilla. Nunc vulputate mi elementum ante rhoncus, vitae lacinia leo vulputate. Nam a odio at purus viverra efficitur in id nulla. Nulla egestas iaculis lorem, vitae vestibulum odio porttitor molestie. Fusce. " \
  }, \
  { \
    "image": "images/news7.jpg", \
    "title": "Aliquam id massa.", \
    "description": "Vivamus quis faucibus nisi. Nunc tincidunt erat ac erat lacinia, ut eleifend orci vulputate. Nullam consequat vulputate mauris, ut venenatis ex iaculis vel. Proin sapien massa, euismod et condimentum vitae, aliquam sit amet est. Fusce porta quam ut ultrices feugiat. Orci varius natoque penatibus et magnis dis parturient montes, nascetur. " \
  }, \
  { \
    "image": "images/news8.jpg", \
    "title": "Nunc cursus at.", \
    "description": "Maecenas maximus congue augue, sit amet convallis purus porta sed. Fusce imperdiet hendrerit massa vitae efficitur. Aenean eget felis risus. Donec lobortis dignissim elit et sagittis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Phasellus accumsan, libero id vulputate semper, massa justo molestie orci, eu. " \
  }, \
  { \
    "image": "images/news9.jpg", \
    "title": "Integer ut quam. ", \
    "description": "Pellentesque sit amet neque at leo feugiat bibendum. Proin eget metus feugiat eros iaculis ultrices sed eget justo. Aenean a gravida arcu. Fusce blandit accumsan nibh, porta vehicula leo. Donec non suscipit est. Sed vulputate lectus vel tincidunt tempor. Aliquam sed enim vitae nunc vulputate volutpat id eu quam. Vestibulum. " \
  } \
  ]';

  window.info.blogs =' [ \
  { \
    "image": "images/blogs1.jpg", \
    "title": "Vivamus bibendum, mauris ut." \
  }, \
  { \
    "image": "images/blogs2.jpg", \
    "title": "Quisque quis est magna." \
  }, \
  { \
    "image": "images/blogs3.jpg", \
    "title": "Fusce arcu dui, faucibus." \
  }, \
  { \
    "image": "images/blogs4.jpg", \
    "title": "Suspendisse consectetur purus eu." \
  }, \
  { \
    "image": "images/blogs5.jpg", \
    "title": "Donec cursus elit vel. " \
  }, \
  { \
    "image": "images/blogs6.jpg", \
    "title": "Sed laoreet finibus quam. " \
  }, \
  { \
    "image": "images/blogs7.jpg", \
    "title": "Vivamus ac lobortis nulla." \
  }, \
  { \
    "image": "images/blogs8.jpg", \
    "title": "Quisque feugiat massa non." \
  }, \
  { \
    "image": "images/blogs9.jpg", \
    "title": "Aenean maximus urna vel." \
  } \
  ]';

  (function(){
  	document.addEventListener("DOMContentLoaded", main);
  	function main(){

  		var homeButton = document.getElementById('home');
  		var newsButton = document.getElementById('news');
  		var blogsButton = document.getElementById('blogs');
  		var tmpButton = homeButton;
  		var content = document.getElementById('content');

  		var tmp = document.createElement('div');
  		tmp.classList.add('home');

  		var tmpText = document.createElement('div');
  		tmpText.classList.add('text');
  		var tmpHeader = document.createElement('h1');
  		tmpHeader.innerHTML = "Lorem Ipsum";
  		tmpText.appendChild(tmpHeader);
  		var tmpDescription = document.createElement('p');
  		tmpDescription.innerHTML = "Mauris vehicula eros tellus, quis consectetur dolor elementum a. Ut nec auctor purus. In viverra, erat non posuere imperdiet, quam ligula blandit neque, id tempus dui risus et erat. Pellentesque tempus ipsum non scelerisque venenatis. Donec volutpat rhoncus massa, a finibus metus porttitor eget. Sed sit amet eleifend velit, et vulputate nisl. Proin eleifend nec eros eget luctus. Cras pellentesque ex id enim laoreet lacinia. Nulla non orci sed turpis mattis elementum at vel nisl. Phasellus molestie, quam a consequat commodo, ante justo commodo nisi, eu finibus diam neque non sapien. Integer ultricies dictum risus, in feugiat lacus scelerisque nec. Morbi mattis viverra nisi sit amet sollicitudin. Mauris maximus posuere mauris, id laoreet dolor elementum in. Etiam ac nibh accumsan, blandit dolor at, vulputate ex. In sit amet eros aliquet, tincidunt velit a, aliquet ante. ";
  		tmpText.appendChild(tmpDescription);
  		tmp.appendChild(tmpText);
  		var tmpImage = document.createElement('img');
  		tmpImage.src = "images/icon.png";
  		tmp.appendChild(tmpImage);
  		content.appendChild(tmp);

  		homeButton.onclick = function(e){
  			tmpButton.classList.remove('active');
  			tmpButton = homeButton;
  			tmpButton.classList.add('active');
  			var home = document.createElement('div');
  			home.classList.add('home');
  			var Text = document.createElement('div');
  			Text.classList.add('text');
  			var Header = document.createElement('h1');
  			Header.innerHTML = "Lorem Ipsum";
  			Text.appendChild(Header);
  			var Description = document.createElement('p');
  			Description.innerHTML = "Mauris vehicula eros tellus, quis consectetur dolor elementum a. Ut nec auctor purus. In viverra, erat non posuere imperdiet, quam ligula blandit neque, id tempus dui risus et erat. Pellentesque tempus ipsum non scelerisque venenatis. Donec volutpat rhoncus massa, a finibus metus porttitor eget. Sed sit amet eleifend velit, et vulputate nisl. Proin eleifend nec eros eget luctus. Cras pellentesque ex id enim laoreet lacinia. Nulla non orci sed turpis mattis elementum at vel nisl. Phasellus molestie, quam a consequat commodo, ante justo commodo nisi, eu finibus diam neque non sapien. Integer ultricies dictum risus, in feugiat lacus scelerisque nec. Morbi mattis viverra nisi sit amet sollicitudin. Mauris maximus posuere mauris, id laoreet dolor elementum in. Etiam ac nibh accumsan, blandit dolor at, vulputate ex. In sit amet eros aliquet, tincidunt velit a, aliquet ante. ";
  			Text.appendChild(Description);
  			home.appendChild(Text);
  			var Image = document.createElement('img');
  			Image.src = "images/icon.png";
  			home.appendChild(Image);
  			content.replaceChild(home, tmp);
  			tmp = home;
  			input = document.createElement('input');
  		}


  		var news = document.createElement('div');
  		var inputNews = document.createElement('input');
  		newsButton.onclick = function(e){
  			tmpButton.classList.remove('active');
  			tmpButton = newsButton;
  			tmpButton.classList.add('active');
  			news = document.createElement('div');
  			news.classList.add('news');
  			var nav = document.createElement('nav');
  			var header = document.createElement('h1');
  			header.innerHTML = "News";
  			nav.appendChild(header);
  			inputNews.type = "text";
  			inputNews.placeholder = "Search...";
  			inputNews.value = "";
  			nav.appendChild(inputNews);	
  			news.appendChild(nav);
  			var articles = document.createElement('div');
  			articles.classList.add('articles');
  			var dataNews = JSON.parse(info.news);
  			for (var i = 0; i < dataNews.length; ++i){
  				var article = document.createElement('article');
  				var title = document.createElement('h2');
  				title.innerHTML = dataNews[i].title;
  				article.appendChild(title);
  				var newsBlock = document.createElement('div');
  				newsBlock.classList.add('block');
  				var description = document.createElement('p');
  				description.innerHTML = dataNews[i].description;
  				newsBlock.appendChild(description);
  				var image = document.createElement('img');
  				image.src = dataNews[i].image;
  				newsBlock.appendChild(image);
  				article.appendChild(newsBlock);
  				articles.appendChild(article);
  			}
  			news.appendChild(articles);
  			content.replaceChild(news, tmp);
  			tmp = news;
  		}


  		var blogs = document.createElement('div');
  		var inputBlogs = document.createElement('input');
  		blogsButton.onclick = function(e){
  			tmpButton.classList.remove('active');
  			tmpButton = blogsButton;
  			tmpButton.classList.add('active');
  			blogs = document.createElement('div');
  			blogs.classList.add('blogs');
  			var nav = document.createElement('nav');
  			var header = document.createElement('h1');
  			header.innerHTML = "Blogs";
  			nav.appendChild(header);
  			inputBlogs.type = "text";
  			inputBlogs.placeholder = "Search...";
  			inputBlogs.value = "";
  			nav.appendChild(inputBlogs);	
  			blogs.appendChild(nav);
  			var articles = document.createElement('div');
  			articles.classList.add('articles');
  			var dataBlogs = JSON.parse(info.blogs);
  			for (var i = 0; i < dataBlogs.length; ++i){
  				var article = document.createElement('article');
  				article.style = "background: url(" + dataBlogs[i].image + ");";
  				var title = document.createElement('h2');
  				title.innerHTML = dataBlogs[i].title;
  				article.appendChild(title);
  				articles.appendChild(article);
  			}
  			blogs.appendChild(articles);
  			content.replaceChild(blogs, tmp);
  			tmp = blogs;
  		}

  		inputNews.addEventListener('keyup', function(e){
  			var inputData = inputNews.value.toLowerCase();
  			for(var i = 0; news.children[1].children[i]; ++i){
  				var tmpString = news.children[1].children[i].children[0].innerHTML.toLowerCase();
  				if(tmpString.indexOf(inputData) != -1){
  					news.children[1].children[i].style.display = "";
  				}
  				else{
  					news.children[1].children[i].style.display = "none";
  				}
  			}
  		});

  		inputBlogs.addEventListener('keyup', function(e){
  			var inputData = inputBlogs.value.toLowerCase();
  			for(var i = 0; blogs.children[1].children[i]; ++i){
  				var tmpString = blogs.children[1].children[i].children[0].innerHTML.toLowerCase();
  				if(tmpString.indexOf(inputData) != -1){
  					blogs.children[1].children[i].style.display = "";
  				}
  				else{
  					blogs.children[1].children[i].style.display = "none";
  				}
  			}
  		});

  	}
  })();