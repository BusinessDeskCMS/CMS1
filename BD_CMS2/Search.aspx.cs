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
    public partial class Search : System.Web.UI.Page
    {
        private string SearchString = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            SearchString = txtSearch.Text;

            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;
            

            // Connect to the database and run the query.
            SqlConnection con = new SqlConnection(connectionString);
            string strSQL = "Select storyid, datepublished, headline,body from story where body LIKE '%" + SearchString + "%' ORDER by datepublished DESC";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            gvResults.DataSource = cmd.ExecuteReader();
            gvResults.DataBind(); 
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;

            int _ColIndex = 2;
            string text = e.Row.Cells[_ColIndex].Text;
            if (text.Length >100)
            {
                e.Row.Cells[_ColIndex].Text = text.Substring(0, 100);
            }
           

        }
    }
}