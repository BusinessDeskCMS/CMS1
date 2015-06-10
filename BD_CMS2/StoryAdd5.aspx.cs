using BD_CMS2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public partial class StoryAdd5 : System.Web.UI.Page
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
        protected void btnSaveStory_Click(object sender, EventArgs e)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var m_username = currentUser.UserName;
            var m_firstname = currentUser.FirstName;
            var m_lastname = currentUser.LastName;
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            var m_byline = "<p>" + "By " + m_firstname + " " + m_lastname + "</p>";
            var m_byline_date = m_nzdate.ToString("MMM. d") + " (BusinessDesk) - ";
            string m_story = (Request.Form["editor1"]); 

           
            m_story = m_story.Insert(3, m_byline_date);
            m_story = m_byline + m_story + "(BusinessDesk)";
            var m_heading = this.tbHeading.Text;
            var m_urgency = this.ddlType.SelectedValue;
            var m_sendtoreview = this.ddlSendToReview.SelectedValue;
            var m_sendtoreview_flag = "N";
            var m_tags = this.tags.Text;
            var m_hold = this.ddlHold.SelectedValue;
            string m_datehold = "";
            m_datehold = this.tbHoldToDate.Text;

            DateTime m_datetoreview = DateTime.MinValue;
            m_datetoreview = m_datetoreview.AddYears(1800);
            DateTime m_dateholdto = DateTime.UtcNow;
            if (m_datehold != "")
            {
                m_dateholdto = Convert.ToDateTime(m_datehold);
            }
            var m_status = "Draft";

            if (m_sendtoreview == "Yes")
            {
                m_sendtoreview_flag = "Y";
                m_datetoreview = System.DateTime.UtcNow;
                m_status = "In Review";
            }
            
            this.tbHeading.Text = "";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO story(body, headline,datecreated,sendtoreview,pub_hold, urgency,datetoreview,status,markets,asx,topic,dateholdto,sftp_published)values(@body,@headline,@datecreated,@sendtoreview,@pub_hold,@urgency,@datetoreview,@status,@nzx,@asx,@tags,@dateholdto,@sftp_published)", con);

                cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_heading;
                cmd.Parameters.Add("@urgency", SqlDbType.NVarChar).Value = m_urgency;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.UtcNow;
                cmd.Parameters.Add("@dateholdto", SqlDbType.DateTime).Value = m_dateholdto;
                cmd.Parameters.Add("@sendtoreview", SqlDbType.NChar).Value = m_sendtoreview_flag;
                cmd.Parameters.Add("@datetoreview", SqlDbType.DateTime).Value = m_datetoreview;
                cmd.Parameters.Add("@pub_hold", SqlDbType.NChar).Value = "N";
                cmd.Parameters.Add("@sftp_published", SqlDbType.NChar).Value = "N";
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = m_status;
                cmd.Parameters.Add("@nzx", SqlDbType.NVarChar).Value = this.ddlTicker.SelectedValue;
                cmd.Parameters.Add("@asx", SqlDbType.NVarChar).Value = this.ddlASX.SelectedValue;
                cmd.Parameters.Add("@tags", SqlDbType.NVarChar).Value = m_tags;


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

        DataSet GetData(String queryString)
        {

            // Retrieve the connection string stored in the Web.config file.
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;

            DataSet ds = new DataSet();

            try
            {
                // Connect to the database and run the query.
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

                // Fill the DataSet.
                adapter.Fill(ds);

            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }

            return ds;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }
    }
}