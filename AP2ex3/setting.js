
window.onload = function () {
    alert(" Alert inside my_code function");

    
    if (!localStorage.length) {
        localStorage.setItem("Rows", "10");
        localStorage.setItem("Cols", "10");
        localStorage.setItem("Algorithm", "BFS");
    }
    document.getElementById("settingRows").value = localStorage.getItem("Rows");
    document.getElementById("settingCols").value = localStorage.getItem("Cols");
    //document.getElementById("settingAlgo").text = localStorage.getItem("Algorithm");;
}


$("#submitbtn").click(function () {
    alert(" Alert insid submitbtn"); 

    localStorage.setItem('Rows', $("#settingRows").val());
    localStorage.setItem('Cols', $("#settingCols").val());
    localStorage.setItem('Algorithm', $("#settingAlgo").val());

});

