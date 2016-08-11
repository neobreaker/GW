<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="GW.Page.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="/Scripts/jquery-1.4.1.js"></script>
    <script src="/3rdpart/highcharts/highcharts.js"></script>
    <script language="JavaScript">
        $(document).ready(function () {
            
        });
    </script>
    <script language="JavaScript">
        function onclick_searchBtn() {
            var credits = {
                enabled: false
            };
            var chart = {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false
            };
            var title = {
                text: '各原材料成本占有比例'
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
            var series = [{
                    type: 'pie',
                    name: 'Browser share',
                    data: [
                    ['Firefox', 45.0],
                    ['IE', 26.8],
                    {
                        name: 'Chrome',
                        y: 12.8,
                        sliced: true,
                        selected: true
                    },
                    ['Safari', 8.5],
                    ['Opera', 6.2],
                    ['Others', 0.7]
                ]
            }];

            var json = {};
            json.credits = credits;
            json.chart = chart;
            json.title = title;
            json.tooltip = tooltip;
            json.plotOptions = plotOptions;
            $("#<%= ButtonSearch.ClientID %>").click();
            $.get("/WCF/ServiceChart.svc/GetPie", function (data, status) {
                json.series = eval("(" + data + ")");
                
                $('#container').highcharts(json);

            });
        }
    </script>
    <title></title>
<style type =  "text/css">
    
div.layout_center
{
    text-align:center;
    margin: 100px 0px 0px 0px;
}

    #searchInput
    {
        height: 34px;
        width: 223px;
    }
    #searchBtn
    {
        height: 22px;
        width: 63px;
    }

</style>
</head>
<body>
    <div>
    <span>
        <input type="text" name="textfield" id="searchInput" /> 
    </span>
    <span>
        <button id=searchBtn onclick="onclick_searchBtn()">查询</button>
    </span>
        
        
    </div>
    <form id="form1" runat="server">
    
    <div >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div>
                <div class=layout_center>
                    <span>
                        <asp:TextBox ID="TextBoxSearch" runat="server" Width="200px" Height="30px"></asp:TextBox>
                    </span>
                    <span>
                        <asp:Button ID="ButtonSearch" runat="server" Text="搜索" 
                        onclick="ButtonSearch_Click" Height="30px" Width="70px" />
                    </span>
                </div>
                <div class=layout_center>
                    <asp:GridView ID="GridViewOutputItem" align="center" runat="server" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="物品名" />
                            
                            <asp:BoundField DataField="avgprice" HeaderText="平均成本" 
                                SortExpression="avgprice" />
                            <asp:BoundField DataField="lowprice" HeaderText="最低成本" 
                                SortExpression="lowprice" />
                            <asp:BoundField DataField="highprice" HeaderText="最高成本" 
                                SortExpression="highprice" />
                            <asp:BoundField DataField="avgsaleprice" HeaderText="平均售价" 
                                SortExpression="avgsaleprice" />
                            <asp:BoundField DataField="avgprofit" HeaderText="平均利润" 
                                SortExpression="avgprofit" />
                            <asp:BoundField DataField="avgprofitrate" HeaderText="平均利润率" 
                                SortExpression="avgprofitrate" />
                            <asp:BoundField DataField="manufacturingcycle" HeaderText="制造周期" 
                                SortExpression="manufacturingcycle" />
                            <asp:BoundField DataField="FWQ" HeaderText="服务器" SortExpression="FWQ" />
                            <asp:BoundField DataField="updatetime" HeaderText="更新时间" 
                                SortExpression="updatetime" />
                            
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
                <div class=layout_center>
                    <asp:GridView 
                        ID="GridViewSearch" align="center" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="759px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="物品名" />
                            <asp:BoundField DataField="avgprice" HeaderText="平均价格" />
                            <asp:BoundField DataField="lowprice" HeaderText="最低价格" 
                                SortExpression="lowprice" />
                            <asp:BoundField DataField="highprice" HeaderText="最高价格" 
                                SortExpression="highprice" />
                            <asp:BoundField DataField="num" HeaderText="数量" SortExpression="num" />
                            <asp:BoundField DataField="total" HeaderText="合计" />
                            <asp:BoundField DataField="FWQ" HeaderText="服务器" SortExpression="FWQ" />
                            <asp:BoundField DataField="updatetime" HeaderText="更新时间" 
                                SortExpression="updatetime" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
                
            </div>
                
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <div class=layout_center id="container" style="width: 550px; height: 400px; margin: 0 auto"></div>
</body>
</html>
