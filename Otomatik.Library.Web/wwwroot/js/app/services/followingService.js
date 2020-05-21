var FollowingService = function () {
    var toggleFollow = function (followeeId, done, fail) {
        $.post({
            url: "/api/followings",
            data: { followeeId: followeeId },
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .done(done)
            .fail(fail);
    };

    var getFollowings = function (userId, done, fail) {
        $.get({
            url: "/api/" + userId + "/followings",
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .done(done)
            .fail(fail);
    };

    var getFollowers = function (userId, done, fail) {
        $.get({
            url: "/api/" + userId + "/followers",
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .done(done)
            .fail(fail);
    };

    return {
        toggleFollow: toggleFollow,
        getFollowings: getFollowings,
        getFollowers: getFollowers
    };
}();