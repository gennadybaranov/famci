document.addEventListener('DOMContentLoaded', function() {
  window.poc = {};
  window.poc.jsonData =' [ \
    { \
      "id": "58bac7ca9bad60c729fdc62f", \
      "imageUrl": "images/ball.jpg", \
      "name": "Вышлв новая серия Шерлока на BBC", \
      "description": "Чемпионат Англии " \
    }, \
    { \
      "id": "58bac7ca9bad60c729fdc62d", \
      "imageUrl": "images/football.png", \
      "name": "остался на поле и не забил пенальти", \
      "description": "Чемпионат Франции " \
    } ] ';

  let data = JSON.parse(window.poc.jsonData);
  let body = document.body;

  let wrapper = document.getElementById('itemsWrapper')

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
})
