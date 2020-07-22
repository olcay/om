// Write your JavaScript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip();

    $('abbr.js-timeago').each(function (index, value) {
        $(this).text(moment.utc($(this).attr("title"), "DD-MMM-YY HH:mm:ss").local().fromNow());
    });

});

$.fn.editable.defaults.mode = "inline";
$.fn.editableform.buttons = '<button type="submit" class="btn btn-info editable-submit">Save</button>' + '<button type="button" class="btn editable-cancel">Cancel</button>';
