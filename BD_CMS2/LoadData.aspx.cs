using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public partial class LoadData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int m_storyid = 1663;
            String connectionString1 = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            String connectionString = ConfigurationManager.ConnectionStrings["BDPublicConnectionString"].ConnectionString;
            string insertstory = "INSERT INTO Story1 ([headline], [body], [datepublished]) VALUES (@headline, @body, @datepublished) ";
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);

                string strSQL = "Select body, headline, datepublished from story where storyid < " + m_storyid;

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(connectionString1);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand(insertstory, con1);
                    cmd1.Parameters.Add("@body", SqlDbType.NVarChar).Value = HttpUtility.HtmlDecode(dr["body"].ToString()); ;
                    cmd1.Parameters.Add("@headline", SqlDbType.NVarChar).Value = (dr["headline"].ToString());
                    cmd1.Parameters.Add("@datepublished", SqlDbType.DateTime).Value = (dr["datepublished"].ToString());
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                    cmd1.Dispose();
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
}