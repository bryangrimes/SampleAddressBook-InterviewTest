<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BookIndex.aspx.cs" Inherits="AddressBook.UI.BookIndex" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Import Namespace="AddressBook.BLL.Entities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="ccl" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc" Namespace="AddressBook.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

    </script>

    <style type="text/css">
        body
        {
            font-family: Verdana;
        }
        /* modal syles*/.modal-inner-wrapper
        {
            width: 500px;
            height: 500px;
            background-color: Gray;
        }
        .modal-inner-wrapper .content
        {
            width: 500px;
            height: 500px;
            background-color: #FFFFFF;
            border: solid 1px Gray;
            z-index: 9999;
            float: none;
            margin-top: 10px;
            margin-right: 10px;
        }
        .modal-inner-wrapper .content .close
        {
            float: right;
        }
        .modal-inner-wrapper .content .body
        {
            margin-top: 20px;
        }
        .rounded-corners
        {
            /*FOR OTHER MAJOR BROWSERS*/
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px; /*FOR IE*/
            behavior: url(border-radius.htc);
        }
        .rel
        {
            position: relative;
            z-index: inherit;
            zoom: 1; /* For IE6 */
        }
        .modal-bg
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.6;
            z-index: 999;
        }
        .modal
        {
            position: absolute;
        }
        .modal-table
        {
            border-style: solid;
            border-color: Gray;
            border-width: 1px 1px 1px 1px;
            width: 80%;
            text-align: left;
        }
        .modal-table-inner
        { .modal-table;}/* end modal syles*/
        .labelTags
        {
            text-align: center;
            font-size: small;
            font-style: italic;
        }
        .user-table
        {
            padding: 10px;
            width: 100%;
            border-width: 1px 1px 1px 1px;
        }
        .user-grid-header
        {
            font-size: medium;
            text-align: left;
        }
        .user-grid-item
        {
            font-size: medium;
            text-align: left;
        }
        .user-grid-image-header
        {
            width: 25px;
            text-align: left;
        }
        .address-grid-header
        {
            font-size: small;
            text-align: center;
        }
        .address-grid-item
        {
            font-size: small;
            text-align: center;
        }
        .address-grid-item-italics
        {
            font-style: italic;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Contacts</h3>
    <div id="contactadd">
        Add Contact
        <img src="~/resources/add.png" id="imgAdd" alt="Add Contact" runat="server" />
    </div>
    <div>
    </div>
    <div>
        <table class="user-table">
            <tr>
                <td>
                    <asp:ScriptManager ID="ScriptManger1" runat="Server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdUsers" runat="server" GridLines="None" AutoGenerateColumns="false"
                                OnSelectedIndexChanged="Grd_SelectedIndexChanged" OnRowDataBound="Grd_RowDataBound"
                                DataKeyNames="Id">
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="user-grid-image-header">
                                        <ItemTemplate>
                                            <a href="javascript:switchViews('div<%# ((User)Container.DataItem).Id %>');">
                                                <img src="resources/right.png" id="imgdiv<%# ((User)Container.DataItem).Id %>" title="Show addresses"
                                                    alt="Show addresses" border="0" />
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="150px" HeaderText="First Name" HeaderStyle-CssClass="user-grid-header">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFirstName" CssClass="user-grid-item" runat="server" Text="<%# ((User)Container.DataItem).FirstName %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="150px" HeaderText="Last Name" HeaderStyle-CssClass="user-grid-header">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastName" CssClass="user-grid-item" runat="server" Text="<%# ((User)Container.DataItem).LastName %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="200px" HeaderText="Email" HeaderStyle-CssClass="user-grid-header">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" CssClass="user-grid-item" runat="server" Text="<%# ((User)Container.DataItem).Email %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnViewDetails" runat="server" Text="edit" CommandName="Select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="0px">
                                        <ItemTemplate>
                                            </td></tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div<%# ((User)Container.DataItem).Id %>" style="display: inline; position: relative;
                                                        left: 25px;">
                                                        <asp:GridView ID="grdInnerGridView" runat="server" Width="80%" AutoGenerateColumns="false"
                                                            DataKeyNames="UserId" EmptyDataText="No addresses" GridLines="Both">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Fav" HeaderStyle-CssClass="address-grid-header">
                                                                    <ItemTemplate>
                                                                        <a href="javascript:markfav('<%# ((Address)Container.DataItem).Id %>', '<%# ((Address)Container.DataItem).Favorite %>');">
                                                                            <img src="resources/unfav_star.png" id="imgdiv<%# ((Address)Container.DataItem).Id %>"
                                                                                title="mark as fav" alt="mark as fav" border="0" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Street 1" HeaderStyle-CssClass="address-grid-header">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStreet1" runat="server" Text="<%# ((Address)Container.DataItem).Street1 %>"
                                                                            CssClass="address-grid-item" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Street 2" HeaderStyle-CssClass="address-grid-header">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStreet2" runat="server" Text="<%# ((Address)Container.DataItem).Street2 %>"
                                                                            CssClass="address-grid-item" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="City" HeaderStyle-CssClass="address-grid-header">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCity" runat="server" Text="<%# ((Address)Container.DataItem).City %>"
                                                                            CssClass="address-grid-item" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="State" HeaderStyle-CssClass="address-grid-header">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblState" runat="server" Text="<%# ((Address)Container.DataItem).State %>"
                                                                            CssClass="address-grid-item" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Zip" HeaderStyle-CssClass="address-grid-header">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblZip" runat="server" Text="<%# ((Address)Container.DataItem).Zip %>"
                                                                            CssClass="address-grid-item" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Tags" HeaderStyle-CssClass="address-grid-header">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTags" runat="server" Text="<%# ((Address)Container.DataItem).Zip %>"
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <!-- edit user -->
                    <asp:Panel ID="pnlPopup" runat="server" Width="500px" Style="display: none;">
                        <asp:UpdatePanel ID="updPnlUserDetail" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnShowPopup" runat="server" Style="display: none;" />
                                <asp:ModalPopupExtender ID="mdlPop" runat="server" BackgroundCssClass="modal-bg"
                                    TargetControlID="btnShowPopup" PopupControlID="pnlPopup" CancelControlID="btnClose">
                                </asp:ModalPopupExtender>
                                <asp:DetailsView ID="dvUser" runat="server" GridLines="None" DefaultMode="Edit" AutoGenerateRows="false"
                                    Visible="false" Width="100%">
                                    <Fields>
                                        <asp:BoundField HeaderText="FirstName" DataField="FirstName" />
                                        <asp:BoundField HeaderText="LastName" DataField="LastName" />
                                        <asp:BoundField HeaderText="Email" DataField="Email" />
                                    </Fields>
                                </asp:DetailsView>
                                <div>
                                    <asp:LinkButton ID="btnUpdateSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnUpdateClose" runat="server" Text="Close"></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    
                    <!-- new user -->
                    
                    <div id="contactaddder">
                        <asp:Panel ID="pnlModal" runat="server" CssClass="modal">
                            <div class="rel">
                                <div class="modal-inner-wrapper rounded-corners">
                                    <div class="content rounded-corners">
                                        <div class="close">
                                            <asp:LinkButton ID="btnClose" runat="server">close</asp:LinkButton>
                                        </div>
                                        <div class="body">
                                            <table class="modal-table">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFName" Text="First Name" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFName" runat="server" Text=" " />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblLName" Text="Last Name" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLName" runat="server" Text=" " />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" Text="Email" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmail" runat="server" Text=" " />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="100%">
                                                        <table class="modal-table">
                                                            <caption title="New address">
                                                                New Address</caption>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblStreet1" runat="server" Text="Street1" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tctStreet1" runat="server" Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text="Street1" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox1" runat="server" Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCity" runat="server" Text="City" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCity" runat="server" Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblState" runat="server" Text="State" />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlState" runat="server">
                                                                        <asp:ListItem Value="AL">Alabama</asp:ListItem>
                                                                        <asp:ListItem Value="AK">Alaska</asp:ListItem>
                                                                        <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                                                                        <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                                                                        <asp:ListItem Value="CA">California</asp:ListItem>
                                                                        <asp:ListItem Value="CO">Colorado</asp:ListItem>
                                                                        <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                                                                        <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                                                                        <asp:ListItem Value="DE">Delaware</asp:ListItem>
                                                                        <asp:ListItem Value="FL">Florida</asp:ListItem>
                                                                        <asp:ListItem Value="GA">Georgia</asp:ListItem>
                                                                        <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                                                                        <asp:ListItem Value="ID">Idaho</asp:ListItem>
                                                                        <asp:ListItem Value="IL">Illinois</asp:ListItem>
                                                                        <asp:ListItem Value="IN">Indiana</asp:ListItem>
                                                                        <asp:ListItem Value="IA">Iowa</asp:ListItem>
                                                                        <asp:ListItem Value="KS">Kansas</asp:ListItem>
                                                                        <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                                                                        <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                                                                        <asp:ListItem Value="ME">Maine</asp:ListItem>
                                                                        <asp:ListItem Value="MD">Maryland</asp:ListItem>
                                                                        <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                                                                        <asp:ListItem Value="MI">Michigan</asp:ListItem>
                                                                        <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                                                                        <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                                                                        <asp:ListItem Value="MO">Missouri</asp:ListItem>
                                                                        <asp:ListItem Value="MT">Montana</asp:ListItem>
                                                                        <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                                                                        <asp:ListItem Value="NV">Nevada</asp:ListItem>
                                                                        <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                                                                        <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                                                                        <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                                                                        <asp:ListItem Value="NY">New York</asp:ListItem>
                                                                        <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                                                                        <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                                                                        <asp:ListItem Value="OH">Ohio</asp:ListItem>
                                                                        <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                                                                        <asp:ListItem Value="OR">Oregon</asp:ListItem>
                                                                        <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                                                                        <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                                                                        <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                                                                        <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                                                                        <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                                                                        <asp:ListItem Value="TX">Texas</asp:ListItem>
                                                                        <asp:ListItem Value="UT">Utah</asp:ListItem>
                                                                        <asp:ListItem Value="VT">Vermont</asp:ListItem>
                                                                        <asp:ListItem Value="VA">Virginia</asp:ListItem>
                                                                        <asp:ListItem Value="WA">Washington</asp:ListItem>
                                                                        <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                                                                        <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                                                                        <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblZip" runat="server" Text="Zip" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtZip" runat="server" Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblType" runat="server" Text="Addr Type" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtType" runat="server" Text=" " />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Close" OnClientClick="return Hidepopup()"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modal-bg"
                            TargetControlID="imgAdd" PopupControlID="pnlModal" CancelControlID="btnClose">
                        </asp:ModalPopupExtender>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
