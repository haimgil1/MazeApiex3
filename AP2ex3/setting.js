$("nav").load("Menu.html");

window.onload = function () {
    
    if (!localStorage.length) {
        localStorage.setItem("Rows", "10");
        localStorage.setItem("Cols", "10");
        localStorage.setItem("Algorithm", "1");
    }

    $("#settingRows").val(localStorage.Rows);
    $("#settingCols").val(localStorage.Cols);
    var select = document.querySelector(".algo");
    var selectOption = select.options[select.selectedIndex];
    var lastSelected = localStorage.Algorithm;
    if (lastSelected) {
        select.value = lastSelected;
    }

}





$("#submitbtn").click(function () {
    alert("Settings as been saved succefully!");
    localStorage.setItem('Rows', $("#settingRows").val());
    localStorage.setItem('Cols', $("#settingCols").val());
    localStorage.setItem('Algorithm', $("#settingAlgo").val());
    window.location.replace("MainMenu.html");
});

$("#canceltbtn").click(function () {

    window.location.replace("MainMenu.html");
});

