// '.tbl-content' consumed little space for vertical scrollbar, scrollbar width depend on browser/os/platfrom. Here calculate the scollbar width .
$(window).on("load resize ", function () {
  var scrollWidth = $('.tbl-content').width() - $('.tbl-content table').width();
  $('.tbl-header').css({ 'padding-right': scrollWidth });
}).resize();


function sortTable(n) {
  var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
  table = document.getElementById("employee_table");
  switching = true;
  dir = "desc";
  while (switching) {
    switching = false;
    rows = table.rows;
    for (i = 1; i < (rows.length - 1); i++) {
      shouldSwitch = false;
      x = rows[i].getElementsByTagName("TD")[n];
      y = rows[i + 1].getElementsByTagName("TD")[n];
      if (dir == "asc") {
        if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
          shouldSwitch = true;
          break;
        }
      } else if (dir == "desc") {
        if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
          shouldSwitch = true;
          break;
        }
      }
    }
    if (shouldSwitch) {
      rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
      switching = true;
      switchcount++;
    } else {
      if (switchcount == 0 && dir == "desc") {
        dir = "asc";
        switching = true;
      }
    }
  }
}

$(document).ready(function () {
  $.getJSON("employee.json", function (data) {
    var employee_data = ' ';
    $.each(data, function (key, value) {
      employee_data += '<tr>';
      employee_data += '<td>' + value.name + '</td>';
      employee_data += '<td>' + value.DB1 + '%' + '</td>';
      employee_data += '<td>' + value.DB2 + '%' + '</td>';
      employee_data += '<td>' + value.DB3 + '%' + '</td>';
      employee_data += '<td>' + value.DB4 + '%' + '</td>';
      employee_data += '<td>' + value.DB5 + '%' + '</td>';
      var sum = +value.DB1 + +value.DB2 + +value.DB3 + +value.DB4 + +value.DB5;
      var avg = (sum / 5).toFixed(2);
      employee_data += '<td>' + avg + '%' + '</td>';
      employee_data += '</tr>';
    });
    $('#employee_table').append(employee_data);
  });
});