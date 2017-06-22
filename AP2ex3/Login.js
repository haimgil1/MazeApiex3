$("nav").load("Menu.html");

$(function () {
    $('#nav h4 button').on('click', function (e) {
        e.preventDefault();
        $('#content').load("SignUp.html");
    });
});


$body = $("body");
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});


function invalidUser(jqXHR) {
    if (jqXHR.status === 409) {
        alert("wrong password");
    } else {
        alert("you need to sign up first");
    }
}

$("#loginButton").click(function () {
    var userName = $("#Username").val();
    var password = $("#Password").val();
    var user = {
        UserName: userName,
        Password: password
    };
    var userUri = "/api/user/Login";
    var req = $.post(userUri, user).done(function (item) {
        alert("Thank you for login");
        sessionStorage.setItem("userName", userName);
        window.location.replace("MainMenu.html");
    });
    req.fail(function (jqXHR, textStatus, errorThrown) {
        invalidUser(jqXHR);
    });
});


