using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Tamir.SharpSsh;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using System.IO;

namespace BD_CMS2
{
    public partial class Test_SFTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var m_headline = "";
            var m_body = "";
            var msgid = this.ddlStory.SelectedValue.ToString();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select headline, body from test_story where storyid = " + msgid;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_headline = (dr["headline"].ToString());
                    m_body = (dr["body"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
                this.tbBody.Text = m_body;
                this.tbHeadline.Text = m_headline;
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // send the message
            if (this.rbList1.SelectedValue=="APN")
            {                                
                APN(1,this.tbHeadline.Text, this.tbBody.Text);
            }
        }
        public void APN(int storyid, string p_headline, string p_story)
        {
            // check to see if the APN feed is active
            // if Yes, create the NewsML record and send it out    
            string m_headline = p_headline;
            string m_story = p_story;
            string m_active = "";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select active from customermessage where customerid = '2' and active = 'Y'";
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
                    XmlWriter writer = null;
                    var m_id = Convert.ToString(storyid);
                    //Trace.WriteLine("Processing Story ID: " + m_id);
                    string file_now = m_nzdate.ToString("yyyMMddHHmmss") + "-" + m_id + ".XML";
                    //LocalResource myStorage = RoleEnvironment.GetLocalResource("XMLStorage");
                    string pathToFiles = Server.MapPath("/Stories/");                   
                    string filepath = Path.Combine(pathToFiles, file_now);
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    // write the NewsML file for APN, 
                    writer = XmlWriter.Create(filepath, settings);
                    string x_story = ReplaceWordChars(m_story);
                    m_story = x_story;
                    string[] lines = m_story.Replace("\r", "").Split('\n');
                    string m_embargoed = string.Empty;
                    m_headline = m_headline.Replace("\r", string.Empty);
                    //var m_reporter = lines[0];
                    var m_reporter = "Staff Reporter";
                    //if (m_reporter.Length > 3)
                    //{
                    //    m_reporter = m_reporter.Substring(3, m_reporter.Length - 3);
                    //}
                    //else
                    //{
                    //    m_reporter = "Staff Reporter";
                    //}
                    // write the NewsML file for APN, IRG                    
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
                    writer.WriteString(m_nzdate.ToString("yyyMMddHHmmss"));
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
                    writer.WriteAttributeString("residref", m_nzdate.ToString("yyyMMddHHmmss"));
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    // END PackageItem
                    //
                    // START NewsItem
                    writer.WriteStartElement("newsItem");
                    writer.WriteAttributeString("guid", "BD-" + m_nzdate.ToString("yyyMMddHHmmss"));
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
                    writer.WriteString(m_nzdate.ToString("yyyMMddHHmmss"));
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
                        //
                        // now send to APN
                        //
                        SshTransferProtocolBase sshCpAPN;
                        sshCpAPN = new Sftp("ftp1.apnz.co.nz", "ContentLtd");
                        sshCpAPN.Password = "c0nt3nt";
                        sshCpAPN.Connect();
                        string lfile = filepath;
                        string rfile = "/incoming/" + file_now;
                        sshCpAPN.Put(lfile, rfile);
                        sshCpAPN.Close();
                        //Trace.WriteLine(m_id, "APN - Success");

                    }
                    catch (Exception ex)
                    {

                        //Trace.WriteLine(m_id, "APN - Fail");
                    }
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {

            }

        }
        public void ShareChat(int storyid, string p_headline, string p_story)
        {
            // check to see if the Sharechat feed is active
            // if Yes, create the RSS record and send it out
            string m_headline = p_headline;
            string m_story = p_story;
            string m_active = "";
            String connectionString = "Server=tcp:mo2rt6wqgd.database.windows.net,1433;Database=BDCMS;User ID=ContentLTD@mo2rt6wqgd;Password=L0chmara5664;Trusted_Connection=False;Encrypt=True;Connection Timeout=120;";

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
                    //LocalResource myStorage = RoleEnvironment.GetLocalResource("XMLStorage");
                    //string RSS_FilePath = Path.Combine(myStorage.RootPath, m_RSS_File);

                    string pathToFiles = Server.MapPath("/Stories/");
                    string RSS_FilePath = pathToFiles + m_RSS_File;
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    string x_story = ReplaceWordChars(m_story);
                    m_story = x_story;
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
                        string lfile = m_RSS_File;
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