$(function () {
  $.ajax({
    type: 'GET',
    // url: 'https://api.myjson.com/bins/z9efy', // with columnType
    // url: 'https://api.myjson.com/bins/17w6pq', // without columnType
    url: 'https://raw.githubusercontent.com/sigstat/sigstat/596ff6d7b14de0125123704ed2744754f2aae67e/docs/site/scripts/Results.json', // from GH
    dataType: 'json',
    success: function (data) {
      var lengthDB = Object.keys(data.databases).length;
      var th_open = "<th class=\"rotate\">";
      var th_end = "</th>";

      $("#header").append("<th class=\"align-middle\">" + "Verifier's" + "<br>" + "Name" + th_end);
      $("#header").append("<th class=\"align-middle\">" + "Average" + th_end);
      $("#header").append("<th class=\"align-middle\">" + "Average" + "<br>" + "Core" + th_end);
      for (var i = 0; i < lengthDB; i++) {
        $("#header").append(th_open + "<div>" + "<span>" + data.databases[i].Name + "</div>" + "</span>" + th_end);
      }

      var res_length = Object.keys(data.results).length;

      var databaseNames = Object.keys(data.results[0]);
      databaseNames.splice(0, 3);

      var dynamicContent = "";
      for (var i = 0; i < res_length; i++) {
        dynamicContent += "<tr>" + "<td class=\"verName\">" + data.results[i].verifierName + "</td>";
        dynamicContent += "<td>" + "<p class=\"summary\">" + data.results[i]["Average"].AER.toFixed(2) + "%" + "</p>" + "</td>";
        dynamicContent += "<td>" + "<p class=\"summary\">" + data.results[i]["Average Core"].AER.toFixed(2) + "%" + "</p>" + "</td>";
        for (var j = 0; j < lengthDB; j++) {
          dynamicContent += "<td>" + "<em>" + (data.results[i][databaseNames[j]].AER == null ? " " : (data.results[i][databaseNames[j]].AER).toFixed(2) + "%") + "</em>" + "</td>";
        }
        dynamicContent += "</tr>";
      }
      $("#content").append(dynamicContent);

      var IsValueSummary = data.columnType;
      if (IsValueSummary === 'summary') {
        var bar = newSheet();
        bar.insertRule("p.summary {color: white;}", 0);
        bar.insertRule("p.summary {background-color: chocolate;}", 0);
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

        var found = data.databases.find(x => x.Name === temp);
        var tooltipContent = "";

        for (var i = 1; i <= 5; i++) {
          tooltipContent += Object.keys(found)[i] + sep + found[Object.keys(found)[i]] + eol
        }
        tooltipContent += Object.keys(found)[6] + sep + "<a href=" + found[Object.keys(found)[i]] + " target=\"_blank\">" + "<font size=\"3\">" + found[Object.keys(found)[i]] + "</font>" + "</a>";


        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: tooltipContent,
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


        var found = data.results.find(x => x.verifierName === temp);
        var tooltipContent = "";

        for (var i = 1; i <= 2; i++) {
          tooltipContent += Object.keys(found)[i] + sep + found[Object.keys(found)[i]] + eol;
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: tooltipContent,
          delay: 0,
          theme: 'tooltipster-default',
          multiple: true
        })
        tooltipInstance.tooltipster('open');
      })

      $("body").on('mouseover', 'p:not(.tooltipstered)', function () {
        var rowData = ($(this).parent().parent().find('td:first').text());
        var currentColumnIndex = $(this).parent().index();
        var temp = data.results.find(x => x.verifierName === rowData);
        var preContent = "";
        if (currentColumnIndex === 1) {
          preContent = temp[Object.keys(temp)[Object.keys(temp).length - 2]];
        }
        if (currentColumnIndex === 2) {
          preContent = temp[Object.keys(temp)[Object.keys(temp).length - 1]];
        }

        var tooltipContent = "";

        for (var i = 0; i <= 2; i++) {
          tooltipContent += Object.keys(preContent)[i] + ": " + preContent[Object.keys(preContent)[i]].toFixed(2) + "%" + "<br>";
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: tooltipContent,
          delay: 0,
          theme: 'tooltipster-noir',
        })

        tooltipInstance.tooltipster('open');
      })

      $("body").on('mouseover', 'em:not(.tooltipstered)', function () {
        var rowData = ($(this).parent().parent().find('td:first').text());
        var currentColumnIndex = $(this).parent().index() - 3;
        var currentDb = data.databases[currentColumnIndex];
        var dbName = currentDb[Object.keys(currentDb)[0]];

        var temp = data.results.find(x => x.verifierName === rowData);
        var preContent = temp[Object.keys(temp)[currentColumnIndex + 3]];

        var tooltipInstance = null;

        var row = data.results.find(x => x.verifierName === rowData);
        var tooltipContent = "";

        tooltipContent += Object.keys(row)[0] + ": " + row[Object.keys(row)[0]] + "<br>";
        tooltipContent += "databaseName: " + dbName + "<br>";


        for (var i = 0; i <= 2; i++) {
          tooltipContent += Object.keys(preContent)[i] + ": " + preContent[Object.keys(preContent)[i]].toFixed(2) + "%" + "<br>";
        }

        if (Object.keys(preContent)[3] === 'Comment') {
          tooltipContent += Object.keys(preContent)[3] + ": " + preContent[Object.keys(preContent)[3]];
        }


        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: tooltipContent,
          delay: 0,
          theme: 'tooltipster-punk'

        })
        tooltipInstance.tooltipster('open');

      })
    }
  });
});