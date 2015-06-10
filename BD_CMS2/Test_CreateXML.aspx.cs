using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BD_CMS2
{
    public partial class Test_CreateXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select body from story where storyid = " + this.tbStoryID.Text;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.TextBox1.Text = (dr["body"].ToString());
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
                
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Generate some XML...
        
            MemoryStream strm = new MemoryStream();
            var m_story = this.TextBox1.Text;
            m_story = Regex.Replace(m_story, @"<[^>]*>", String.Empty);
            string x_story = ReplaceWordChars(m_story);
            m_story = x_story;
            string file_now = "Test1XML.xml";
            string[] lines = m_story.Replace("\r", " ").Split('\n');
            string m_timeID = DateTime.Now.TimeOfDay.ToString();
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
            writer.WriteString( DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("sender");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("origin");
            writer.WriteString("BusinessDesk");
            writer.WriteEndElement();
            writer.WriteStartElement("transmitId");
            writer.WriteString("Transmit ID ");
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
            writer.WriteString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteEndElement();
            writer.WriteStartElement("embargoed");
            writer.WriteString("Not Embargoed");
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
            writer.WriteString("Headline");
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
            writer.WriteString("Reporter Mike");
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
            using (FileStream file = new FileStream("C:/testmike1.xml",FileMode.Create,FileAccess.Write))
            {
                strm.WriteTo(file);

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