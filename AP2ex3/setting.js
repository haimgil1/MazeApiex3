$("#submitbtn").click(function () {

    localStorage.setItem('rows', $("#settingRows").val());
    localStorage.setItem('cols', $("#settingCols").val());
    localStorage.setItem('searchAlgo', $("#settingAlgo").val());

});
