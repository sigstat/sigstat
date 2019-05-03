$(function () {
  $.ajax({
    type: 'GET',
    url: 'https://api.myjson.com/bins/fpxfo',
    dataType: 'json',
    success: function (data) {
      var length = Object.keys(data.databases).length;
      var th = "<th class=\"align-middle\"";
      var th_end = "</th>";
      var rowspan2 = " rowspan=\"2\">";

      $("#asd").append(th + rowspan2 + "Verifier's Name" + th_end);
      for (var i = 0; i < length; i++) {
        $("#asd").append("<th colspan=\"3\">" + "<span>" + data.databases[i].Name + "</span>" + "</th>");
      }
      for (var i = 0; i < length; i++) {
        $("#asd2").append("<th><em>" + "AER" + "</em></th>");
        $("#asd2").append("<th><em>" + "FAR" + "</em></th>");
        $("#asd2").append("<th><em>" + "FRR" + "</em></th>");
      }

      var res_length = Object.keys(data.results).length;
      console.log(res_length);

      var databaseNames = Object.keys(data.results[0]);
      databaseNames.splice(0, 3);
      console.log(databaseNames[0]);
      var temp = databaseNames[0];


      for (var i = 0; i < res_length; i++) {
          $("#content").append("<tr>" + "<td>" + data.results[i].verifierName + "</td>"
            + "<td>" + data.results[i][databaseNames[0]].AER + "</td>" + "<td>" + data.results[i][databaseNames[0]].FAR + "</td>" + "<td>" + data.results[i][databaseNames[0]].FRR + "</td>"
            + "<td>" + data.results[i][databaseNames[1]].AER + "</td>" + "<td>" + data.results[i][databaseNames[1]].FAR + "</td>" + "<td>" + data.results[i][databaseNames[1]].FRR + "</td>"
            + "<td>" + data.results[i][databaseNames[2]].AER + "</td>" + "<td>" + data.results[i][databaseNames[2]].FAR + "</td>" + "<td>" + data.results[i][databaseNames[2]].FRR + "</td>"
            + "<td>" + data.results[i][databaseNames[3]].AER + "</td>" + "<td>" + data.results[i][databaseNames[3]].FAR + "</td>" + "<td>" + data.results[i][databaseNames[3]].FRR + "</td>"
            + "</tr>")
      }

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