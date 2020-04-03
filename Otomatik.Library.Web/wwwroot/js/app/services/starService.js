var StarService = function() {
    var toggleStar = function(shelfId, done, fail) {
        $.post({
                url: "/api/stars",
                data: { shelfId: shelfId },
                headers:
                {
                    RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                }
            })
            .done(done)
            .fail(fail);
    };

    return {
        toggleStar: toggleStar
    };
}();