using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Tamir.SharpSsh;

namespace BD_CMS2
{
    public partial class Message_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Test Message ID  = 2076
            int msgid = 2076;
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
                    this.Label4.Text = (dr["headline"].ToString());
                    this.Label5.Text = (dr["body"].ToString());
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex > -1)
            {
                Label1.Text = "Selected Customer: " + RadioButtonList1.SelectedItem.ToString();
                string m_customer = RadioButtonList1.SelectedItem.ToString();
                string m_ftp_host = "";
                string m_ftp_password = "";
                string m_ftp_username = "";
                string m_messagetype = "";
                string m_customerid = "";
                string m_sftp_path = "";
                var i_customerid = 0;
                int msgid = 2076;
                String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    string strSQL = "Select cm.customerid,c.name,ftp_host, ftp_password,ftp_username,messagetype,sftp_path from customermessage cm, customer c where cm.customerid = c.customerid and c.name = @name";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = m_customer;
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
                                FTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer);
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
            else
            {
                Label1.Text = "Invalid Customer Selection: " ;
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

                    //PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription);
                    this.Label2.Text = "Host welcome: " + response.WelcomeMessage;
                    this.Label3.Text = "Response from Remote Server : " + response.StatusDescription;
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {

                    //PublishLog(m_msgid, m_headline, m_customer, ex.Message);
                    
                    this.Label3.Text = "Error : " + ex.Message;
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

                            //PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription);
                            requestFTPUploader = null;
                        }
                        catch (Exception ex)
                        {

                            //PublishLog(m_msgid, m_headline, m_customer, ex.Message);
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
    }
}