$(function () {
    //类型地区点击后样式变换
    $('.typeTags li a').bind('click', function () {
        //$(this).parent().parent().attr("class").split(' ')[1];
        $('.' + $(this).parent().parent().attr("class").split(' ')[1] + ' li a').removeClass('active'); //移除所有页码的active类
        $(this).addClass('active'); //为当前页码加入active类
    });

    /*自动加载地区*/
    $.getJSON("/MovieControl/CinemaList.json",null, function (data) {
        var $cinema = $('.area');
        var strHTML = "";
        $cinema.empty();
        $.each(data, function (infoIndex1, info1) {
            if (infoIndex1 == "广州") {
                $.each(info1, function (infoIndex2, info2) {
                    $.each(info2, function (info3) {
                        strHTML += "<li><a onclick='areaClick()'>" + info3 + "</a></li>";
                    });
                });
            }
        });
        $cinema.html(strHTML);
    });


});

/*加载时间段*/
function times(price) {
    $(".date li a").bind('click', function () {
        var $times = $('#times tbody');
        var movieDay = new Date();
        var h = movieDay.getHours() + 1;

        var strHTML = "";
        var isToday = $(this).text();
        isToday = isToday.substring(isToday.indexOf("今天"), 2);

        $times.empty();
        if (isToday == "今天") {
            for (var i = h; i < 23; i += 2) {
                strHTML += "<tr><td>" + i + ":" + "00</td>";
                strHTML += "<td>" + price + "</td>"
                strHTML += "<td><a class='seat' title='" + i + ":" + "00' onclick='selectSeatClick()'>选座购票</a></td></tr>"
            }
        }
        else {
            for (var i = 10; i < 23; i += 2) {
                strHTML += "<tr><td>" + i + ":" + "00</td>";
                strHTML += "<td>" + price + "</td>"
                strHTML += "<td><a class='seat' title='" + i + ":" + "00' onclick='selectSeatClick()'>选座购票</a></td></tr>"
            }
        }
        $times.html(strHTML);
    });
}

//电影院点击事件
function cinemaClick() {
    $('.cinema li a').bind('click', function () {
        $('.cinema li a').removeClass('active');
        $(this).addClass("active");
        $('#times').css('display', 'inline');
    });
}

/*地区点击事件*/
function areaClick() {
    $('.area li a').bind('click', function () {
        $('.area li a').removeClass('active');
        $(this).addClass('active');
        $('#times').css('display', 'none');

        /*加载影院*/
        var areaTXT = $(this).text();
        $.getJSON("/MovieControl/CinemaList.json",null, function (data) {
            var $cinema = $('.cinema');
            var strHTML = "";
            var i = 1;
            $cinema.empty();

            $.each(data, function (infoIndex1, info1) {
                $.each(info1, function (infoIndex2, info2) {
                    $.each(info2, function (infoIndex3, info3) {
                        if (infoIndex3.toString() == areaTXT) {
                            $.each(info3, function (infoIndex4, info4) {
                                if ((i % 5) == 0) {
                                    strHTML += "<li><a onclick='cinemaClick()'>" + info4.影院 + "</a></li><br/>"
                                }
                                else {
                                    strHTML += "<li><a onclick='cinemaClick()'>" + info4.影院 + "</a></li>"
                                }
                                i++;
                            });
                        }
                    });
                });
            });
            i = 1;
            $cinema.html(strHTML);
        });
    });
}

//点击选座购票按钮时
//将选择信息添加到跳转下一个页面的url后
function selectSeatClick() {
    $('.seat').on('click', function () {
        var t = $('.date li .active').attr('title') + "-" + $(this).attr('title');
        var a = $('.area li .active').text();
        var c = $('.cinema li .active').text();
        var id = $('#movieID').text();
        var u = "?date=" + t + "&cinema=" + c + "&id=" + id+"&area="+a;
        var href = "/Movie/selectSeat";
        window.location.href = href + u
    });
}
