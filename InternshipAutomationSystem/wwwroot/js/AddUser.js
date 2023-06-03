
$(document).ready(function () {

    $('#Input_Role').change(function () {
        var selection = $('#Input_Role option:selected').text();
        if (selection === 'Student') {
            $('#next-btn').show();
            $('#registerSubmit').hide();

        } else {
            $('#next-btn').hide();
            $('#registerSubmit').show();

        }
    });
    $('#next-btn').click(function () {
        $('#registerSubmit').show();
        $('.backButton').show();
        $('#first-slide').hide();
        $('#next-btn').hide();
        $('#second-slide').show();


    });

    $('.backButton').click(function () {
        $('#registerSubmit').hide();
        $('#first-slide').show();
        $('#second-slide').hide();
        $('#next-btn').show();
    });
});