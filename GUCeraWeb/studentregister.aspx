<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studentregister.aspx.cs" Inherits="GUCera.studentregister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label5" runat="server"></asp:Label>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back to login page</asp:LinkButton>
            <br />
            <br />
         <asp:Label ID="lbl_username" runat="server" Text="Firstname: "></asp:Label>
    
            <br />
    
        <asp:TextBox ID="firstname" runat="server"></asp:TextBox>
    
            <br />
    
        <br />
            <asp:Label ID="Label1" runat="server" Text="Lastname: "></asp:Label>
    
            <br />
    
        <asp:TextBox ID="lastname" runat="server"></asp:TextBox>
    
            <br />
    
        <br />
             <asp:Label ID="lbl_password" runat="server" Text="Password: "></asp:Label>
            <br />
        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
    
            <br />
    
        <br />
               <asp:Label ID="Label2" runat="server" Text="Email: "></asp:Label>
    
            <br />
    
        <asp:TextBox ID="email" runat="server"></asp:TextBox>
    
            <br />
    
        <br />
               <asp:Label ID="Label3" runat="server" Text="Gender: "></asp:Label>
    
            <br />
    
            <asp:DropDownList ID="gender" runat="server">
                <asp:ListItem Value="0">Male</asp:ListItem>
                <asp:ListItem Value="1">Female</asp:ListItem>
            </asp:DropDownList>
    
            <br />
    
        <br />
               <asp:Label ID="Label4" runat="server" Text="Address: "></asp:Label>
    
            <br />
    
        <asp:TextBox ID="address" runat="server"></asp:TextBox>
    
            <br />
    
        <br />
                        
        <asp:Button ID="student" runat="server" Text="Register as student" onclick="sregister" Width="311px"/>
        <asp:Button ID="instructor" runat="server" Text="Register as instructor" onclick="iregister" Width="311px"/>
        </div>
    </form>
</body>
</html>
