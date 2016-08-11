<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HighChartsTest.aspx.cs" Inherits="GW.Page.HighChartsTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.4.1.js"></script>
    <script src="/3rdpart/highcharts/highcharts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="container" style="width: 550px; height: 400px; margin: 0 auto" runat="server"></div>
    <div id="container2" style="width: 550px; height: 400px; margin: 0 auto"></div>
<script language="JavaScript">
    $(document).ready(function () {

        var credits = {
            enabled: false
        };

        var title = {
            text: '月平均气温'
        };
        var subtitle = {
            text: 'Source: runoob.com'
        };
        var xAxis = {
            categories: ['一月', '二月', '三月', '四月', '五月', '六月'
              , '七月', '八月', '九月', '十月', '十一月', '十二月']
        };
        var yAxis = {
            title: {
                text: 'Temperature (\xB0C)'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        };

        var tooltip = {
            valueSuffix: '\xB0C'
        }

        var legend = {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        };
        var json = {};
        json.credits = credits;
        json.title = title;
        json.subtitle = subtitle;
        json.xAxis = xAxis;
        json.yAxis = yAxis;
        json.tooltip = tooltip;
        json.legend = legend;

        $.get("/WCF/ServiceChart.svc/GetChart", function (data, status) {
            json.series = eval("(" + data + ")");
            $('#container').highcharts(json);
        });
        
    });
    </script>
    <script language="JavaScript">
$(document).ready(function() {  
   var chart = {
       plotBackgroundColor: null,
       plotBorderWidth: null,
       plotShadow: false
   };
   var title = {
      text: '2014 年各浏览器市场占有比例'   
   };      
   var tooltip = {
      pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
   };
   var plotOptions = {
      pie: {
         allowPointSelect: true,
         cursor: 'pointer',
         dataLabels: {
            enabled: true,
            format: '<b>{point.name}%</b>: {point.percentage:.1f} %',
            style: {
               color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
            }
         }
      }
   };
   var series= [{
      type: 'pie',
      name: 'Browser share',
      data: [
         ['Firefox',   45.0],
         ['IE',       26.8],
         {
            name: 'Chrome',
            y: 12.8,
            sliced: true,
            selected: true
         },
         ['Safari',    8.5],
         ['Opera',     6.2],
         ['Others',   0.7]
      ]
   }];     
      
   var json = {};   
   json.chart = chart; 
   json.title = title;     
   json.tooltip = tooltip;  
   json.plotOptions = plotOptions;
   
   $.get("/WCF/ServiceChart.svc/GetPie", function (data, status) {
       json.series = eval("(" + data + ")");
       $('#container2').highcharts(json);
   });
});
</script>
    </div>
    </form>
</body>
</html>
