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
    public partial class ErrorDetail : System.Web.UI.Page
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
                    string strSQL = "Select body, headline,datepublished from story where storyid = " + m_storyid;
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        this.lbHeadline.Text = (dr["headline"].ToString());
                        this.tbStory.Text = HttpUtility.HtmlDecode(dr["body"].ToString());
                        this.lbPublishedDate.Text = (dr["datepublished"].ToString());
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

        }
        protected void gvErrorQ_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = gvErrorQ.SelectedRow;
            var m_id = row.Cells[3].Text;
            var m_name = row.Cells[6].Text;
            var m_msglogid = row.Cells[2].Text;
            string m_ftp_host = "";
            string m_ftp_password = "";
            string m_ftp_username = "";
            string m_messagetype = "";
            string m_customerid = "";
            string m_sftp_path = "";
            var i_customerid = 0;
            string m_customer = "";
            var msgid = Convert.ToInt16(m_id);
            //Response.Redirect("ErrorDetail.aspx?storyid=" + m_id);
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select cm.customerid,c.name,ftp_host, ftp_password,ftp_username,messagetype,sftp_path from customermessage cm, customer c where cm.customerid = c.customerid and c.name = '" + m_name +"'";
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
                            FTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_msglogid);
                            break;
                        case "SFTP":
                            APN_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer, m_msglogid);
                            break;

                        case "YAHOO":
                            // send to Yahoo
                            YAHOO_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_msglogid);
                            break;
                        case "RSS":
                            ShareChat(msgid, m_msglogid);
                            break;
                        default:
                            break;
                    }
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
                gvErrorQ.DataBind();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
        }
        protected void gvErrorQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var data = (DataRowView)e.Row.DataItem;
                string date = data["DateCreated"].ToString();
                if (date != null)
                {
                    e.Row.Cells[8].Text = GetNZ(date);
                }
                DateTime dtcurrentDateToReview = DateTime.Parse(date);
                DateTime m_NZ_Now = DateTimeStuff.GetNZDateTimefromUTC(DateTime.UtcNow);
                string status = data["Status"].ToString();
                
                if (status == "Publish OK")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                }
                else if (status == "Pub Error")
                {
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                }
                else if (status == "Retry")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Orange;
                }
                else if (status == "")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                }
                else if (status == "Reset")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        protected void gvErrorQ_RowCommand(Object sender, GridViewCommandEventArgs e)
        {                        
            if (e.CommandName == "Ignore")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);
                // Get the msglogid
                GridViewRow selectedRow = gvErrorQ.Rows[index];
                TableCell logid = selectedRow.Cells[2];
                var m_msglogid = logid.Text;
                var connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE MsgLog set status = @status where msglogid = @msglogid", con);

                    cmd.Parameters.Add("@msglogid", SqlDbType.NVarChar).Value = m_msglogid;
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = "Reset";

                    cmd.ExecuteNonQuery();
                    con.Close();
                    gvErrorQ.DataBind();
                }
                catch (Exception )
                {
                    // The connection failed. Display an error message.
                    //Message.Text = "Unable to connect to the database.";
                }  
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
        protected string FTP_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_customer, string m_msglogid)
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
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
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
                    PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription, "Publish OK", m_msglogid);
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {
                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
                }
                return m_error;
            }

        }
        protected string YAHOO_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_msglogid)
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
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
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

                            PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription, "Publish OK", m_msglogid);
                            requestFTPUploader = null;
                        }
                        catch (Exception ex)
                        {
                            m_error = "True";
                            PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
                        }

                    }
                }
                catch (Exception ex)
                {

                    m_error = "True";
                    PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
                }
            }
            catch (Exception ex)
            {

                m_error = "True";
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
            }
            return m_error;
        }
        protected void APN_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_sftp_path, string m_msglogid)
        {
            var m_headline = "";
            // var m_error = "False";
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
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
            }
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
                PublishLog(m_msgid, m_headline, m_customer, "APN SFTP Pub OK", "Publish OK", m_msglogid);
            }
            catch (Exception ex)
            {
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
            }

        }
        public void ShareChat(int msgid, string m_msglogid)
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
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
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
                        PublishLog(m_msgid, m_headline, m_customer, "SFTP ShareChat Message", "Publish OK", m_msglogid);

                    }
                    catch (Exception ex)
                    {
                        PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
                    }
                }
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                PublishLog(m_msgid, m_headline, m_customer, ex.Message, "Pub Error", "");
            }

        }
        public static void PublishLog(string p_story_id, string p_headline, string p_customer, string p_status, string p_story_status, string p_msglogid)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO MsgLog(storyID,headline,Customer,MsgStatus,datecreated,status)values(@storyid,@headline,@customer,@msgstatus,@datecreated,@status)", con);

                cmd.Parameters.Add("@storyid", SqlDbType.NVarChar).Value = p_story_id;
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = p_headline;
                cmd.Parameters.Add("@customer", SqlDbType.NVarChar).Value = p_customer;
                cmd.Parameters.Add("@msgstatus", SqlDbType.NVarChar).Value = p_status;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = p_story_status;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception )
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            if (p_msglogid != "")
            {
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE MsgLog set status = @status where msglogid = @msglogid", con);

                    cmd.Parameters.Add("@msglogid", SqlDbType.NVarChar).Value = p_msglogid;
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = "Retry";

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception )
                {
                    // The connection failed. Display an error message.
                    //Message.Text = "Unable to connect to the database.";
                }
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