var FollowingModule = function(followingService) {
    var button;

    var init = function(container) {
        $(container).on("click", ".js-toggle-follow", toggleFollow);
    };

    var toggleFollow = function(e) {
        button = $(this);

        followingService.toggleFollow(button.data("user-id"), done, fail);
    };

    var done = function(data) {
        if (data === "following") {
            button
                .removeClass("btn-default")
                .addClass("btn-success");
        } else {
            button
                .removeClass("btn-success")
                .addClass("btn-default");
        }
    };

    var fail = function() {
        swal("Something failed!", "We could not process your request.", "error");
    };

    return {
        init: init
    };
}(FollowingService);