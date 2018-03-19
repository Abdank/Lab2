<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Labaratorinis2.aspx.cs" Inherits="Labaratorinis2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .newStyle1 {
            font-style: italic;
        }
        .newStyle2 {
            font-style: italic;
        }
        .newStyle3 {
            position: fixed;
            top: 43px;
            left: 660px;
            height: 343px;
            width: 399px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text=" Atrinktų studentų sąrašas"></asp:Label>
        <br />
        <br />
        <asp:Table ID="Table1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" style="width: 30px">
        </asp:Table>
        <br />
        <asp:Label ID="Label2" runat="server" Text=" Atrinkti studentus pagal grupę"></asp:Label>
        <asp:Image ID="Image1" runat="server" CssClass="newStyle3" ImageUrl="https://media.giphy.com/media/X8omQqfFyeq1a/giphy.gif" />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ieškoti" />
        <br />
        <br />
        <asp:Table ID="Table2" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both">
        </asp:Table>
        <br />
        <asp:Label ID="Label3" runat="server" Text="<b> Pradiniai duomenys :</b>"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="U14a.txt failas :" CssClass="newStyle1"></asp:Label>
        <br />
        <br />
        <asp:Table ID="Table3" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both">
        </asp:Table>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="U14b.txt failas :" CssClass="newStyle2"></asp:Label>
        <br />
        <br />
        <asp:Table ID="Table5" runat="server">
        </asp:Table>
        <br />
        <br />
        <asp:Table ID="Table4" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both">
        </asp:Table>
    </form>
</body>
</html>
