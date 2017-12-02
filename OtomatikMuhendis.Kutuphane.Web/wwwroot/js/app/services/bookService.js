var BookService = function () {
    var remove = function (bookId, done, fail) {
        $.ajax({
        url: "/api/books/" + bookId,
            method: "DELETE",
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
        .done(done)
        .fail(fail);
    };

    return {
        remove: remove
    };
}();