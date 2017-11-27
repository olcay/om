var FollowingService = function() {
    var toggleFollow = function(followeeId, done, fail) {
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

    return {
        toggleFollow: toggleFollow
    };
}();