$(function () {
  $.ajax({
    type: 'GET',
    url: 'https://api.myjson.com/bins/z4m4w',
    dataType: 'json',
    success: function (data) {
      $("body").on('mouseover', 'span:not(.tooltipstered)', function () {
        var tooltipInstance = null;
        var temp = ($(this).html());
        var sep = ": ";
        var eol = "<br>";

        var first = data.databases.find(asd => asd.Name === temp);
        var count = Object.keys(first).length - 1;
        console.log(count);
        var test = "";

        for (var i = 1; i <= count; i++) {
          test += Object.keys(first)[i] + sep + first[Object.keys(first)[i]] + eol
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 0
        });

        tooltipInstance.tooltipster('open');
      })
      $("table tbody tr td:first-child").on('mouseover', function () {
        var tooltipInstance = null;
        var temp = ($(this).html());
        console.log(temp);
        var sep = ": ";
        var eol = "<br>";

        var first = data.results.find(asd => asd.verifierName === temp);
        var count = Object.keys(first).length - 5;
        var test = "";
        for (var i = 1; i <= count; i++) {
          test += Object.keys(first)[i] + sep + first[Object.keys(first)[i]] + eol
        }

        tooltipInstance = $(this).tooltipster({
          contentAsHTML: true,
          content: test,
          delay: 0
        })
        tooltipInstance.tooltipster('open');
      })
    }
  });
});