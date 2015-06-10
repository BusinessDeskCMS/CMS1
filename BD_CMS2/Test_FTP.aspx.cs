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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tamir.SharpSsh;

namespace BD_CMS2
{
    public partial class Test_FTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                //string strSQL = "Select cm.customerid,c.name,ftp_host, ftp_password,ftp_username,messagetype,sftp_path from customermessage cm, customer c where cm.customerid = c.customerid and cm.active = 'Y'";
                string strSQL = "Select cm.customerid,c.name,ftp_host, ftp_password,ftp_username,messagetype,sftp_path from customermessage cm, customer c where cm.customerid = c.customerid and cm.customermessageid = '6'";
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
                    FTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer);
                    //switch (m_messagetype)
                    //{
                    //    case "FTP":
                    //        FTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_customer);
                    //        break;
                    //    case "SFTP":
                    //        // send to SFTP
                    //        //SFTP_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_sftp_path);
                    //        break;
                    //    case "YAHOO":
                    //        // send to Yahoo
                    //        //YAHOO_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password);
                    //        break;
                    //    case "RSS":
                    //        //RSS_Send(msgid, m_ftp_host, m_ftp_username, m_ftp_password, m_sftp_path);
                    //        break;
                    //    default:
                    //        break;
                    //}
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
            string file_now = DateTime.Now.ToString("yyyMMddHHmmss") + "_" + m_msgid + ".xml";
            //string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            //string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                //string x_ftp_host = "ftp://waws-prod-db3-003.ftp.azurewebsites.windows.net/stories/";
                //string x_ftp_username = "businessdeskcmsftp\\CMSdeploy";
                //string x_ftp_password = "L0chmara5664";
                string x_ftp_host = m_ftp_host;
                string x_ftp_username = m_ftp_username;
                string x_ftp_password = m_ftp_password;
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                //string file_now = DateTime.Now.ToString() + "_" + m_msgid + ".xml";
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(x_ftp_host + file_now);
                requestFTPUploader.Credentials = new NetworkCredential(x_ftp_username, x_ftp_password);
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
                    this.Label1.Text = response.StatusDescription;
                    PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription);
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {

                    PublishLog(m_msgid, m_headline, m_customer, ex.Message);
                }

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
                this.TextBox2.Text = ex.ToString();
                //LogError("SFTP - Sharechat", ex.Message);
            }

        }

        //protected void SFTP_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_customer)
        //{

        //    // get the message from the msg file with the msgid param 
        //    var m_headline = "";
        //    String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
        //    try
        //    {
        //        // Connect to the database and run the query.
        //        SqlConnection con = new SqlConnection(connectionString);
        //        string strSQL = "Select headline from story where storyid = " + msgid;
        //        SqlCommand cmd = new SqlCommand(strSQL, con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            m_headline = (dr["headline"].ToString());
        //        }
        //        cmd.Dispose();
        //        con.Close();
        //        con.Dispose();
        //    }
        //    catch (Exception)
        //    {
        //        // The connection failed. Display an error message.
        //        //Message.Text = "Unable to connect to the database.";
        //    }
        //    var m_msgid = msgid.ToString();
        //    m_msgid = "2040";
        //    string m_msg_file = m_msgid + ".xml";
        //    string file_now = DateTime.Now.ToString("yyyMMddHHmmss") + "_" + m_msgid + ".xml";
        //    //string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
        //    //string m_file = m_fileroot + m_msg_file;
        //    CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
        //    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        //    CloudBlobContainer container = blobClient.GetContainerReference("messages");
        //    CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        blockBlob.DownloadToStream(memoryStream);
        //        string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
        //        string m_file = m_fileroot + m_msg_file;
        //        try
        //        {
        //            SshTransferProtocolBase sshCp;
        //            sshCp = new Sftp(m_ftp_host, m_ftp_username);
        //            sshCp.Password = m_ftp_password;
        //            sshCp.Connect();
        //            string lfile = m_file;
        //            string rfile = m_sftp_path + DateTime.Now.ToString("yyyMMddHHmmss") + "-" + m_msg_file;
        //            sshCp.Put(lfile, rfile);
        //            sshCp.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            //LogError("SFTP - Sharechat", ex.Message);
        //        }



        //        FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(x_ftp_host + "/Stories/" + file_now);
        //        requestFTPUploader.Credentials = new NetworkCredential(x_ftp_username, x_ftp_password);
        //        requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;
        //        int bufferLength = 2048;
        //        byte[] buffer = new byte[bufferLength];
        //        Stream uploadStream = requestFTPUploader.GetRequestStream();
        //        memoryStream.Position = 0;
        //        int contentLength = memoryStream.Read(buffer, 0, bufferLength);
        //        try
        //        {
        //            while (contentLength != 0)
        //            {
        //                uploadStream.Write(buffer, 0, contentLength);
        //                contentLength = memoryStream.Read(buffer, 0, bufferLength);
        //            }
        //            uploadStream.Close();
        //            FtpWebResponse response = (FtpWebResponse)requestFTPUploader.GetResponse();
        //            this.Label1.Text = response.StatusDescription;
        //            PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription);
        //            requestFTPUploader = null;
        //        }
        //        catch (Exception ex)
        //        {

        //            PublishLog(m_msgid, m_headline, m_customer, ex.Message);
        //        }

        //    }
        //}
        protected void YAHOO_Send(int msgid, string m_ftp_host, string m_ftp_username, string m_ftp_password, string m_customer)
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
            var m_msgid = msgid.ToString();
            string m_msg_file = m_msgid + "_Yahoo.xml";
            string file_now = DateTime.Now.ToString("yyyMMddHHmmss") + "_" + m_msgid + ".xml";
            //string m_fileroot = "http://businessdesk.blob.core.windows.net/messages/";
            //string m_file = m_fileroot + m_msg_file;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=businessdesk;AccountKey=zWYFxlr7zhbMhXLptJ2lvtrORvoPTVunsDAf/v8B0tDsUWMigwFOJs9wEZ62XU6UdFWM1BQJ/SN9dZ0JsWVlpw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("messages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(m_msg_file);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                string x_ftp_host = "ftp://waws-prod-db3-003.ftp.azurewebsites.windows.net";
                string x_ftp_username = "businessdeskcmsftp\\CMSdeploy";
                string x_ftp_password = "L0chmara5664";
                //text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                //string file_now = DateTime.Now.ToString() + "_" + m_msgid + ".xml";
                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(x_ftp_host + "/Stories/" + file_now);
                requestFTPUploader.Credentials = new NetworkCredential(x_ftp_username, x_ftp_password);
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
                    this.Label1.Text = response.StatusDescription;
                    PublishLog(m_msgid, m_headline, m_customer, response.StatusDescription);
                    requestFTPUploader = null;
                }
                catch (Exception ex)
                {

                    PublishLog(m_msgid, m_headline, m_customer, ex.Message);
                }

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int i_msg_id = Convert.ToInt16(this.TextBox1.Text);
            SendMessage(i_msg_id);
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            APN_Send(2040, "", "", "", "");
        }
    }
}