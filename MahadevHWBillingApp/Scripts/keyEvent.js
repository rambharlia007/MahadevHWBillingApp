$(function () {
    //Yes! use keydown 'cus some keys is fired only in this trigger,
    //such arrows keys
    $("body").keydown(function (e) {

        var validKeys = ["F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "ARROWDOWN", "ArrowUp", "ArrowRight", "ArrowLeft"];
        var key = e.key.toUpperCase();
            
        if (e.target.nodeName.toUpperCase() === "INPUT" || !validKeys.includes(key))
            return;

        e.preventDefault();
        switch (key) {
            case "F1": {
                window.location.href = "/Billing/New";
                break;
            }
            case "F2": {
                window.location.href = "/item/index";
                break;
            }
            case "F3": {
                window.location.href = "/sale/list";
                break;
            }
            case "F4": {
                window.location.href = "/purchase/list";
                break;
            }
            case "F5": {
                window.location.href = "/sale/recordpayment";
                break;
            }
            case "F6": {
                window.location.href = "/estimate/New";
                break;
            }
            case "F7": {
                window.location.href = "/dashboard/New";
                break;
            }
            case "F8": {
                window.location.href = "/contact/list";
                break;
            }
            case "F9": {
                window.location.href = "/profile/New";
                break;
            }
            case "F10": {
                window.location.href = "/Billing/New";
                break;
            }
            case "ARROWDOWN": {
                
                if (window.location.pathname === "/Billing/New") {
                    if ($(".list-group.custom-select-border li.active").length === 0) {
                        $(".list-group.custom-select-border li:first").focus();
                        $(".list-group.custom-select-border li:first").addClass("active");
                    }
                    else {
                        var currentListItem = $(".list-group.custom-select-border li.active");
                        var nextListItem = currentListItem.next();
                        currentListItem.removeClass("active");
                        nextListItem.focus();
                        nextListItem.addClass("active");
                    }
                }
                break;
            }
            case "ArrowUp": {
                window.location.href = "/Billing/New";
                break;
            }
            case "ArrowRight": {
                window.location.href = "/Billing/New";
                break;
            }
            case "ArrowLeft": {
                window.location.href = "/Billing/New";
                break;
            }
        }
    });
});