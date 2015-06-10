using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class MessageResend : System.Web.UI.Page
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

        protected void btSelect_Click(object sender, EventArgs e)
        {

        }
        protected void lvResendView_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (String.Equals(e.CommandName, "AddToList"))
            {
                // Verify that the story ID is not already in the list. If not, add the
                // story to the list.
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                string m_storyID =
                  lvResend.DataKeys[dataItem.DisplayIndex].Value.ToString();

                if (SelectedStoriesListBox.Items.FindByValue(m_storyID) == null)
                {
                    ListItem item = new ListItem(e.CommandArgument.ToString(), m_storyID);
                    SelectedStoriesListBox.Items.Add(item);
                }
            }
        }

        protected void btSendStories_Click(object sender, EventArgs e)
        {

        }

    }
}