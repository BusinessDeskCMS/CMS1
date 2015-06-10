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
    public partial class Message_Bulk_Reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void btOn_Click(object sender, EventArgs e)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE customermessage set active = @active", con);
                cmd.Parameters.AddWithValue("@active", "Y");                
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
        }

        protected void btOff_Click(object sender, EventArgs e)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE customermessage set active = @active", con);
                cmd.Parameters.AddWithValue("@active", "N");
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
        }
    }
}