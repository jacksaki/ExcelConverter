using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConverter.Components
{
    public class LogEventArgs(ConvertLog log) :EventArgs
    {
        public ConvertLog Log => log;
    }
}
