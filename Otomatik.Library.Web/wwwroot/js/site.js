// Write your JavaScript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip();

    $('abbr.js-timeago').each(function (index, value) {
        $(this).text(moment.tz($(this).attr("title"), "UTC").tz(moment.tz.guess()).fromNow());
    });

})