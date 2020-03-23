var ItemController = function (itemService) {
    var init = function (itemId) {
        //$("#js-upload-cover").click(addBookToShelf);

        $('#myform').on('submit', function (e) {
            e.preventDefault();

            var form = $(this);
            var formdata = false;
            if (window.FormData) {
                formdata = new FormData(form[0]);
            }

            $.ajax({
                url: '/api/items/'+itemId+'/cover',
                data: formdata ? formdata : form.serialize(),
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
        $('#coverImage').attr('src', data);

        swal("Image uploaded!", "The cover image is uploaded successfully.", "success");
    };

    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    return {
        init: init
    };
}(ItemService);