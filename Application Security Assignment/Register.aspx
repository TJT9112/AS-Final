<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Application_Security_Assignment.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>

        <script type ="text/javascript">
            function validate() {
                var str = document.getElementById('<%=tb_password.ClientID %>').value;

                if (str.length < 8) {
                    document.getElementById("lbl_pwdchecker").innerHTML = "Password Length Must be at Least 8 Characters";
                    document.getElementById("lbl_pwdchecker").style.color = "Red";
                    return ("too_short");
                }

                else if (str.search(/[0-9]/) == -1) {
                    document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 Number";
                    document.getElementById("lbl_pwdchecker").style.color = "red";
                    return ("no_number");
                }


                else if (str.search(/[A-Z]/) == -1) {
                    document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 Upper Case Letter";
                    document.getElementById("lbl_pwdchecker").style.color = "red";
                    return ("no_uppercase");
                }

                else if (str.search(/[a-z]/) == -1) {
                    document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 Lower Case Letter";
                    document.getElementById("lbl_pwdchecker").style.color = "red";
                    return ("no_uppercase");
                }

                else if (str.search(/[^A-Za-z0-9]/) == -1) {
                    document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 Special Character";
                    document.getElementById("lbl_pwdchecker").style.color = "red";
                    return ("no_specialcharacter");
                }

                document.getElementById("lbl_pwdchecker").innerHTML = "Status : Good";
                document.getElementById("lbl_pwdchecker").style.color = "Green";
            }

            function validatecardnumber() {

                var str = document.getElementById('<%=tb_cardnumber.ClientID %>').value;

                if (str.length == 16) {
                    if (str.search(/[A-Z]/) > 0) {
                        document.getElementById("lbl_cardnumberalert").innerHTML = "Card Number cannot contain a UpperCase Character!!!!!";
                        document.getElementById("lbl_cardnumberalert").style.color = "red";
                    }

                    else if (str.search(/[a-z]/) > 0) {
                        document.getElementById("lbl_cardnumberalert").innerHTML = "Card Number cannot contain a LowerCase Character!!!!!";
                        document.getElementById("lbl_cardnumberalert").style.color = "red";
                    }

                    else if (str.search(/[^A-Za-z0-9]/) > 0) {
                        document.getElementById("lbl_cardnumberalert").innerHTML = "Card Number cannot contain a Special Character!!!!!";
                        document.getElementById("lbl_cardnumberalert").style.color = "red";
                    }

                    else {
                        document.getElementById("lbl_cardnumberalert").innerHTML = "Status : Good";
                        document.getElementById("lbl_cardnumberalert").style.color = "Green";
                    }

                }

                else if (str.length != 16) {
                    document.getElementById("lbl_cardnumberalert").innerHTML = "Card Number must contain 16 Numbers!!!!!";
                    document.getElementById("lbl_cardnumberalert").style.color = "Red";
                }

            }

            function validatecardcvc() {

                var str = document.getElementById('<%=tb_cardcvc.ClientID %>').value;

                if (str.length == 3) {
                    if (str.search(/[A-Z]/) > 0) {
                        document.getElementById("lbl_cardcvcalert").innerHTML = "Card Number cannot contain a UpperCase Character!!!!!";
                        document.getElementById("lbl_cardcvcalert").style.color = "red";
                    }

                    else if (str.search(/[a-z]/) > 0) {
                        document.getElementById("lbl_cardcvcalert").innerHTML = "Card Number cannot contain a LowerCase Character!!!!!";
                        document.getElementById("lbl_cardcvcalert").style.color = "red";
                    }

                    else if (str.search(/[^A-Za-z0-9]/) > 0) {
                        document.getElementById("lbl_cardcvcalert").innerHTML = "Card Number cannot contain a Special Character!!!!!";
                        document.getElementById("lbl_cardcvcalert").style.color = "red";
                    }

                    else {
                        document.getElementById("lbl_cardcvcalert").innerHTML = "Status : Good";
                        document.getElementById("lbl_cardcvcalert").style.color = "Green";
                    }

                }

                else if (str.length != 3) {
                    document.getElementById("lbl_cardcvcalert").innerHTML = "Card CVC must contain 3 Numbers!!!!!";
                    document.getElementById("lbl_cardcvcalert").style.color = "Red";
                }

            }

            function validatedob() {

                var str = document.getElementById('<%=tb_dob.ClientID %>').value;

                if (str.match(/^\s*(3[01]|[12][0-9]|0?[1-9])\/(1[012]|0?[1-9])\/((?:19|20)\d{2})\s*$/)) {
                    document.getElementById("lbl_dobchecker").innerHTML = "Status : Good";
                    document.getElementById("lbl_dobchecker").style.color = "Green";
                }

                else {
                    document.getElementById("lbl_dobchecker").innerHTML = "Date Of Birth can only be in the format of DD/MM/YYYY!!!!!";
                    document.getElementById("lbl_dobchecker").style.color = "Red";
                }

            }

            function validatefirstname() {

                var str = document.getElementById('<%=tb_firstname.ClientID %>').value;

                if (str.search(/[^A-Za-z0-9]/) > 0) {
                    document.getElementById("lbl_firstnamechecker").innerHTML = "First Name cannot contain a Special Character!!!!!";
                    document.getElementById("lbl_firstnamechecker").style.color = "Red";
                }

                else {
                    document.getElementById("lbl_firstnamechecker").innerHTML = "Status : Good";
                    document.getElementById("lbl_firstnamechecker").style.color = "Green";
                }

            }

            function validatelastname() {

                var str = document.getElementById('<%=tb_lastname.ClientID %>').value;

                if (str.search(/[^A-Za-z0-9]/) > 0) {
                    document.getElementById("lbl_lastnamechecker").innerHTML = "Last Name cannot contain a Special Character!!!!!";
                    document.getElementById("lbl_lastnamechecker").style.color = "Red";
                }

                else {
                    document.getElementById("lbl_lastnamechecker").innerHTML = "Status : Good";
                    document.getElementById("lbl_lastnamechecker").style.color = "Green";
                }

            }

            function validateemail() {

                var str = document.getElementById('<%=tb_emailaddress.ClientID %>').value;

                if (str.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{3}$/)) {
                    document.getElementById("lbl_emailchecker").innerHTML = "Status : Good";
                    document.getElementById("lbl_emailchecker").style.color = "Green";
                }

                else {
                    document.getElementById("lbl_emailchecker").innerHTML = "Please enter a valid email address!!!!!";
                    document.getElementById("lbl_emailchecker").style.color = "Red";
                }

            }

            function validatecardexpire() {

                var str = document.getElementById('<%=tb_cardexpire.ClientID %>').value;

                if (str.match(/^(0[1-9]|1[0-2])\/([0-9]{2})$/)) {
                    document.getElementById("lbl_cardexpirealert").innerHTML = "Status : Good";
                    document.getElementById("lbl_cardexpirealert").style.color = "Green";
                }

                else {
                    document.getElementById("lbl_cardexpirealert").innerHTML = "Card expire should only be in the format of MM/YY!!!!!";
                    document.getElementById("lbl_cardexpirealert").style.color = "Red";
                }

            }

        </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            Registration Form
            <br />
            <br />
            <asp:Label ID="lbl_registermsg" runat="server"></asp:Label>
            <br />

            <table>
                <tr>
                    <td>First Name :</td>
                    <td><asp:TextBox ID="tb_firstname" runat="server" onkeyup="javascript:validatefirstname()"></asp:TextBox><asp:Label ID="lbl_firstnamechecker" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td>Last Name :</td>
                    <td><asp:TextBox ID="tb_lastname" runat="server" onkeyup="javascript:validatelastname()"></asp:TextBox><asp:Label ID="lbl_lastnamechecker" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td>Email Address :</td>
                    <td><asp:TextBox ID="tb_emailaddress" runat="server" placeholder="jyongthien@gmail.com" onkeyup="javascript:validateemail()"></asp:TextBox><asp:Label ID="lbl_emailchecker" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td>Date Of Birth :</td>
                    <td><asp:TextBox ID="tb_dob" runat="server" placeholder="DD/MM/YYYY" onkeyup="javascript:validatedob()"></asp:TextBox><asp:Label ID="lbl_dobchecker" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td>Password :</td>
                    <td><asp:TextBox ID="tb_password" runat="server" onkeyup="javascript:validate()"></asp:TextBox><asp:Label ID="lbl_pwdchecker" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td>Credit Card Number :</td>
                    <td><asp:TextBox ID="tb_cardnumber" runat="server" onkeyup="javascript:validatecardnumber()" ></asp:TextBox><asp:Label ID="lbl_cardnumberalert" runat="server"></asp:Label></td>
                </tr>
                
                <tr>
                    <td>Credict Card CVC : </td>
                    <td><asp:TextBox ID="tb_cardcvc" runat="server" onkeyup="javascript:validatecardcvc()" ></asp:TextBox><asp:Label ID="lbl_cardcvcalert" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td>Credict Card Expire Date : </td>
                    <td><asp:TextBox ID="tb_cardexpire" runat="server" placeholder="MM/YY" onkeyup="javascript:validatecardexpire()"></asp:TextBox><asp:Label ID="lbl_cardexpirealert" runat="server"></asp:Label></td>
                </tr>

            </table>

            <br />

            <asp:Button ID="bth_register" runat="server" OnClick="btn_register_Click" Text="Register" Width="325px"/>

            <br />
            <br />
            <asp:Button ID="btn_loginpage" runat="server" OnClick="btn_loginpage_Click" Text="Login Page" Width="322px" />

        </div>
        <asp:Label ID="lb_error1" runat="server"></asp:Label>
    </form>
</body>
</html>
