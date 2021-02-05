<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Application_Security_Assignment.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbl_message" runat="server"></asp:Label>
        </div>
        <asp:Button ID="btn_logout" runat="server" Text="Logout" OnClick="btn_logout_Click" />
    </form>
</body>
</html>
