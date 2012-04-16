<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="AddressBook.UI.Contact" %>
<%@ Register TagPrefix="uca" TagName="AddressUC" Src="~/UserControls/UCAddress.ascx" %>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.DynamicControlsPlaceholder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add/Edit Contact</title>
    <script src="scripts/utilities.js" type="text/javascript"></script>

    <script type="text/javascript">

    </script>
    
    <link href="css/style.css" type="text/css" rel="Stylesheet" />
    <link href="css/extras.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server"> 
        </asp:ScriptManager>
        
         <div class="bPageTitle">
            <div class="ptBody secondaryPalette">
                <div class="content">
                    <img src="http://na3.salesforce.com/img/icon/form32.gif" alt="" class="pageTitleIcon" title="" />
                    <h1>Contact Address Book</h1>    
                </div>
            </div>
        </div>
       
        <div class="main">
            <div class="content">
                <div class="padded-div">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <p>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/resources/loadingAnim.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                    <p>
                                    </p>
                                    <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
                                </p>  
                             <div class="boldText">
                                <asp:Label ID="lblHeader" runat="server"></asp:Label>
                            </div>
                                                    
                            <asp:Panel ID="pnlEdit" runat="server">
                                 <div class="body" id="divUserAddress">
                                        <table class="modal-table" width="600px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFName" Text="First Name" runat="server" CssClass="boldText" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFName" runat="server" CssClass="textBox"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblLName" Text="Last Name" runat="server" CssClass="boldText" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLName" runat="server" CssClass="textBox"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEmail" Text="Email" runat="server" CssClass="boldText"/>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textBox"/>
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td>
                                                    <asp:Label ID="lblUserTags" Text="Tags" runat="server"  CssClass="boldText" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUserTags" runat="server" CssClass="textBox  tag-box" Width="200px"/>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="60%">
                                                    <asp:Button runat="server" Visible="false" id="btnNewAddressUC" CssClass="btn" Text="Add A Second Address" OnClick="btnNewAddressUC_Click" />
                                                </td>
                                                
                                            </tr>
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
                                                            <div id="dvDeleteAddr" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDeleteAddr" runat="server" Text="Delete Address" CssClass="boldText" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkDeleteAddr1" runat="server"/>
                                                                </td>
                                                            </tr>
                                                            </div>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                                                                    <td colspan="100%">
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td colspan="100%"><hr /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2"><asp:Label ID="Label1" runat="server" CssClass="address-header"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text="Street1" CssClass="boldText"/>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtStreet1_2" runat="server"  CssClass="textbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text="Street2" CssClass="boldText"/>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtStreet2_2" runat="server" CssClass="textbox"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text="City" CssClass="boldText" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCity_2" runat="server" CssClass="textbox"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text="State" CssClass="boldText" />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlState_2" runat="server" CssClass="dropdown">
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
                                                                    <asp:Label ID="Label6" runat="server" Text="Zip" CssClass="boldText" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtZip_2" runat="server" CssClass="textbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text="Addr Type" CssClass="boldText" />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlType_2" runat="server" CssClass="dropdown">
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
                                                                    <asp:Label ID="Label8" runat="server" Text="Tags" CssClass="boldText" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTags_2" runat="server" CssClass="textbox tag-box" Width="200px" />
                                                                </td>
                                                            </tr>
                                                            
                                                            <div id="dvDeleteAddr2" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDeleteAddr2" runat="server" Text="Delete Address" CssClass="boldText" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkDeleteAddr2" runat="server"/>
                                                                </td>
                                                            </tr>
                                                            </div>
                                                            
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CSSClass="btn" OnClick="btnSave_Click"></asp:Button>
                                                </td>
                                                <td>
                                                    <button id="btnCancel" class="btn" title="Cancel" value="Cancel" OnClick="javascript:window.close();">Cancel</button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                                             
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
       </div>
        
    </form>
</body>
</html>
