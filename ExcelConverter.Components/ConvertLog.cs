using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConverter.Components
{
    public enum LogType
    {
        [EnumText("Info")]
        Info,
        [EnumText("Warning")]
        Warning,
        [EnumText("Error")]
        Error,
    }
    public class ConvertLog(ExcelFile excelFile,LogType t, string message)
    {
        public ExcelFile ExcelFile => excelFile;
        public LogType Type => t;
        public string Message => message;
        public DateTime Date = TimeProvider.System.GetLocalNow().DateTime;
    }
}
