using System.Runtime.Serialization;
using Utf8Json;
using Utf8Json.Resolvers;

namespace ExcelConverter.Models
{
    public class AppConfig
    {
        public static string Path => System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");

        [DataMember(Name = "theme")]
        public ThemeConfig ThemeConfig { get; protected set; }

        internal void InitThemeConfig()
        {
            this.ThemeConfig = new ThemeConfig()
            {
                IsDarkTheme = false,
                IsColorAdjusted = false,
                DesiredContrastRatio = 0f,
                ContrastValue = MaterialDesignThemes.Wpf.Contrast.None,
                ColorSelectionValue = MaterialDesignThemes.Wpf.ColorSelection.None,
                PrimaryColor = System.Windows.Media.Color.FromArgb(255, 156, 39, 176),
                AccentColor = System.Windows.Media.Color.FromArgb(255, 255, 234, 0),
            };
        }

        public void Save()
        {
            System.IO.File.WriteAllBytes(Path, JsonSerializer.Serialize<AppConfig>(this, StandardResolver.AllowPrivate));
        }
    }
}
