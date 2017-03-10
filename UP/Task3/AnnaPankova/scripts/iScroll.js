(function () {
  var data = window.poc.xmldata
  var body = document.body
  const tableWidth = 322
  var lastTableNumber = 0
  const tableMargin = 20
  var numberOfTablesNow = Math.floor(window.innerWidth / tableWidth)
  var wrapper = document.createElement('div')

  function showTableNumber (number) {
    var offset = number * tableWidth
    lastTableNumber = number
    wrapper.style.transform = 'translateX(-' + offset + 'px)'

    for (var g = 0; g < wrapper.children.length; g++) {
      if (wrapper.children[g].offsetLeft - offset - tableMargin > window.innerWidth - tableWidth) {
        wrapper.children[g].style.opacity = 0
      } else {
        wrapper.children[g].style.opacity = 1
      }
    }
  };

  var parser = new window.DOMParser()
  var xmlDoc = parser.parseFromString(data, 'text/xml')
  var title = document.createElement('div')
  title.textContent = 'Title / Header of Lane'
  title.className = 'title'

  body.appendChild(title)
  wrapper.className = 'wrapper'
  body.appendChild(wrapper)

  for (var i = 0; i < xmlDoc.children[0].children.length; i++) {
    var tableWrapper = document.createElement('div')

    var dataTable = xmlDoc.children[0].children[i]
    var columns = dataTable.getElementsByTagName('column')
    var rows = dataTable.getElementsByTagName('row')
    var table = document.createElement('table')

    var tr = document.createElement('tr')
    var header = document.createElement('th')
    header.colSpan = columns.length
    var headerName = dataTable.attributes.name.textContent
    header.textContent = headerName
    header.className = 'header'

    tr.appendChild(header)
    table.appendChild(tr)

    tr = document.createElement('tr')
    table.appendChild(tr)
    tr.className = 'columns'

    for (var j = 0; j < columns.length; j++) {
      var label = columns[j].attributes.label.textContent
      var th = document.createElement('th')
      th.innerHTML = label
      th.setAttribute('class', 'cells')
      tr.appendChild(th)
    }

    for (var k = 0; k < rows.length; k++) {
      tr = document.createElement('tr')
      td = document.createElement('td')
      td.className = 'company'
      td.innerHTML = rows[k].attributes.name.textContent
      tr.appendChild(td)
      td.colSpan = rows.length
      table.appendChild(tr)

      tr = document.createElement('tr')
      tr.className = 'numbers'
      var price = document.createElement('td')
      var change = document.createElement('td')
      var percentChange = document.createElement('td')

      price.className = 'price'
      change.className = 'change'
      percentChange.className = 'change'

      price.innerHTML = rows[k].attributes.price.textContent
      change.innerHTML = rows[k].attributes.change.textContent
      percentChange.innerHTML = rows[k].attributes.percentChange.textContent
      tr.appendChild(price)
      tr.appendChild(change)
      tr.appendChild(percentChange)

      table.appendChild(tr)

      var hr = document.createElement('div')
      hr.className = 'hr'
      table.appendChild(hr)

      tr = document.createElement('tr')
      var td = document.createElement('td')
      td.colSpan = 3
      td.appendChild(hr)
      tr.appendChild(td)
      table.appendChild(tr)
    }
    wrapper.appendChild(tableWrapper)
    tableWrapper.appendChild(table)
    tableWrapper.className = 'tableWrapper'
  }

  var elements = Array.from(document.getElementsByClassName('change'))
  for (var e = 0; e < elements.length; e++) {
    if (elements[e].textContent[0] === '-') {
      elements[e].className = 'red'
    } else {
      elements[e].className = 'green'
    }
  }

  var buttons = document.createElement('div')
  buttons.className = 'buttons'
  body.appendChild(buttons)

  function generateButtons () {
    var buttons = document.getElementsByClassName('buttons')[0]
    var wrapper = document.getElementsByClassName('wrapper')[0]
    numberOfTablesNow = Math.floor(window.innerWidth / tableWidth)
    var numberOfButtons = Math.ceil(wrapper.children.length / numberOfTablesNow)
    while (buttons.children.length > 0) {
      buttons.removeChild(buttons.children[0])
    }
    for (var i = 0; i < numberOfButtons; i++) {
      var button = document.createElement('div')
      buttons.appendChild(button);
      (function (i) {
        button.addEventListener('click', function () {
          showTableNumber(i * numberOfTablesNow)
          highlightButton(i)
        })
      })(i)
    }
  }

  function highlightButton (number) {
    for (var i = 0; i < buttons.children.length; i++) {
      buttons.children[i].className = ''
    }
    buttons.children[number].className = 'active'
  }

  window.addEventListener('resize', function () {
    generateButtons()
    showTableNumber(lastTableNumber)
  })

  generateButtons()
  showTableNumber(0)
  highlightButton(0)

  var curXPos = 0
  var move = 0

  window.addEventListener('mousedown', function (e) {
    curXPos = e.pageX
    e.preventDefault()
  })

  window.addEventListener('mouseup', function (e) {
    var numberOfTablesOnScreen = Math.floor(window.innerWidth / tableWidth)

    move = curXPos - e.pageX
    if (move < 0) {
      if (lastTableNumber < numberOfTablesOnScreen) {
        showTableNumber(0)
      } else {
        showTableNumber(lastTableNumber - numberOfTablesOnScreen)
      }
    } else if (move > 0) {
      if (lastTableNumber + numberOfTablesOnScreen < wrapper.children.length) {
        showTableNumber(lastTableNumber + numberOfTablesOnScreen)
      }
    }
    highlightButton(lastTableNumber / numberOfTablesOnScreen)
  })
})()
