$(function () {
  $.ajax({
    type: 'GET',
    // url: 'https://api.myjson.com/bins/z9efy', // with columnType
    // url: 'https://api.myjson.com/bins/17w6pq', // without columnType
    url: 'https://raw.githubusercontent.com/sigstat/sigstat/596ff6d7b14de0125123704ed2744754f2aae67e/docs/site/scripts/Results.json', // from GH
    dataType: 'json',
    success: function (data) {
      var length = Object.keys(data.databases).length;
      var th = "<th class=\"rotate\">";
      var th_end = "</th>";

      $("#header").append("<th class=\"align-middle\">" + "Verifier's" + "<br>"+ "Name" + th_end);
      $("#header").append("<th class=\"align-middle\">" + "Average" + th_end);
      $("#header").append("<th class=\"align-middle\">" + "Average" + "<br>" + "Core" + th_end);
      for (var i = 0; i < length; i++) {
        $("#header").append(th + "<div>" + "<span>" + data.databases[i].Name + "</div>" + "</span>" + th_end);
      }

      var res_length = Object.keys(data.results).length;

      var databaseNames = Object.keys(data.results[0]);
      databaseNames.splice(0, 3);

      var dynamic = "";

      for (var i = 0; i < res_length; i++) {
        dynamic += "<tr>" + "<td class=\"verName\">" + data.results[i].verifierName + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + (data.results[i]["Average"].AER).toFixed(2) + "%"+ "</p>" + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + data.results[i]["Average Core"].AER.toFixed(2) + "%"+ "</p>" + "</td>";
        for (var j = 0; j < length; j++) {
          dynamic += "<td>" + "<em>" + (data.results[i][databaseNames[j]].AER == null ? " " : (data.results[i][databaseNames[j]].AER).toFixed(2) + "%") + "</em>" + "</td>";
        }
        dynamic += "</tr>";

      }
      $("#content").append(dynamic);

      var IsValueSummary = data.columnType;
      if (IsValueSummary === 'summary'){
        var bar = newSheet();
        bar.insertRule("p.summary {color: white;}",0);
        bar.insertRule("p.summary {background-color: chocolate;}",0);
      }

      function newSheet() {
        var style = document.createElement("style");
       
        style.appendChild(document.createTextNode(""));
        document.head.appendChild(style);
       
        return style.sheet;
       };

      $("body").on('mouseover', 'span:not(.tooltipstered)', function () {
        var tooltipInstance = null;
        var temp = ($(this).html());
        var sep = ": ";
        var eol = "<br>";

        var first = data.databases.find(asd => asd.Name === temp);
        var test = "";

        for (var i = 1; i <= 5; i++) {
          test += Object.keys(first)[i] + sep + first[Object.keys(first)[i]] + eol
        }
        test += Object.keys(first)[6] + sep + "<a href=" + first[Object.keys(first)[i]] + " target=\"_blank\">" + first[Object.keys(first)[i]] + "</a>";


        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 100,
          theme: 'tooltipster-light',
          interactive: true
        })

        tooltipInstance.tooltipster('open');
      })

      $(".table-bordered tbody tr td:first-child").on('mouseover', function () {
        var tooltipInstance = null;
        var temp = ($(this).html());
        var sep = ": ";
        var eol = "<br>";


        var first = data.results.find(asd => asd.verifierName === temp);
        var test = "";

        for (var i = 1; i <= 2; i++) {
          test += Object.keys(first)[i] + sep + first[Object.keys(first)[i]] + eol;
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 0,
          theme: 'tooltipster-default',
          multiple: true
        })
        tooltipInstance.tooltipster('open');
      })

      $("body").on('mouseover', 'p:not(.tooltipstered)', function () {
        var rowData = ($(this).parent().parent().find('td:first').text());
        console.log(rowData);
        var currentColumnIndex = $(this).parent().index();


        var temp = data.results.find(asd => asd.verifierName === rowData);
        console.log(temp);
        var almost = "";
        if(currentColumnIndex === 1){
          almost = temp[Object.keys(temp)[Object.keys(temp).length-2]];
        }
        if(currentColumnIndex === 2){
          almost = temp[Object.keys(temp)[Object.keys(temp).length-1]];
        }
        
        var test = "";

        for (var i = 0; i <= 2; i++) {
          test += Object.keys(almost)[i] + ": " + almost[Object.keys(almost)[i]].toFixed(2) + "%" + "<br>";
        }

        

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 0,
          theme: 'tooltipster-noir',
        })

        tooltipInstance.tooltipster('open');
      })

      $("body").on('mouseover', 'em:not(.tooltipstered)', function () {
        var rowData = ($(this).parent().parent().find('td:first').text());
        var currentColumnIndex = $(this).parent().index() - 3;
        var db = data.databases[currentColumnIndex];
        var final = db[Object.keys(db)[0]];

        var temp = data.results.find(asd => asd.verifierName === rowData);
        var almost = temp[Object.keys(temp)[currentColumnIndex + 3]];
        


        var tooltipInstance = null;


        var row = data.results.find(asd => asd.verifierName === rowData);
        var test = "";

        test += Object.keys(row)[0] + ": " + row[Object.keys(row)[0]] + "<br>";
        test += "databaseName: " + final + "<br>";

        
        for (var i = 0; i <= 2; i++) {
          test += Object.keys(almost)[i] + ": " + almost[Object.keys(almost)[i]].toFixed(2) + "%" + "<br>";
        }
        
        if(Object.keys(almost)[3] === 'Comment'){
          test += Object.keys(almost)[3] + ": " + almost[Object.keys(almost)[3]];
        }


        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 0,
          theme: 'tooltipster-punk'

        })
        tooltipInstance.tooltipster('open');

      })
    }
  });
});