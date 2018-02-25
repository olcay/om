var BookController = function (bookService) {
    var bookTitle;

    var init = function () {
        $("#js-select-shelf").change(selectShelf);

        $(".js-add-book").click(addBookToShelf);
    };

    var selectShelf = function (e) {
        var selectedShelf = $("#js-select-shelf option:selected");

        $('.js-add-book').attr("data-original-title", "Add to " + selectedShelf.text());
    };

    var addBookToShelf = function (e) {
        var shelfId = $("#js-select-shelf option:selected").val();
        bookTitle = $(this).data("title");
        var gBookId = $(this).data("book-id");

        bookService.add(gBookId, shelfId, bookTitle, done, fail);
    };

    var done = function (data) {
        var selectedShelf = $("#js-select-shelf option:selected");

        swal("New book!", bookTitle +" is added successfully to "+ selectedShelf.text() +".", "success");
    };

    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    return {
        init: init
    };
}(BookService);