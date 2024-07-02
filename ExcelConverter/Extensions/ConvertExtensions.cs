using System;
using System.Globalization;
using System.Reflection;

namespace ExcelConverter.Extensions
{
    public static class ConvertExtensions
    {
        public static int? ToIntN(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            return int.TryParse(value.ToString(), out var ret) ? ret : null;
        }

        public static int ToInt32(this object value, int defaultValue)
        {
            return value.ToIntN() ?? defaultValue;
        }

        public static float? ToFloatN(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            return float.TryParse(value.ToString(), out var ret) ? ret : null;
        }

        public static float ToFloat(this object value, int defaultValue)
        {
            return value.ToFloatN() ?? defaultValue;
        }

        public static double? ToDoubleN(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            return double.TryParse(value.ToString(), out var ret) ? ret : null;
        }
        public static double ToDouble(this object value, int defaultValue)
        {
            return value.ToDoubleN() ?? defaultValue;
        }

        public static decimal? ToDecimalN(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            return decimal.TryParse(value.ToString(), out var ret) ? ret : null;
        }

        public static decimal ToDecimal(this object value, int defaultValue)
        {
            return value.ToDecimalN() ?? defaultValue;
        }

        public static DateTime? ToDateTimeN(this object value, string dateFormat)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            return DateTime.TryParseExact(value.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d) ? d : null;
        }

        public static DateTime ToDateTime(this object value, string dateFormat, DateTime defaultValue)
        {
            return value.ToDateTimeN(dateFormat) ?? defaultValue;
        }

        public static DateTime? ToDateTime(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            if (value is DateTime)
            {
                return (DateTime)value;
            }
            if (value is DateTime?)
            {
                return (DateTime?)value;
            }
            return null;
        }

        public static long? ToLongN(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            long ret;
            return long.TryParse(value.ToString(), out ret) ? ret : (long?)null;
        }

        public static long ToLong(this object value, long defaultValue)
        {
            return value.ToLongN() ?? defaultValue;
        }
    }
}
