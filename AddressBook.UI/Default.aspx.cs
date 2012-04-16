using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Services;
using Castle.Windsor;

namespace AddressBook.UI
{
    public partial class Default : System.Web.UI.Page
    {
       private static IAddressBookService svcAdddress;
       public IContainerAccessor accessor;

        protected override void  OnInit(EventArgs e)
        {
           // IContainerAccessor 
            accessor = Context.ApplicationInstance as IContainerAccessor;

            if (accessor == null || accessor.Container == null)
                throw new InvalidOperationException("Castle not configured properly");

            svcAdddress = accessor.Container.Resolve<IAddressBookService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.Button1);

           // gvUsers.DataSource = users;
           // gvUsers.DataBind();
            if (!Page.IsPostBack)
            {
                FillGrid();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        public void FillGrid()
        {
            
                /*
                var tags = new List<Tag>
                               {
                                   new Tag {Id = 1, Name = "test tag", WhatId = 1},
                                   new Tag {Id = 2, Name = "test tag", WhatId = 1}
                               };

                var address = new List<Address>
                                  {
                                      new Address
                                          {
                                              City = "test",
                                              State = "PA",
                                              Street1 = "1234 Test St",
                                              Street2 = "Suite 100",
                                              Id = 1,
                                              Type = "Business",
                                              Zip = "19147",
                                              Tags = tags
                                          },
                                      new Address
                                          {
                                              City = "test2",
                                              State = "PA",
                                              Street1 = "ghdcfghdfghdfg",
                                              Street2 = "Suite 100",
                                              Zip = "19147",
                                              Id = 2,
                                              Type = "dfghdfghdfgh",
                                              Tags = tags
                                          }
                                  };
                */
                var users = svcAdddress.GetAllUsers();
                Session["gridUsers"] = users;

                /*users.Add(new User
                              {
                                  FirstName = "Bryan",
                                  LastName = "Grimes",
                                  Email = "bryan@bryangrimes.com",
                                  Id = 1,
                                  Addresses = address,
                                  Tags = tags
                              });
                users.Add(new User
                {
                    FirstName = "Bryan2",
                    LastName = "Grimes2",
                    Email = "bryan@bryangrimes.com",
                    Id = 2,
                    Addresses = address,
                    Tags = tags
                });*/


                grdUsers.DataSource = users;
                grdUsers.DataBind();

      //          LoadAddressUC();
           
        }

        protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }       

        protected void Grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var user = (User)e.Row.DataItem;
                var innerGridView = (GridView)e.Row.FindControl("grdInnerGridView");
                FillInnerGrid(user, innerGridView);   
            }
            
        }

        private static void FillInnerGrid(User user, GridView grdInnerGridView)
        {
            grdInnerGridView.DataSource = user.Addresses;
            grdInnerGridView.DataBind();
        }

        protected void Grd_InnerRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // tags are are a list prop on the address, throw all tags into a string for display.
           /* if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string tagText = "";

                var addr = (Address)e.Row.DataItem;
                if(addr.Tags.Count > 0)
                {
                    tagText = addr.Tags.Aggregate(tagText, (current, t) => current + (t.Name + " "));
                }

                addr.TagString = tagText;
            }*/
        }

        public string RenderFavImage(bool fav)
        {
            return fav ? "/resources/fav_star.png" : "/resources/unfav_star.png";
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

        [WebMethod]
        public static void DeleteUser(int id)
        {
            svcAdddress.DeleteUser(svcAdddress.GetUser(id));
        }

        public void DeleteUserPostBack(int id)//(User user)
        {
            svcAdddress.DeleteUser(svcAdddress.GetUser(id));
            FillGrid();
        }

        [WebMethod]
        public static void MarkAddressAsFavorite(int addrId, int userId)
        {
            User user = svcAdddress.GetUser(userId);
            var addr = user.Addresses.Where(x => x.Id == addrId).First();

            addr.Favorite = !addr.Favorite;

            svcAdddress.UpdateUser(user);
        }

        public void MarkAsFavoritePostBack(int addrId, int userId)
        {
            User user = svcAdddress.GetUser(userId);
            var addr = user.Addresses.Where(x => x.Id == addrId).First();

            addr.Favorite = !addr.Favorite;

            svcAdddress.UpdateUser(user);
            FillGrid();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            IList<User> users;

            // get all and the use linq to filter.
            if (ddlOperator.SelectedIndex > 0 && ddlField.SelectedIndex > 0)
                users = svcAdddress.GetUsersByFilter(ddlField.SelectedValue, ddlOperator.SelectedValue, txtValue.Text);
            else
                users = svcAdddress.GetAllUsers();

            Session["filteredUsers"] = users;
            grdUsers.DataSource = users;
            grdUsers.DataBind();

            lnkClearFilters.Visible = true;

        }

        protected void btnTagFilter_Click(object sender, EventArgs e)
        {
            if(txtTagSearch.Text != String.Empty)
            {
                // filter the Users with the tags entered and rebind the grid.
                var users = (List<User>)Session["gridUsers"];

                string[] tags = txtTagSearch.Text.Split(' ');

                // for each tag entered, look for results to add to the return list...thanks to LINQ
                IList<User> taggedUsers = tags.SelectMany(t1 => (from u in users where u.Tags.Any(ol => ol.Name == t1) select u)).ToList();

                Session["taggedUsers"] = taggedUsers;
                grdUsers.DataSource = taggedUsers;
                grdUsers.DataBind();

                lnkClearTagFilter.Visible = true;
            }
        }

        protected void lnkClearTagFilter_Click(object sender, EventArgs e)
        {
            txtTagSearch.Text = "";
            lnkClearTagFilter.Visible = false;

            FillGrid();
        }

        protected void lnkClearFilters_Click(object sender, EventArgs e)
        {
            ddlOperator.SelectedIndex = 0;
            ddlField.SelectedIndex = 0;
            txtValue.Text = "";
            lnkClearFilters.Visible = false;

            FillGrid();
        }
        
        protected void btnRefreshAll_Click(object sender, EventArgs e)
        {
            var users = svcAdddress.GetAllUsers();
            Session["gridUsers"] = users;
            grdUsers.DataSource = users;
            grdUsers.DataBind();
        }
    }
    
}
