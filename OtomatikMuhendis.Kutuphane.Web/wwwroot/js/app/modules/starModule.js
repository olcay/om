var StarModule = function(starService) {
    var button;

    var init = function(container) {
        $(container).on("click", ".js-toggle-star", toggleStar);
    };

    var toggleStar = function(e) {
        button = $(this);

        starService.toggleStar(button.data("shelf-id"), done, fail);
    };

    var done = function(data) {
        if (data === "starred") {
            button
                .removeClass("btn-default")
                .addClass("btn-warning");
        } else {
            button
                .removeClass("btn-warning")
                .addClass("btn-default");
        }
    };

    var fail = function() {
        swal("Something failed!", "We could not process your request.", "error");
    };

    return {
        init: init
    };
}(StarService);