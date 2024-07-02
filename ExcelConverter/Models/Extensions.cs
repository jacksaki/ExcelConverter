using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExcelConverter.Models
{
    public static class Extensions
    {
        public static bool Between(this int value, int from, int to)
        {
            return value >= from && value <= to;
        }
        public static int GetCharCount(this string value, char searchChar)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            return value.Where(x => x == searchChar).Count();
        }
    }
}