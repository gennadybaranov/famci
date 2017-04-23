(function()
{
	document.addEventListener("DOMContentLoaded", script);

	function script()
	{
		var xmlStr = window.poc.xmldata;
		var parser = new DOMParser();
		var xml = parser.parseFromString(xmlStr, "application/xml");
		var data = xml.getElementsByTagName('data')[0];
		var base = data.getElementsByTagName('dataTable');

		var pack = document.createElement('div');
			pack.classList.add('pack');
		document.body.appendChild(pack);

		var title = document.createElement('div');
			title.classList.add('title');
			title.innerHTML = "TITLE";
		pack.appendChild(title);

		var struct = document.createElement('div');
			struct.classList.add('struct');
		pack.appendChild(struct);

		var iscroll = document.createElement('div');
			iscroll.classList.add('iscroll');
		pack.appendChild(iscroll);
		
		var buttonstr = document.createElement('div');
			buttonstr.classList.add('buttonstr');

		for (var i = 0; i < base.length; i++){
			createtable(base[i]);
		}
		var nowpage;
		var pagesnumber;
		var width = struct.children[0].offsetWidth;
		var getnum;
		refresh();

		function createtable(tempTable)
		{
			var tablestruct = document.createElement('div');
			tablestruct.classList.add("tablestruct");
			struct.appendChild(tablestruct);

			var tempstruct = document.createElement('table');
			tempstruct.classList.add('table');
			tablestruct.appendChild(tempstruct);	

			var str = tempTable.getElementsByTagName('column');
			var tabletitle  = tempstruct.insertRow();
			var tabletitles = tabletitle.insertCell();
			tabletitles.innerHTML = tempTable.getAttribute('name');
			tabletitles.colSpan   = str.length;

			tabletitles.classList.add('tableTitle');

			var strname = tempstruct.insertRow();
			strname.classList.add('strname');
			var curAver = [];
			for (var i = 0; i < str.length; i++)
			{
				strname.insertCell().innerHTML = str[i].getAttribute('label');
				curAver[i] = str[i].getAttribute('name');
			}

			var miniStr = tempTable.getElementsByTagName('row');
			for (var i = 0; i < miniStr.length; i++)
			{
				var miniTitle = tempstruct.insertRow();	
				var titlestr = miniTitle.insertCell(); 
				titlestr.innerHTML = miniStr[i].getAttribute('name');
				titlestr.colSpan = str.length;
				titlestr.classList.add('miniTitle');
				var curStr = tempstruct.insertRow();
				curStr.classList.add('infoStr');

				for (var j = 0; j < str.length; j++)
				{
					var pane = curStr.insertCell();
					pane.innerHTML = miniStr[i].getAttribute(curAver[j]);	
					if (curAver[j] === 'change' || curAver[j] === 'percentChange')
					{
						if (parseFloat(miniStr[i].getAttribute(curAver[j])) < 0)
						{
							pane.classList.add('colour1');
						}
						if (parseFloat(miniStr[i].getAttribute(curAver[j])) > 0)
						{
							pane.classList.add('colour2');
						}
					}
				}
			}
		}
		struct.onmousedown = function(e)
		{
			var start = e.clientX;
			struct.onmousemove = function(e)
			{
				var del = e.clientX - start;

				struct.onmouseup = function()
				{
					if (nowpage > 0 && del > 0)
					{
						buttonstr.children[nowpage].classList.remove('now');
						page(nowpage - 1); 
						buttonstr.children[nowpage].classList.add('now');
					}
					if (del < 0 && nowpage != (pagesnumber - 1))
					{
						buttonstr.children[nowpage].classList.remove('now');
						page(nowpage + 1);
						buttonstr.children[nowpage].classList.add('now');
					}
					struct.onmousemove = null;
				}
			}
			e.preventDefault();
		}
		function page(n)
		{
			if (nowpage !== 0)
			{
				struct.style.transition = "transform 1s ease-out";
				for (var i = 0; i < base.length; i++)
				{
					struct.children[i].style.transition = "opacity 1s ease";
				}
			}
			else
			{
				nowpage = n;
			}

			var first = n * getnum;
			var last  = first + getnum - 1;

			if (last >= base.length)
			{
				last = base.length - 1;		
			}
			var reform = ( ( width + 40 ) * getnum ) * ( -n );

			for (var i = 0; i < base.length; ++i)
			{
				if (i > last || i < first)	
				{			
					struct.children[i].style.opacity = "0";
				}
				else				
				{
					struct.children[i].style.opacity = "1";
				}
			}	

			if (n > nowpage)
			{
				for (var i = 0; i <= last; i++)
				{
					struct.children[i].style.opacity = "1";
				}
			}

			struct.style.transform = "translateX(" + reform + "px)";
			nowpage = n;	
		}
		function buttons()
		{		
			if ( !iscroll.hasChildNodes() )
			{
				iscroll.appendChild(buttonstr);
			}
			while (buttonstr.firstChild)
			{
				buttonstr.removeChild(buttonstr.firstChild);
			}

			for (var i = 0; i < pagesnumber; i++)
			{
				var key = document.createElement('div');
				key.classList.add('key');
				buttonstr.appendChild(key);

				(function(i)
				{ 
					var sword = buttonstr.children[i];
					sword.onmouseover = function()
					{
						if ( !sword.classList.contains('now') )
						sword.classList.add('next');
					}
					sword.onmouseleave = function()
					{
						sword.classList.remove('next');
					}
					sword.addEventListener('click', function()
					{
						sword.classList.remove('next');
						for (var j = 0; j < pagesnumber; j++)
						{
							buttonstr.children[j].classList.remove('now');
						}
						sword.classList.add('now');
						page(i);
					});
				})(i);

				buttonstr.children[0].classList.add('active');
			}
		}
		function refreshSize()
		{
			var first = nowpage * getnum;
			refreshNumber();
			var curPage = Math.ceil( (first+1) / (getnum) ) - 1;
			buttons();
			for (var i = 0; i < pagesnumber; i++)
			{
				buttonstr.children[i].classList.remove('now');
			}
			buttonstr.children[curPage].classList.add('now');
			page(curPage);
		}
		function refreshNumber()
		{
			getnum = Math.floor(struct.offsetWidth / (width + 40) );
			pagesnumber = Math.ceil(base.length / getnum);
		}
		function refresh()
		{
			refreshNumber();
			buttons();
			page(0);		
			window.addEventListener('resize', refreshSize);
		}	
	}
})();