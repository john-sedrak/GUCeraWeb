<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Instructor_home.aspx.cs" Inherits="GUCeraWeb.Instructor_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Welcome,
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </h2>
            <asp:linkButton ID="addCourse" runat="server" Text="Add a new course" OnClick="addCourse_Click" />
            <br />
            <br />
            <asp:linkButton ID="defineAssign" runat="server" Text="Define an assignment" OnClick="defineAssign_Click" />
            <br />
            <br />
            <asp:linkButton ID="viewAndGrade" runat="server" Text="View and grade submitted assignments" OnClick="viewAndGrade_Click" />
            <br />
            <br />
            <asp:linkButton ID="viewFeedback" runat="server" Text="View Feedback on your courses" OnClick="viewFeedback_Click" />
            <br />
            <br />
            <asp:linkButton ID="issueCert" runat="server" Text="Issue a certificate" OnClick="issueCert_Click" />
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
