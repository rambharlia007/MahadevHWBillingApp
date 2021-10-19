var selectSearchClickEvent = function (ele, callBack) {
    var cb = callBack;
    $(ele).next().find("li").click(function (e) {
        var value = $(e.target).text().trim();
        var inputEle = $(e.target).parent().prev();
        inputEle.val(value);
        var data = $(e.target).attr("data");
        $(e.target).parent().removeClass("custom-select-scroll");
        $(e.target).parent().find("li").remove();
        cb(inputEle, unescape(data));
    });
};
var selectSearchEvent = function (config) {

    var ele = config.ele;
    var endPoint = config.endPoint;

    $(document).click(function (e) {
        if ($(e.target).parent().is(ele)) {
            $(ele).find("li").remove();
        }
        var container = $(ele);
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            container.find("li").remove();
        }
    });

    var timeout = null;
    $(ele).keyup(function (currentEle) {
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            var cl = $(currentEle)[0].target;
            if ($(cl).val().trim().length > 0) {
                var options = [];
                var searchValue = $(cl).val().trim();
                $.get(endPoint + "?q=" + searchValue, function (res) {
                    if (res.length) {
                        for (var i = 0; i < res.length; i++) {
                            var item = escape(JSON.stringify(res[i]));
                            options.push(`<li class="list-group-item list-group-item-custom-select custom-options" data = "${item}">  ${res[i][config.prop]} </li>`);
                        }
                    }
                    else { //<button type="button" class="btn btn-link">Add edit value </button> 
                        options = [];
                        options.push('<li class="list-group-item list-group-item-custom-select custom-options custom-disabled"> No results found.</li>');
                    }
                    $(cl).next().html(options.join(" "));
                    $(cl).next().addClass("custom-select-scroll");
                    selectSearchClickEvent(cl, config.callback);
                });
            }
            else {
                $(cl).next().html("");
            }
        }, 500);
    });
};