var data = window.poc.xmldata
var body = document.body
const tableWidth = 322
var lastTableNumber = 0
const tableMargin = 20

function showTableNumber (number) {
  var wrapper = document.getElementsByClassName('wrapper')[0]
  var offset = number * tableWidth
  lastTableNumber = number
  wrapper.style.left = -offset + 'px'

  for (var g = 0; g < wrapper.children.length; g++) {
    if (wrapper.children[g].offsetLeft - offset - tableMargin > window.innerWidth - tableWidth) {
      wrapper.children[g].style.visibility = 'hidden'
    } else {
      wrapper.children[g].style.visibility = 'visible'
    }
  }
};

function generateButtons() {
  var buttons = document.getElementsByClassName('buttons')[0]
  var wrapper = document.getElementsByClassName('wrapper')[0]
  var numberOfTablesNow = Math.floor(window.innerWidth / tableWidth)
  var numberOfButtons = Math.ceil(wrapper.children.length / numberOfTablesNow)
  while (buttons.children.length > 0) {
    buttons.removeChild(buttons.children[0])
  }
  for (var i = 0; i < numberOfButtons; i++) {
    var button = document.createElement('button')
    button.textContent = 'button' + i
    buttons.appendChild(button);
    (function (i) {
      button.addEventListener('click', function () {
        showTableNumber(i * numberOfTablesNow)
      })
    })(i)
  }
};

(function () {
  var parser = new window.DOMParser()
  var xmlDoc = parser.parseFromString(data, 'text/xml')

  var wrapper = document.createElement('div')
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
    header.setAttribute('id', 'header')

    tr.appendChild(header)
    table.appendChild(tr)

    tr = document.createElement('tr')
    table.appendChild(tr)
    tr.setAttribute('id', 'columns')

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

  var elements = document.getElementsByClassName('change')
  for (var e = 0; e < elements.length; e++) {
    if (elements[e].textContent.substring(0, 1) === '-') {
      elements[e].style.color = '#E80000'
    } else {
      elements[e].style.color = '#339900'
    }
  }
}())

var buttons = document.createElement('div')
buttons.className = 'buttons'

body.appendChild(buttons)

window.addEventListener('resize', function () {
  generateButtons()
  showTableNumber(lastTableNumber)
})
