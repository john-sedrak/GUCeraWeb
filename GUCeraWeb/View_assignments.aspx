<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_assignments.aspx.cs" Inherits="GUCeraWeb.View_assignments" %>

<!DOCTYPE html>
<script runat="server">

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
</script>


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
            <asp:Button ID="Button3" runat="server" OnClick="ViewAssignments" Text="View Assignments" />
            <br />
            <br />
            <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
            <br />
            
        </div>
        <div>

            <br />
            Assignment Type:<br />
            <asp:DropDownList ID="type" runat="server" DataSourceID="TypeData" DataTextField="assignmenttype" DataValueField="assignmenttype" Disabled="true" AutoPostBack="True" OnSelectedIndexChanged="ViewAssignments">
            </asp:DropDownList>
            <asp:SqlDataSource ID="TypeData" runat="server" ConnectionString="<%$ ConnectionStrings:GUCera %>" SelectCommand="SELECT DISTINCT s.assignmenttype FROM Course AS c INNER JOIN StudentTakeAssignment AS s ON s.cid = c.id WHERE (s.cid = @cid) AND (c.instructorId = @instrId)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cid" Name="cid" PropertyName="Text" Type="Int32" />
                    <asp:SessionParameter Name="instrId" SessionField="user" Type="Int32" DefaultValue="-1"/>
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
            Assignment Number:<br />
            <asp:DropDownList ID="number" runat="server" DataSourceID="NumberData" DataTextField="assignmentNumber" DataValueField="assignmentNumber" Disabled="true" AutoPostBack="True" OnSelectedIndexChanged="ViewAssignments">
            </asp:DropDownList>
            <br />
            <asp:SqlDataSource ID="NumberData" runat="server" ConnectionString="<%$ ConnectionStrings:GUCera %>" SelectCommand="SELECT DISTINCT s.assignmentNumber FROM Course AS c INNER JOIN StudentTakeAssignment AS s ON s.cid = c.id WHERE (s.cid = @cid) AND (c.instructorId = @instrId) AND (s.assignmenttype = @type)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cid" Name="cid" PropertyName="SelectedValue" />
                    <asp:SessionParameter Name="instrId" SessionField="user" DefaultValue="-1"/>
                    <asp:ControlParameter ControlID="type" DefaultValue="&quot;&quot;" Name="type" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
            Student ID:<br />
            <asp:DropDownList ID="sid" runat="server" DataSourceID="SidData" DataTextField="sid" DataValueField="sid" disabled="true" AutoPostBack="True" OnSelectedIndexChanged="ViewAssignments">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SidData" runat="server" ConnectionString="<%$ ConnectionStrings:GUCera %>" SelectCommand="SELECT DISTINCT s.sid FROM Course AS c INNER JOIN StudentTakeAssignment AS s ON s.cid = c.id WHERE  (s.cid = @cid) AND (c.instructorId = @instrId) AND (s.assignmenttype = @type) AND (s.assignmentnumber = @number)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cid" Name="cid" PropertyName="SelectedValue" />
                    <asp:SessionParameter Name="instrId" SessionField="user" DefaultValue="-1"/>
                    <asp:ControlParameter ControlID="type" DefaultValue="&quot;&quot;" Name="type" PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="number" DefaultValue="-1" Name="number" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
            Grade:<br />
            <asp:TextBox ID="grade" runat="server" onkeypress="return CheckInputs()" autocomplete="off" Disabled="true"></asp:TextBox>
            (Decimal)
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Grade Assignment" Disabled="true" OnClick="GradeAssignment"/>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back to main page</asp:LinkButton>
        </div>
    </form>
    <script>
        function CheckInputs() {

            var t1 = (event.keyCode <= 57 && event.keyCode >= 48);
            var t2 = event.keyCode == 46;
            var text = document.getElementById("<%=grade.ClientID%>");
            var i;

            if (t2) {
                for (i = 0; i < text.value.length - 1; i++) {
                    if (text.value.charAt(i) == '.') {
                        return false;
                    }
                }
            }
            return t1 || t2;
        }
    </script>
</body>
</html>
