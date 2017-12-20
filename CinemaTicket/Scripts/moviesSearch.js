$(function () {
    //Ajax搜索
    var ajaxFormSubmit = function () {
        $('.typeTags li a').removeClass('active');
        $('.movie-area li a:first').addClass('active');
        $('.type li a:first').addClass('active');

        //获取表单对象
        var $form = $(this);

        //获取参数
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        //使用ajax请求
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-movies-target"));
            $target.replaceWith(data);
        });

        return false;
    };

    //自动填充
    var createAutoComplete = function () {
        var $input = $(this);

        var options = {
            select: submitAutoCompleteForm,
            source: $input.attr("data-movies-autocomplete")
        };
        $input.autocomplete(options);
    };

    //选择自动填充后的执行搜索
    var submitAutoCompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parent("form:first");
        $form.submit();
    }


    $("form[data-movies-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-movies-autocomplete]").each(createAutoComplete);
});

function typeSearch(searchStr, searchType) {
    $('.typeTags li a').bind('click', function () {
        $('.' + $(this).parent().parent().attr("class").split(' ')[1] + ' li a').removeClass('active');
        $(this).addClass('active');
    });
    $("input[data-movies-autocomplete]").val("");

    //获取参数
    if (searchType == "type") {
        var options = {
            url: "/Movie",
            type: "get",
            data: { typeString: searchStr, areaString: $(".movie-area li a[class='active']").text() }
        };
    } else {
        var options = {
            url: "/Movie",
            type: "get",
            data: { areaString: searchStr, typeString: $(".type li a[class='active']").text() }
        };
    }
    
    //使用ajax请求
    $.ajax(options).done(function (data) {
        var $target = $("#movieList");
        $target.replaceWith(data);
    });
}