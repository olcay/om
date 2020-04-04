var NotificationController = function (notificationService) {
    var _notificationTemplate, _popoverTemplate;

    var init = function (notificationTemplate, popoverTemplate) {
        _notificationTemplate = notificationTemplate;
        _popoverTemplate = popoverTemplate;

        notificationService.get(getNotificationsDone);
    };

    var getNotificationsDone = function (notifications) {
        if (notifications.length === 0)
            return;

        var count = 0;
        $.each(notifications,
            function (index, value) {
                if (!value.isRead) {
                    count++;
                }
            });

        if (count > 0) {
            $(".js-notifications-count")
                .text(count)
                .removeClass("hide")
                .addClass("animated wobble");
        }

        $(".notifications").popover({
            html: true,
            title: "Notifications",
            trigger: "focus",
            content: function () {
                var compiled = _.template($(_notificationTemplate).html());
                return compiled({ notifications: notifications });
            },
            placement: "bottom",
            template: $(_popoverTemplate).html()
        }).on("shown.bs.popover",
            function () {
                $.post({
                    url: "/api/notifications/",
                    headers:
                    {
                        RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                    }
                })
                    .done(function () {
                        $(".js-notifications-count")
                            .text("")
                            .addClass("hide");
                    });
            });
    };

    return {
        init: init
    };
}(NotificationService);