using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class Dashboard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                CheckError();
            }



        }
        protected void CheckError()
        {
            string m_msgid = "";
            string m_headline = "";
            string m_message = "";
            string m_customer = "";

            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select storyid, headline, msgstatus,customer from msglog where status = 'Pub Error' ";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_msgid = (dr["storyid"].ToString());
                    m_customer = (dr["customer"].ToString());
                    m_headline = (dr["headline"].ToString());
                    m_message = (dr["message"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            if (m_msgid != "")
            {
                // show panel and error message
                this.PanError.Visible = true;

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

        protected void gvDailyQ_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = gvDailyQ.SelectedRow;
            var m_id = row.Cells[1].Text;
            Response.Redirect("DailyQedit4.aspx?storyid=" + m_id);
        }

        protected void gvDailyQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                //e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>"; 
                var data = (DataRowView)e.Row.DataItem;
                var date = data["DatePublished"];
                if (date != null)
                {
                    e.Row.Cells[3].Text = GetNZ(date);
                }
                string m_status = data["Status"] as string;
                if (m_status != null)
                {
                    if ((string)m_status == "In Review")
                    {
                        e.Row.Cells[4].Font.Bold = true;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                    else
                        if ((string)m_status == "Published")
                        {
                            e.Row.Cells[4].Font.Bold = true;
                            e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                        }
                }
                else
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.LightGray;
                }
            }
        }

        protected void gvReviewQ_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = gvReviewQ.SelectedRow;
            var m_id = row.Cells[1].Text;
            Response.Redirect("ReviewQedit4.aspx?storyid=" + m_id);
        }
        protected void gvReviewQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var data = (DataRowView)e.Row.DataItem;
                string date = data["DateToReview"].ToString();
                if (date != null)
                {
                    e.Row.Cells[3].Text = GetNZ(date);
                }
                DateTime dtcurrentDateToReview = DateTime.Parse(date);
                DateTime m_NZ_Now = DateTimeStuff.GetNZDateTimefromUTC(DateTime.UtcNow);
                TimeSpan timediff = m_NZ_Now - dtcurrentDateToReview;
                var ElapsedReview = timediff.TotalMinutes;
                if (ElapsedReview > 20)
                {
                    e.Row.Cells[3].Font.Bold = true;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard_Error.aspx");
        }
    }
}