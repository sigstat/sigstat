$(function () {
  $.ajax({
    type: 'GET',
    // url: 'https://api.myjson.com/bins/z9efy', // with columnType example
    url: 'http://sigstat.org/site/scripts/Results.json', // directly from the site
    dataType: 'json',
    success: function (data) {
      let db_length = Object.keys(data.databases).length;
      let th = "<th class=\"rotate\">";
      let th_end = "</th>";

      $("#header").append("<th class=\"align-middle\">" + "Verifier's" + "<br>" + "Name" + th_end);
      $("#header").append("<th class=\"align-middle\">" + "Average" + th_end);
      $("#header").append("<th class=\"align-middle\">" + "Average" + "<br>" + "Core" + th_end);
      for (let i = 0; i < db_length; i++) {
        $("#header").append(th + "<div>" + "<span>" + data.databases[i].Name + "</div>" + "</span>" + th_end);
      }

      let res_length = Object.keys(data.results).length;

      let databaseNames = Object.keys(data.results[0]);
      databaseNames.splice(0, 3);

      let dynamic = "";

      for (let i = 0; i < res_length; i++) {
        dynamic += "<tr>" + "<td class=\"verName\">" + data.results[i].verifierName + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + (data.results[i]["Average"].AER).toFixed(2) + "%" + "</p>" + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + data.results[i]["Average Core"].AER.toFixed(2) + "%" + "</p>" + "</td>";
        for (let j = 0; j < db_length; j++) { // if AER is missing due to exception
          dynamic += "<td>" + "<em>" + (data.results[i][databaseNames[j]].AER == null ? " " : (data.results[i][databaseNames[j]].AER).toFixed(2) + "%") + "</em>" + "</td>";
        }
        dynamic += "</tr>";

      }
      $("#content").append(dynamic);

      let IsValueSummary = data.columnType;
      if (IsValueSummary === 'summary') {
        let bar = newSheet();
        bar.insertRule("p.summary {color: white;}", 0);
        bar.insertRule("p.summary {background-color: chocolate;}", 0);
      }

      function newSheet() {
        let style = document.createElement("style");

        style.appendChild(document.createTextNode(""));
        document.head.appendChild(style);

        return style.sheet;
      };

      // table header
      $("body").on('mouseover', 'span:not(.tooltipstered)', function () {
        let tooltipInstance = null;
        let temp = ($(this).html());
        let sep = ": ";
        let eol = "<br>";

        let singleDB = data.databases.find(asd => asd.Name === temp);
        let headers = "";

        for (var i = 1; i <= 5; i++) {
          headers += Object.keys(singleDB)[i] + sep + singleDB[Object.keys(singleDB)[i]] + eol
        }
        headers += Object.keys(singleDB)[6] + sep + "<a href=" + singleDB[Object.keys(singleDB)[i]] + " target=\"_blank\">" + singleDB[Object.keys(singleDB)[i]] + "</a>";


        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: headers,
          delay: 100,
          theme: 'tooltipster-light',
          interactive: true
        })

        tooltipInstance.tooltipster('open');
      })

      // verifier's name in the first column
      $("#myTable tbody tr td:first-child").on('mouseover', function () {
        let tooltipInstance = null;
        let temp = ($(this).html());
        let sep = ": ";
        let eol = "<br>";


        let singleVerifier = data.results.find(asd => asd.verifierName === temp);
        let firstColumn = "";

        for (let i = 1; i <= 2; i++) {
          firstColumn += Object.keys(singleVerifier)[i] + sep + singleVerifier[Object.keys(singleVerifier)[i]] + eol;
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: firstColumn,
          delay: 0,
          theme: 'tooltipster-default',
          multiple: true
        })
        tooltipInstance.tooltipster('open');
      })

      // Average and Average Core datas
      $("body").on('mouseover', 'p:not(.tooltipstered)', function () {
        let rowData = ($(this).parent().parent().find('td:first').text());
        let currentColumnIndex = $(this).parent().index();


        let currentRow = data.results.find(asd => asd.verifierName === rowData);
        let preCell = "";
        if (currentColumnIndex === 1) {
          preCell = currentRow[Object.keys(currentRow)[Object.keys(currentRow).length - 2]];
        }
        if (currentColumnIndex === 2) {
          preCell = currentRow[Object.keys(currentRow)[Object.keys(currentRow).length - 1]];
        }

        let content = "";

        for (let i = 0; i <= 2; i++) {
          content += Object.keys(preCell)[i] + ": " + preCell[Object.keys(preCell)[i]].toFixed(2) + "%" + "<br>";
        }



        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: content,
          delay: 0,
          theme: 'tooltipster-noir',
        })

        tooltipInstance.tooltipster('open');
      })

      // all cells related to databases
      $("body").on('mouseover', 'em:not(.tooltipstered)', function () {
        let rowData = ($(this).parent().parent().find('td:first').text());
        let currentColumnIndex = $(this).parent().index() - 3;
        let singleDB = data.databases[currentColumnIndex];
        let firstValue = singleDB[Object.keys(singleDB)[0]];

        let getDB = data.results.find(asd => asd.verifierName === rowData);
        let currentData = getDB[Object.keys(getDB)[currentColumnIndex + 3]];

        let tooltipInstance = null;

        let row = data.results.find(asd => asd.verifierName === rowData);
        let content = "";

        content += Object.keys(row)[0] + ": " + row[Object.keys(row)[0]] + "<br>";
        content += "databaseName: " + firstValue + "<br>";


        for (let i = 0; i <= 2; i++) {
          content += Object.keys(currentData)[i] + ": " + currentData[Object.keys(currentData)[i]].toFixed(2) + "%" + "<br>";
        }

        if (Object.keys(currentData)[3] === 'Comment') {
          content += Object.keys(currentData)[3] + ": " + currentData[Object.keys(currentData)[3]];
        }


        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: content,
          delay: 0,
          theme: 'tooltipster-punk'

        })
        tooltipInstance.tooltipster('open');

      })
    }
  });
});