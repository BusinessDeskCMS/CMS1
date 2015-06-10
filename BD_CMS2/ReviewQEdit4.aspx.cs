using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Tamir.SharpSsh;
using System.Text.RegularExpressions;
using Renci.SshNet;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BD_CMS2.Models;



namespace BD_CMS2
{
    public partial class ReviewQEdit4 : System.Web.UI.Page
    {
        protected string StoryValue { get; set; }
        protected string s_NZX { get; set; }
        protected string s_ASX { get; set; }
        protected string s_custom_send { get; set; }


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

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["storyid"] != null)
            {
                int m_storyid = Convert.ToInt32(Request.QueryString["storyid"]);
                string mhold = "No";
                s_NZX = null;
                s_ASX = null;
                s_custom_send = null;
                this.lbStoryID.Text = m_storyid.ToString();
                StoryValue = null;
                String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    string strSQL = "Select body, headline,sendtoreview,pub_hold, dateholdto, urgency,topic,nzx_multi,asx_multi,custom_send from story where storyid = " + m_storyid;
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        this.tbHeading.Text = (dr["headline"].ToString());
                        StoryValue = HttpUtility.HtmlDecode(dr["body"].ToString());
                        this.tags.Text = (dr["topic"].ToString());
                        mhold = (dr["pub_hold"].ToString());
                        s_NZX = (dr["nzx_multi"].ToString());
                        s_ASX = (dr["asx_multi"].ToString());
                        s_custom_send = (dr["custom_send"].ToString());
                    }
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                    SetValuesTickers(s_NZX, s_ASX, s_custom_send);
                }
                catch (Exception ex)
                {
                    // The connection failed. Display an error message.
                    //Message.Text = "Unable to connect to the database.";
                }
            }
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
                    StoryValue = StoryValue;
                    PopulateASX();
                    PopulateNZX();

                }
            }
        }


        private void SetValuesTickers(string p_nzx, string p_asx, string p_custom_send)
        {
            string nzx = null;
            string nzx1 = "";
            string asx = null;
            string asx1 = "";
            string custom = null;
            string custom1 = "";
            if (p_nzx != "")
            {
                var elements = p_nzx.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string items in elements)
                {
                    nzx = nzx + items.ToString() + ",";
                }
                // get rid of the last ","
                var len = nzx.Length;
                nzx1 = nzx.Substring(0, len - 1);
            }
            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("$(function () {");
            script.Append(" $('#selNZX').multipleSelect('setSelects',[" + nzx1 + "]);");
            if (p_asx != "")
            {
                var elements_asx = p_asx.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string items in elements_asx)
                {
                    asx = asx + items.ToString() + ",";
                }
                // get rid of the last ","
                var len = asx.Length;
                asx1 = asx.Substring(0, len - 1);
                script.Append(" $('#selASX').multipleSelect('setSelects',[" + asx1 + "]);");
            }
            if (p_custom_send != "")
            {
                var elements_custom = p_custom_send.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string items in elements_custom)
                {
                    custom = custom + items.ToString() + ",";
                }
                // get rid of the last ","
                var len = custom.Length;
                custom1 = custom.Substring(0, len - 1);
                script.Append(" $('#ms').multipleSelect('setSelects',[" + custom1 + "]);");
            }
            script.Append(" });");
            script.Append("</script>");
            Page.ClientScript.RegisterClientScriptBlock(typeof(object), "JavaScriptBlock", script.ToString());

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
        protected void btnPublishStory_Click(object sender, EventArgs e)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            int m_story_id = Convert.ToInt32(this.lbStoryID.Text);
            var m_heading = this.tbHeading.Text;

            string m_customdelivery = "N";
            var m_msgid = this.lbStoryID.Text;
            var elements = Request["wlListValue"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string items in elements)
            {
                var m_customer = items.ToString();
                m_customdelivery = "Y";
                // insert a row into CustomerDelivery table
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO CustomDelivery(CustomerID, StoryID,datecreated)values(@customerid,@storyid,@datecreated)", con);

                    cmd.Parameters.Add("@customerid", SqlDbType.NVarChar).Value = Convert.ToInt16(m_customer);
                    cmd.Parameters.Add("@storyid", SqlDbType.NVarChar).Value = m_story_id;
                    cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.UtcNow;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    PublishLog(m_msgid, m_heading, " ", ex.Message, "Unable to connect to CustomDelivery");
                }
            }

            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            string m_story = (Request.Form["editor1"]);

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var m_username = currentUser.UserName;
            var m_firstname = currentUser.FirstName;
            var m_lastname = currentUser.LastName;
            var m_reporter = m_firstname + " " + m_lastname;
            var m_NZX = Request["wlNZX"];
            var m_ASX = Request["wlASX"];
            //var m_hold = this.ddlHold.SelectedValue;
            var m_hold = this.ddlHold1.Value;
            if (m_hold == "No" || m_hold == "")
            {
                DateTime m_datetopublish = DateTime.MinValue;
                m_datetopublish = m_datetopublish.AddYears(1800);
                var m_status = "Published";
                var m_reviewaction_flag = "Y";
                m_datetopublish = DateTime.UtcNow;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE story set body = @body, headline = @headline, sendtoreview = @sendtoreview, status = @status, sendtopublish = @sendtopublish, datepublished = @datetopublish, DatePublished_NZ = @datepublished_nz,nzx_multi=@nzx_multi where storyid = @storyid", con);
                    cmd.Parameters.AddWithValue("@body", m_story);
                    cmd.Parameters.AddWithValue("@headline", m_heading);
                    cmd.Parameters.AddWithValue("@storyid", m_story_id);
                    cmd.Parameters.AddWithValue("@sendtoreview", "N");
                    cmd.Parameters.AddWithValue("@sendtopublish", m_reviewaction_flag);
                    cmd.Parameters.AddWithValue("@status", m_status);
                    cmd.Parameters.AddWithValue("@datetopublish", m_datetopublish);
                    cmd.Parameters.AddWithValue("@datepublished_nz", m_nzdate);
                    cmd.Parameters.AddWithValue("@nzx_multi", m_NZX);
                    cmd.Parameters.AddWithValue("@asx_multi", m_ASX);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                    PublishLog(m_msgid, m_heading, " ", ex.Message, "Pub Button CLick - unable to connect to story (UPDATE)");
                }
                // send the story to the Public website if the flag is set.
                var m_public = "N";
                String controlconnectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                SqlConnection con2 = new SqlConnection(controlconnectionString);
                string strSQL = "SELECT [public_site_feed_active] FROM [cmscontrol] ";
                SqlCommand cmd2 = new SqlCommand(strSQL, con2);
                cmd2.CommandType = CommandType.Text;
                con2.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    m_public = (dr["public_site_feed_active"].ToString());
                }
                cmd2.Dispose();
                con2.Close();
                con2.Dispose();

                if (m_public == "Y")
                {
                    string bdpublicconnectionString = ConfigurationManager.ConnectionStrings["BDPublicConnectionString"].ConnectionString;
                    try
                    {
                        // Connect to the database and run the query.
                        SqlConnection con = new SqlConnection(bdpublicconnectionString);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO story(body, headline,datepublished)values(@body,@headline,@datepublished)", con);

                        cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = m_story;
                        cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_heading;
                        cmd.Parameters.Add("@datepublished", SqlDbType.DateTime).Value = System.DateTime.UtcNow;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PublishLog(m_msgid, m_heading, " ", ex.Message, "Pub Button CLick - unable to connect to story on Public Website (INSERT)");
                    }
                }

                NewsMLCreate(m_story_id);
                NewsML_NBRCreate(m_story_id);
                News_AAPCreate(m_story_id);
                NewsML_HTMLCreate(m_story_id);
                NewsML_YahooCreate(m_story_id);
                RSSCreate_BLob(m_story_id);
                //this.CKStory.Text = "";
                this.tbHeading.Text = "";
                if (m_customdelivery == "N")
                {
                    SendMessage(m_story_id);
                    Email_Send(m_story_id);
                }
                else
                {
                    SendCustomDelivery(m_story_id);
                }

            }
            else
            {
                var m_status = "On Hold";
                connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE story set body = @body, headline = @headline, sendtoreview = @sendtoreview, status = @status, sendtopublish = @sendtopublish where storyid = @storyid", con);
                    cmd.Parameters.AddWithValue("@body", m_story);
                    cmd.Parameters.AddWithValue("@headline", m_heading);
                    cmd.Parameters.AddWithValue("@storyid", m_story_id);
                    cmd.Parameters.AddWithValue("@sendtoreview", "N");
                    cmd.Parameters.AddWithValue("@sendtopublish", "N");
                    cmd.Parameters.AddWithValue("@status", m_status);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    PublishLog(m_msgid, m_heading, " ", ex.Message, "Pub Button CLick - unable to connect to story (UPDATE) for On Hold");
                }
            }
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            string m_story = (Request.Form["editor1"]);
            var m_heading = this.tbHeading.Text;
            int m_story_id = Convert.ToInt32(this.lbStoryID.Text);
            var m_msgid = this.lbStoryID.Text;
            var m_reviewaction_flag = "N";
            var m_hold = this.ddlHold1.Value;
            var m_status = "Draft";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE story set body = @body, headline = @headline, sendtoreview = @sendtoreview, status = @status, sendtopublish = @sendtopublish where storyid = @storyid", con);
                cmd.Parameters.AddWithValue("@body", m_story);
                cmd.Parameters.AddWithValue("@headline", m_heading);
                cmd.Parameters.AddWithValue("@storyid", m_story_id);
                cmd.Parameters.AddWithValue("@sendtoreview", "N");
                cmd.Parameters.AddWithValue("@sendtopublish", m_reviewaction_flag);
                cmd.Parameters.AddWithValue("@status", m_status);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                PublishLog(m_msgid, m_heading, " ", ex.Message, "Save Draft Button CLick - unable to connect to story (UPDATE)");
            }
            Response.Redirect("dashboard1.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }

        protected void NewsMLCreate(int storyid)
        {
            MemoryStream strm = new MemoryStream();
            string m_story = (Request.Form["editor1"]);
            string x_story = ReplaceWordChars(m_story);
            m_story = x_story;
            var m_headline = this.tbHeading.Text;
            var m_tags = this.tags.Text;
            string[] tags = m_tags.Split(',');
            string m_id = storyid.ToString();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_timeID = m_nzdate.ToString("yyyMMddHHmmss");
            string file_now = m_timeID;
            string RSS_File = m_id + "_RSS.xml";
            file_now = m_id + ".xml";
            string yahoo_file = m_id + "_Yahoo.xml";
            string[] lines = m_story.Replace("\r", "").Split('\n');
            string m_embargoed = string.Empty;
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var m_username = currentUser.UserName;
            var m_firstname = currentUser.FirstName;
            var m_lastname = currentUser.LastName;
            var m_reporter = m_firstname + " " + m_lastname;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = null;
            // write the NewsML file 
            writer = XmlWriter.Create(strm, settings);
            string m_body1 = "";
            foreach (string str in lines)
            {
                if (str.Length > 1)
                {
                    m_body1 += str + "\r\n";
                }
            }
            m_body1 = Regex.Replace(m_body1, @"<[^>]*>", String.Empty);
            StringWriter myWriter = new StringWriter();
            // Decode the encoded string.
            // HttpUtility.HtmlDecode(m_body1, myWriter);
            writer.WriteComment("BusinessDesk NewsML");
            writer.WriteStartElement("newsMessage", "http://iptc.org/std/nar/2006-10-01/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/nar/2006-10-01/", "http://www.iptc.org/std/NAR/1.9/specification/NewsML-G2_2.8-spec-NewsItem-Power.xsd");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.8");
            writer.WriteAttributeString("conformance", "power");
            writer.WriteAttributeString("lang", "en-US");
            // START Header
            writer.WriteStartElement("Header");
            writer.WriteStartElement("Sent");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("sender");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("origin");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("transmitId");
            writer.WriteString(m_timeID);
            writer.WriteEndElement();
            writer.WriteStartElement("priority");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("destination");
            writer.WriteString("All");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END Header
            //
            // START ItemSet
            writer.WriteStartElement("itemSet");
            // START Package Item
            writer.WriteStartElement("packageItem");
            writer.WriteStartElement("itemRef");
            writer.WriteAttributeString("residref", m_timeID);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END PackageItem
            //
            // START NewsItem
            writer.WriteStartElement("newsItem");
            writer.WriteAttributeString("guid", "BD-" + m_timeID);
            writer.WriteAttributeString("version", "3");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.7");
            writer.WriteStartElement("catalogRef");
            writer.WriteAttributeString("href", "http://www.iptc.org/std/catalog/catalog.IPTC-G2-Standards_13.xml");
            writer.WriteEndElement();
            // START RightsInfo
            writer.WriteStartElement("rightsInfo");
            writer.WriteStartElement("copyrightHolder");
            writer.WriteAttributeString("literal", "");
            writer.WriteEndElement();
            writer.WriteStartElement("copyrightNotice");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END RightsInfo
            //
            // START ItemMeta
            writer.WriteStartElement("itemMeta");
            writer.WriteStartElement("itemClass");
            writer.WriteAttributeString("qcode", "ninat:text");
            writer.WriteEndElement();
            writer.WriteStartElement("provider");
            writer.WriteAttributeString("literal", "CL");
            writer.WriteEndElement();
            writer.WriteStartElement("versionCreated");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("embargoed");
            writer.WriteString(m_embargoed);
            writer.WriteEndElement();
            writer.WriteStartElement("ednote");
            writer.WriteString("");
            writer.WriteEndElement();
            writer.WriteStartElement("pubStatus");
            writer.WriteAttributeString("qcode", "stat:usable");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END ItemMeta
            //
            // START ContentMeta
            writer.WriteStartElement("contentMeta");
            writer.WriteStartElement("urgency");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("keyword");
            writer.WriteAttributeString("role", "krole:index");
            writer.WriteString("");
            writer.WriteEndElement();
            // Taxonomy field value
            string smarket = "";
            string mqcode = "type";
            writer.WriteStartElement("subject");
            writer.WriteAttributeString(mqcode, "Topic");
            writer.WriteStartElement("name");
            writer.WriteString(smarket);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END COntentMeta
            //
            // START COntentSet
            writer.WriteStartElement("contentSet");
            // START INLINEXML
            writer.WriteStartElement("inlineXML");
            writer.WriteAttributeString("contenttype", "application/nitf+xml");
            writer.WriteStartElement("nitf");
            //writer.WriteAttributeString("xmlns", "http://iptc.org/std/NITF/2006-10-18/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/NITF/2006-10-18/", "http://www.iptc.org/std/NITF/3.5/specification/nitf-3-5.xsd");

            // HEAD
            writer.WriteStartElement("head");
            writer.WriteStartElement("title");
            writer.WriteString(m_headline);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END HEAD
            //
            // START BODY
            writer.WriteStartElement("body");
            // START BODY Head
            writer.WriteStartElement("body.head");
            writer.WriteStartElement("byline");
            writer.WriteString("By");
            writer.WriteStartElement("person");
            writer.WriteString(m_reporter);
            writer.WriteEndElement();
            writer.WriteStartElement("byttl");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("abstract");
            writer.WriteString(" ");
            writer.WriteEndElement();
            writer.WriteEndElement();
            //END Body Head
            //
            // START Body Content
            writer.WriteStartElement("body.content");

            // writer.WriteCData(myWriter.ToString());
            writer.WriteCData(m_body1);
            writer.WriteEndElement();
            // END Body Content                                                       
            writer.WriteEndElement();
            //END BODY 
            writer.WriteEndElement();
            // END NITF
            writer.WriteEndElement();
            // END INLINEXML                                                
            writer.WriteEndElement();
            // END CONTENTSET                                           
            writer.WriteEndElement();
            // END NewsItem                                        
            writer.WriteEndElement();
            // END ItemSet                                 
            writer.WriteEndElement();
            // END NewsMessage
            writer.Flush();
            writer.Close();
            //
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("messages");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file_now);
                strm.Position = 0;
                using (var filestream = strm)
                {
                    blockBlob.UploadFromStream(filestream);
                }
            }
            catch (Exception ex)
            {

                PublishLog("NewsML", "", " ", ex.Message, "NewsML Create - cannot connect to blob");
            }
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO msg(storyid, message, datecreated, yahoo,headline)values(@storyid,@message,@datecreated,@yahoo,@headline)", con);
                cmd.Parameters.Add("@storyid", SqlDbType.Int).Value = storyid;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.Parameters.Add("@yahoo", SqlDbType.VarChar).Value = "N";
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_headline;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                PublishLog("NewsML", "", " ", ex.Message, "NewsML Create - cannot connect to table [msg]");
            }
        }
        protected void NewsML_NBRCreate(int storyid)
        {
            MemoryStream strm = new MemoryStream();
            string m_story = (Request.Form["editor1"]);
            string x_story = ReplaceWordChars(m_story);
            m_story = x_story;
            var m_headline = this.tbHeading.Text;
            string m_id = storyid.ToString();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_timeID = m_nzdate.ToString("yyyMMddHHmmss");
            string file_now = m_timeID;
            file_now = m_id + "_NBR.xml";
            m_story = Regex.Replace(m_story, @"<[^>]*>", String.Empty);
            // string[] lines = m_story.Split('\n');
            string[] lines = m_story.Replace("\r", "").Split('\n');
            string m_embargoed = string.Empty;
            var m_reporter = "";
            if (m_reporter.Length > 3)
            {
                m_reporter = m_reporter.Substring(3, m_reporter.Length - 3);
            }
            else
            {
                m_reporter = "Staff Reporter";
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = null;
            // write the NewsML file 
            writer = XmlWriter.Create(strm, settings);
            string m_body1 = "";
            foreach (string str in lines)
            {
                if (str.Length > 1)
                {
                    // m_body1 += str + "\r\n";
                    m_body1 += "<p> " + str + " </p>";
                }
            }
            // m_body1 = Regex.Replace(m_body1, @"<[^>]*>", String.Empty);
            StringWriter myWriter = new StringWriter();
            // Decode the encoded string.
            // HttpUtility.HtmlDecode(m_body1, myWriter);
            writer.WriteComment("BusinessDesk NewsML");
            writer.WriteStartElement("newsMessage", "http://iptc.org/std/nar/2006-10-01/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/nar/2006-10-01/", "http://www.iptc.org/std/NAR/1.9/specification/NewsML-G2_2.8-spec-NewsItem-Power.xsd");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.8");
            writer.WriteAttributeString("conformance", "power");
            writer.WriteAttributeString("lang", "en-US");
            // START Header
            writer.WriteStartElement("Header");
            writer.WriteStartElement("Sent");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("sender");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("origin");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("transmitId");
            writer.WriteString(m_timeID);
            writer.WriteEndElement();
            writer.WriteStartElement("priority");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("destination");
            writer.WriteString("All");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END Header
            //
            // START ItemSet
            writer.WriteStartElement("itemSet");
            // START Package Item
            writer.WriteStartElement("packageItem");
            writer.WriteStartElement("itemRef");
            writer.WriteAttributeString("residref", m_timeID);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END PackageItem
            //
            // START NewsItem
            writer.WriteStartElement("newsItem");
            writer.WriteAttributeString("guid", "BD-" + m_timeID);
            writer.WriteAttributeString("version", "3");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.7");
            writer.WriteStartElement("catalogRef");
            writer.WriteAttributeString("href", "http://www.iptc.org/std/catalog/catalog.IPTC-G2-Standards_13.xml");
            writer.WriteEndElement();
            // START RightsInfo
            writer.WriteStartElement("rightsInfo");
            writer.WriteStartElement("copyrightHolder");
            writer.WriteAttributeString("literal", "");
            writer.WriteEndElement();
            writer.WriteStartElement("copyrightNotice");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END RightsInfo
            //
            // START ItemMeta
            writer.WriteStartElement("itemMeta");
            writer.WriteStartElement("itemClass");
            writer.WriteAttributeString("qcode", "ninat:text");
            writer.WriteEndElement();
            writer.WriteStartElement("provider");
            writer.WriteAttributeString("literal", "CL");
            writer.WriteEndElement();
            writer.WriteStartElement("versionCreated");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("embargoed");
            writer.WriteString(m_embargoed);
            writer.WriteEndElement();
            writer.WriteStartElement("ednote");
            writer.WriteString("");
            writer.WriteEndElement();
            writer.WriteStartElement("pubStatus");
            writer.WriteAttributeString("qcode", "stat:usable");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END ItemMeta
            //
            // START ContentMeta
            writer.WriteStartElement("contentMeta");
            writer.WriteStartElement("urgency");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("keyword");
            writer.WriteAttributeString("role", "krole:index");
            writer.WriteString("");
            writer.WriteEndElement();
            // Taxonomy field value
            string smarket = "";
            string mqcode = "type";
            writer.WriteStartElement("subject");
            writer.WriteAttributeString(mqcode, "Topic");
            writer.WriteStartElement("name");
            writer.WriteString(smarket);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END COntentMeta
            //
            // START COntentSet
            writer.WriteStartElement("contentSet");
            // START INLINEXML
            writer.WriteStartElement("inlineXML");
            writer.WriteAttributeString("contenttype", "application/nitf+xml");
            writer.WriteStartElement("nitf");
            //writer.WriteAttributeString("xmlns", "http://iptc.org/std/NITF/2006-10-18/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/NITF/2006-10-18/", "http://www.iptc.org/std/NITF/3.5/specification/nitf-3-5.xsd");

            // HEAD
            writer.WriteStartElement("head");
            writer.WriteStartElement("title");
            writer.WriteString(m_headline);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END HEAD
            //
            // START BODY
            writer.WriteStartElement("body");
            // START BODY Head
            writer.WriteStartElement("body.head");
            writer.WriteStartElement("byline");
            writer.WriteString("By");
            writer.WriteStartElement("person");
            writer.WriteString(m_reporter);
            writer.WriteEndElement();
            writer.WriteStartElement("byttl");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("abstract");
            writer.WriteString(" ");
            writer.WriteEndElement();
            writer.WriteEndElement();
            //END Body Head
            //
            // START Body Content
            writer.WriteStartElement("body.content");

            // writer.WriteCData(myWriter.ToString());
            writer.WriteCData(m_body1);
            writer.WriteEndElement();
            // END Body Content                                                       
            writer.WriteEndElement();
            //END BODY 
            writer.WriteEndElement();
            // END NITF
            writer.WriteEndElement();
            // END INLINEXML                                                
            writer.WriteEndElement();
            // END CONTENTSET                                           
            writer.WriteEndElement();
            // END NewsItem                                        
            writer.WriteEndElement();
            // END ItemSet                                 
            writer.WriteEndElement();
            // END NewsMessage
            writer.Flush();
            writer.Close();
            //
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("messages");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file_now);
                strm.Position = 0;
                using (var filestream = strm)
                {
                    blockBlob.UploadFromStream(filestream);
                }
            }
            catch (Exception ex)
            {

                PublishLog("NewsML", "", " ", ex.Message, "NBR Create - cannot connect to blob");
            }
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO msg(storyid, message, datecreated, yahoo,headline)values(@storyid,@message,@datecreated,@yahoo,@headline)", con);
                cmd.Parameters.Add("@storyid", SqlDbType.Int).Value = storyid;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.Parameters.Add("@yahoo", SqlDbType.VarChar).Value = "N";
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_headline;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                PublishLog("NewsML", "", " ", ex.Message, "NBR Create - cannot connect to [msg]");
            }
        }
        protected void NewsML_HTMLCreate(int storyid)
        {
            MemoryStream strm = new MemoryStream();
            string m_story = (Request.Form["editor1"]);
            string x_story = ReplaceWordChars(m_story);
            m_story = x_story;
            var m_headline = this.tbHeading.Text;
            string m_id = storyid.ToString();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_timeID = m_nzdate.ToString("yyyMMddHHmmss");
            string file_now = m_timeID;
            file_now = m_id + "_HTML.xml";
            string[] lines = m_story.Replace("\r", " ").Split('\n');
            string m_embargoed = string.Empty;
            var m_reporter = "";
            if (m_reporter.Length > 3)
            {
                m_reporter = m_reporter.Substring(3, m_reporter.Length - 3);
            }
            else
            {
                m_reporter = "Staff Reporter";
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = null;
            // write the NewsML file for APN, IRG 
            writer = XmlWriter.Create(strm, settings);
            writer.WriteComment("BusinessDesk NewsML");
            writer.WriteStartElement("newsMessage", "http://iptc.org/std/nar/2006-10-01/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/nar/2006-10-01/", "http://www.iptc.org/std/NAR/1.9/specification/NewsML-G2_2.8-spec-NewsItem-Power.xsd");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.8");
            writer.WriteAttributeString("conformance", "power");
            writer.WriteAttributeString("lang", "en-US");
            // START Header
            writer.WriteStartElement("Header");
            writer.WriteStartElement("Sent");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("sender");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("origin");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("transmitId");
            writer.WriteString(m_timeID);
            writer.WriteEndElement();
            writer.WriteStartElement("priority");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("destination");
            writer.WriteString("All");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END Header
            //
            // START ItemSet
            writer.WriteStartElement("itemSet");
            // START Package Item
            writer.WriteStartElement("packageItem");
            writer.WriteStartElement("itemRef");
            writer.WriteAttributeString("residref", m_timeID);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END PackageItem
            //
            // START NewsItem
            writer.WriteStartElement("newsItem");
            writer.WriteAttributeString("guid", "BD-" + m_timeID);
            writer.WriteAttributeString("version", "3");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.7");
            writer.WriteStartElement("catalogRef");
            writer.WriteAttributeString("href", "http://www.iptc.org/std/catalog/catalog.IPTC-G2-Standards_13.xml");
            writer.WriteEndElement();
            // START RightsInfo
            writer.WriteStartElement("rightsInfo");
            writer.WriteStartElement("copyrightHolder");
            writer.WriteAttributeString("literal", "");
            writer.WriteEndElement();
            writer.WriteStartElement("copyrightNotice");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END RightsInfo
            //
            // START ItemMeta
            writer.WriteStartElement("itemMeta");
            writer.WriteStartElement("itemClass");
            writer.WriteAttributeString("qcode", "ninat:text");
            writer.WriteEndElement();
            writer.WriteStartElement("provider");
            writer.WriteAttributeString("literal", "CL");
            writer.WriteEndElement();
            writer.WriteStartElement("versionCreated");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("embargoed");
            writer.WriteString(m_embargoed);
            writer.WriteEndElement();
            writer.WriteStartElement("ednote");
            writer.WriteString("");
            writer.WriteEndElement();
            writer.WriteStartElement("pubStatus");
            writer.WriteAttributeString("qcode", "stat:usable");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END ItemMeta
            //
            // START ContentMeta
            writer.WriteStartElement("contentMeta");
            writer.WriteStartElement("urgency");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("keyword");
            writer.WriteAttributeString("role", "krole:index");
            writer.WriteString("");
            writer.WriteEndElement();
            // Taxonomy field value
            string smarket = "";
            string mqcode = "type";
            writer.WriteStartElement("subject");
            writer.WriteAttributeString(mqcode, "Topic");
            writer.WriteStartElement("name");
            writer.WriteString(smarket);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END COntentMeta
            //
            // START COntentSet
            writer.WriteStartElement("contentSet");
            // START INLINEXML
            writer.WriteStartElement("inlineXML");
            writer.WriteAttributeString("contenttype", "application/nitf+xml");
            writer.WriteStartElement("nitf");
            //writer.WriteAttributeString("xmlns", "http://iptc.org/std/NITF/2006-10-18/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/NITF/2006-10-18/", "http://www.iptc.org/std/NITF/3.5/specification/nitf-3-5.xsd");

            // HEAD
            writer.WriteStartElement("head");
            writer.WriteStartElement("title");
            writer.WriteString(m_headline);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END HEAD
            //
            // START BODY
            writer.WriteStartElement("body");
            // START BODY Head
            writer.WriteStartElement("body.head");
            writer.WriteStartElement("byline");
            writer.WriteString("By");
            writer.WriteStartElement("person");
            writer.WriteString(m_reporter);
            writer.WriteEndElement();
            writer.WriteStartElement("byttl");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("abstract");
            writer.WriteString(" ");
            writer.WriteEndElement();
            writer.WriteEndElement();
            //END Body Head
            //
            // START Body Content
            writer.WriteStartElement("body.content");
            writer.WriteString("![CDATA[");
            foreach (string str in lines)
            {
                if (str.Length > 1)
                {
                    //writer.WriteStartElement("p");
                    writer.WriteString(str);
                    //writer.WriteEndElement();
                }
            }
            writer.WriteString("]]");
            writer.WriteEndElement();
            // END Body Content                                                       
            writer.WriteEndElement();
            //END BODY 
            writer.WriteEndElement();
            // END NITF
            writer.WriteEndElement();
            // END INLINEXML                                                
            writer.WriteEndElement();
            // END CONTENTSET                                           
            writer.WriteEndElement();
            // END NewsItem                                        
            writer.WriteEndElement();
            // END ItemSet                                 
            writer.WriteEndElement();
            // END NewsMessage
            writer.Flush();
            writer.Close();
            //
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("messages");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file_now);
                strm.Position = 0;
                using (var filestream = strm)
                {
                    blockBlob.UploadFromStream(filestream);
                }
            }
            catch (Exception)
            {

                //throw;
            }
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO msg(storyid, message, datecreated, yahoo,headline)values(@storyid,@message,@datecreated,@yahoo,@headline)", con);
                cmd.Parameters.Add("@storyid", SqlDbType.Int).Value = storyid;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.Parameters.Add("@yahoo", SqlDbType.VarChar).Value = "N";
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_headline;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }


        }

        protected void News_AAPCreate(int storyid)
        {
            MemoryStream strm = new MemoryStream();
            string m_story = (Request.Form["editor1"]);
            string x_story = ReplaceWordChars(m_story);
            m_story = Regex.Replace(x_story, @"<[^>]*>", String.Empty);
            var m_headline = this.tbHeading.Text;
            string m_id = storyid.ToString();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_timeID = m_nzdate.ToString("yyyMMddHHmmss");
            string file_now = m_timeID;
            file_now = m_id + "_AAP.xml";
            string[] lines = m_story.Replace("\r", "").Split('\n');
            string m_embargoed = string.Empty;
            var m_reporter = "";
            if (m_reporter.Length > 3)
            {
                m_reporter = m_reporter.Substring(3, m_reporter.Length - 3);
            }
            else
            {
                m_reporter = "Staff Reporter";
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = null;
            // write the NewsML file  
            writer = XmlWriter.Create(strm, settings);
            writer.WriteComment("BusinessDesk NewsML");
            writer.WriteStartElement("newsMessage", "http://iptc.org/std/nar/2006-10-01/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/nar/2006-10-01/", "http://www.iptc.org/std/NAR/1.9/specification/NewsML-G2_2.8-spec-NewsItem-Power.xsd");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.8");
            writer.WriteAttributeString("conformance", "power");
            writer.WriteAttributeString("lang", "en-US");
            // START Header
            writer.WriteStartElement("Header");
            writer.WriteStartElement("Sent");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("sender");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("origin");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("transmitId");
            writer.WriteString(m_timeID);
            writer.WriteEndElement();
            writer.WriteStartElement("priority");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("destination");
            writer.WriteString("All");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END Header
            //
            // START ItemSet
            writer.WriteStartElement("itemSet");
            // START Package Item
            writer.WriteStartElement("packageItem");
            writer.WriteStartElement("itemRef");
            writer.WriteAttributeString("residref", m_timeID);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END PackageItem
            //
            // START NewsItem
            writer.WriteStartElement("newsItem");
            writer.WriteAttributeString("guid", "BD-" + m_timeID);
            writer.WriteAttributeString("version", "3");
            writer.WriteAttributeString("standard", "NewsML-G2");
            writer.WriteAttributeString("standardversion", "2.7");
            writer.WriteStartElement("catalogRef");
            writer.WriteAttributeString("href", "http://www.iptc.org/std/catalog/catalog.IPTC-G2-Standards_13.xml");
            writer.WriteEndElement();
            // START RightsInfo
            writer.WriteStartElement("rightsInfo");
            writer.WriteStartElement("copyrightHolder");
            writer.WriteAttributeString("literal", "");
            writer.WriteEndElement();
            writer.WriteStartElement("copyrightNotice");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END RightsInfo
            //
            // START ItemMeta
            writer.WriteStartElement("itemMeta");
            writer.WriteStartElement("itemClass");
            writer.WriteAttributeString("qcode", "ninat:text");
            writer.WriteEndElement();
            writer.WriteStartElement("provider");
            writer.WriteAttributeString("literal", "CL");
            writer.WriteEndElement();
            writer.WriteStartElement("versionCreated");
            writer.WriteString(m_nzdate.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("embargoed");
            writer.WriteString(m_embargoed);
            writer.WriteEndElement();
            writer.WriteStartElement("ednote");
            writer.WriteString("");
            writer.WriteEndElement();
            writer.WriteStartElement("pubStatus");
            writer.WriteAttributeString("qcode", "stat:usable");
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END ItemMeta
            //
            // START ContentMeta
            writer.WriteStartElement("contentMeta");
            writer.WriteStartElement("urgency");
            writer.WriteString("5");
            writer.WriteEndElement();
            writer.WriteStartElement("keyword");
            writer.WriteAttributeString("role", "krole:index");
            writer.WriteString("");
            writer.WriteEndElement();
            // Taxonomy field value
            string smarket = "";
            string mqcode = "type";
            writer.WriteStartElement("subject");
            writer.WriteAttributeString(mqcode, "Topic");
            writer.WriteStartElement("name");
            writer.WriteString(smarket);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END COntentMeta
            //
            // START COntentSet
            writer.WriteStartElement("contentSet");
            // START INLINEXML
            writer.WriteStartElement("inlineXML");
            writer.WriteAttributeString("contenttype", "application/nitf+xml");
            writer.WriteStartElement("nitf");
            //writer.WriteAttributeString("xmlns", "http://iptc.org/std/NITF/2006-10-18/");
            writer.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/NITF/2006-10-18/", "http://www.iptc.org/std/NITF/3.5/specification/nitf-3-5.xsd");

            // HEAD
            writer.WriteStartElement("head");
            writer.WriteStartElement("title");
            writer.WriteString(m_headline);
            writer.WriteEndElement();
            writer.WriteEndElement();
            // END HEAD
            //
            // START BODY
            writer.WriteStartElement("body");
            // START BODY Head
            writer.WriteStartElement("body.head");
            writer.WriteStartElement("byline");
            writer.WriteString("By");
            writer.WriteStartElement("person");
            writer.WriteString(m_reporter);
            writer.WriteEndElement();
            writer.WriteStartElement("byttl");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("abstract");
            writer.WriteString(" ");
            writer.WriteEndElement();
            writer.WriteEndElement();
            //END Body Head
            //
            // START Body Content
            writer.WriteStartElement("body.content");
            foreach (string str in lines)
            {
                if (str.Length > 1)
                {
                    writer.WriteStartElement("p");
                    writer.WriteString(str.Trim());
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            // END Body Content                                                       
            writer.WriteEndElement();
            //END BODY 
            writer.WriteEndElement();
            // END NITF
            writer.WriteEndElement();
            // END INLINEXML                                                
            writer.WriteEndElement();
            // END CONTENTSET                                           
            writer.WriteEndElement();
            // END NewsItem                                        
            writer.WriteEndElement();
            // END ItemSet                                 
            writer.WriteEndElement();
            // END NewsMessage
            writer.Flush();
            writer.Close();
            //
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("messages");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file_now);
                strm.Position = 0;
                using (var filestream = strm)
                {
                    blockBlob.UploadFromStream(filestream);
                }
            }
            catch (Exception)
            {

                //throw;
            }
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO msg(storyid, message, datecreated, yahoo,headline)values(@storyid,@message,@datecreated,@yahoo,@headline)", con);
                cmd.Parameters.Add("@storyid", SqlDbType.Int).Value = storyid;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.Parameters.Add("@yahoo", SqlDbType.VarChar).Value = "N";
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_headline;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }


        }

        protected void NewsML_YahooCreate(int storyid)
        {
            MemoryStream strm = new MemoryStream();
            string m_story = (Request.Form["editor1"]);
            string x_story = ReplaceWordChars(m_story);
            m_story = Server.HtmlDecode(x_story);
            var m_headline = this.tbHeading.Text;
            string m_id = storyid.ToString();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_timeID = m_nzdate.ToString("yyyMMddHHmmss");
            string file_now = m_timeID;
            string yahoo_file = m_id + "_Yahoo.xml";
            string[] lines = m_story.Replace("\r", "").Split('\n');
            string m_embargoed = string.Empty;
            var m_reporter = "";
            if (m_reporter.Length > 3)
            {
                m_reporter = m_reporter.Substring(3, m_reporter.Length - 3);
            }
            else
            {
                m_reporter = "Staff Reporter";
            }
            // write the Yahoo stream
            //
            MemoryStream yahoostrm = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writerYahoo = null;
            writerYahoo = XmlWriter.Create(yahoostrm, settings);
            // string yahoodatetime = DateTime.UtcNow.ToString("yyyyMMddTHHmmzzz");
            string yahoodatetime = DateTime.Now.ToString("yyyyMMddTHHmmzzz");
            string copyright = "\u00A9";
            writerYahoo.WriteComment("BusinessDesk NewsML");
            writerYahoo.WriteStartElement("newsMessage", "http://iptc.org/std/nar/2006-10-01/");
            writerYahoo.WriteAttributeString("xsi", "schemaLocation", "http://iptc.org/std/nar/2006-10-01/", "http://www.iptc.org/std/NAR/1.9/specification/NewsML-G2_2.8-spec-NewsItem-Power.xsd");
            writerYahoo.WriteAttributeString("standard", "NewsML-G2");
            writerYahoo.WriteAttributeString("standardversion", "2.8");
            writerYahoo.WriteAttributeString("conformance", "power");
            writerYahoo.WriteAttributeString("lang", "en-US");
            // the NewsEnvelope
            writerYahoo.WriteStartElement("NewsEnvelope");
            writerYahoo.WriteStartElement("DateAndTime");
            writerYahoo.WriteString(yahoodatetime);
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            // START NewsItem
            writerYahoo.WriteStartElement("NewsItem");
            // START IDENTIFICATION
            writerYahoo.WriteStartElement("Identification");
            // START News Identifier
            writerYahoo.WriteStartElement("NewsIdentifier");
            writerYahoo.WriteStartElement("ProviderId");
            writerYahoo.WriteString("BusinessDesk");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("DateId");
            writerYahoo.WriteString(m_nzdate.ToString("yyyyMMdd"));
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("NewsItemId");
            writerYahoo.WriteString(m_id);
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("RevisionId");
            writerYahoo.WriteAttributeString("PreviousRevision", "0");
            writerYahoo.WriteAttributeString("Update", "N");
            writerYahoo.WriteString("1");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("PublicIdentifier");
            // writerYahoo.WriteString(currentDate.ToString("yyyy-MM-dd HH:mm:ss"));
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            // END News Identifier
            //
            writerYahoo.WriteStartElement("NameLabel");
            writerYahoo.WriteString("Headline");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            // END Identification
            // START NewsManagement block
            writerYahoo.WriteStartElement("NewsManagement");
            writerYahoo.WriteStartElement("NewsItemType");
            writerYahoo.WriteAttributeString("FormalName", "News");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("FirstCreated");
            writerYahoo.WriteString(yahoodatetime);
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("ThisRevisionCreated");
            writerYahoo.WriteString(yahoodatetime);
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("Status");
            writerYahoo.WriteAttributeString("FormalName", "Usable");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            // END NewsManagement
            //
            // START NewsComponent blcok
            writerYahoo.WriteStartElement("NewsComponent");
            writerYahoo.WriteStartElement("NewsLines");
            writerYahoo.WriteStartElement("Headline");
            writerYahoo.WriteString(m_headline);
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("ByLine");
            // writerYahoo.WriteString("Headline");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("DateLine");
            writerYahoo.WriteString("Wellington");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("CopyRightLine");
            writerYahoo.WriteString(copyright + " BusinessDesk 2014");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("AdministrativeMetadata");
            writerYahoo.WriteStartElement("Provider");
            writerYahoo.WriteStartElement("Party");
            writerYahoo.WriteAttributeString("FormalName", "PROVIDER");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("DescriptiveMetadata");
            writerYahoo.WriteStartElement("Language");
            writerYahoo.WriteAttributeString("FormalName", "en");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteEndElement();
            // START ContentItem
            writerYahoo.WriteStartElement("ContentItem");
            writerYahoo.WriteStartElement("MediaType");
            writerYahoo.WriteAttributeString("FormalName", "Text");
            writerYahoo.WriteEndElement();
            writerYahoo.WriteStartElement("Format");
            writerYahoo.WriteAttributeString("FormalName", "bcNITF2.5");
            writerYahoo.WriteEndElement();
            //START Data COntent  
            writerYahoo.WriteStartElement("DataContent");
            writerYahoo.WriteString("![CDATA[");
            foreach (string str in lines)
            {
                if (str.Length > 1)
                {
                    writerYahoo.WriteStartElement("p");
                    writerYahoo.WriteString(str.Trim());
                    writerYahoo.WriteEndElement();
                }
            }
            writerYahoo.WriteString("]]");


            //string m_body2 = "";
            //foreach (string str in lines)
            //{
            //    if (str.Length > 1)
            //    {
            //        m_body2 += str + "\r\n";
            //    }
            //}
            //m_body2 = Regex.Replace(m_body2, @"<[^>]*>", String.Empty);
            //StringWriter myWriter1 = new StringWriter();
            //// Decode the encoded string.
            //HttpUtility.HtmlDecode(m_body2, myWriter1);
            //writerYahoo.WriteCData(myWriter1.ToString());
            writerYahoo.WriteEndElement();
            //END Data COntent
            writerYahoo.WriteEndElement();
            // END ContentItem
            writerYahoo.WriteEndElement();
            // END NewsComponent
            writerYahoo.WriteEndElement();
            // END NewsItem
            writerYahoo.WriteEndElement();
            // END NewsML
            writerYahoo.Flush();
            writerYahoo.Close();

            //
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("messages");
                CloudBlockBlob blockBlob_yahoo = container.GetBlockBlobReference(yahoo_file);

                yahoostrm.Position = 0;
                using (var yahoofilestream = yahoostrm)
                {
                    blockBlob_yahoo.UploadFromStream(yahoofilestream);
                }
            }
            catch (Exception)
            {
                //  throw;
            }
        }

        protected void RSSCreate(int storyid)
        {
            string m_story = (Request.Form["editor1"]);
            var m_headline = this.tbHeading.Text;
            string m_id = storyid.ToString();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_timeID = m_nzdate.ToString("yyyMMddHHmmss");
            string file_now = m_timeID;
            string RSS_File = m_id + "_RSS.xml";
            string[] lines = m_story.Replace("\r", "").Split('\n');
            string m_embargoed = string.Empty;
            var m_reporter = "";
            if (m_reporter.Length > 3)
            {
                m_reporter = m_reporter.Substring(3, m_reporter.Length - 3);
            }
            else
            {
                m_reporter = "Staff Reporter";
            }

            MemoryStream rssstrm = new MemoryStream();
            string[] rsslines = m_story.Replace("\r", "").Split('\n');
            string m_body = string.Empty;
            foreach (string str in rsslines)
            {
                if (str.Length > 1)
                {
                    m_body += "<p> ";
                    m_body += str;
                    m_body += " </p>";
                }
            }
            var currentdate = m_nzdate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlTextWriter objX = new XmlTextWriter(rssstrm, Encoding.UTF8);
            objX.WriteStartDocument();
            objX.WriteStartElement("rss");
            objX.WriteAttributeString("version", "2.0");
            objX.WriteStartElement("channel");
            objX.WriteElementString("title", "BusinessDesk");
            objX.WriteElementString("link", "http://businessdesk.co.nz/");
            objX.WriteElementString("description", "BusinessDesk News Feed");
            objX.WriteElementString("copyright", "(c) 2014, Content Ltd. All rights reserved.");
            objX.WriteElementString("ttl", "5");
            objX.WriteStartElement("item");
            objX.WriteElementString("title", m_headline);
            objX.WriteElementString("description", m_body);
            objX.WriteElementString("link", "http://www.businessdesk.co.nz/");
            objX.WriteElementString("pubDate", currentdate);
            objX.WriteEndElement();
            objX.WriteEndElement();
            objX.WriteEndElement();
            objX.WriteEndDocument();
            objX.Flush();
            objX.Close();
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("messages");
                CloudBlockBlob blockBlob_rss = container.GetBlockBlobReference(RSS_File);
                rssstrm.Position = 0;
                using (var rssfilestream = rssstrm)
                {
                    blockBlob_rss.UploadFromStream(rssfilestream);
                }
            }
            catch (Exception)
            {
                //  throw;
            }
        }
        protected void RSSCreate_BLob(int storyid)
        {
            string m_story = (Request.Form["editor1"]);
            var m_headline = this.tbHeading.Text;
            string m_id = storyid.ToString();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_timeID = m_nzdate.ToString("yyyMMddHHmmss");
            string file_now = m_timeID;
            string RSS_File = m_id + "_RSS.xml";
            string[] lines = m_story.Replace("\r", "").Split('\n');
            string m_embargoed = string.Empty;
            var m_reporter = "";
            if (m_reporter.Length > 3)
            {
                m_reporter = m_reporter.Substring(3, m_reporter.Length - 3);
            }
            else
            {
                m_reporter = "Staff Reporter";
            }

            MemoryStream rssstrm = new MemoryStream();
            string[] rsslines = m_story.Replace("\r", "").Split('\n');
            string m_body = string.Empty;
            foreach (string str in rsslines)
            {
                if (str.Length > 1)
                {
                    //m_body += "<p> ";
                    m_body += str;
                    //m_body += " </p>";
                }
            }
            var currentdate = m_nzdate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = null;
            writer = XmlWriter.Create(rssstrm, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteStartElement("channel");
            writer.WriteElementString("title", "BusinessDesk");
            writer.WriteElementString("link", "http://businessdesk.co.nz/");
            writer.WriteElementString("description", "BusinessDesk News Feed");
            writer.WriteElementString("copyright", "(c) 2014, Content Ltd. All rights reserved.");
            writer.WriteElementString("ttl", "5");
            writer.WriteStartElement("item");
            writer.WriteElementString("title", m_headline);
            writer.WriteElementString("description", m_body);
            writer.WriteElementString("link", "http://www.businessdesk.co.nz/");
            writer.WriteElementString("pubDate", currentdate);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("messages");
                CloudBlockBlob blockBlob_rss = container.GetBlockBlobReference(RSS_File);
                rssstrm.Position = 0;
                using (var rssfilestream = rssstrm)
                {
                    blockBlob_rss.UploadFromStream(rssfilestream);
                }
            }
            catch (Exception)
            {
                //  throw;
            }
        }

        protected string FTP_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_customer, string m_messagename)
        {
            // for messages NOT going to Yahoo
            // get the message from the msg file with the msgid param           
            var m_error = "False";
            var m_headline = "";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline from story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }
            string m_msg_prefix = m_msgid;
            switch (m_messagename)
            {
                case "NEWSML_HTML":
                    m_msg_prefix = m_msg_prefix + "_HTML";
                    break;
                case "NEWSML_AAP":
                    m_msg_prefix = m_msg_prefix + "_AAP";
                    break;
                case "NEWSML_NBR":
                    m_msg_prefix = m_msg_prefix + "_NBR";
                    break;
                default:
                    break;
            }
            string m_msg_file = m_msg_prefix + ".xml";
            //string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            //string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                string file_now = m_msgid + ".xml";
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + file_now);
                requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                Stream uploadStream = requestFTPUploader.GetRequestStream();
                memoryStream.Position = 0;
                int contentLength = memoryStream.Read(buffer, 0, bufferLength);
                try
                {
                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = memoryStream.Read(buffer, 0, bufferLength);
                    }
                    uploadStream.Close();
                    FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
                    PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription, "Publish OK");
                    requestFTPUploader = null;
                }
                catch (WebException ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }
                return m_error;
            }

        }
        protected string YAHOO_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password)
        {
            // for messages going to Yahoo
            // get the message from the msg file with the msgid param           
            var m_headline = "";
            var m_error = "False";
            string m_customer = "YAHOO";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline from story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            string m_msg_file_yahoo = m_msgid + "_Yahoo.xml";

            try
            {
                //String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                // get the story from the blob storage
                try
                {
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
                    //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("messages");
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file_yahoo);
                    using (var memoryStream = new MemoryStream())
                    {
                        blockBlob.DownloadToStream(memoryStream);
                        //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());                       
                        string file_now = m_nzdate.ToString("yyyMMddHHmmss") + "-" + m_msgid + ".xml";
                        FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + file_now);
                        requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                        requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

                        int bufferLength = 2048;
                        byte[] buffer = new byte[bufferLength];
                        Stream uploadStream = requestFTPUploader.GetRequestStream();
                        memoryStream.Position = 0;
                        int contentLength = memoryStream.Read(buffer, 0, bufferLength);
                        try
                        {
                            while (contentLength != 0)
                            {
                                uploadStream.Write(buffer, 0, contentLength);
                                contentLength = memoryStream.Read(buffer, 0, bufferLength);
                            }
                            uploadStream.Close();
                            FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();

                            PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription, "Publish OK");
                            requestFTPUploader = null;
                        }
                        catch (WebException ex)
                        {
                            m_error = "True";
                            PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                        }

                    }
                }
                catch (Exception ex)
                {

                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }
            }
            catch (Exception ex)
            {

                m_error = "True";
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }
            return m_error;
        }
        protected void APN_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_sftp_path, string m_messagename)
        {
            var m_headline = "";
            string m_customer = "APN";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline from story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }
            //PublishLog(m_msgid, m_headline, m_customer, "APN Step 1", "Publish OK");
            string m_msg_file = m_msgid + ".xml";
            string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);
            string pathToFiles = Server.MapPath("/Stories/");

            string m_File = pathToFiles + m_msg_file;
            //PublishLog(m_msgid, m_headline, m_customer, "APN Step 2", "Publish OK");
            try
            {
                using (var fileStream = File.OpenWrite(m_File))
                {
                    blockBlob.DownloadToStream(fileStream);
                }
            }
            catch (Exception ex)
            {
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }

            try
            {
                SshTransferProtocolBase sshCpAPN;
                sshCpAPN = new Sftp("ftp1.apnz.co.nz", "ContentLtd");
                sshCpAPN.Password = "c0nt3nt";
                sshCpAPN.Connect();
                string lfile = m_File;
                string rfile = "/incoming/" + m_msg_file;
                sshCpAPN.Put(lfile, rfile);
                sshCpAPN.Close();
                PublishLog(m_msgid, m_headline, m_customer, "APN SFTP Pub OK", "Publish OK");
            }
            catch (WebException ex)
            {
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }

        }
        public void ShareChat(int msgid)
        {
            // check to see if the Sharechat feed is active
            // if Yes, create the RSS record and send it out
            string m_headline = "";
            string m_story = "";
            string m_active = "";
            string m_customer = "ShareChat";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline,body from story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                    m_story = (dr["body"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }

            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select active from customermessage where customerid = '5' and active = 'Y'";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_active = (dr["active"].ToString());
                    TimeZoneInfo timeZoneInfo;
                    //Set the time zone information to New Zealand Standard Time 
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
                    //Get date and time in New Zealand Standard Time
                    var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);

                    string m_RSS_File = m_nzdate.ToString("yyyMMddHHmmss") + "-" + m_msgid + "RSS.xml";
                    string pathToFiles = Server.MapPath("/Stories/");

                    string RSS_FilePath = Path.Combine(pathToFiles, m_RSS_File);
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;

                    string[] lines = m_story.Replace("\r", "").Split('\n');
                    string m_embargoed = string.Empty;
                    m_headline = m_headline.Replace("\r", string.Empty);

                    // write the RSS file for ShareChat    
                    //                   
                    string[] rsslines = m_story.Replace("\r", "").Split('\n');
                    string m_body = string.Empty;
                    foreach (string str in rsslines)
                    {
                        if (str.Length > 1)
                        {
                            m_body += "<p> ";
                            m_body += str;
                            m_body += " </p>";
                        }
                    }
                    var currentdate = m_nzdate.ToString("yyyy-MM-dd HH:mm:ss");
                    XmlTextWriter objX = new XmlTextWriter(RSS_FilePath, Encoding.UTF8);
                    objX.WriteStartDocument();
                    objX.WriteStartElement("rss");
                    objX.WriteAttributeString("version", "2.0");
                    objX.WriteStartElement("channel");
                    objX.WriteElementString("title", "BusinessDesk");
                    objX.WriteElementString("link", "http://businessdesk.co.nz/");
                    objX.WriteElementString("description", "BusinessDesk News Feed");
                    objX.WriteElementString("copyright", "(c) 2014, Content Ltd. All rights reserved.");
                    objX.WriteElementString("ttl", "5");
                    objX.WriteStartElement("item");
                    objX.WriteElementString("title", m_headline);
                    objX.WriteElementString("description", m_body);
                    objX.WriteElementString("link", "http://www.businessdesk.co.nz/");
                    objX.WriteElementString("pubDate", currentdate);
                    objX.WriteEndElement();
                    objX.WriteEndElement();
                    objX.WriteEndElement();
                    objX.WriteEndDocument();
                    objX.Flush();
                    objX.Close();
                    try
                    {

                        // Send message to ShareChat using SFTP
                        //
                        SshTransferProtocolBase sshCp;
                        sshCp = new Sftp("www.sharechat.co.nz", "root");
                        sshCp.Password = "NTGaf9eA";
                        sshCp.Connect();
                        string lfile = RSS_FilePath;
                        string rfile = "/root/BusinessDesk/" + m_RSS_File;
                        sshCp.Put(lfile, rfile);
                        sshCp.Close();
                        PublishLog(m_msgid, m_headline, m_customer, "SFTP ShareChat Message", "Publish OK");

                    }
                    catch (Exception ex)
                    {
                        PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                    }
                }
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }

        }

        protected void BusinessDesk_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_customer, string m_messagename)
        {
            // Sedn all message formats to the BusinessDesk FTP site
            // get the message from the msg file with the msgid param           
            var m_error = "False";
            var m_headline = "";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and get the headline so it can be put in the log later.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline from story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }

            string m_msg_file = m_msgid + ".xml";
            //string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            //string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                string file_now = m_msgid + ".xml";
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + file_now);
                requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                Stream uploadStream = requestFTPUploader.GetRequestStream();
                memoryStream.Position = 0;
                int contentLength = memoryStream.Read(buffer, 0, bufferLength);
                try
                {
                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = memoryStream.Read(buffer, 0, bufferLength);
                    }
                    uploadStream.Close();
                    FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
                    PublishLog(m_msgid, m_headline, "BusDesk NewsML", response.StatusDescription, "Publish OK");
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }

            }
            m_msg_file = m_msgid + "_HTML.xml";
            CloudBlockBlob blockBlob1 = container.GetBlockBlobReference(m_msg_file);

            using (var memoryStream1 = new MemoryStream())
            {
                blockBlob1.DownloadToStream(memoryStream1);
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                string file_now = m_msgid + "_HTML.xml";
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + file_now);
                requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                Stream uploadStream = requestFTPUploader.GetRequestStream();
                memoryStream1.Position = 0;
                int contentLength = memoryStream1.Read(buffer, 0, bufferLength);
                try
                {
                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = memoryStream1.Read(buffer, 0, bufferLength);
                    }
                    uploadStream.Close();
                    FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
                    PublishLog(m_msgid, m_headline, "BusDesk HTML", response.StatusDescription, "Publish OK");
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }

            }
            m_msg_file = m_msgid + "_RSS.xml";
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference(m_msg_file);
            using (var memoryStream2 = new MemoryStream())
            {
                blockBlob2.DownloadToStream(memoryStream2);
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                string file_now = m_msgid + "_RSS.xml";
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + file_now);
                requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                Stream uploadStream = requestFTPUploader.GetRequestStream();
                memoryStream2.Position = 0;
                int contentLength = memoryStream2.Read(buffer, 0, bufferLength);
                try
                {
                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = memoryStream2.Read(buffer, 0, bufferLength);
                    }
                    uploadStream.Close();
                    FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
                    PublishLog(m_msgid, m_headline, "BusDesk RSS", response.StatusDescription, "Publish OK");
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }

            }
            m_msg_file = m_msgid + "_Yahoo.xml";
            CloudBlockBlob blockBlob3 = container.GetBlockBlobReference(m_msg_file);
            using (var memoryStream3 = new MemoryStream())
            {
                blockBlob3.DownloadToStream(memoryStream3);
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                string file_now = m_msgid + "_Yahoo.xml";
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + file_now);
                requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                Stream uploadStream = requestFTPUploader.GetRequestStream();
                memoryStream3.Position = 0;
                int contentLength = memoryStream3.Read(buffer, 0, bufferLength);
                try
                {
                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = memoryStream3.Read(buffer, 0, bufferLength);
                    }
                    uploadStream.Close();
                    FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
                    PublishLog(m_msgid, m_headline, "Bus Desk Yahoo", response.StatusDescription, "Publish OK");
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }

            }
            m_msg_file = m_msgid + "_NBR.xml";
            CloudBlockBlob blockBlob4 = container.GetBlockBlobReference(m_msg_file);
            using (var memoryStream4 = new MemoryStream())
            {
                blockBlob4.DownloadToStream(memoryStream4);
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());                
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + m_msg_file);
                requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                Stream uploadStream = requestFTPUploader.GetRequestStream();
                memoryStream4.Position = 0;
                int contentLength = memoryStream4.Read(buffer, 0, bufferLength);
                try
                {
                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = memoryStream4.Read(buffer, 0, bufferLength);
                    }
                    uploadStream.Close();
                    FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
                    PublishLog(m_msgid, m_headline, "Bus Desk NBR", response.StatusDescription, "Publish OK");
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }

            }

            m_msg_file = m_msgid + "_AAP.xml";
            CloudBlockBlob blockBlob5 = container.GetBlockBlobReference(m_msg_file);
            using (var memoryStream5 = new MemoryStream())
            {
                blockBlob5.DownloadToStream(memoryStream5);
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());                
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(m_ftp_host + m_msg_file);
                requestFTPUploader.Credentials = new NetworkCredential(m_ftp_username, m_ftp_password);
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                Stream uploadStream = requestFTPUploader.GetRequestStream();
                memoryStream5.Position = 0;
                int contentLength = memoryStream5.Read(buffer, 0, bufferLength);
                try
                {
                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = memoryStream5.Read(buffer, 0, bufferLength);
                    }
                    uploadStream.Close();
                    FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
                    PublishLog(m_msgid, m_headline, "Bus Desk AAP", response.StatusDescription, "Publish OK");
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
                }
            }
        }
        protected void NBR_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_sftp_path, string m_messagename)
        {
            var m_headline = "";

            string m_customer = "NBR";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline from story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }
            //PublishLog(m_msgid, m_headline, m_customer, "NBR NEW SFTP Step 1", "Step 1 OK");



            string m_msg_file = m_msgid + "_NBR.xml";
            string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);
            string pathToFiles = Server.MapPath("/Stories/");

            string m_File = pathToFiles + m_msg_file;


            //PublishLog(m_msgid, m_File, m_customer, "NBR Before Blob Download", "Blob ");

            try
            {
                using (var fileStream = File.OpenWrite(m_File))
                {
                    blockBlob.DownloadToStream(fileStream);
                }
            }
            catch (Exception ex)
            {
                PublishLog(m_msgid, "Blob Error", m_customer, ex.Message, "Blob");
            }
            //PublishLog(m_msgid, m_File, m_customer, "NBR After Blob Download", "Blob");

            string host = "sftp.nbr.co.nz";
            string username = "businessdesk";
            string password = "rokEpwud1";
            var proxy_host = "us-east-1-static-brooks.quotaguard.com";
            var proxy_username = "quotaguard1890";
            var proxy_password = "d85e2cc306c0";
            int port = 22;
            ConnectionInfo infoConnection = new ConnectionInfo(host, port, username, ProxyTypes.Socks5, proxy_host, 1080, proxy_username, proxy_password, new PasswordAuthenticationMethod(username, password));
            //PublishLog(m_msgid, m_headline, m_customer, "NBR NEW SFTP con init OK", "Init OK");
            try
            {
                SftpClient client = new SftpClient(infoConnection);
                client.Connect();
                // PublishLog(m_msgid, m_headline, m_customer, "NBR NEW SFTP Connx OK", "Connx OK");

                using (var fileStream = new FileStream(m_File, FileMode.Open))
                {

                    client.UploadFile(fileStream, m_msg_file, true);

                }
                PublishLog(m_msgid, m_headline, m_customer, "NBR NEW SFTP Pub OK", "Publish OK");

            }
            catch (WebException ex)
            {
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "NBR Pub Error");
            }

        }

        protected void TechDay2_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_sftp_path, string m_messagename)
        {
            var m_headline = "";

            string m_customer = "TechDay";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline from story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }
            //PublishLog(m_msgid, m_headline, m_customer, "APN Step 1", "Publish OK");
            string m_msg_file = m_msgid + ".xml";
            string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);
            string pathToFiles = Server.MapPath("/Stories/");

            string m_File = pathToFiles + m_msg_file;
            //PublishLog(m_msgid, m_headline, m_customer, "TechDay Step 2", "Publish OK");
            try
            {
                using (var fileStream = File.OpenWrite(m_File))
                {
                    blockBlob.DownloadToStream(fileStream);
                }
            }
            catch (Exception ex)
            {
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }

            try
            {
                SshTransferProtocolBase sshCpTech;
                sshCpTech = new Sftp("sv3.techday.com", "businessdesk");
                sshCpTech.Password = "KOAlsApv8Cg61yM";
                sshCpTech.Connect(31822);
                string lfile = m_File;
                string rfile = "/businessdesk/" + m_msg_file;
                sshCpTech.Put(lfile, rfile);
                sshCpTech.Close();
                PublishLog(m_msgid, m_headline, m_customer, "TechDay2 Pub OK", "Publish OK");
            }
            catch (Exception ex)
            {
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error");
            }

        }
        protected void Email_Send(int msgid)
        {
            //TODO
            // check the control table - cmscontrol - and get email_active value (Y for email on. N for off)
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            string m_active = "";
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL_control = "Select email_active from cmscontrol";
                SqlCommand cmd = new SqlCommand(strSQL_control, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    m_active = (dr["email_active"].ToString());
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
            if (m_active == "Y")
            {
                string m_storyid = msgid.ToString();
                string m_headline = "";
                string m_body = "";
                StringBuilder sb = new StringBuilder();
                string emailout = string.Empty;
                // the new bcc segment
                //String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    string strSQL = "Select body,headline from story where storyid = " + m_storyid;
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        m_body = (dr["body"].ToString());
                        m_headline = (dr["headline"].ToString());
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
                List<String> bcc = new List<string>();
                using (SqlConnection conx = new SqlConnection(connectionString))
                {
                    conx.Open();
                    string query = "Select email from customercontact where active = 'Y' OR active = 'Yes'";
                    SqlCommand command = new SqlCommand(query, conx);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            emailout = (string)reader.GetString(0);
                            sb.Append(emailout).Append(",");
                        }
                    }
                    conx.Close();
                }
                SendEmail(sb.ToString(), m_headline, m_body);
            }
        }
        public void SendEmail(string emailAddress, string subject, string body)
        {
            var smtpUserName = "editor@businessdesk.co.nz";
            var smtpPassword = "BusinessDesk500";
            //smtpServer = "hknprd0410.outlook.com";
            var smtpServer = "smtp.office365.com";
            var smtpPort = "587";
            // var fromEmailAddress = "editor@businessdesk.co.nz";
            // var toEmailAddress = "newz@xtra.co.nz";
            // set the right time
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            // put the story into an array so we can find the paragraph markers
            char[] delimiterChars = { ',' };
            string[] bccEmailAddress = emailAddress.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            string[] lines = body.Replace("\r", "").Split('\n');
            // build up the template
            string m_body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            m_body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            m_body += "</HEAD><BODY><DIV>";
            //m_body += "<p> <img alt=\" \" src= \"http://www.businessdesk.co.nz/images/header-object.png \" /></p>";
            m_body += "<p style= \"font-size: small;color: #999999\">" + m_nzdate.ToString("dddd dd MMMM yyyy hh:mm tt") + "</p>";
            m_body += "<p><strong style=\"font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #0099FF\">";
            m_body += subject;
            m_body += "</strong></p>";
            m_body += "<p style= \"font-size: small\">";
            m_body += "</p>";
            foreach (string str in lines)
            {
                if (str.Length > 1)
                {
                    m_body += "<p> " + str + "</p>";
                }
            }
            m_body += "</DIV></BODY></HTML>";
            // send the message to the mail server
            var msg = new MailMessage();
            //msg.To.Add("newz@xtra.co.nz");
            msg.From = new MailAddress("editor@businessdesk.co.nz");
            msg.Subject = subject;
            msg.Body = m_body;
            msg.IsBodyHtml = true;
            msg.BodyEncoding = Encoding.UTF8;
            foreach (string s in bccEmailAddress)
            {
                // ignore any blank addresses - it screws up the mailer.
                if (s != "")
                {
                    msg.Bcc.Add(s);
                }
            }
            var smtpClient = new SmtpClient(smtpServer, int.Parse(smtpPort))
            {
                Credentials = new NetworkCredential(smtpUserName, smtpPassword),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                PublishLog(this.lbStoryID.Text, subject, "Email List", ex.Message, "Email Error");
            }
        }
        protected void SendMessage(int msgid)
        {
            // get the message recipient details from the customer table and send out messages.
            string m_ftp_host = "";
            string m_ftp_password = "";
            string m_ftp_username = "";
            string m_messagetype = "";
            string m_messagename = "";
            string m_customerid = "";
            string m_sftp_path = "";
            var i_customerid = 0;
            string m_customer = "";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select cm.customerid,c.name,ftp_host, ftp_password,ftp_username,messagetype,messagename,sftp_path from customermessage cm, customer c where cm.customerid = c.customerid and cm.active = 'Y'";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_customerid = (dr["customerid"].ToString());
                    m_customer = (dr["name"].ToString());
                    m_ftp_host = (dr["ftp_host"].ToString());
                    m_ftp_password = (dr["ftp_password"].ToString());
                    m_ftp_username = (dr["ftp_username"].ToString());
                    m_sftp_path = (dr["sftp_path"].ToString());
                    m_messagetype = (dr["messagetype"].ToString());
                    m_messagename = (dr["messagename"].ToString());
                    i_customerid = Convert.ToInt16(m_customerid);
                    switch (m_messagetype)
                    {
                        case "BusinessDesk":
                            try
                            {
                                BusinessDesk_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            }
                            catch (WebException e)
                            {
                                PublishLog(m_msgid, "", m_customer, e.Message, "Error on Host Connection");
                            }
                            catch (Exception ex)
                            {
                                PublishLog(m_msgid, "", m_customer, ex.Message, "Error on Host Connection");
                            }
                            break;
                        case "FTP":
                            try
                            {
                                FTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            }
                            catch (WebException e)
                            {
                                PublishLog(m_msgid, "", m_customer, e.Message, "Error on Host Connection");
                            }
                            catch (Exception ex)
                            {
                                PublishLog(m_msgid, "", m_customer, ex.Message, "Error on Host Connection");
                            }
                            break;
                        case "SFTP":
                            try
                            {
                                APN_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            }
                            catch (WebException e)
                            {
                                PublishLog(m_msgid, "", m_customer, e.Message, "Error on Host Connection");
                            }
                            catch (Exception ex)
                            {
                                PublishLog(m_msgid, "", m_customer, ex.Message, "Error on Host Connection");
                            }
                            break;
                        case "NBR":
                            try
                            {
                                NBR_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            }
                            catch (WebException e)
                            {
                                PublishLog(m_msgid, "", m_customer, e.Message, "Error on Host Connection");
                            }
                            catch (Exception ex)
                            {
                                PublishLog(m_msgid, "", m_customer, ex.Message, "Error on Host Connection");
                            }
                            break;                       
                        case "YAHOO":
                            try
                            {
                                YAHOO_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password);
                            }
                            catch (WebException e)
                            {
                                PublishLog(m_msgid, "", m_customer, e.Message, "Error on Host Connection");
                            }
                            catch (Exception ex)
                            {
                                PublishLog(m_msgid, "", m_customer, ex.Message, "Error on Host Connection");
                            }
                            break;
                        case "RSS":
                            try
                            {
                                ShareChat(msgid);
                            }
                            catch (WebException e)
                            {
                                PublishLog(m_msgid, "", m_customer, e.Message, "Error on Host Connection");
                            }
                            catch (Exception ex)
                            {
                                PublishLog(m_msgid, "", m_customer, ex.Message, "Error on Host Connection");
                            }
                            break;
                        default:
                            break;
                    }
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
                PublishLog(m_msgid, "", m_customer, ex.Message, "Error Selecting Customer");
            }
        }
        protected void SendCustomDelivery(int msgid)
        {
            // get the message recipient details from the customer table and send out messages.
            string m_ftp_host = "";
            string m_ftp_password = "";
            string m_ftp_username = "";
            string m_messagetype = "";
            string m_messagename = "";
            string m_customerid = "";
            string m_sftp_path = "";
            var i_customerid = 0;
            string m_customer = "";
            var m_msgid = msgid.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                // this gets the customers who have an active messaging flag set.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select cd.customerid, cd.storyid, c.name, cm.ftp_host, cm.ftp_password, cm.ftp_username, cm.messagetype, cm.messagename, cm.sftp_path from customdelivery cd, customer c, customermessage cm where cd.customerid = c.customerid and cd.customerid = cm.customerid and cm.active = 'Y' and cd.storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_customerid = (dr["customerid"].ToString());
                    m_customer = (dr["name"].ToString());
                    m_ftp_host = (dr["ftp_host"].ToString());
                    m_ftp_password = (dr["ftp_password"].ToString());
                    m_ftp_username = (dr["ftp_username"].ToString());
                    m_sftp_path = (dr["sftp_path"].ToString());
                    m_messagetype = (dr["messagetype"].ToString());
                    m_messagename = (dr["messagename"].ToString());
                    i_customerid = Convert.ToInt16(m_customerid);
                    switch (m_messagetype)
                    {
                        case "BusinessDesk":
                            BusinessDesk_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            continue;
                        case "FTP":
                            FTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            continue;
                        case "SFTP":
                            APN_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            continue;
                        case "NBR":
                            NBR_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            continue;
                        case "TechDay":
                            TechDay2_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_messagename);
                            continue;
                        case "YAHOO":
                            YAHOO_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password);
                            continue;
                        case "RSS":
                            ShareChat(msgid);
                            continue;
                        default:
                            continue;
                    }
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.                
                PublishLog(m_msgid, "", m_customer, ex.Message, "Error Selecting Customer in CustomDelivery");
            }
        }
        public static void PublishLog(string p_story_id, string p_headline, string p_customer, string p_status, string p_story_status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO MsgLog(storyID,headline,Customer,MsgStatus,datecreated,status,DatePublished_NZ)values(@storyid,@headline,@customer,@msgstatus,@datecreated,@status,@datepublished_nz)", con);

                cmd.Parameters.Add("@storyid", SqlDbType.NVarChar).Value = p_story_id;
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = p_headline;
                cmd.Parameters.Add("@customer", SqlDbType.NVarChar).Value = p_customer;
                cmd.Parameters.Add("@msgstatus", SqlDbType.NVarChar).Value = p_status;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = p_story_status;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.Parameters.Add("@datepublished_nz", SqlDbType.DateTime).Value = m_nzdate;

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
        }
        public static void ErrorLog(string p_story_id, string p_headline, string p_customer, string p_status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ErrorLog(storyID,headline,Customer,MsgStatus,datecreated)values(@storyid,@headline,@customer,@status,@datecreated)", con);

                cmd.Parameters.Add("@storyid", SqlDbType.NVarChar).Value = p_story_id;
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = p_headline;
                cmd.Parameters.Add("@customer", SqlDbType.NVarChar).Value = p_customer;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = p_status;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
        }
        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        public static string ReplaceWordChars(string text)
        {
            var s = text;
            // smart single quotes and apostrophe 
            s = Regex.Replace(s, "[\u2018|\u2019|\u201A]", "'");
            // smart double quotes
            s = Regex.Replace(s, "[\u201C|\u201D|\u201E]", "\"");
            // ellipsis 
            s = Regex.Replace(s, "\u2026", "...");
            // dashes
            s = Regex.Replace(s, "[\u2013|\u2014]", "-");
            // circumflex 
            s = Regex.Replace(s, "\u02C6", "^");
            // open angle bracket 
            s = Regex.Replace(s, "\u2039", "<");
            // close angle bracket 
            s = Regex.Replace(s, "\u203A", ">");
            // spaces 
            s = Regex.Replace(s, "[\u02DC|\u00A0]", " ");
            return s;
        }


    }
}