using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConverter.Components
{
    public class ConvertParameter
    {
        public string SheetNameText { get; set; }

        public IEnumerable<string> SheetNames
        {
            get
            {
                if (string.IsNullOrEmpty(this.SheetNameText))
                {
                    return new List<string>(); ;
                }
                else
                {
                    return this.SheetNameText.Split(",").Where(x => !string.IsNullOrWhiteSpace(x));
                }
            }
        }

        public string SourceText { get; set; }

        public bool IsSourceFormula { get; set; }

        public string DestText { get; set; }

        public bool IsDestFormula { get; set; }

        public bool UseRegEx { get; set; }

        public bool CreateBackup { get; set; }

        public string CellAddress { get; set; }
    }
}
