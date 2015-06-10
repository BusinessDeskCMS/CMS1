using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class Dashboard : System.Web.UI.Page
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
        protected string GetNZ(object p_utc)
        {
            
            string m1 = p_utc.ToString();
            string m_dateNZ = "";
            if (m1 != "")
            {
                DateTime dt1 = Convert.ToDateTime(m1);
                return m_dateNZ = DateTimeStuff.GetNZDateTimefromUTC(dt1).ToString("dd/MM HH:mm");                
            }
            else
            {
                return m_dateNZ;
            }
        }

        protected void lvDailyQ_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            var item = lvDailyQ.Items[e.NewEditIndex];
            var m_id = lvDailyQ.DataKeys[item.DataItemIndex].Value;
            string id = lvDailyQ.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("DailyQedit1.aspx?storyid=" + m_id);

        }
        protected void lvDailyQ_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label StatusLabel;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                // 
                StatusLabel = (Label)e.Item.FindControl("StatusLabel");
                //

                System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
                string currentStatus = rowView["Status"].ToString();
                if (currentStatus == "In Review")
                {
                    StatusLabel.Font.Bold = true;
                    StatusLabel.ForeColor = System.Drawing.Color.Red;
                }
                else
                    if (currentStatus == "Published")
                    {
                        StatusLabel.Font.Bold = true;
                        StatusLabel.ForeColor = System.Drawing.Color.Green;
                    }
            }

        }
        protected void lvReviewQ_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label DateToReviewLabel;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Display the e-mail address in italics.
                DateToReviewLabel = (Label)e.Item.FindControl("DateToReviewLabel");
                //StatusLabel.Font.Italic = true;

                System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
                string s_currentDate = rowView["DateToReview"].ToString();
                DateTime dtcurrentDateToReview = DateTime.Parse(s_currentDate);
                TimeSpan timediff = DateTime.Now - dtcurrentDateToReview;
                var ElapsedReview = timediff.TotalMinutes;

                if (ElapsedReview > 20)
                {
                    DateToReviewLabel.Font.Bold = true;
                    DateToReviewLabel.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        protected void lvReviewQ_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            var item = lvReviewQ.Items[e.NewEditIndex];
            var m_id = lvReviewQ.DataKeys[item.DataItemIndex].Value;
            string id = lvReviewQ.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("ReviewQedit1.aspx?storyid=" + m_id);

        }
        protected string LocalTime(DateTime Published)
        {
            if (Published is DBNull)
            {
                return "";
            }
            TimeZoneInfo LocalTimeZone = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(Published, LocalTimeZone).ToString("MMM dd - HH:mm");
        }
    }
}