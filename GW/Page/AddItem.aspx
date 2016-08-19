<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="GW.Page.AddItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="克尔苏加德"></asp:Label>
                <asp:TextBox ID="TextBoxItem" runat="server" Height="30px" Width="250px"></asp:TextBox>
                <asp:CheckBox
                    ID="CheckBoxBind" runat="server" Text="是否绑定" Checked="false" 
                    oncheckedchanged="CheckBoxBind_CheckedChanged"/>
                <asp:Button ID="ButtonAdd" runat="server" style="margin-top: 0px" Text="增加" 
                    Height="30px" Width="76px" onclick="ButtonAdd_Click" />
                <asp:Label ID="LabelHint" runat="server" Text="" ></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
