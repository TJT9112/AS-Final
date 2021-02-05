<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Application_Security_Assignment.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>

    <script src=""></script>

</head>
<body>

    <form id="form1" runat="server">
        <div>

            Login
            <br />
            <br />

            <table>

                <tr>
                    <td>Email : </td>
                    <td><asp:TextBox ID="tb_userid" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Passowrd : </td>
                    <td><asp:TextBox ID="tb_passwordlogin" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td><asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click" Width="131px" />
                        <br />
                        <br />
                        <asp:Button ID="btn_registerpage" runat="server" OnClick="btn_registerpage_Click" Text="Register Page" Width="131px" />
                    </td>
                </tr>

                <tr>
                    <td><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
                </tr>

            </table>


            <br />
            <br />

             <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response" />

            <asp:Label ID="lbl_captchamessage" runat="server"></asp:Label>

            <asp:Label ID="lbl_gScore" runat="server" ></asp:Label>

        </div>
    </form>

    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>

</body>
</html>
