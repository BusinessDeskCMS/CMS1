using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;

namespace BD_CMS2
{
    public partial class TestBootstrap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]

        public static List<string> GetAutoCompleteData(string username)
        {
            List<string> result = new List<string>();
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select DISTINCT scoop from taxo where scoop LIKE '%'+@SearchText+'%'", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchText", username);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result.Add(dr["scoop"].ToString());
                    }
                    return result;
                }
            }
        }
    }
}