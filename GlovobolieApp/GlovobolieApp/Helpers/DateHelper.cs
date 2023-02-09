using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Helpers
{
    public static class DateHelper
    {
        private static readonly string MY_SQL_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static String FormatMySQL(DateTime date)
        {
            return date.ToString(MY_SQL_DATE_FORMAT);
        }
    }
}
