var ProfileController = function (followingModule, starModule, shelfService, followingService) {
    var _followerTemplate;

    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    var displayFollowList = function (title, followers) {
        var render = _.template($(_followerTemplate).html());

        bootbox.dialog({
            title: title,
            message: render({ followers: followers }),
            backdrop: true,
            onEscape: true,
            scrollable: true
        });
    };

    var displayFollowers = function (data) {
        displayFollowList("Followers", data);
    };

    var displayFollowings = function (data) {
        displayFollowList("Followings", data);
    };

    var init = function (container, userId, page, followerTemplate) {
        _followerTemplate = followerTemplate;

        starModule.init(container);
        followingModule.init(container);

        shelfService.get(userId, page);

        $(document).on("click", "#btnFollowers", function (e) {
            followingService.getFollowers(userId, displayFollowers, fail);
        });

        $(document).on("click", "#btnFollowing", function (e) {
            followingService.getFollowings(userId, displayFollowings, fail);
        });
    };

    return {
        init: init
    };
}(FollowingModule, StarModule, ShelfService, FollowingService);