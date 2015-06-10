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
    public partial class ANZSX_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            var m_ticker = this.tbTicker.Text ;
            var m_name = this.tbName.Text;
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ANZSX(ticker, name,datecreated)values(@ticker,@name,@datecreated)", con);

                cmd.Parameters.Add("@ticker", SqlDbType.NVarChar).Value = m_ticker;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = m_name;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }
            Response.Redirect("ANZSX.aspx");
        }
    }
}