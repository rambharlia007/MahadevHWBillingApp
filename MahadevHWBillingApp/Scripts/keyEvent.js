$(function () {
    //Yes! use keydown 'cus some keys is fired only in this trigger,
    //such arrows keys
    $("body").keydown(function (e) {

        var validKeys = ["F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10"];
        var key = e.key.toUpperCase();
            
        console.log(e.key);
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
        }
    });
});