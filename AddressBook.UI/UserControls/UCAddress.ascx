<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddress.ascx.cs" Inherits="AddressBook.UI.UserControls.UCAddress" %>

    <link rel="Stylesheet" media="screen" type="text/css" href="<%=ResolveUrl("~/css/style.css") %>" /> 
    <link rel="Stylesheet" media="screen" type="text/css" href="<%=ResolveUrl("~/css/extras.css") %>" /> 
    
   
    <tr>
        <td colspan="100%">
            <div>
                <table>
                    <tr>
                        <td colspan="100%"><hr /></td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Label ID="lblAddressHeader" runat="server" CssClass="address-header"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStreet1" runat="server" Text="Street1" CssClass="boldText"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStreet1" runat="server"  CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStreet2" runat="server" Text="Street2" CssClass="boldText"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStreet2" runat="server" CssClass="textbox"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCity" runat="server" Text="City" CssClass="boldText" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="textbox"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblState" runat="server" Text="State" CssClass="boldText" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdown">
                                <asp:ListItem Value="NULL">-- Please Select --</asp:ListItem>
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
                            <asp:Label ID="lblZip" runat="server" Text="Zip" CssClass="boldText" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtZip" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblType" runat="server" Text="Addr Type" CssClass="boldText" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="dropdown">
                                <asp:ListItem Text="-- Please Select --" Value="null" />
                                <asp:ListItem Text="Residential" Value="Residential" />
                                <asp:ListItem Text="Business" Value="Business" />
                                <asp:ListItem Text="Church" Value="Church" />
                                <asp:ListItem Text="Governmental" Value="Governmental" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTags" runat="server" Text="Tags" CssClass="boldText" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtTags" runat="server" CssClass="textbox tag-box" Width="200px" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>

