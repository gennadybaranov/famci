(function() {
	document.addEventListener("DOMContentLoaded", ready);
  function ready() {

    var data = window.information.cakes;
		var body = document.body;
		var body_div = document.createElement("div");
    var context="";
    body_div.className="body_div";
		body.appendChild(body_div);

    var elements = JSON.parse(window.information.cakes);


    for (var i=0;i<elements.length;i++)
    {
      var line_div=document.createElement("div");
			var foto_line_div=document.createElement("div");
			var head_line_div=document.createElement("div");
			var info_line_div=document.createElement("div");
			var image=document.createElement("img");
			line_div.className="line_div";
		  foto_line_div.className="foto_line_div";
			info_line_div.className="info_line_div";
			head_line_div.className="head_line_div";
			image.className="image";

			image.src=elements[i].picture;
			foto_line_div.appendChild(image);
			line_div.appendChild(foto_line_div);
			head_line_div.textContent=elements[i].cake;
			line_div.appendChild(head_line_div);
			info_line_div.textContent=elements[i].description;
			line_div.appendChild(info_line_div);

			body_div.appendChild(line_div);
    }

		var search=document.getElementsByTagName("input")[0];
    search.addEventListener("keyup",function(event){
    var context=search.value;
    var num=body_div.children.length;
    for(i=0;i<num;i++)
		{
			if (elements[i].description.indexOf(context)!=-1||elements[i].cake.indexOf(context)!=-1){
				body_div.children[i].style.display="";
			}
			else
			{
    body_div.children[i].style.display="none";
		  }
    }
  });

  };
}());
