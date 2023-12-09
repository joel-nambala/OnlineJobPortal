$(document).ready(function () {
    $(".siderbar_menu li").click(function () {
        $(".siderbar_menu li").removeClass("active");
        $(this).addClass("active");
    });

    $(".hamburger").click(function () {
        $(".wrapper").addClass("active");
    });

    $(".close, .bg_shadow").click(function () {
        $(".wrapper").removeClass("active");
    });
});

$(function () {
    // this will get the full URL at the address bar
    var url = window.location.href;

    // passes on every "a" tag
    $("#sub-header a").each(function () {
        // checks if its the same on the address bar
        if (url == this.href) {
            $(this).closest("li").addClass("active");
        }
    });
});