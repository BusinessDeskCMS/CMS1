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
    public partial class StoryAdd1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSaveStory_Click(object sender, EventArgs e)
        {

            var m_story = this.CKEditor1.Text;
            var m_heading = this.tbHeading.Text;
            var m_urgency = this.ddlType.SelectedValue;
            var m_sendtoreview = this.ddlSendToReview.SelectedValue;
            var m_sendtoreview_flag = "N";
            DateTime m_datetoreview = DateTime.MinValue;
            m_datetoreview = m_datetoreview.AddYears(1800);
            var m_status = "Draft";

            if (m_sendtoreview == "Yes")
            {
                m_sendtoreview_flag = "Y";
                m_datetoreview = System.DateTime.Now;
                m_status = "In Review";


            }           
            this.CKEditor1.Text = "";
            this.tbHeading.Text = "";
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO story(body, headline,datecreated,sendtoreview,pub_hold, urgency,datetoreview,status,markets,asx)values(@body,@headline,@datecreated,@sendtoreview,@pub_hold,@urgency,@datetoreview,@status,@nzx,@asx)", con);

                cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = m_story;
                cmd.Parameters.Add("@headline", SqlDbType.NVarChar).Value = m_heading;
                cmd.Parameters.Add("@urgency", SqlDbType.NVarChar).Value = m_urgency;
                cmd.Parameters.Add("@datecreated", SqlDbType.DateTime).Value = System.DateTime.Now;
                cmd.Parameters.Add("@sendtoreview", SqlDbType.NChar).Value = m_sendtoreview_flag;
                cmd.Parameters.Add("@datetoreview", SqlDbType.DateTime).Value = m_datetoreview;
                cmd.Parameters.Add("@pub_hold", SqlDbType.NChar).Value = "N";
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = m_status;
                cmd.Parameters.Add("@nzx", SqlDbType.NVarChar).Value = this.ddlTicker.SelectedValue;
                cmd.Parameters.Add("@asx", SqlDbType.NVarChar).Value = this.ddlASX.SelectedValue;

                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }
            Response.Redirect("dashboard.aspx");


        }       

        DataSet GetData(String queryString)
        {

            // Retrieve the connection string stored in the Web.config file.
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;

            DataSet ds = new DataSet();

            try
            {
                // Connect to the database and run the query.
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

                // Fill the DataSet.
                adapter.Fill(ds);

            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }

            return ds;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }
    }
}