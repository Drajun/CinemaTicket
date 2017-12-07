$(function () {
    var rowsShown = 10;
    var rowsTotal = $('#movieList div').length; 
    var numPages = Math.ceil(rowsTotal / rowsShown); 

    for(var i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#page').append('<a href="#" rel="' + i + '">' + pageNum + '</a>&nbsp;');
    }
    $('#page a').css('padding','6px 13px');

    $('#movieList div').hide();
    $('#movieList div').slice(0, rowsShown).show();
    $('#page a:first').addClass('active');

    $('#page a').bind('click', function() {
        $('#page a').removeClass('active'); 
        $(this).addClass('active'); 
        var currPage = $(this).attr('rel'); 
        var startItem = currPage * rowsShown; 
        var endItem = startItem + rowsShown; 
        $('#movieList div').hide();

        $('#movieList div').slice(startItem, endItem).css('display', 'block');

        $('#movieList div').css('opacity','0.5').hide().slice(startItem,endItem).css('display','table-row').animate({opacity:1},400);
    });
		
    $('.typeTags li a').bind('click', function() {
        $('.'+$(this).parent().parent().attr("class").split(' ')[1]+' li a').removeClass('active');
        $(this).addClass('active'); 
    });
});