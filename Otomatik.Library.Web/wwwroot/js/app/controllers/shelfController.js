var ShelfController = function (itemService, shelfService) {
    var username;

    var init = function () {
        $.fn.editable.defaults.mode = "inline";

        $(".js-editable").editable({
            ajaxOptions: {
                headers:
                {
                    RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                }
            }
        });
        
        $(".js-remove-book").click(removeBook);
        $("#js-remove-shelf").click(removeShelf);
        $("#js-operation").on("click", "#js-makePublic-shelf", makeShelfPublic);
        $("#js-operation").on("click", "#js-makePrivate-shelf", makeShelfPrivate);
    };

    var makeShelfPublic = function(e) {
        e.preventDefault();
        var button = $("#js-makePublic-shelf");

        shelfService.makePublic(button.data("shelf-id"), makeShelfPublicDone, fail);
    };

    var makeShelfPrivate = function (e) {
        e.preventDefault();
        var button = $("#js-makePrivate-shelf");

        shelfService.makePrivate(button.data("shelf-id"), makeShelfPrivateDone, fail);
    };

    var makeShelfPublicDone = function () {
        $("#js-makePublic-shelf").hide();
        $("#js-makePrivate-shelf").show();
        $("#lblIsPublic").hide();
    };

    var makeShelfPrivateDone = function () {
        $("#js-makePublic-shelf").show();
        $("#js-makePrivate-shelf").hide();
        $("#lblIsPublic").show();
    };

    var removeShelf = function (e) {
        e.preventDefault();
        var button = $(this);

        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this shelf!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        })
            .then((willDelete) => {
                if (willDelete) {
                    username = button.data("user-name");

                    shelfService.remove(button.data("shelf-id"), removeShelfDone, fail);
                }
            });
    };

    var removeShelfDone = function () {
        window.location = "/p/" + username;
    };

    var removedBook;

    var removeBook = function () {
        removedBook = $(this);

        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this book!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        })
            .then((willDelete) => {
                if (willDelete) {
                    itemService.remove(removedBook.data("book-id"), removeBookDone, fail);
                }
            });
    };

    var removeBookDone = function () {
        removedBook.parents(".thumbnail").fadeOut(function () {
            $(this).remove();
            salvattore.recreateColumns(document.querySelector("#grid"));
        });
    };

    var fail = function () {
        swal("Something failed!", "We could not process your request.", "error");
    };

    return {
        init: init
    };
}(ItemService, ShelfService);