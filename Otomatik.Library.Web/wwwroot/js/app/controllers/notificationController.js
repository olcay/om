var NotificationController = function (notificationService) {
    var _notificationTemplate, _notifications;

    var init = function (notificationTemplate, notificationsDropdown) {
        _notificationTemplate = notificationTemplate;
        _notifications = notificationsDropdown;

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
            $(_notifications + " .js-notifications-count")
                .text(count)
                .removeClass("hide");

            $(_notifications + " .fa-bell")
                .addClass("animate__animated animate__swing animate__repeat-2");
        }

        var compiled = _.template($(_notificationTemplate).html());

        $(_notifications + " .dropdown-menu").html(compiled({ notifications: notifications }));

        $(_notifications).on("shown.bs.dropdown",
            function () {
                $.post({
                    url: "/api/notifications/",
                    headers:
                    {
                        RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                    }
                })
                    .done(function () {
                        $(_notifications + " .js-notifications-count")
                            .text("")
                            .addClass("hide");
                    });
            });
    };

    return {
        init: init
    };
}(NotificationService);