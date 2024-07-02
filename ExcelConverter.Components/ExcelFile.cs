namespace ExcelConverter.Components
{
    public class ExcelFile(string path)
    {
        public static IEnumerable<ExcelFile>GetAll(string dir)
        {
            return System.IO.Directory.EnumerateFiles(dir, "*.xlsx").Select(x =>  new ExcelFile(x));
        }

        public string Path => path;
        public string FileName => System.IO.Path.GetFileName(this.Path);
        public string CreateBackup()
        {
            var dir = System.IO.Path.GetDirectoryName(this.Path);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(this.Path) + TimeProvider.System.GetLocalNow().ToString("yyyyMMddHHmmss");
            var ext = System.IO.Path.GetExtension(this.Path);
            var dest= System.IO.Path.Combine(dir, fileName + ext);
            System.IO.File.Copy(this.Path, dest);
            return dest;
        }
    }
}
