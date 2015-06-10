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


namespace BD_CMS2
{
    public partial class ReviewQEdit1 : System.Web.UI.Page
    {
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
                        this.CKStory.Text = HttpUtility.HtmlDecode(dr["body"].ToString());
                        this.tags.Text = (dr["topic"].ToString());
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
            var m_story = this.CKStory.Text;
            var m_heading = this.tbHeading.Text;
            int m_story_id = Convert.ToInt32(this.lbStoryID.Text);
            var m_reviewaction = this.ddlReviewAction.SelectedValue;
            var m_reviewaction_flag = "N";
            DateTime m_datetopublish = DateTime.MinValue;
            m_datetopublish = m_datetopublish.AddYears(1800);
            var m_status = "Rejected";
            if (m_reviewaction == "Publish")
            {
                m_reviewaction_flag = "Y";
                m_datetopublish = DateTime.UtcNow;
                m_status = "Published";
            }
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE story set body = @body, headline = @headline, sendtoreview = @sendtoreview, status = @status, sendtopublish = @sendtopublish, datepublished = @datetopublish where storyid = @storyid", con);
                cmd.Parameters.AddWithValue("@body", m_story);
                cmd.Parameters.AddWithValue("@headline", m_heading);
                cmd.Parameters.AddWithValue("@storyid", m_story_id);
                cmd.Parameters.AddWithValue("@sendtoreview", "N");
                cmd.Parameters.AddWithValue("@sendtopublish", m_reviewaction_flag);
                cmd.Parameters.AddWithValue("@status", m_status);
                cmd.Parameters.AddWithValue("@datetopublish", m_datetopublish);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            // send the story to the Public website
            connectionString = ConfigurationManager.ConnectionStrings["BDPublicConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO story(body, headline,datepublished)values(@body,@headline,@datepublished)", con);

                cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_heading;
                cmd.Parameters.Add("@datepublished", SqlDbType.DateTime).Value = System.DateTime.UtcNow;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            NewsMLCreate(m_story_id);
            this.CKStory.Text = "";
            this.tbHeading.Text = "";
            
            SendMessage(m_story_id);
            Email_Send(m_story_id);
            Response.Redirect("dashboard.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }

        protected void NewsMLCreate(int storyid)
        {
            MemoryStream strm = new MemoryStream();
            var m_story = this.CKStory.Text;
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
            file_now = m_id + ".xml";
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
                CloudBlockBlob blockBlob_yahoo = container.GetBlockBlobReference(yahoo_file);
                CloudBlockBlob blockBlob_RSS = container.GetBlockBlobReference(RSS_File);
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
            // write the Yahoo stream
            //
            MemoryStream yahoostrm = new MemoryStream();
            settings.Indent = true;
            XmlWriter writerYahoo = null;
            writerYahoo = XmlWriter.Create(yahoostrm, settings);
            string yahoodatetime = m_nzdate.ToString("s" + "Z");
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
            writerYahoo.WriteString(copyright + " BusinessDesk 2012");
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
            writerYahoo.WriteString("<![CDATA[");
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
            objX.WriteElementString("copyright", "(c) 2012, Content Ltd. All rights reserved.");
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
                yahoostrm.Position = 0;
                using (var rssfilestream = rssstrm)
                {
                    blockBlob_rss.UploadFromStream(rssfilestream);
                }
            }
            catch (Exception)
            {
                //  throw;
            }
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO msg(storyid, message, datecreated, yahoo)values(@storyid,@message,@datecreated,@yahoo)", con);
                cmd.Parameters.Add("@storyid", SqlDbType.Int).Value = storyid;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = m_nzdate;
                cmd.Parameters.Add("@yahoo", SqlDbType.VarChar).Value = "Y";
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }
        }

        protected void FTP_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_customer)
        {
            // for messages NOT going to Yahoo
            // get the message from the msg file with the msgid param           
            var m_headline = "";
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
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            var m_msgid = msgid.ToString();
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
                    
                    PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription);
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {

                    PublishLog(m_msgid, m_headline, m_customer, ex.Message);
                }
            }

        }
        protected void YAHOO_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password)
        {
            // for messages going to Yahoo
            // get the message from the msg file with the msgid param           
            var m_headline = "";
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
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            var m_msgid = msgid.ToString();
            string m_msg_file_yahoo = m_msgid + "_Yahoo.xml";
            string m_customer = "YAHOO";
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

                            PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription);
                            requestFTPUploader = null;
                        }
                        catch (Exception ex)
                        {

                            PublishLog(m_msgid, m_headline, m_customer, ex.Message);
                        }
                    }
                }
                catch (Exception)
                {
                    //  throw;
                }
            }
            catch (Exception ex)
            {
                //LogError("Yahoo", ex.Message);
            }
        }
        protected void APN_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_sftp_path)
        {
            var m_msgid = msgid.ToString();
            string m_msg_file = m_msgid + ".xml";
            string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);
            string pathToFiles = Server.MapPath("/Stories/");

            string m_File = pathToFiles + m_msg_file;
            using (var fileStream = File.OpenWrite(m_File))
            {
                blockBlob.DownloadToStream(fileStream);

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
            }
            catch (Exception ex)
            {
               
                //LogError("SFTP - Sharechat", ex.Message);
            }

        }
        public void ShareChat(int storyid)
        {
            // check to see if the Sharechat feed is active
            // if Yes, create the RSS record and send it out
            string m_headline = "";
            string m_story = "";
            string m_active = "";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline,body from story where storyid = " + storyid;
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
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
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

                    var m_id = Convert.ToString(storyid);
                    
                    string m_RSS_File = m_nzdate.ToString("yyyMMddHHmmss") + "-" + m_id + "RSS.xml";
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
                    objX.WriteElementString("copyright", "(c) 2012, Content Ltd. All rights reserved.");
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
                      
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }

        }
        protected void Email_Send(int msgid)
        {
            string m_storyid = msgid.ToString();
            string m_headline = "";
            string m_body = "";
            StringBuilder sb = new StringBuilder();
            string emailout = string.Empty;
            // the new bcc segment
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
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
        public void SendEmail(string emailAddress, string subject, string body)
        {
            var smtpUserName = "editor@businessdesk.co.nz";
            var smtpPassword = "BusinessDesk500";
            //smtpServer = "hknprd0410.outlook.com";
            var smtpServer = "smtp.office365.com";
            var smtpPort = "587";
            var fromEmailAddress = "editor@businessdesk.co.nz";
            var toEmailAddress = "newz@xtra.co.nz";
            // set the right time
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            // put the story into an array so we can find the paragraph markers
            char[] delimiterChars = { ',' };
            string[] bccEmailAddress = emailAddress.Split(delimiterChars);
            string[] lines = body.Replace("\r", "").Split('\n');
            // build up the template
            string m_body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            m_body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            m_body += "</HEAD><BODY><DIV>";
            m_body += "<p> <img alt=\" \" src= \"http://www.businessdesk.co.nz/images/header-object.png \" /></p>";
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

            }

        }

        protected void SendMessage(int msgid)
        {
            // get the message recipient details from the customer table and send out messages.
            string m_ftp_host = "";
            string m_ftp_password = "";
            string m_ftp_username = "";
            string m_messagetype = "";
            string m_customerid = "";
            string m_sftp_path = "";
            var i_customerid = 0;
            string m_customer = "";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select cm.customerid,c.name,ftp_host, ftp_password,ftp_username,messagetype,sftp_path from customermessage cm, customer c where cm.customerid = c.customerid and cm.active = 'Y'";
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
                    i_customerid = Convert.ToInt16(m_customerid);
                    switch (m_messagetype)
                    {
                        case "FTP":
                            FTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password,m_customer);
                            break;
                        case "SFTP":
                            APN_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer);
                            break;

                        case "YAHOO":
                            // send to Yahoo
                            YAHOO_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password);
                            break;
                        case "RSS":
                            ShareChat(msgid);
                            break;
                        default:
                            break;
                    }
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
        public static void PublishLog(string p_story_id, string p_headline, string p_customer, string p_status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO MsgLog(storyID,headline,Customer,MsgStatus,datecreated)values(@storyid,@headline,@customer,@status,@datecreated)", con);

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
       
    }
}