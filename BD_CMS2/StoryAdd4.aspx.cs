using BD_CMS2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class StoryAdd4 : System.Web.UI.Page
    {
        public class Story
        {
            public int StoryID { get; set; }

            public int? Revision { get; set; }

            public int? ParentID { get; set; }

            public string Body { get; set; }

            public string Headline { get; set; }

            public string Summary { get; set; }

            public string Reporter { get; set; }

            public string AssignedTo { get; set; }

            public string ReviewerNominated { get; set; }

            public string ReviewedBy { get; set; }

            public string AdditionalReporters { get; set; }

            public string Pub_Hold { get; set; }

            public string Visible { get; set; }

            public string SendToReview { get; set; }

            public string Urgency { get; set; }

            public string Status { get; set; }

            public string Tweet { get; set; }

            public DateTime? DateCreated { get; set; }

            public DateTime? DateToReview { get; set; }

            public DateTime? DateReviewed { get; set; }

            public DateTime? DateToPublish { get; set; }

            public DateTime? DatePublished { get; set; }

            public string Link { get; set; }

            public string Story_Metadata { get; set; }

            public string Published { get; set; }

            public string Topic { get; set; }

            public string Places { get; set; }

            public string Markets { get; set; }

            public string People { get; set; }

            public string SendToPublish { get; set; }

            public string asx { get; set; }

            public DateTime? DateHoldTo { get; set; }

            public string SFTP_Published { get; set; }

            public string newsml { get; set; }

            public string rss { get; set; }

            public string yahoo { get; set; }

            public DateTime? DatePublished_NZ { get; set; }

            public string MobilePub { get; set; }

            public string Lock_Flag { get; set; }

            public string ActiveUser { get; set; }

            public DateTime? DateLocked { get; set; }
            public string NZX_multi { get; set; }
            public string ASX_multi { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    PopulateNZX();
                    PopulateASX();                  

                }
            }
        }
        public void PopulateNZX()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            string strSQL = "SELECT [ANZSXID],[Name], [Ticker] FROM [ANZSX] ORDER BY [Ticker]";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader d_ddlNZX;
            d_ddlNZX = cmd.ExecuteReader();
            this.selNZX.DataSource = d_ddlNZX;
            this.selNZX.DataValueField = "ANZSXID";
            this.selNZX.DataTextField = "Name";
            this.selNZX.DataBind();
            cmd.Connection.Close();
            cmd.Connection.Dispose();

        }
        public void PopulateASX()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            string strSQL = "SELECT [ASXID],[Name], [Ticker] FROM [ASX] ORDER BY [Ticker]";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader d_ddlASX;
            d_ddlASX = cmd.ExecuteReader();
            this.selASX.DataSource = d_ddlASX;
            this.selASX.DataValueField = "ASXID";
            this.selASX.DataTextField = "Name";
            this.selASX.DataBind();
            cmd.Connection.Close();
            cmd.Connection.Dispose();

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
            var m_set_byline = this.selByLine.Value;
            var m_byline = "";
            if (m_set_byline == "Yes")
            { 
             m_byline = "<p>" + "By " + m_firstname + " " + m_lastname +"</p>"  ;
            }
            var m_month_alias = GetMonth(m_nzdate.ToString("MMMM"));

            var m_byline_date = m_month_alias+ m_nzdate.ToString(" d") + " (BusinessDesk) - ";
            string m_story = (Request.Form["editor1"]); 
            m_story = m_story.Insert(3, m_byline_date);
            m_story = m_byline + m_story + "(BusinessDesk)";
            var m_heading = this.tbHeading.Text;
            var m_urgency = "News";
            var m_sendtoreview = this.selReview.Value;
            var m_sendtoreview_flag = "N";
            var m_tags = this.tags.Text;
            var m_hold = this.selHold.Value;
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
            // this.CKStory.Text = "";
            this.tbHeading.Text = "";
            var m_NZX = Request["wlNZX"];
            var m_ASX = Request["wlASX"];
            var m_custom_send = Request["wlListValue"];
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO story(body, headline,datecreated,sendtoreview,pub_hold, urgency,datetoreview,status,topic,dateholdto,sftp_published,nzx_multi,asx_multi,custom_send)values(@body,@headline,@datecreated,@sendtoreview,@pub_hold,@urgency,@datetoreview,@status,@tags,@dateholdto,@sftp_published,@mnzx,@masx,@mcustom_send)", con);
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
                cmd.Parameters.Add("@tags", SqlDbType.NVarChar).Value = m_tags;
                cmd.Parameters.AddWithValue("@mnzx", m_NZX);
                cmd.Parameters.AddWithValue("@masx", m_ASX);
                cmd.Parameters.AddWithValue("@mcustom_send", m_custom_send );   
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }
            Response.Redirect("dashboard1.aspx");
        }

        public string GetMonth(string m_month)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            string m_month_alias = null;
           
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select month_alias from month where month  = '" + m_month +"'";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_month_alias = (dr["month_alias"].ToString());                    

                }
                
                cmd.Dispose();
                con.Close();
                con.Dispose();
                return m_month_alias;
            
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
        protected void btnByline_Click(object sender, EventArgs e)
        {
            // set byline inhibitor

        }
        
    }
}