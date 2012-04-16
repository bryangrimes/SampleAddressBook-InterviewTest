using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Services;
using System.Runtime.Serialization.Json;
using AjaxControlToolkit;
using Castle.Windsor;

namespace AddressBook.UI
{
    public partial class BookIndex : System.Web.UI.Page
    {
        private static IAddressBookService svcAdddress;

        protected override void OnInit(EventArgs e)
        {
            svcAdddress = Master.accessor.Container.Resolve<IAddressBookService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
           // gvUsers.DataSource = users;
           // gvUsers.DataBind();
            FillGrid();
        }

        private void FillGrid()
        {
            if (!Page.IsPostBack)
            {

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

                var users = svcAdddress.GetAllUsers();
                users.Add(new User
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
                });


                grdUsers.DataSource = users;
                grdUsers.DataBind();
            }
        }

        protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*GridViewRow row = ((GridView)sender).SelectedRow;

            if (row == null) return;

            ModalPopupExtender extender = row.FindControl("extAddress") as ModalPopupExtender;

            if (extender != null)
                extender.Show();
            *
             * 
             */
            GridViewRow row = ((GridView) sender).SelectedRow;
            var user = (User)row.DataItem;
            dvUser.DataSource = user;
            dvUser.DataBind();
        }
        
        protected void btnSave_Click(object sender, EventArgs args)
        {
            /*dvUser.UpdateItem(false);
            dvUser.Visible = false;

            mdlPop.Hide();

            grdUsers.DataBind();
            updatePanel.Update();*/
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

       // #region Web Methods
        //[WebMethod]
        public static User CreateUser(User obj)
        {
            return svcAdddress.CreateUser(obj);
        }

        //[WebMethod]
        public static void DeleteUser(User obj)
        {
            svcAdddress.DeleteUser(obj);
        }

        //[WebMethod]
        public static User UpdateUser(User obj)
        {
            return svcAdddress.UpdateUser(obj);
        }

        [WebMethod]
        public static void MarkAddressAsFavorite(int id)
        {
            svcAdddress.MarkAddressFavorite(id);
            // reload grid

        }

        [WebMethod]
        public static void RemoveAddressAsFavorite(int id)
        {
            svcAdddress.MarkAddressNotFavorite(id);
            // reload grid
        }

       // #endregion

        #region Utils
        private static string SerializeObjectIntoJson(Object o)
        {
            var serializer = new DataContractJsonSerializer(o.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, o);
                ms.Flush();
                byte[] bytes = ms.GetBuffer();
                string jsonString = Encoding.UTF8.GetString(bytes, 0, bytes.Length).Trim('\0');
                return jsonString;
            }
        }
        #endregion

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}
