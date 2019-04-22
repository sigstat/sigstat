var resultData =
[
  {
  ver_name: "Simple Verifier",
  verifierDescription: "asd123",
  verifierURL: "http://index.hu",
    svc_aer: 12.4,
    svc_far: 8.2,
    svc_frr: 20.2,
    svc_comment: "",
    mcy_aer: 11.4,
    mcy_far: 4.2,
    mcy_frr: 30.2,
    mcy_comment: "Best result",
    swc11_aer: 31,
    swc11_far: 51,
    swc11_frr: 11,
    swc11_comment: "",
    swc13_aer: 12,
    swc13_far: 8,
    swc13_frr: 20,
    swc13_comment: ""
},
{
ver_name: "Simple Verifier123",
  verifierDescription: "asd456",
  verifierURL: "http://index.hu",
    svc_aer: 12.4,
    svc_far: 8.2,
    svc_frr: 20.2,
    svc_comment: "",
    swc11_aer: 31,
    swc11_far: 51,
    swc11_frr: 11,
    mcy_aer: 31.4,
    mcy_far: 14.2,
    mcy_frr: 11.2,
    mcy_comment: "worst",
    swc11_comment: "",
    swc13_aer: 12,
    swc13_far: 8,
    swc13_frr: 20,
    swc13_comment: "xxx"
},
{
  ver_name: "Simple Verifier1234",
    verifierDescription: "asd",
    verifierURL: "http://index.hu",
      svc_aer: 12.4,
      svc_far: 8.2,
      svc_frr: 20.2,
      svc_comment: "sdf",
      mcy_aer: 31.4,
      mcy_far: 14.2,
      mcy_frr: 11.2,
      mcy_comment: "worst",
      swc11_aer: 31,
      swc11_far: 51,
      swc11_frr: 11,
      swc11_comment: "",
      swc13_aer: 12,
      swc13_far: 8,
      swc13_frr: 20,
      swc13_comment: ""
  },
  {
    ver_name: "Simple Verifier12345",
      verifierDescription: "asd",
      verifierURL: "http://index.hu",
        svc_aer: 13.4,
        svc_far: 8.2,
        svc_frr: 20.2,
        svc_comment: "this is a test comment",
        mcy_aer: 21.4,
        mcy_far: 13,
        mcy_frr: 61,
        mcy_comment: "worst",
        swc11_aer: 31,
        swc11_far: 51,
        swc11_frr: 11,
        swc11_comment: "exception",
        swc13_aer: 12,
        swc13_far: 8,
        swc13_frr: 20,
        swc13_comment: ""
  }
];

var databases = [
  {
  Name: "SVC2004",
  Description: "Siganture Verification Competition 2004",
  Signers: 20,
  GenuineSignatures: 20,
  ForgedSignatures: 20,
  TotalSignatures: 800,
  url: "http://index.hu"
},
{
  Name: "MCYT100",
  Description: "Siganture Verification Competition 2004",
  Signers: 20,
  GenuineSignatures: 20,
  ForgedSignatures: 20,
  TotalSignatures: 800,
  url: "http://index.hu"
},
{
  Name: "SigWiComp'11", 
  Description: "Siganture Verification Competition 2004",
  Signers: 20,
  GenuineSignatures: 40,
  ForgedSignatures: 20,
  TotalSignatures: 800,
  url: "http://index.hu"
},
{
  Name: "SigWiComp'13",
  Description: "Siganture Verification Competition 2004",
  Signers: 20,
  GenuineSignatures: 20,
  ForgedSignatures: 20,
  TotalSignatures: 8001,
  url: "http://index.hu"
},
]

/*var getExtraData = function (cell){
  return cell.getColumn().getField().verifierDescription;
}*/

function getDbByName(name){
  return databases.find(item => {
    return item.Name == name;
  })
}

function getVerifierByName(name){
  return resultData.find(item => {
    return item.ver_name == name;
  })
}

//create Tabulator on DOM element with id "example-table"
var table = new Tabulator("#example-table", {
  //height:800, // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
  data:resultData, //assign data to table
  tooltipsHeader: true,
  tooltips: true,
  layout: "fitDataFill",
  responsiveLayout: true,
  columns:[ //Define Table Columns
    {title:"Verifier's name", field:"ver_name", width:150, resizable: false},
    {title:"SVC2004", align: "center", width:150, resizable: false, headerDblClick: function(e, column){
      var found = getDbByName('SVC2004');
      alert ("Description: " + found.Description + "\n" + "Signers: " + found.Signers + "\n" +
      "GenuineSignatures: " + found.GenuineSignatures + "\n" + "ForgedSignatures: " + found.ForgedSignatures +
      "\n" + "TotalSignatures: " + found.TotalSignatures + "\n" + "Url: " + found.url);
    },
    columns:[
      {title: "AER", field: "svc_aer", width:65, resizable: false },
      {title: "FAR", field: "svc_far", width:65, resizable: false },
      {title: "FRR", field: "svc_frr", width:65, resizable: false },
    ]},
    {title:"MCYT100", align: "center", width:195, resizable: false, headerDblClick: function(e, column){
      var found = getDbByName('MCYT100');
      alert ("Description: " + found.Description + "\n" + "Signers: " + found.Signers + "\n" +
      "GenuineSignatures: " + found.GenuineSignatures + "\n" + "ForgedSignatures: " + found.ForgedSignatures +
      "\n" + "TotalSignatures: " + found.TotalSignatures + "\n" + "Url: " + found.url);
    },
    columns:[
      {title: "AER", field: "mcy_aer", width:65, resizable: false},
      {title: "FAR", field: "mcy_far", width:65, resizable: false},
      {title: "FRR", field: "mcy_frr", width:65, resizable: false},
    ]},
    {title:"SigWiComp'11", field:"swc11", width:195, resizable: false, headerDblClick: function(e, column){
      var found = getDbByName("SigWiComp'11");
      alert ("Description: " + found.Description + "\n" + "Signers: " + found.Signers + "\n" +
      "GenuineSignatures: " + found.GenuineSignatures + "\n" + "ForgedSignatures: " + found.ForgedSignatures +
      "\n" + "TotalSignatures: " + found.TotalSignatures + "\n" + "Url: " + found.url);
    },
    columns:[
      {title: "AER", field: "swc11_aer", width:65, resizable: false},
      {title: "FAR", field: "swc11_far", width:65, resizable: false},
      {title: "FRR", field: "swc11_frr", width:65, resizable: false},
    ]},
    {title:"SigWiComp'13", field:"swc13", align:"left", width: 195, resizable: false, headerDblClick: function(e,column){
      var found = getDbByName("SigWiComp'13");
      alert ("Description: " + found.Description + "\n" + "Signers: " + found.Signers + "\n" +
      "GenuineSignatures: " + found.GenuineSignatures + "\n" + "ForgedSignatures: " + found.ForgedSignatures +
      "\n" + "TotalSignatures: " + found.TotalSignatures + "\n" + "Url: " + found.url);
    },
    columns:[
      {title: "AER", field: "swc13_aer", width:65, resizable: false},
      {title: "FAR", field: "swc13_far", width:65, resizable: false},
      {title: "FRR", field: "swc13_frr", width:65, resizable: false},
    ]},
  ],
  rowClick:function(e, row){ //trigger an alert message when the row is clicked
    var found = getVerifierByName(row.getData().ver_name);
    //alert("Row " + row.getData().id + " Clicked!!!!");
    if (found.svc_comment === ''){
      found.svc_comment = "Everything OK";
    }
    if (found.mcy_comment === ''){
      found.mcy_comment = "Everything OK";
    }
    if (found.swc11_comment === ''){
      found.swc11_comment = "Everything OK";
    }
    if (found.swc13_comment === ''){
      found.swc13_comment = "Everything OK";
    }
    alert("Description: " + found.verifierDescription + "\n" + "Url: " + found.verifierURL + "\n" + "SVC Comment: " + found.svc_comment + "\n"
    + "MCY Comment: " + found.mcy_comment + "\n" + "SWC11 Comment: " + found.swc11_comment + "\n" + "SWC13 Comment: " + found.swc13_comment + "\n");
  }
});
