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
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {

           
 
            string m_cal_src = "";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                string strSQL = "Select calendar from cmscontrol" ;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m_cal_src = (dr["calendar"].ToString());                    
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {
                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";
            }
            this.iCalendar.Src = m_cal_src;
            }
        }
    }
}