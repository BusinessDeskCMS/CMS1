using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class SearchDisplay1 : System.Web.UI.Page
    {
        protected string StoryValue { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["storyid"] != null)
            {
                int m_storyid = Convert.ToInt32(Request.QueryString["storyid"]);
                
                StoryValue = null;
                String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    string strSQL = "Select storyid,body, headline,datepublished from story where storyid = " + m_storyid;
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        this.tbHeading.Text = (dr["headline"].ToString());
                        // this.tbStory.Text = HttpUtility.HtmlDecode(dr["body"].ToString());
                        this.tbStory.Text = HtmlRemoval.StripTagsCharArray((dr["body"].ToString()));
                        cmd.Dispose();
                        con.Close();
                        con.Dispose();
                    }
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
            if (!IsPostBack)
            {
                StoryValue = StoryValue;                

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
        public static class HtmlRemoval
        {
            /// <summary>
            /// Remove HTML from string with Regex.
            /// </summary>
            public static string StripTagsRegex(string source)
            {
                return Regex.Replace(source, "<.*?>", string.Empty);
            }

            /// <summary>
            /// Compiled regular expression for performance.
            /// </summary>
            static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

            /// <summary>
            /// Remove HTML from string with compiled Regex.
            /// </summary>
            public static string StripTagsRegexCompiled(string source)
            {
                return _htmlRegex.Replace(source, string.Empty);
            }

            /// <summary>
            /// Remove HTML tags from string using char array.
            /// </summary>
            public static string StripTagsCharArray(string source)
            {
                char[] array = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }
                }
                return new string(array, 0, arrayIndex);
            }
        }
    }
}