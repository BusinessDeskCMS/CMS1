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
    public partial class CustomerContactEdit : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {


                if (Request.QueryString["contactid"] != null)
                {
                    int m_contactid = Convert.ToInt32(Request.QueryString["contactid"]);
                    this.lbContactID.Text = m_contactid.ToString();


                    String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
                    try
                    {
                        // Connect to the database and run the query.
                        SqlConnection con = new SqlConnection(connectionString);
                        string strSQL = "Select firstname, lastname,phone,email,notes,active from customercontact where customercontact = " + m_contactid;

                        SqlCommand cmd = new SqlCommand(strSQL, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            this.tbFirstName.Text = (dr["firstname"].ToString());
                            this.tbLastName.Text = (dr["lastname"].ToString());
                            this.tbPhone.Text = (dr["phone"].ToString());
                            this.tbemail.Text = (dr["email"].ToString());
                            this.tbNotes.Text = (dr["notes"].ToString());

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
                }
            }

        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            int m_contactid = Convert.ToInt32(this.lbContactID.Text);
            
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            try
            {
                // Connect to the database and run the query.
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE customercontact set firstname = @firstname, lastname = @lastname, phone = @phone, email = @email, notes = @notes, active = @active,datemodified =@datemodified where customercontact = @contactid", con);
                cmd.Parameters.AddWithValue("@firstname", this.tbFirstName.Text);
                cmd.Parameters.AddWithValue("@lastname", this.tbLastName.Text);
                cmd.Parameters.AddWithValue("@contactid", m_contactid);
                cmd.Parameters.AddWithValue("@phone", this.tbPhone.Text);
                cmd.Parameters.AddWithValue("@email", this.tbemail.Text);
                cmd.Parameters.AddWithValue("@notes", this.tbNotes.Text);
                cmd.Parameters.AddWithValue("@active", this.ddlActive.SelectedValue);
                cmd.Parameters.AddWithValue("@datemodified", DateTime.Now);

                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception)
            {

                // The connection failed. Display an error message.
                //Message.Text = "Unable to connect to the database.";

            }
            Response.Redirect("customercontact1.aspx");
        }
    }
}