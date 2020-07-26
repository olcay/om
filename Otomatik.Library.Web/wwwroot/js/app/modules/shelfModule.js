var ShelfModule = function (shelfService) {
    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    var gotForm = function (formHtml) {
        var container = ".bootbox-body";

        bootbox.dialog({
            title: "New Shelf",
            onEscape: true,
            backdrop: true,
            message: formHtml,
            buttons: {
                cancel: {
                    label: "Cancel",
                    className: 'btn-danger-soft'
                },
                confirm: {
                    label: "Create",
                    className: 'btn-teal',
                    callback: function () {
                        var form = $(container + " form")[0];
                        if (form.checkValidity() === false) {
                            form.classList.add('was-validated');
                            return false;
                        }
                        form.submit();
                    }
                }
            },
            onShown: function (e) {
                $(container + " #Title").focus();
            }
        });
    }

    var showForm = function (e) {
        e.preventDefault();
        shelfService.getForm(gotForm, fail);
    };

    var init = function () {
        $(document).on("click", ".js-add-shelf", showForm);
    };

    return {
        init: init
    };
}(ShelfService);