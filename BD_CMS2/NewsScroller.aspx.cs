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
    public partial class NewsScroller : System.Web.UI.Page
    {
        public string NewsTicker = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sbNews = new StringBuilder();

            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select TOP (15) headline from story order by storyid desc";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader()) 
                {
                    while (dr.Read())
                    {                        
                        sbNews.Append(dr[0].ToString());
                        sbNews.Append(" ** ");
                    }
                }
                NewsTicker = sbNews.ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}