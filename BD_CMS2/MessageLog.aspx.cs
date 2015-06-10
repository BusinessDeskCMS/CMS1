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
    public partial class MessageLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                //lvMsgLog.DataSource = LoadDataSet();
                //lvMsgLog.DataBind();
            }
        }

        protected string GetNZ(object p_utc)
        {

            string m1 = p_utc.ToString();
            string m_dateNZ = "";
            if (m1 != "")
            {
                DateTime dt1 = Convert.ToDateTime(m1);
                return m_dateNZ = DateTimeStuff.GetNZDateTimefromUTC(dt1).ToString("dd/MM HH:mm");
            }
            else
            {
                return m_dateNZ;
            }
        }
        protected void btSelect_Click(object sender, EventArgs e)
        {
            
        }
        public DataSet LoadDataSet()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(" SELECT [StoryID], [Headline], [Customer], [MsgStatus], [DateCreated] FROM [MsgLog] WHERE CAST(datecreated AS DATE) = CAST(GETDATE() AS DATE) ORDER BY StoryID DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            using (con)
            {
                con.Open();
                adapter.Fill(ds);
            }

            return ds;
        }

    }
}