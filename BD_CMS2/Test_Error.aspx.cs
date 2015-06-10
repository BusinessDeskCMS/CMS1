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
    public partial class Test_Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE MsgLog set status = @status where msglogid = @msglogid", con);

                cmd.Parameters.Add("@msglogid", SqlDbType.NVarChar).Value = "9";
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = "Pub Error";

                cmd.ExecuteNonQuery();
                con.Close();
                this.Label1.Text = "Story Reset OK";
            }
            catch (Exception ex)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }  
        }
    }
}