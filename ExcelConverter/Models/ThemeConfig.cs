using MaterialDesignThemes.Wpf;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace ExcelConverter.Models
{
    public class ThemeConfig
    {
        [DataMember(Name = "is_dark_theme")]
        public bool IsDarkTheme { get; set; }
        [DataMember(Name = "is_color_adjusted")]
        public bool IsColorAdjusted { get; set; }
        [DataMember(Name = "desired_contrast_ratio")]
        public float DesiredContrastRatio { get; set; }
        [DataMember(Name = "contrast_value")]
        public Contrast ContrastValue { get; set; }

        [DataMember(Name = "color_selection_value")]
        public ColorSelection ColorSelectionValue { get; set; }

        [DataMember(Name = "primay_color")]
        public Color PrimaryColor { get; set; }

        [DataMember(Name = "accent_color")]
        public Color AccentColor { get; set; }
    }
}
