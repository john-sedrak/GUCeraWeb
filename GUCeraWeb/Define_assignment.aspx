<%@ Page Language="C#" AutoEventWireup="True" Inherits="GUCeraWeb.Define_assignment" CodeBehind="Define_assignment.aspx.cs" CodeFile="Define_assignment.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:Label ID="Label1" runat="server"></asp:Label><br />
    <asp:PlaceHolder ID="Placeholder1" runat="server">

    </asp:PlaceHolder>
    <form id="form1" runat="server" >
        <div>
            <br />
            Course ID:<br />
            <asp:DropDownList ID="cid" runat="server" DataSourceID="SqlDataSource1" DataTextField="id" DataValueField="id">
            </asp:DropDownList>
            &nbsp;(Choose an id from the above table)<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GUCera %>" SelectCommand="InstructorViewAcceptedCoursesByAdmin" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="instrId" SessionField="user" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            
            <br />
            <br />
            Assignment Type:
            <br />
            <asp:DropDownList ID="type" runat="server" Disabled ="true" >
                <asp:ListItem>Quiz</asp:ListItem>
                <asp:ListItem>Exam</asp:ListItem>
                <asp:ListItem>Project</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            Full Grade:<br />
            <asp:TextBox ID="fullGrade" runat="server" autocomplete="off" onkeypress="return (event.keyCode<=57 && event.keyCode>=48)" Disabled ="true" ></asp:TextBox>
            (Integer)
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" style="color:red" runat="server" ErrorMessage="Full Grade cannot be left empty" ControlToValidate="fullGrade"></asp:RequiredFieldValidator>
            <br />
            <br />
            Weight:<br />
            <asp:TextBox ID="weight" runat="server" AutoComplete="off" onkeypress="return CheckInputs()" Disabled ="true"></asp:TextBox>
            (Decimal number)
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" style="color:red" ErrorMessage="Weight cannot be left empty." ControlToValidate="weight"></asp:RequiredFieldValidator>
            &nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" style="color:red" ErrorMessage="Weight cannot be greater than 100." MinimumValue="0.0" MaximumValue="100.0" ControlToValidate="weight" Type="Double"></asp:RangeValidator>
            <br />
            <br />
            Deadline:<br />
            <asp:TextBox ID="deadline" runat="server" Disabled ="true"></asp:TextBox>
            (mm/dd/yyyy)
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" style="color:red" ErrorMessage="Deadline cannot be left empty." ControlToValidate="deadline"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" style="color:red" ErrorMessage="Format must be mm/dd/yyyy" ControlToValidate="deadline" ValidationExpression="[0-1][0-9][/][0-3][0-9][/][0-9][0-9][0-9][0-9]"></asp:RegularExpressionValidator>
            <br />
            <br />
            Time:<br />
            <asp:TextBox ID="time" runat="server" autocomplete="off" Disabled ="true" ></asp:TextBox>
            (hh:mm)&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Time cannot be left empty." style="color:red" ControlToValidate="time"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" style="color:red" ErrorMessage="Format must be hh:mm." ValidationExpression="[0-2][0-9][:][0-6][0-9]" ControlToValidate="time"></asp:RegularExpressionValidator>
            <br />
            <br />
            Content:<br />
            <asp:TextBox ID="content" runat="server" AutoComplete="off" onkeypress="return this.value.length<200" Disabled ="true" ></asp:TextBox>
            (Max 200 characters)
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Content cannot be left empty." style="color:red" ControlToValidate="content"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Define Assignment" OnClick="DefineAssignment" Disabled ="true"/>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="LinkButton1_Click">Back to main page</asp:LinkButton>

            
        </div>
    </form>
    <script>
        function CheckInputs() {

            var t1 = (event.keyCode <= 57 && event.keyCode >= 48);
            var t2 = event.keyCode == 46;
            var text = document.getElementById("<%=weight.ClientID%>");
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
