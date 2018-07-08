<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WcfDemoWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h4>Simple WCF Book Service</h4>
<asp:Label ID="lblMsg" runat="server"></asp:Label>
<table>
    <tr>
        <td>
            Isbn :
        </td>
        <td>
            <asp:TextBox ID="txtIsbn" runat="server" Enabled="false" />
        </td>
    </tr>
    <tr>
        <td>
            Title :
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" style="width: 300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Author :
        </td>
        <td>
            <asp:TextBox ID="txtAuthor" runat="server" style="width: 300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Pages :
        </td>
        <td>
            <asp:TextBox ID="txtPages" runat="server" style="width: 300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Publisher :
        </td>
        <td>
            <asp:TextBox ID="txtPublisher" runat="server" style="width: 300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="ButtonInsert" runat="server" Text="Add" OnClick="InsertButton_Click"/>
            <asp:Button ID="ButtonUpdate" runat="server" visible="false" Text="Update" OnClick="InsertButton_Click"/>
            <asp:Button ID="ButtonDelete" runat="server" visible="false" Text="Delete" OnClick="DeleteButton_Click"/>
            <asp:Button ID="ButtonCancel" runat="server" visible="false" Text="Cancel" OnClick="CancelButton_Click"/>
        </td>
    </tr>
</table>
<asp:GridView ID="GridViewBookDetails" DataKeyNames="isbn" AutoGenerateColumns="false"
        runat="server" OnSelectedIndexChanged="GridViewBookDetails_SelectedIndexChanged" Width="700">
        <HeaderStyle BackColor="#0A9A9A" ForeColor="White" Font-Bold="true" Height="30" />
        <AlternatingRowStyle BackColor="#f5f5f5" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" Text="Select" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Author">
                <ItemTemplate>
                    <asp:Label ID="lblAuthor" runat="server" Text='<%#Eval("author") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pages">
                <ItemTemplate>
                    <asp:Label ID="lblPages" runat="server" Text='<%#Eval("pages") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Publisher">
                <ItemTemplate>
                    <asp:Label ID="lblPublisher" runat="server" Text='<%#Eval("publisher") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
