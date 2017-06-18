
$(function () {
    $('#nav h4 button').on('click', function (e) {
        e.preventDefault();
        $('#content').load("SignUp.html");
    });
});