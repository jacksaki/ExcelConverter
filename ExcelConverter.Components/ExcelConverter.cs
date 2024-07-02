using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcelConverter.Components
{
    public class ExcelConverter(ConvertParameter p)
    {
        public delegate void LogEventHandler(object sender, LogEventArgs e);
        public event LogEventHandler Log = delegate { };
        public ConvertParameter Parameter => p;
        public void ConvertAll(IEnumerable<ExcelFile> files)
        {
            foreach(var file in files)
            {
                Convert(file);
            }
        }

        private IEnumerable<IXLCell> GetCells(IXLWorksheet sheet)
        {
            if (string.IsNullOrWhiteSpace(this.Parameter.CellAddress))
            {
                return sheet.CellsUsed();
            }
            else
            {
                return new List<IXLCell> { sheet.Cell(this.Parameter.CellAddress) };
            }
        }

        private bool IsFileOpen(ExcelFile file)
        {
            try
            {
                using (var stream = File.Open(file.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
                return false;
            }
            catch 
            {
                return true;
            }
        }

        private bool Validate(ExcelFile file)
        {
            if (!System.IO.File.Exists(file.Path))
            {
                Log(this, new LogEventArgs(new ConvertLog(file, LogType.Error, $"File not found.")));
                return false;
            }
            if (IsFileOpen(file))
            {
                Log(this, new LogEventArgs(new ConvertLog(file, LogType.Error, $"File is opened by another process.")));
                return false;
            }
            return true;
        }
        private bool CreateBackup(ExcelFile file)
        {
            try
            {
                if (this.Parameter.CreateBackup)
                {
                    file.CreateBackup();
                    Log(this, new LogEventArgs(new ConvertLog(file, LogType.Info, $"Backup created.")));
                }
                return true;
            }
            catch (Exception ex)
            {
                Log(this, new LogEventArgs(new ConvertLog(file, LogType.Error, $"Backup failed.: {ex.Message}")));
                return false;
            }
        }
        private IEnumerable<IXLWorksheet>GetWorksheets(XLWorkbook book)
        {
            var allSheets = book.Worksheets;

            if (this.Parameter.SheetNames.Any())
            {
                return allSheets.Where(x => this.Parameter.SheetNames.Where(y => y == x.Name).Any());
            }
            else
            {
                return allSheets;
            }
        }

        private void Convert(ExcelFile file, IXLCell cell)
        {
            var src = GetSourceValue(cell);
            if (IsMatched(src))
            {
                var dest = GetConvertedText(src);
                if (this.Parameter.IsDestFormula)
                {
                    cell.FormulaA1 = dest;
                    Log(this, new LogEventArgs(new ConvertLog(file, LogType.Info, $"Convert Formula. {src} -> {dest}")));
                }
                else
                {
                    cell.Value = XLCellValue.FromObject(dest);
                    Log(this, new LogEventArgs(new ConvertLog(file, LogType.Info, $"Convert value. {src} -> {dest}")));
                }
            }
        }
        private string GetConvertedText(string src)
        {
            if (this.Parameter.UseRegEx)
            {
                var reg = new Regex(this.Parameter.SourceText);
                return reg.Replace(src, this.Parameter.DestText);
            }
            else
            {
                return src.Replace(src, this.Parameter.DestText);
            }
        }
        private bool IsMatched(string src)
        {
            if (this.Parameter.UseRegEx)
            {
                var reg = new Regex(this.Parameter.SourceText);
                return reg.IsMatch(src);
            }
            else
            {
                return src.Contains(this.Parameter.SourceText);
            }
        }
        private string GetSourceValue(IXLCell cell)
        {
            if (this.Parameter.IsSourceFormula)
            {
                return cell.FormulaA1;
            }
            else
            {
                return cell.IsEmpty() ? string.Empty : cell.GetString();
            }
        }

        private string GetDestValue(IXLCell cell)
        {
            if (this.Parameter.IsSourceFormula)
            {
                return cell.FormulaA1;
            }
            else
            {
                return cell.IsEmpty() ? string.Empty : cell.GetString();
            }
        }

        private void Convert(ExcelFile file)
        {
            if (!Validate(file))
            {
                return;
            }
            if (!CreateBackup(file))
            {
                return;
            }
            using (var book = new XLWorkbook(file.Path))
            {
                var worksheets = GetWorksheets(book);
                if (!worksheets.Any())
                {
                    Log(this, new LogEventArgs(new ConvertLog(file, LogType.Info, "Target sheet not found.")));
                    return;
                }
                foreach(var sheet in worksheets)
                {
                    var cells = GetCells(sheet);
                    if (!cells.Any())
                    {
                        Log(this, new LogEventArgs(new ConvertLog(file, LogType.Info, $"{sheet.Name}({this.Parameter.CellAddress}): cell not found.")));
                        continue;
                    }
                    foreach(var cell in cells)
                    {
                        Convert(file, cell);
                    }
                }
            }
        }
    }
}
