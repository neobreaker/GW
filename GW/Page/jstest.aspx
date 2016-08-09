<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jstest.aspx.cs" Inherits="GW.Page.jstest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <script language="JavaScript">
        $.get("/WCF/ServiceChart.svc/GetChart", function (data, status) {
            alert("Data: " + data + "\nStatus: " + status);
        });
    </script>
    </div>
    </form>
</body>
</html>
