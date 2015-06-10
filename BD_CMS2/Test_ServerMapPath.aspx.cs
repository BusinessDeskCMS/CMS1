using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BD_CMS2
{
    public partial class Test_ServerMapPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pathToFiles = Server.MapPath("/Stories/");
            this.Label1.Text = pathToFiles;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZoneInfo);
            var currentdate = m_nzdate.ToString("yyyy-MM-dd HH:mm:ss");
            string pathToFiles = Server.MapPath("/Stories/");
            string filename = "miketest.xml";
            string m_RSS_File = pathToFiles + filename;
            string m_headline = "BusinessDesk Test Message";
            string m_body = "Body - Testing";
            try
            {

                XmlTextWriter objX = new XmlTextWriter(m_RSS_File, Encoding.UTF8);
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
            this.Label2.Text = "File Successfully Created";

            }
            catch (Exception ex)
            {
                this.Label2.Text = "File Creation Error: " + ex.ToString() ; 
            }
            
            
            
           
        }
    }
}