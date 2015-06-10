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
    public partial class DailyQEdit4 : System.Web.UI.Page
    {
        protected string StoryValue { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            StoryValue = StoryValue;
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

                    string strSQL = "Select body, headline,sendtoreview,pub_hold, urgency,topic,markets from story where storyid = " + m_storyid;

                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        this.tbHeading.Text = (dr["headline"].ToString());                       
                        StoryValue = HttpUtility.HtmlDecode(dr["body"].ToString());
                        this.tags.Text = (dr["topic"].ToString());
                    }
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
                catch (Exception)
                {
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
        protected void btnReviewStory_Click(object sender, EventArgs e)
        {
            string m_story = (Request.Form["editor1"]);
            var m_heading = this.tbHeading.Text;
            var m_topic = this.tags.Text;
            int m_story_id = Convert.ToInt32(this.lbStoryID.Text);            
            this.tbHeading.Text = "";           
            DateTime m_datetoreview = System.DateTime.Now;               
            var m_sendtoreview_flag = "Y";           
            var m_status = "In Review";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE story set body = @body, headline = @headline, status = @status, sendtoreview = @sendtoreview, datetoreview = @datetoreview, topic = @topic where storyid = @storyid", con);
                cmd.Parameters.AddWithValue("@body", m_story);
                cmd.Parameters.AddWithValue("@headline", m_heading);
                cmd.Parameters.AddWithValue("@storyid", m_story_id);
                cmd.Parameters.AddWithValue("@sendtoreview", m_sendtoreview_flag);
                cmd.Parameters.AddWithValue("@datetoreview", m_datetoreview);
                cmd.Parameters.AddWithValue("@status", m_status);
                cmd.Parameters.AddWithValue("@topic", m_topic);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            Response.Redirect("dashboard1.aspx");
        }
        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            string m_story = (Request.Form["editor1"]);
            var m_heading = this.tbHeading.Text;
            var m_topic = this.tags.Text;
            int m_story_id = Convert.ToInt32(this.lbStoryID.Text);            
            this.tbHeading.Text = "";           
            var m_sendtoreview_flag = "N";
            var m_status = "Draft";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE story set body = @body, headline = @headline, status = @status, sendtoreview = @sendtoreview, topic = @topic where storyid = @storyid", con);
                cmd.Parameters.AddWithValue("@body", m_story);
                cmd.Parameters.AddWithValue("@headline", m_heading);
                cmd.Parameters.AddWithValue("@storyid", m_story_id);
                cmd.Parameters.AddWithValue("@sendtoreview", m_sendtoreview_flag);               
                cmd.Parameters.AddWithValue("@status", m_status);
                cmd.Parameters.AddWithValue("@topic", m_topic);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            Response.Redirect("dashboard1.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
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
            Response.Redirect("dashboard1.aspx");
        }
    }
}