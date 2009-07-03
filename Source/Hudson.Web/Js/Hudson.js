/// <reference path="jquery-1.2.6.min-vsdoc.js" />
$(document).ready(function() {
    setFontSize();
});

$(window).resize(function() {
    setFontSize();
});

function setFontSize() {
    var height = $(window).height() / 8;

    if (height > 20) height = height - 20;

    $("p").css("font-size", height + "px");

    $("img").css("height", (height * 2) + "px");

    $(".double").children(".info").css("font-size", (height * .6) + "px");
}