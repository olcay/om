// Write your JavaScript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip();

    $('abbr.js-timeago').each(function (index, value) {
        $(this).text(moment.utc($(this).attr("title"), "DD-MMM-YY HH:mm:ss").local().fromNow());
    });

});