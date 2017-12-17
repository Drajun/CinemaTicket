$(function () {
    /*座位点击事件*/
    var checkedSeatNum = 0;
    $('.choice td div').bind('click', function () {
        if ($(this).attr('class') == "check") {
            $(this).removeClass('check');
            $(this).addClass('empty');
            checkedSeatNum--;

            /*移除边栏已选座位信息*/
            var removieSpan = "#isSeatChecked span[title='" + $(this).attr('title') + "']";
            $(removieSpan).remove();

            /*计算总价*/
            var price = parseFloat($('#price').text().substring($('#price').text().indexOf('￥') + 1));
            var totalPrice = price * $('#isSeatChecked span').length;
            $('#totalPrice span').text("￥" + totalPrice);

        } else if ($(this).attr('class') == "sell") {

        } else {

            if (checkedSeatNum < 4) {
                $(this).removeClass('empty');
                $(this).addClass('check');

                /*添加边栏已选座位信息*/
                var seatInfo = $(this).attr('title').split(',')[0] + "排" + $(this).attr('title').split(',')[1] + "座";
                $('#isSeatChecked').append("<span title=" + $(this).attr('title') + ">" + seatInfo + "</span>");
                checkedSeatNum++;

                /*计算总价*/
                var price = parseFloat($('#price').text().substring(1));
                alert($('#price').text().substring(1));
                var totalPrice = price * $('#isSeatChecked span').length;
                $('#totalPrice span').text("￥" + totalPrice);

            } else {
                alert("一次最多购买四张票");
            }

        }
    });
});

/*加入购物车点击事件*/
function shopCartClick() {

}

/*购买点击事件*/
function buyClick(type) {
    var movieID = $('#movieID').text();
    var movieTime = $('#movieTime').text();
    var cinemaName = $('#cinemaName').text();
    var movieTotalPrice = $('#totalPrice span').text().substring($('#totalPrice span').text().indexOf("￥") + 1);

    var seats = "";
    $('#isSeatChecked').find('span').each(function () {
        seats += $(this).attr('title') + ";";
    });

    var para = "?id=" + movieID + "&movieTime=" + movieTime + "&cinemaName=" + cinemaName + "&movieTotalPrice=" + movieTotalPrice + "&seats=" + seats;
    console.log(para);

    if (type == "buy") {
        window.location.href = "payMent" + para;
    }
    else {
        window.location.href = "" + para;
    }
}
