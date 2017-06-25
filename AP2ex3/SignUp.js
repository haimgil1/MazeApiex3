$("nav").load("Menu.html");

window.onload = function () {
    document.getElementById("signupConfirmPassword").onchange = validatePassword;
 //   document.getElementById("signupName").onchange = validateName;
}

function validatePassword() {
    var password = document.getElementById("signupPassword");
    var confirmPassword = document.getElementById("signupConfirmPassword");
    var email = document.getElementById("signupEmail");

    if (email.value.indexOf("@") == -1)
    {
        alert("email must conatin @ ");
        return false;
    }
    if (password.value.length < 6) {
        alert("password must be at least 6 characters long");
        return false;
    }

    if (password.value != confirmPassword.value) {
        alert("password dont match -  please try again");
        return false;
    }


    return true;
}

function invalidUser(jqXHR) {
    if (jqXHR.status === 409) {
        alert("this user name is already exists");
    }

}

$body = $("body");
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$("#signupSubmit").click(function () {
    if (!validatePassword()) {
        return;
    }
//    $("#loader").show();
    var userName = $("#signupName").val();
    var email = $("#signupEmail").val();
    var password = $("#signupPassword").val();
    var user = {
        UserName: userName,
        Email: email,
        Password: password,
        Wins: 0,
        Losses: 0
    };
    var userUri = "/api/Users";
    var req = $.post(userUri, user).done(function (item) {
        alert("Thank you for signing up");
        sessionStorage.Username = userName;
        sessionStorage.Password = password;
        window.location.replace("MainMenu.html");
    });
    req.fail(function (jqXHR, textStatus, errorThrown) {
        invalidUser(jqXHR);
    });
});