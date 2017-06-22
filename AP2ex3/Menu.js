window.onload = function () {
    var multyPlayer = document.getElementById("multiPlayer");
    if (!sessionStorage.getItem("userName")) {
        multyPlayer.href = "#";
        multyPlayer.onclick = function () {
            alert("you need to login first!");
            window.location.replace("Login.html");
        };
    }
}

$("nav").load("Menu.html", function () {
    var multyPlayer = document.getElementById("multiPlayer");
    if (sessionStorage.getItem("userName")) {
        var register = document.getElementById("register");
        var login = document.getElementById("login");
        register.textContent = sessionStorage.getItem("userName");
        register.href = "#";
        login.href = "#";
        login.textContent = "Log Out";
        login.onclick = function () {
            alert("are you sure?");
            sessionStorage.removeItem("userName");
            window.location.replace("MainMenu.html");
        };
    } else {
        var multyPlayer = document.getElementById("multiPlayer");
        multyPlayer.href = "#";
        multyPlayer.onclick = function () {
            alert("you need to login first!");
            window.location.replace("Login.html");
        };
    }
});

