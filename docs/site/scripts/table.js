$(function () {
  $.ajax({
    type: 'GET',
    url: 'https://api.myjson.com/bins/z4m4w',
    dataType: 'json',
    success: function (data) {

      /*tableData = data.results;
      //console.log(tableData);
      columns = Object.keys(tableData[0]);
      //console.log(columns);
  
      //console.log(y.MCYT100);
      /*for(const prop in y){
        console.log((prop));
      }
  
      var x = document.getElementsByClassName("sep-left-cell");
      //console.log(x);
      var databaseNames = [];
      for(var i = 3; i<=6; i++){
        databaseNames.push(columns[i]);
      }
      console.log(databaseNames);
  
      var y = tableData[0];
      var asd = (Object.keys(y.MCYT100));
      console.log(asd);
  
      for(var j = 0; j<=3; j++){
        x[j].textContent = databaseNames[j];
        //console.log(x[j].textContent);
      }
  
      var z = document.getElementsByClassName("subcolumn");
      //for(var j = 0; j<=11; j++){
  
        z[0].textContent = asd[0];
        z[1].textContent = asd[1];
        z[2].textContent = asd[2];
        z[3].textContent = asd[0];
        z[4].textContent = asd[1];
        z[5].textContent = asd[2];
     // }*/



      $("body").on('mouseover', 'span:not(.tooltipstered)', function () {
        var tooltipInstance = null;
        var temp = ($(this).html());
        var sep = ": ";
        var eol = "<br>";

        var first = data.databases.find(asd => asd.Name === temp);
        var count = Object.keys(first).length - 1;
        var test = "";

        for (var i = 1; i <= count; i++) {
          test += Object.keys(first)[i] + sep + first[Object.keys(first)[i]] + eol
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 0,
          theme: 'tooltipster-punk'
        });

        tooltipInstance.tooltipster('open');
      })
      $("table tbody tr td:first-child").on('mouseover', function () {
        var tooltipInstance = null;
        var temp = ($(this).html());
        var sep = ": ";
        var eol = "<br>";

        var first = data.results.find(asd => asd.verifierName === temp);
        var count = Object.keys(first).length - 5;
        var test = "";

        for (var i = 1; i <= count; i++) {
          test += Object.keys(first)[i] + sep + first[Object.keys(first)[i]] + eol;
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 0,
          theme: 'tooltipster-punk'
        })
        tooltipInstance.tooltipster('open');
      })
      $("body").on('mouseover', 'em:not(.tooltipstered)', function () {
        var temp = ($(this).html());
        var tooltipInstance = null;
        var content = null;

        if (temp == "AER") {
          content = "Average Error Rate";
        }
        if (temp == "FAR") {
          content = "False Acceptance Rate";
        }
        if (temp == "FRR") {
          content = "False Rejection Rate";
        }
        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: content,
          delay: 0,
          theme: 'tooltipster-light'
        })
        tooltipInstance.tooltipster('open');

      })
    }
  });
});