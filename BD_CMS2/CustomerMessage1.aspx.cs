using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class CustomerMessage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void gvCustomer_SelectedIndexChanging(Object sender, EventArgs e)
        {
            dvCustomer.DataBind();

        }

        protected void dvCustomer_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
        {
            gvCustomer.DataBind();
            dvCustomer.DataBind();
        }

        protected void dvCustomer_ItemDeleted(Object sender, DetailsViewDeletedEventArgs e)
        {
            gvCustomer.DataBind();
        }

        protected void ContactDetailsUpdate_OnInserted(Object sender, SqlDataSourceStatusEventArgs e)
        {
            System.Data.Common.DbCommand command = e.Command;
            gvCustomer.DataBind();
            dvCustomer.DataBind();
        }  
    }
}