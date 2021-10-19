function showLoader() {
    $(".global-spinner").removeClass("hide");
    $(".body-content").removeClass("blur-bg");
    $(".body-content").addClass("blur-bg");
}
function hideLoader() {
    $(".global-spinner").removeClass("hide");
    $(".body-content").removeClass("blur-bg");
    $(".global-spinner").addClass("hide");
}
(function initGameIIFE() {
    //$(document).ajaxComplete(function () {
    //    hideLoader();
    //});

    //$(document).ajaxStart(function () {
    //    showLoader();
    //});
    $(document).ajaxError(function () {
        hideLoader();
    });
    $(document).ajaxStop(function () {
        hideLoader();
    });
    //$(document).ajaxSuccess(function () {
    //    hideLoader();
    //});

    $(window).on("load", function () {
        hideLoader();
    });

    $("#navbarColor01 .nav-link").on("click", function (e) {
        if (e.target.id !== "backup-link-li" || e.target.id !== "backup-link-li")
            showLoader();
    });

}());