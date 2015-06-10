using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class Dashboard_Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
        protected void gvPublishedQ_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = gvPublishedQ.SelectedRow;
            var m_id = row.Cells[1].Text;
            //Response.Redirect("DailyQedit1.aspx?storyid=" + m_id);
        }

        protected void gvPublishedQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                //e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>"; 
                var data = (DataRowView)e.Row.DataItem;
                var date = data["DateCreated"];
                if (date != null)
                {
                    e.Row.Cells[7].Text = GetNZ(date);
                }                
            }
        }

        protected void gvErrorQ_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = gvErrorQ.SelectedRow;
            var m_id = row.Cells[2].Text;
            Response.Redirect("ErrorDetail.aspx?storyid=" + m_id);
        }
        protected void gvErrorQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var data = (DataRowView)e.Row.DataItem;
                string date = data["DateCreated"].ToString();
                if (date != null)
                {
                    e.Row.Cells[7].Text = GetNZ(date);
                }
                DateTime dtcurrentDateToReview = DateTime.Parse(date);
                DateTime m_NZ_Now = DateTimeStuff.GetNZDateTimefromUTC(DateTime.UtcNow);
                
            }
        }
    }
}