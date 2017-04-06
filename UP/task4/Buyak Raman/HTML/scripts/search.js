(function () {
  var data = JSON.parse(window.poc.data);
  var wrapper=document.createElement('div');
  wrapper.className=" middle bgcolor22";
  wrapper.style="border: 1px solid gray;"
  document.body.appendChild(wrapper);
  for(var i=0;i<data.length;i++)
  {
    var wrappelem=document.createElement('div');
    wrapper.appendChild(wrappelem);
    wrappelem.className="wrappelem";
    var image=document.createElement('img');
    wrappelem.appendChild(image);
    image.src=data[i].image;
    var header=document.createElement('h');
    header.textContent=data[i].name;
      header.className="newsName";
    wrappelem.appendChild(header);
    var descr=document.createElement('p');
    descr.className="description";
    descr.textContent=data[i].description;
    wrappelem.appendChild(descr);
  }

    var search=document.getElementsByTagName("input")[0];

    search.addEventListener("keyup",function(event){
      var context=search.value;
      var num=wrapper.children.length;
      for(i=0;i<num;++i)
      {
      wrapper.removeChild(wrapper.children[0]);
    }
    for(var i=0;i<data.length;i++)
    {
      if(data[i].description.indexOf(context)!=-1||data[i].name.indexOf(context)!=-1)
      {var wrappelem=document.createElement('div');
      wrapper.appendChild(wrappelem);
      wrappelem.className="wrappelem";
      var image=document.createElement('img');
      wrappelem.appendChild(image);
      image.src=data[i].image;
      var header=document.createElement('h');
      header.textContent=data[i].name;
      header.className="newsName";
      wrappelem.appendChild(header);
      var descr=document.createElement('p');
      descr.className="description";
      descr.textContent=data[i].description;
      wrappelem.appendChild(descr);
    }
    }
  });
})()
