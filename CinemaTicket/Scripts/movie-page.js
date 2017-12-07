$(document).ready(
	function() {
		var rowsShown = 10; //每页显示的行
		var rowsTotal = $('#movieList div').length; //获取总共的行
		var numPages = Math.ceil(rowsTotal / rowsShown); //计算出有多少页

		//显示页码，将页面加入#nav内
		for(var i = 0; i < numPages; i++) {
			var pageNum = i + 1;
			$('#page').append('<a href="#" rel="' + i + '">' + pageNum + '</a>&nbsp;');
		}
		$('#page a').css('padding','6px 13px');

		/* 初次分页操作
		 * 先将全部行隐藏
		 * 再显示第一页应该显示的行数(本示例为3)
		 * 为第一个页码加一个值为active的class属性，方便加样式
		 * */
		$('#movieList div').hide();
		$('#movieList div').slice(0, rowsShown).show();
		$('#page a:first').addClass('active');

		//页码点击事件
		$('#page a').bind('click', function() {
			$('#page a').removeClass('active'); //移除所有页码的active类
			$(this).addClass('active'); //为当前页码加入active类
			var currPage = $(this).attr('rel'); //取出页码上的值
			var startItem = currPage * rowsShown; //行数的开始=页码*每页显示的行
			var endItem = startItem + rowsShown; //行数的结束=开始+每页显示的行
			$('#movieList div').hide(); //全部隐藏

			//显示从开始到结束的行
			$('#movieList div').slice(startItem, endItem).css('display', 'block');

			$('#movieList div').css('opacity','0.5').hide().slice(startItem,endItem).css('display','table-row').animate({opacity:1},400);
		});
		
		//类型地区点击后样式变换
		$('.typeTags li a').bind('click', function() {
			//$(this).parent().parent().attr("class").split(' ')[1];
			$('.'+$(this).parent().parent().attr("class").split(' ')[1]+' li a').removeClass('active'); //移除所有页码的active类
			$(this).addClass('active'); //为当前页码加入active类
		});
	}
);