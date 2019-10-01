$(function () {
    var url = "https://cors-anywhere.herokuapp.com/https://github.com/sigstat/sigstat/blob/develop/docs/site/Summary_short.xlsx?raw=true";
    var oReq = new XMLHttpRequest();
    oReq.open("GET", url, true);
    oReq.responseType = "arraybuffer";

    oReq.onload = function (e) {
        var arraybuffer = oReq.response;

        /* convert data to binary string */
        var data = new Uint8Array(arraybuffer);
        var arr = new Array();
        for (var i = 0; i != data.length; ++i) arr[i] = String.fromCharCode(data[i]);
        var bstr = arr.join("");

        /* Call XLSX */
        var workbook = XLSX.read(bstr, { type: "binary" });

        /* DO SOMETHING WITH workbook HERE */
        var first_sheet_name = workbook.SheetNames[0];
        /* Get worksheet */
        var worksheet = workbook.Sheets[first_sheet_name];
        var data = XLSX.utils.sheet_to_json(worksheet, { raw: true });
        // console.log(data);



        var th = "<th class=\"rotate\">";
        var th_end = "</th>";
        var a = new Array();
        for (var k in data) {
            a.push((data[k].Database));
        }
        var uniqueArray = new Array();
        uniqueArray = a.filter(onlyUnique);
        //console.log(uniqueArray.toString());
        var uniqueDatabasesCount = uniqueArray.length;
        //console.log(uniqueDatabasesCount);


        $("#header").append("<th class=\"align-middle\">" + "Verifier's" + "<br>" + "Name" + th_end);
        $("#header").append("<th class=\"align-middle\">" + "average" + th_end);

        for (var i = 0; i < uniqueDatabasesCount; i++) {
            $("#header").append(th + "<div>" + "<span>" + uniqueArray[i] + "</div>" + "</span>" + th_end);
        }

        var Translation;
        var Scaling;
        var ResamplingTypeFilter;
        var Features;

        //1. sor

        Translation = "CogToOriginXY";
        Scaling = "None";
        ResamplingTypeFilter = "None";
        Features = "XY";

        var x = data.filter(function (el) {
            return el.Translation == Translation &&
                el.Scaling == Scaling &&
                el.ResamplingType_Filter == ResamplingTypeFilter &&
                el.Features == Features;
        });

        //console.log(x);
        var dynamic;
        var averageAER = 0;
        var averageFAR = 0;
        var averageFRR = 0;

        for (var i = 0; i < uniqueDatabasesCount; i++) {
            averageAER += x[i].AER * 100;
        }
        //console.log(averageAER);
        averageAER = (averageAER / uniqueDatabasesCount).toFixed(2);

        dynamic += "<tr>" + "<td class=\"verName\">" + Translation.substr(0, Translation.length - 2) + ", " + Features + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + averageAER + "%" + "</p>" + "</td>";
        for (var i = 0; i < uniqueDatabasesCount; i++) {
            dynamic += "<td>" + "<em>" + (x[i].AER * 100).toFixed(2) + "%" + "</em>" + "</td>";
        }


        //2. sor

        Translation = "None";
        var translationAppear;
        if (Translation = "None") {
            translationAppear = "Normalization";
        }
        Scaling = "X01Y01";
        ResamplingTypeFilter = "None";
        Features = "XY";

        x = data.filter(function (el) {
            return el.Translation == Translation &&
                el.Scaling == Scaling &&
                el.ResamplingType_Filter == ResamplingTypeFilter &&
                el.Features == Features;
        });

        averageAER = 0;
        averageFAR = 0;
        averageFRR = 0;
        for (var i = 0; i < uniqueDatabasesCount; i++) {
            averageAER += x[i].AER * 100;
        }
        averageAER = (averageAER / uniqueDatabasesCount).toFixed(2);

        dynamic += "<tr>" + "<td class=\"verName\">" + translationAppear + ", " + Features + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + averageAER + "%" + "</p>" + "</td>";
        for (var i = 0; i < uniqueDatabasesCount; i++) {
            dynamic += "<td>" + "<em>" + (x[i].AER * 100).toFixed(2) + "%" + "</em>" + "</td>";
        }

        // 3. sor

        Translation = "CogToOriginXY";
        Scaling = "None";
        ResamplingTypeFilter = "None";
        Features = "XYP";

        x = data.filter(function (el) {
            return el.Translation == Translation &&
                el.Scaling == Scaling &&
                el.ResamplingType_Filter == ResamplingTypeFilter &&
                el.Features == Features;
        });



        averageAER = 0;
        averageFAR = 0;
        averageFRR = 0;
        for (var i = 0; i < uniqueDatabasesCount; i++) {
            averageAER += x[i].AER * 100;
            averageFAR += x[i].FAR * 100;
        }
        averageAER = (averageAER / uniqueDatabasesCount).toFixed(2);
        averageFAR = (averageFAR / uniqueDatabasesCount).toFixed(2);

        dynamic += "<tr>" + "<td class=\"verName\">" + Translation.substr(0, Translation.length - 2) + ", " + Features + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + averageAER + "%" + "</p>" + "</td>";
        for (var i = 0; i < uniqueDatabasesCount; i++) {
            dynamic += "<td>" + "<em>" + (x[i].AER * 100).toFixed(2) + "%" + "</em>" + "</td>";
        }

        // 4. sor

        Translation = "None";
        Scaling = "X01Y01";
        ResamplingTypeFilter = "None";
        Features = "XYP";

        x = data.filter(function (el) {
            return el.Translation == Translation &&
                el.Scaling == Scaling &&
                el.ResamplingType_Filter == ResamplingTypeFilter &&
                el.Features == Features;
        });



        averageAER = 0;
        averageFAR = 0;
        averageFRR = 0;
        for (var i = 0; i < uniqueDatabasesCount; i++) {
            averageAER += x[i].AER * 100;
            averageFAR += x[i].FAR * 100;
        }
        averageAER = (averageAER / uniqueDatabasesCount).toFixed(2);
        averageFAR = (averageFAR / uniqueDatabasesCount).toFixed(2);

        dynamic += "<tr>" + "<td class=\"verName\">" + translationAppear + ", " + Features + "</td>";
        dynamic += "<td>" + "<p class=\"summary\">" + averageAER + "%" + "</p>" + "</td>";
        for (var i = 0; i < uniqueDatabasesCount; i++) {
            dynamic += "<td>" + "<em>" + (x[i].AER * 100).toFixed(2) + "%" + "</em>" + "</td>";
        }

        $("#content").append(dynamic);

        $("body").on('mouseover', 'p:not(.tooltipstered)', function () {
            var rowData = ($(this).parent().parent().find('td:first').text());
            console.log(rowData);
            var currentColumnIndex = $(this).parent().index();
            //console.log(currentColumnIndex);
            var compare = "";
            var features = "";
            if (rowData.includes("Normalization")) {
                compare = "None";
            } else if (rowData.includes("CogToOrigin")) {
                compare = "CogToOriginXY";
            }
            if (rowData.includes("XY")) {
                features = "XY";
            }
            if (rowData.includes("XYP")) {
                features = "XYP";
            }
            if (rowData.includes("CogToOrigin, XY")) {
                Scaling = "None";
            }
            if (rowData.includes("CogToOrigin, XYP")) {
                Scaling = "None";
            }



            var temp = data.filter(function (el) {
                return el.Translation == compare &&
                    el.Features == features &&
                    el.ResamplingType_Filter == ResamplingTypeFilter &&
                    el.Scaling == Scaling;
            })
            //console.log(temp);
            /*var almost = "";
            if(currentColumnIndex === 1){
              almost = temp[Object.keys(temp)[Object.keys(temp).length-2]];
            }
            if(currentColumnIndex === 2){
              almost = temp[Object.keys(temp)[Object.keys(temp).length-1]];
            }*/

            var rowData = ($(this).parent().text());
            averageFAR = 0;
            averageFRR = 0;
            for (var i = 0; i < 6; i++) {
                //averageAER += temp[i].AER * 100;
                averageFAR += temp[i].FAR * 100;
                averageFRR += temp[i].FRR * 100;
            }

            averageFAR = (averageFAR / uniqueDatabasesCount).toFixed(2);
            averageFRR = (averageFRR / uniqueDatabasesCount).toFixed(2);

            var test = "AER: " + rowData + "<br>" + "FAR: " + averageFAR + "%" + "<br>" + "FRR: " + averageFRR + "%";

            /* for (var i = 0; i <= 2; i++) {
               test += Object.keys(almost)[i] + ": " + almost[Object.keys(almost)[i]].toFixed(2) + "%" + "<br>";
             }*/



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
            console.log(rowData);
            var currentColumnIndex = $(this).parent().index() - 2;
            console.log(currentColumnIndex);

            var compare = "";
            var features = "";
            if (rowData === "Normalization, XY") {
                compare = "None";
                features = "XY";
                Scaling = "X01Y01";
            }
            if (rowData === "Normalization, XYP") {
                compare = "None";
                features = "XYP";
                Scaling = "X01Y01";
            }
            if (rowData === "CogToOrigin, XY") {
                compare = "CogToOriginXY";
                features = "XY";
                Scaling = "None";
            }
            if (rowData === "CogToOrigin, XYP") {
                compare = "CogToOriginXY";
                features = "XYP";
                Scaling = "None";
            }
            


            var temp = data.filter(function (el) {
                return el.Translation == compare &&
                    el.Features == features &&
                    el.ResamplingType_Filter == ResamplingTypeFilter &&
                    el.Scaling == Scaling;
            })

            console.log(temp);
            var db = temp[currentColumnIndex];
            console.log(db);
            /*var final = db[Object.keys(db)[0]];

            var temp = data.results.find(asd => asd.verifierName === rowData);
            var almost = temp[Object.keys(temp)[currentColumnIndex + 3]];



            var tooltipInstance = null;


            var row = data.results.find(asd => asd.verifierName === rowData);*/
            var test = "";

            test += "verifierName" + ": " + rowData + "<br>";
            test += "databaseName: " + db.Database + "<br>";
            test += "AER: " + (db.AER*100).toFixed(2) + "%" + "<br>";
            test += "FAR: " + (db.FAR*100).toFixed(2) + "%" + "<br>";
            test += "FRR: " + (db.FRR*100).toFixed(2) + "%" + "<br>";


            /*for (var i = 0; i <= 2; i++) {
                test += Object.keys(almost)[i] + ": " + almost[Object.keys(almost)[i]].toFixed(2) + "%" + "<br>";
            }

            if (Object.keys(almost)[3] === 'Comment') {
                test += Object.keys(almost)[3] + ": " + almost[Object.keys(almost)[3]];
            }*/


            tooltipInstance = $(this).tooltipster({
                contentAsHTML: true,
                content: test,
                delay: 0,
                theme: 'tooltipster-punk'

            })
            tooltipInstance.tooltipster('open');

        })

    }

    oReq.send();

});

function onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
}