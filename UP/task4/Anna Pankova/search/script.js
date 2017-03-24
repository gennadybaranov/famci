document.addEventListener('DOMContentLoaded', function() {
  window.poc = {};
  window.poc.jsonData =' [ \
    { \
      "id": "58bac7ca9bad60c729fdc620", \
      "imageUrl": "images/ball.jpg", \
      "name": "Мининформ вынес предупреждения TUT.by", \
      "description": "Речь идет о материале: родители написали сначала..." \
    }, \
    { \
      "id": "58bac7ca9bad60c729fdc621", \
      "imageUrl": "images/ball.jpg", \
      "name": "В Африке и в Америке", \
      "description": "Повышение цен не затронет основых ресурсов страны" \
    }, \
    { \
      "id": "58bac7ca9bad60c729fdc622", \
      "imageUrl": "images/ball.jpg", \
      "name": "Польша отказывается покупать", \
      "description": "Введение санкций" \
    }, \
    { \
      "id": "58bac7ca9bad60c729fdc623", \
      "imageUrl": "images/ball.jpg", \
      "name": "Вышла новая серия Шерлока на BBC", \
      "description": "Новые фильмы и сериалы" \
    }, \
    { \
      "id": "58bac7ca9bad60c729fdc624", \
      "imageUrl": "images/football.png", \
      "name": "Остался на поле и не забил пенальти", \
      "description": "Чемпионат Франции " \
    } ] ';

  let data = JSON.parse(window.poc.jsonData);
  let body = document.body;

  let wrapper = document.getElementById('itemsWrapper')
  wrapper.className = "wrapper"

  for (let i = 0; i < data.length; i++) {

    let news = document.createElement('li')
    let name = document.createElement('h1')
    let description = document.createElement('p')
    let imageDiv = document.createElement('div')
    let img = document.createElement('img')
    let textContent = document.createElement('div')

    name.innerHTML = data[i].name
    name.className = 'header'

    description.innerHTML = data[i].description
    description.className = 'description'

    textContent.className = 'textContent'
    textContent.appendChild(name)
    textContent.appendChild(description)

    img.src = data[i].imageUrl
    imageDiv.className = 'image'
    imageDiv.appendChild(img)

    news.className = 'news'
    news.setAttribute('data-id', data[i].id)

    news.appendChild(textContent)
    news.appendChild(imageDiv)
    wrapper.appendChild(news)

  }

  let search = document.getElementById('search');
  search.addEventListener('keyup', function() {
    let currentValue = search.value
    for (let i = 0; i < data.length; i++) {
      if (data[i].name.toLowerCase().indexOf(currentValue.toLowerCase()) != -1) {
        document.querySelector('[data-id="' + data[i].id +'"]').style.display = 'flex'
      } else {
        document.querySelector('[data-id="' + data[i].id +'"]').style.display = 'none'
      }
    }
  })

  function nameFocus( e ) {
    var element = e.target || window.event.srcElement;
    if (element.value == "search") element.value = "";
  }

  function nameBlur( e ) {
    var element = e.target || window.event.srcElement;
    if (element.value == "") element.value = "search";
  }

  if (search.addEventListener) {
    search.addEventListener("focus", nameFocus, false);
    search.addEventListener("blur", nameBlur, false);
  } else if (search.attachEvent) {
    search.attachEvent("onfocus", nameFocus);
    search.attachEvent("onblur", nameBlur);
  }


})
