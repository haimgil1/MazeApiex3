$("nav").load("Menu.html", function () {
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
          //  register.href = "SignUp.html";
        //    login.href = "Login.html";
            window.location.replace("MainMenu.html");

        };

    }
});
