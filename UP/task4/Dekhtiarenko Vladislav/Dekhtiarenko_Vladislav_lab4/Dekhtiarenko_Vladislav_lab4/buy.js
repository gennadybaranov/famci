document.addEventListener("DOMContentLoaded", page);

function page() {
    var data = JSON.parse(window.data.cars);
    var wrapper = document.getElementById("wrapper");
    
    for (var i = 0; i < 5; i++) {
        var div = document.createElement("div");
        div.className = ("block");
        div.id = data[i].id;

        var image = document.createElement("img");
        image.src = data[i].imageUrl;
        image.className = ("images");

        var h1 = document.createElement("h1");
        h1.innerHTML = data[i].name;

        var p = document.createElement("p");
        p.innerHTML = data[i].description;

        div.appendChild(h1);
        div.appendChild(p);
        div.appendChild(image);
        wrapper.appendChild(div);
    }
}

function search() {
    var input = document.getElementById("myInput");
    var filter = input.value.toUpperCase();
    for (j = 0; j < 9; j++) {
        var searchDiv = document.getElementById("id" + j);
        if (searchDiv) {
            if ((searchDiv.childNodes[1].innerHTML.toUpperCase().indexOf(filter) > -1) || (searchDiv.childNodes[0].innerHTML.toUpperCase().indexOf(filter) > -1)) {
                searchDiv.style.display = "";
            } else {
                searchDiv.style.display = "none";
            }
        }
    }

}