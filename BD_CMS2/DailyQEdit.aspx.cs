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
    public partial class DailyQEdit : System.Web.UI.Page
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
        protected void Page_Init(object sender, EventArgs e)
        {
            
            if (Request.QueryString["storyid"] != null)
            {
            int m_storyid = Convert.ToInt32(Request.QueryString["storyid"]);
            this.lbStoryID.Text = m_storyid.ToString();
           

            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString); 
                string strSQL = "Select body, headline,sendtoreview,pub_hold, urgency from story where storyid = " + m_storyid;
                
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.tbHeading.Text = (dr["headline"].ToString());
                    this.CKStory.Text = HttpUtility.HtmlDecode(dr["body"].ToString());
                    
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
            }

        }
        DataSet GetData(String queryString)
        {

            // Retrieve the connection string stored in the Web.config file.
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;

            DataSet ds = new DataSet();

            try
            {
                // Connect to the database and run the query.
                SqlConnection connection = new SqlConnection(connectionString);
               

            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }

            return ds;

        }
        protected void btnSaveStory_Click(object sender, EventArgs e)
        {

            var m_story = this.CKStory.Text;
            var m_heading = this.tbHeading.Text;
            int m_story_id = Convert.ToInt32(this.lbStoryID.Text);
            this.CKStory.Text = "";
            this.tbHeading.Text = "";
            var m_sendtoreview = this.ddlSendToReview.SelectedValue;
            var m_sendtoreview_flag = "N";
            DateTime m_datetoreview = DateTime.MinValue;
            m_datetoreview = m_datetoreview.AddYears(1800);
            var m_status = "Draft";

            if (m_sendtoreview == "Yes")
            {
                m_sendtoreview_flag = "Y";
                m_datetoreview = System.DateTime.Now;
                m_status = "In Review";


            }
          
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE story set body = @body, headline = @headline, status = @status, sendtoreview = @sendtoreview, datetoreview = @datetoreview where storyid = @storyid", con);
                cmd.Parameters.AddWithValue("@body", m_story);
                cmd.Parameters.AddWithValue("@headline", m_heading);
                cmd.Parameters.AddWithValue("@storyid",  m_story_id);
                cmd.Parameters.AddWithValue("@sendtoreview", m_sendtoreview_flag);
                cmd.Parameters.AddWithValue("@datetoreview", m_datetoreview); 
                cmd.Parameters.AddWithValue("@status", m_status);
                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }
            Response.Redirect("dashboard.aspx");


        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int m_story_id = Convert.ToInt32(this.lbStoryID.Text);
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE from story where storyid = @storyid", con);                
                cmd.Parameters.AddWithValue("@storyid", m_story_id);                
                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }
            Response.Redirect("dashboard.aspx");

        }
    }
}