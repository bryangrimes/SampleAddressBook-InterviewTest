<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AddressBook.UI.Default" %>

<%@ Import Namespace="AddressBook.BLL.Entities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="ccl" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc" Namespace="AddressBook.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bryan Grimes: Sample</title>

    <script src="scripts/utilities.js" type="text/javascript"></script>

    <link href="css/style.css" type="text/css" rel="Stylesheet" />
    <link href="css/extras.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bPageTitle">
        <div class="ptBody secondaryPalette">
            <div class="content">
                <img src="http://na3.salesforce.com/img/icon/form32.gif" alt="" class="pageTitleIcon"
                    title="" />
                <h1>
                    Contact Address Book</h1>
            </div>
        </div>
    </div>
    <div id="main">
        <!-- Header -->
        <div class="header">
            <asp:Label runat="server" ID="lblHeaderTitle" CssClass="hdrLabel" Text="Sample NHibernate, Castle Windsor, OO Design"></asp:Label>
        </div>
        <!-- End Header -->
        <div class="content">
            <div>
                <div class="boldText">
                    Select user field, operator and value to filter User results.
                </div>
                <br />
                <p>
                    <asp:DropDownList ID="ddlField" runat="server" CssClass="dropDown">
                        <asp:ListItem Text="-- Select Field -- " Value=""></asp:ListItem>
                        <asp:ListItem Text="User.LastName" Value="LastName"></asp:ListItem>
                        <asp:ListItem Text="User.FirstName" Value="FirstName"></asp:ListItem>
                        <asp:ListItem Text="User.Email" Value="Email"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlOperator" runat="server" CssClass="dropDown">
                        <asp:ListItem Text="-- Select Operator -- " Value=""></asp:ListItem>
                        <asp:ListItem Text="Equals" Value="eq"></asp:ListItem>
                        <asp:ListItem Text="Not Equals" Value="ne"></asp:ListItem>
                        <asp:ListItem Text="Contains" Value="co"></asp:ListItem>
                        <asp:ListItem Text="Starts With" Value="sw"></asp:ListItem>
                        <asp:ListItem Text="Ends With" Value="ew"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" />
                    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click"
                        CssClass="btn" />
                        
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkClearFilters" runat="server" 
                        onclick="lnkClearFilters_Click" Text="Clear Filter Search" Visible="false" ></asp:LinkButton>
                </p>
            </div>
            <div id="dvcontactadd" class="boldText">
                Add Contact
                <!--<img src="/resources/add.png" id="imgAdd" alt="Add Contact" runat="server" />-->
                <a href="#" onclick="PopContactPage()">
                    <img src="/resources/add.png" alt="Add Contact" />
                </a>
            </div>
            <div align="center">
                <br />
                <asp:Button ID="btnRefreshAll" Text="Reload Users" CssClass="btn" runat="server" 
                    onclick="btnRefreshAll_Click" />
            </div>
            <div class="bordered-div">
                <table class="user-table" width="100%">
                    <tr>
                        <td>
                            <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
                            <asp:UpdatePanel ID="updatePanel" runat="server" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:GridView ID="grdUsers" runat="server" GridLines="None" AutoGenerateColumns="false"
                                        OnSelectedIndexChanged="Grd_SelectedIndexChanged" OnRowDataBound="Grd_RowDataBound"
                                        DataKeyNames="Id">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text="<%# ((User)Container.DataItem).Id %>"
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="user-grid-image-header" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <a href="javascript:switchViews('div<%# ((User)Container.DataItem).Id %>');">
                                                        <img src="resources/right.png" id="imgdiv<%# ((User)Container.DataItem).Id %>" title="Show addresses"
                                                            alt="Show addresses" border="0" />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200px" HeaderText="First Name" HeaderStyle-CssClass="user-grid-header">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstName" CssClass="user-grid-item" runat="server" Text="<%# ((User)Container.DataItem).FirstName %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200px" HeaderText="Last Name" HeaderStyle-CssClass="user-grid-header">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastName" CssClass="user-grid-item" runat="server" Text="<%# ((User)Container.DataItem).LastName %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="250px" HeaderText="Email" HeaderStyle-CssClass="user-grid-header">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" CssClass="user-grid-item" runat="server" Text="<%# ((User)Container.DataItem).Email %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Width="250px" HeaderText="Tags" HeaderStyle-CssClass="user-grid-header">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTags" CssClass="user-grid-item" runat="server" Text="<%# CreateTagString(((User)Container.DataItem).Tags) %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Action" HeaderStyle-CssClass="user-grid-header">
                                                <ItemTemplate>
                                                    <a href="#" onclick="PopContactPage(<%# ((User)Container.DataItem).Id %>)">edit </a>&nbsp;|&nbsp;
                                                    <a href="javascript:deleteUser(<%# ((User)Container.DataItem).Id %>);">delete </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="0px">
                                                <ItemTemplate>
                                                    </td></tr>
                                                    <tr>
                                                        <td colspan="100%">
                                                            <div id="div<%# ((User)Container.DataItem).Id %>" style="display: none; position: relative;
                                                                left: 25px;">
                                                                <asp:GridView ID="grdInnerGridView" runat="server" Width="80%" AutoGenerateColumns="false"
                                                                    DataKeyNames="UserId" EmptyDataText="No addresses" GridLines="Both" OnRowDataBound="Grd_InnerRowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Width="100px" HeaderText="Fav" HeaderStyle-CssClass="address-grid-header">
                                                                            <ItemTemplate>
                                                                                 <a href="javascript:markfav('<%# ((Address)Container.DataItem).Id %>', '<%# ((Address)Container.DataItem).UserId %>');"> 
                                                                                        <img src="<%# RenderFavImage(((Address)Container.DataItem).Favorite) %>" id="imgfav<%# ((Address)Container.DataItem).Id %>" title="mark as fav" alt="mark as fav" border="0" />
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="300px" HeaderText="Street 1" HeaderStyle-CssClass="address-grid-header">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStreet1" runat="server" Text="<%# ((Address)Container.DataItem).Street1 %>"
                                                                                    CssClass="address-grid-item" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="300px" HeaderText="Street 2" HeaderStyle-CssClass="address-grid-header">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStreet2" runat="server" Text="<%# ((Address)Container.DataItem).Street2 %>"
                                                                                    CssClass="address-grid-item" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="350px" HeaderText="City" HeaderStyle-CssClass="address-grid-header">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCity" runat="server" Text="<%# ((Address)Container.DataItem).City %>"
                                                                                    CssClass="address-grid-item" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="60px" HeaderText="State" HeaderStyle-CssClass="address-grid-header">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblState" runat="server" Text="<%# ((Address)Container.DataItem).State %>"
                                                                                    CssClass="address-grid-item" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="120px" HeaderText="Zip" HeaderStyle-CssClass="address-grid-header">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblZip" runat="server" Text="<%# ((Address)Container.DataItem).Zip %>"
                                                                                    CssClass="address-grid-item" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="500px" HeaderText="Tags" HeaderStyle-CssClass="address-grid-header">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTags" runat="server" Text="<%# CreateTagString(((Address)Container.DataItem).Tags)%>"
                                                                                    CssClass="address-grid-item address-grid-item-italics" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Style="display: none" />
                                </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                               </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="contactadd2" class="boldText" style="display:none;">
                <br />
                Add Contact
                <img src="~/resources/add.png" id="imgAdd2" alt="Add Contact" runat="server" />
            </div>
            <div class="bordered-div">
                <span class="boldText"> Tag Search: </span>
                enter tags separated by a space search the grid's results even finer
                <br />
                <asp:TextBox runat="server" ID="txtTagSearch" CssClass="textBox tag-box" Width="300px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnTagFilter" runat="server" Text="Search Tags" CssClass="btn" 
                    onclick="btnTagFilter_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkClearTagFilter" runat="server" 
                    onclick="lnkClearTagFilter_Click" Text="Clear Tag Search" Visible="false"></asp:LinkButton>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
