using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Helpers
{
    public static class DateHelper
    {
        private static readonly string MY_SQL_DATE_FORMAT = "YYYY-MM-DD HH:MM:SS";
        public static String FormatMySQL(DateTime date)
        {
            return date.ToString(MY_SQL_DATE_FORMAT);
        }
    }
}
