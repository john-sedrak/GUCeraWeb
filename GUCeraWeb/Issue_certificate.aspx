<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Issue_certificate.aspx.cs" Inherits="GUCeraWeb.Issue_certificate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" ></asp:Label>
        <br />
    If a student is not in this list for a certain course, it is either because they don&#39;t take the course, or they have not received a final grade above 2.0 for said course. The student may also have already received a certificate in this course.<br />
    <asp:PlaceHolder ID ="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <div>
            <br />
            Course ID:<br />
            <asp:DropDownList ID="cid" runat="server" DataSourceID="CourseData" DataTextField="cid" DataValueField="cid" AutoPostBack="True">
            </asp:DropDownList>
            <asp:SqlDataSource ID="CourseData" runat="server" ConnectionString="<%$ ConnectionStrings:GUCera %>" SelectCommand="SELECT DISTINCT cid FROM (SELECT [cid], sid FROM [StudentTakeCourse] WHERE ([grade]&gt;2.0) AND ([insid]=@insId) EXCEPT SELECT s.cid, s.sid FROM [StudentTakeCourse] AS s INNER JOIN [StudentCertifyCourse] AS sc ON  (s.sid = sc.sid) WHERE ([grade]&gt;2.0) AND ([insid]=@insId) AND (sc.cid = s.cid)) as tmp">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="insId" SessionField="user" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
            Student ID:<br />
            <asp:DropDownList ID="sid" runat="server" DataSourceID="StudentData" DataTextField="sid" DataValueField="sid" AutoPostBack="True">
            </asp:DropDownList>
            <asp:SqlDataSource ID="StudentData" runat="server" ConnectionString="<%$ ConnectionStrings:GUCera %>" SelectCommand="SELECT [cid], sid FROM [StudentTakeCourse] WHERE ([grade]&gt;2.0) AND ([insid]=@insId) AND (cid = @cid) EXCEPT SELECT s.cid, s.sid FROM [StudentTakeCourse] AS s INNER JOIN [StudentCertifyCourse] AS sc ON  (s.sid = sc.sid) WHERE ([grade]&gt;2.0) AND ([insid]=@insId) AND (sc.cid = s.cid) AND (s.cid = @cid)">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="insId" SessionField="user" />
                    <asp:ControlParameter ControlID="cid" DefaultValue="-1" Name="cid" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Issue Certificate" OnClick="Button1_Click"/>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back to main page</asp:LinkButton>
            <br />
        </div>
    </form>
</body>
</html>
