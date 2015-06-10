using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BD_CMS2
{
    public class DateTimeStuff
    {

        public static string GetNZTime()
        {
            DateTime mNZdateTime;
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time 
            mNZdateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo);
            return mNZdateTime.ToString();
        }
        public static string GetNZTimefromUTC(DateTime m_utc)
        {
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(m_utc, timeZoneInfo);
            return m_nzdate.ToString();
        }
        public static DateTime GetNZDateTimefromUTC(DateTime m_utc)
        {
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to New Zealand Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            //Get date and time in New Zealand Standard Time
            var m_nzdate = TimeZoneInfo.ConvertTimeFromUtc(m_utc, timeZoneInfo);
            return m_nzdate;
        }
    }
}