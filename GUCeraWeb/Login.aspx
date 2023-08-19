<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please Log in<br />
            <br />
            Username:<br />
            <asp:TextBox ID="username" runat="server"></asp:TextBox>
            <br />
            Password:</div>
        <p>
            <asp:TextBox ID="password" TextMode="Password" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="signin" onClick="loginfunc" runat="server" Text="Login" />
        </p>
    </form>
</body>
</html>
