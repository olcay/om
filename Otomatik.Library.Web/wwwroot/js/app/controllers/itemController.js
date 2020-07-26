var ItemController = function (itemService) {
    var init = function (itemId) {
        $("#file").change(function () {
            var fileName = $(this).val();
            if (fileName) {
                $('#frmUploadCover').submit();
            }
        });

        $('#frmUploadCover').on('submit', function (e) {
            e.preventDefault();

            var form = $(this);
            var formData = false;
            if (window.FormData) {
                formData = new FormData(form[0]);
            }
            
            $.ajax({
                url: '/api/items/'+itemId+'/cover',
                data: formData ? formData : form.serialize(),
                cache: false,
                contentType: false,
                processData: false,
                type: 'PUT',
                success: function (data, textStatus, jqXHR) {
                    done(data);
                }
            });
        });
    };

    var done = function (data) {
        $('header').css('background-image', 'url(' + data + ')');

        swal("Image uploaded!", "The cover image is uploaded successfully.", "success");
    };

    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    return {
        init: init
    };
}(ItemService);