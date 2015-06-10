using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class CustomerContact1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {

            }
        }
        protected void lvContacts_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            var item = lvContacts.Items[e.NewEditIndex];
            var m_id = lvContacts.DataKeys[item.DataItemIndex].Value;
            string id = lvContacts.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("CustomerContactEdit.aspx?contactid=" + m_id);

        }

        protected void lvContacts_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            DropDownList ddl = e.Item.FindControl("ddlInsertCompany") as DropDownList;
            if (ddl != null)
            {
                e.Values["Customer"] = ddl.SelectedValue;
            }
        }
    }
}