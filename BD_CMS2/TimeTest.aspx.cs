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
    public partial class TimeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           var m_date = this.tbTimeToConvert.Text;
           var m_date_toconvert = Convert.ToDateTime(m_date);
           var m_utc_future = m_date_toconvert.ToUniversalTime();

            this.tbDateNow.Text = DateTime.Now.ToString();
            this.tbUTCNow.Text = DateTime.UtcNow.ToString();
            var localdt = DateTime.Now.ToLocalTime();
            string s_utc = "2013-12-13 07:35 PM";
            DateTime d_utc = Convert.ToDateTime(s_utc);
            this.tbUTCNZ.Text = m_utc_future.ToString();
            TimeZoneInfo timeZoneInfo;
            DateTime mNZdateTime;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time 
            mNZdateTime = TimeZoneInfo.ConvertTime(d_utc, timeZoneInfo);
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(d_utc, timeZoneInfo);
            this.tbLocalNow.Text = mNZdateTime.ToString();
            var m_nz = DateTimeStuff.GetNZTime();
            this.tbLocalNow.Text = m_nz;
        }
        protected string GetNZ(object p_utc )
        {
            string m1 = p_utc.ToString();
            DateTime dt1 = Convert.ToDateTime(m1);
            return DateTimeStuff.GetNZTimefromUTC(dt1);
        }
    }
}