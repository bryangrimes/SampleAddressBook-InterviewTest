using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Services;
using Castle.Windsor;
using AddressBook.BLL;

namespace AddressBook.UI
{
    /*
     * Disclaimer:
     * 
     * Most of this page is a hack I have to admit.
     * I worked on dynamically adding a user control as needed but postback issues made it take just too long.
     * Also, I had issues reading back the controls in the controls in the placeholder control for the time I had.
     * 
     * I wouldn't ever do this in production code, it's a hack from the population of the textboxes
     * to the save which I think is frankyl hard to look at.
     *
     */

    public partial class Contact : System.Web.UI.Page
    {
        //private User _user;
     //   private readonly IAddressBookService _service;
       // public Contact(IContainerAccessor accessor, User user)
       // {
      //      _service = accessor.Container.Resolve<IAddressBookService>();
       // }

        private static IAddressBookService svcAdddress;
        public IContainerAccessor accessor;
        private int _userId;

        protected override void OnInit(EventArgs e)
        {
            // IContainerAccessor 
            accessor = Context.ApplicationInstance as IContainerAccessor;

            if (accessor == null || accessor.Container == null)
                throw new InvalidOperationException("Castle not configured properly");

            svcAdddress = accessor.Container.Resolve<IAddressBookService>();

            // get around dynamic control issues...set the controls HERE before the postback stuff
            // check below as to get into the viewstate in tact.
            // credit: http://codebetter.com/jefferypalermo/2004/11/25/key-to-ensuring-dynamic-asp-net-controls-save-viewstate-level-300/
            LoadAddressUC();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "close", "window.close();", true);

            if (Page.IsPostBack) return;

            _userId = Convert.ToInt32(Request.QueryString.Get("uId"));

            lblHeader.Text = "Add new contact";

            if (_userId > 0)
            {
                // get the user and popualte the data
                var user = svcAdddress.GetUser(_userId);
                if (user != null)
                {
                    txtFName.Text = user.FirstName;
                    txtLName.Text = user.FirstName;
                    txtEmail.Text = user.Email;

                    txtUserTags.Text = CreateTagString(user.Tags);

                    if(user.Addresses.Count>0)
                    {
                        txtStreet1.Text = user.Addresses[0].Street1;
                        txtStreet2.Text = user.Addresses[0].Street2;
                        txtCity.Text = user.Addresses[0].City;
                        ddlState.SelectedValue = user.Addresses[0].State;
                        txtZip.Text = user.Addresses[0].Zip;
                        ddlType.SelectedValue = user.Addresses[0].Type;
                        txtTags.Text = CreateTagString(user.Addresses[0].Tags);
                        
                        dvDeleteAddr.Visible = true;

                        if(user.Addresses.Count == 2)
                        {
                            txtStreet1_2.Text = user.Addresses[1].Street1;
                            txtStreet2_2.Text = user.Addresses[1].Street2;
                            txtCity_2.Text = user.Addresses[1].City;
                            ddlState_2.SelectedValue = user.Addresses[1].State;
                            txtZip_2.Text = user.Addresses[1].Zip;
                            ddlType_2.SelectedValue = user.Addresses[1].Type;
                            txtTags_2.Text = CreateTagString(user.Addresses[1].Tags);

                            dvDeleteAddr2.Visible = true;
                        }
                    }

                    lblHeader.Text = "Edit existing contact";
                    
                    // throw it in the session for use later.
                    Session["editUser"] = user;

                }
                else
                {
                    Session["editUser"] = null;
                }
            }
        }

        public string CreateTagString(IList<Tag> tags)
        {
            // spit out the tag names like "tag1 tag2 tag3"
            var sb = new StringBuilder();
            foreach (var tag in tags)
            {
                sb.Append(tag.Name + " ");
            }

            return sb.ToString();
        }

        private void LoadAddressUC()
        {
            //var uc = (UserControl)LoadControl("UserControls/ucAddress.ascx");
            //PropertyInfo ucControlId = uc.GetType().GetProperty("ControlId");
           // ucControlId.SetValue(uc, ucHolder.Controls.Count, null);
           // ucHolder.Controls.Add(uc);

            //Control uc2 = Page.LoadControl("UserControls/ucAddress.ascx");
           // uc2.ID = "uc" + DCP.Controls.Count;
          //  DCP.Controls.Add(uc2);
        }

        protected void btnNewAddressUC_Click(object sender, EventArgs e)
        {
            LoadAddressUC();
            btnNewAddressUC.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // new or edit?
            if(Session["editUser"] != null)
                EditUser();
            else
                AddNewUser();
        }

        private void AddNewUser()
        {
            var user = new User
            {
                FirstName = txtFName.Text,
                LastName = txtLName.Text,
                Email = txtEmail.Text,
                Addresses = new List<Address>()
            };

            // create user
            user = svcAdddress.CreateUser(user);

            // add tags
            user.Tags = SetTags(txtUserTags.Text, user.Id);

            // add addresses


            // since i'm lacking proper validation for this sample, 
            // only create where there is a street/city/state/zip (thus making them required)
            if (txtStreet1.Text != String.Empty && txtCity.Text != String.Empty & ddlState.SelectedValue != "null")
            {
                // add the address
                var addr = new Address
                {
                    Street1 = txtStreet1.Text,
                    Street2 = txtStreet2.Text,
                    City = txtCity.Text,
                    State = ddlState.SelectedValue,
                    Zip = txtZip.Text,
                    Type = ddlType.SelectedValue,
                    UserId = user.Id
                };

                user.Addresses.Add(addr);

                // add the address
                addr = svcAdddress.CreateAddress(addr);

                // set any address tags
                addr.Tags = SetTags(txtTags.Text, addr.Id);
                
            }

            if (txtStreet1_2.Text != String.Empty && txtCity_2.Text != String.Empty & ddlState_2.SelectedValue != "null")
            {
                // add the address
                var addr = new Address
                {
                    Street1 = txtStreet1_2.Text,
                    Street2 = txtStreet2_2.Text,
                    City = txtCity_2.Text,
                    State = ddlState_2.SelectedValue,
                    Zip = txtZip_2.Text,
                    Type = ddlType_2.SelectedValue,
                    UserId = user.Id
                };

                user.Addresses.Add(addr);



                addr.Tags = SetTags(txtTags_2.Text, addr.Id);
            }


            // any addresses here?
            //foreach(var control in FindControl("0"))
            //ucHolder.Controls)
            //{
            // since i'm lacking proper validation for this sample, 
            // only create where there is a street/city/state/zip (thus making them required)
            //     Control myControl1 = FindControl("TextBox2"); 
            // }


            //var textBox = ((TextBox)((UserControl)ucHolder.FindControl("ASP.usercontrols_ucaddress_ascx")).FindControl("txtStreet1"));
            //var streetVal = textBox.Text;

            svcAdddress.UpdateUser(user);

        }

        private void EditUser()
        {
            var user = (User)Session["editUser"];

            user.FirstName = txtFName.Text;
            user.LastName = txtLName.Text;
            user.Email = txtEmail.Text;

            // just overwrite the tags...
            if (txtUserTags.Text != String.Empty)
            {
                var tags = txtUserTags.Text.Split(' ');
                IList<Tag> newTags = tags.Select(tag => new Tag { Name = tag }).ToList();
                user.Tags = newTags;
            }

            user.Tags = SetTags(txtUserTags.Text, user.Id);

            // delete addresses from the user and readd if the delete cck == true
            //var udpatedUser = svcAdddress.UpdateUser(user);
            
            // add any addresses
            // since i'm lacking proper validation for this sample, 
            // only create where there is a street/city/state/zip (thus making them required)
            if (txtStreet1.Text != String.Empty && txtCity.Text != String.Empty & ddlState.SelectedValue != "null")
            {
                if(!chkDeleteAddr1.Checked)
                {
                    // add the address
                    var addr = new Address
                    {
                        Street1 = txtStreet1.Text,
                        Street2 = txtStreet2.Text,
                        City = txtCity.Text,
                        State = ddlState.SelectedValue,
                        Zip = txtZip.Text,
                        Type = ddlType.SelectedValue,
                        UserId = user.Id
                    };

                    user.Addresses.Add(addr);

                    addr.Tags = SetTags(txtTags.Text, addr.Id);
                }
            }

            if (txtStreet1_2.Text != String.Empty && txtCity_2.Text != String.Empty & ddlState_2.SelectedValue != "null")
            {
                if (!chkDeleteAddr2.Checked)
                {
                    // add the address
                    var addr = new Address
                                   {
                                       Street1 = txtStreet1_2.Text,
                                       Street2 = txtStreet2_2.Text,
                                       City = txtCity_2.Text,
                                       State = ddlState_2.SelectedValue,
                                       Zip = txtZip_2.Text,
                                       Type = ddlType_2.SelectedValue,
                                       UserId = user.Id
                                   };

                    user.Addresses.Add(addr);

                    addr.Tags = SetTags(txtTags_2.Text, addr.Id);
                }
            }

            var test = svcAdddress.UpdateUser(user);


            var allUsers = svcAdddress.GetAllUsers();
            Session["gridUsers"] = allUsers;
            //Default de = new Default();
            //de.FillGrid();
        }

        private IList<Tag>SetTags(string tagString, int id)
        {
            IList<Tag> newTags = new List<Tag>();
            if (tagString != String.Empty)
            {
                var tags = tagString.Split(' ');
                newTags = tags.Select(tag => new Tag { Name = tag, WhatId = id}).ToList();
            }
            return newTags;
        }
    }
}
;