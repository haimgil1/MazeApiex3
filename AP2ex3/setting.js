
window.onload = function () {
    
    if (!localStorage.length) {
        localStorage.setItem("Rows", "10");
        localStorage.setItem("Cols", "10");
        localStorage.setItem("Algorithm", "1");
    }

    $("#settingRows").val(localStorage.Rows);
    $("#settingCols").val(localStorage.Cols);
    $("#settingAlgo").val(localStorage.Algorithm);
   // document.getElementById("settingRows").value = localStorage.getItem("Rows");
    //document.getElementById("settingCols").value = localStorage.getItem("Cols");
    //document.getElementById("settingAlgo").text = localStorage.getItem("Algorithm");

}





$("#submitbtn").click(function () {

    localStorage.setItem('Rows', $("#settingRows").val());
    localStorage.setItem('Cols', $("#settingCols").val());
    localStorage.setItem('Algorithm', $("#settingAlgo").val());

});

