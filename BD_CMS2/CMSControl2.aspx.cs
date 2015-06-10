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
    public partial class CMSControl2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            GetData();
        }
        protected void GetData()
        {
            
                string m_messaging = "";
                string m_public = "";
                string m_email = "";
                string m_name = "";
                string m_calendar = "";
                String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                try
                {
                    // Connect to the database and run the query.
                    SqlConnection con = new SqlConnection(connectionString);
                    string strSQL = "Select name, calendar, messaging, email_active,public_site_feed_active from cmscontrol";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        m_name = (dr["name"].ToString());
                        m_calendar = (dr["calendar"].ToString());
                        m_messaging = (dr["messaging"].ToString());    
                        m_email = (dr["email_active"].ToString());
                        m_public = (dr["public_site_feed_active"].ToString());

                    }
                    this.tbCalendar.Text = m_calendar;
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                    SetValues(m_email, m_public, m_messaging);
                }
                catch (Exception ex)
                {
                    // The connection failed. Display an error message.
                    //Message.Text = "Unable to connect to the database.";
                }
            
        }

        private void SetValues(string p_email, string p_public, string p_messaging)
        {

            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("$(function () {");
            if (p_email != "")
            {
                string m_email = "2";
                if (p_email == "Y")
                {
                    m_email = "1";
                }
                script.Append(" $('#selEmail').multipleSelect('setSelects',[" + m_email + "]);");
            }
            if (p_public != "")
            {
                string m_public = "2";
                if (p_public == "Y")
                {
                    m_public = "1";
                }
                script.Append(" $('#selPublic').multipleSelect('setSelects',[" + m_public + "]);");
            }
            if (p_messaging != "")
            {
                string m_messaging = "2";
                if (p_messaging == "Y")
                {
                    m_messaging = "1";
                }
                script.Append(" $('#selMessaging').multipleSelect('setSelects',[" + m_messaging + "]);");
            }
            script.Append(" });");
            script.Append("</script>");
            Page.ClientScript.RegisterClientScriptBlock(typeof(object), "JavaScriptBlock", script.ToString());

        }
    }
}