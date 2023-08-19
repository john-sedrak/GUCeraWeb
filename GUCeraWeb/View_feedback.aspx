<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_feedback.aspx.cs" Inherits="GUCeraWeb.View_feedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:PlaceHolder ID="Placeholder1" runat="server">

    </asp:PlaceHolder>
    <form id="form1" runat="server">
        <div>
            <br />
            Course ID:<br />
            <asp:DropDownList ID="cid" runat="server" DataSourceID="CidData" DataTextField="id" DataValueField="id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="CidData" runat="server" ConnectionString="<%$ ConnectionStrings:GUCera %>" SelectCommand="InstructorViewAcceptedCoursesByAdmin" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter Name="instrId" SessionField="user" Type="Int32" DefaultValue="-1"/>
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="View Feedback" OnClick="ViewFeedback" />
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back to main page</asp:LinkButton>
            <br />
            <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
