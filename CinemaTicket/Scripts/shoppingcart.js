$(function () {
    $('#allCheck').bind('click', function () {
        //全选
        $('.checkOrder').each(function () {
            $(this).prop('checked', !!$('#allCheck').prop('checked'));
        });

    });

    //有没选的则取消全选
    $('.checkOrder').bind('click', function () {
        $('.checkOrder').each(function () {
            if ($(this).is(':checked')) {
            }
            else {
                $('#allCheck').prop('checked', false);
            }
        });
    });
});

function submitOrder() {
    var orderList = new Array();
    var i = 0;
    $(".checkOrder").each(function () {
        if ($(this).is(':checked')) {
            orderList[i++] = $(this).val();
        }
    });
    i = 0;

    var b = orderList.join('-');
    if (b == "" || b == null) {

    } else {
        window.location.href = "/Movie/success?orderList="+b;
    }
}
