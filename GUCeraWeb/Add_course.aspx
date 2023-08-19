<%@ Page Language="C#" AutoEventWireup="true" Inherits="GUCeraWeb.Add_course" CodeBehind="Add_course.aspx.cs" CodeFile="Add_course.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            
            <h3>Create a course.</h3>
            Course Name: <br />
            <asp:TextBox ID="name" runat="server" autocomplete="off" onkeypress="return this.value.length<10"></asp:TextBox>
            &nbsp;(10 characters maximum)<br />
            <br />
            Credit Hours:<br />
            <asp:TextBox ID="hours" runat="server" autocomplete="off" onkeypress="return (event.keyCode<=57 && event.keyCode>=48)"></asp:TextBox>
            (Integer)<br />
            <br />
            Course Price:<br />
            <asp:TextBox ID="price" runat="server" autocomplete="off" onkeypress="return CheckInputs()"></asp:TextBox>
            (Decimal)<br />
            <br />
            Course Content (Optional):<br />
            <asp:TextBox ID="content" runat="server" autocomplete="off" onkeypress="return this.value.length<200"></asp:TextBox>
            &nbsp;(200 characters maximum)<br />
            <br />
            Course Description (Optional):<br />
            <asp:TextBox ID="desc" runat="server" autocomplete="off" onkeypress="return this.value.length<200"></asp:TextBox>
            &nbsp;(200 characters maximum)<br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="CreateCourse" Text="Create Course" />
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back to main page</asp:LinkButton>
            
            <br />
        </div>
    </form>
    
</body>
</html>
