using ExcelConverter.Models;
using MaterialDesignThemes.Wpf;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ExcelConverter.ViewModels
{
    public class ThemeSettingsViewModel : ViewModelBase
    {
        public BindableReactiveProperty<bool> IsDarkTheme { get; }
        public BindableReactiveProperty<bool> IsColorAdjusted { get; }
        public BindableReactiveProperty<float> DesiredContrastRatio { get; }
        public IEnumerable<Contrast> ContrastValues => Enum.GetValues(typeof(Contrast)).Cast<Contrast>();
        public BindableReactiveProperty<Contrast> ContrastValue { get; }
        public IEnumerable<ColorSelection> ColorSelectionValues => Enum.GetValues(typeof(ColorSelection)).Cast<ColorSelection>();
        public BindableReactiveProperty<ColorSelection> ColorSelectionValue { get; }
        public ThemeConfig Config { get; }
        private void SetDarkMode(bool isDarkMode)
        {
            ModifyTheme(theme => theme.SetBaseTheme(isDarkMode ? Theme.Dark : Theme.Light));
        }
        private void SetColorAdjusted(bool isColorAdjusted)
        {
            ModifyTheme(theme =>
            {
                if (theme is Theme internalTheme)
                {
                    internalTheme.ColorAdjustment = isColorAdjusted
                        ? new ColorAdjustment
                        {
                            DesiredContrastRatio = this.DesiredContrastRatio.Value,
                            Contrast = this.ContrastValue.Value,
                            Colors = this.ColorSelectionValue.Value,
                        }
                        : null;
                }
            });
        }
        private void SetDesiredContrastRatio(float desiredContrastRatio)
        {
            ModifyTheme(theme =>
            {
                if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
                    internalTheme.ColorAdjustment.DesiredContrastRatio = desiredContrastRatio;
            });
        }
        private void SetContrastValue(Contrast contrast)
        {
            ModifyTheme(theme =>
            {
                if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
                    internalTheme.ColorAdjustment.Contrast = contrast;
            });
        }
        private void SetColorSelectionValue(ColorSelection colorSelection)
        {
            ModifyTheme(theme =>
            {
                if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
                    internalTheme.ColorAdjustment.Colors = colorSelection;
            });
        }
        public ThemeSettingsViewModel(ThemeConfig config)
        {
            this.Config = config;

            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            this.IsDarkTheme = new BindableReactiveProperty<bool>(this.Config.IsDarkTheme);
            this.IsColorAdjusted = new BindableReactiveProperty<bool>(this.Config.IsColorAdjusted);
            this.DesiredContrastRatio = new BindableReactiveProperty<float>(this.Config.DesiredContrastRatio);
            this.ContrastValue = new BindableReactiveProperty<Contrast>(this.Config.ContrastValue);
            this.ColorSelectionValue = new BindableReactiveProperty<ColorSelection>(this.Config.ColorSelectionValue);

            SetDarkMode(this.Config.IsDarkTheme);
            SetDesiredContrastRatio(this.Config.DesiredContrastRatio);
            SetContrastValue(this.Config.ContrastValue);
            SetColorSelectionValue(this.Config.ColorSelectionValue);
            SetColorAdjusted(this.Config.IsColorAdjusted);

            this.IsDarkTheme.Subscribe(x =>
            {
                ModifyTheme(theme => theme.SetBaseTheme(x ? Theme.Dark : Theme.Light));
            });
            this.IsColorAdjusted.Subscribe(x =>
            {
                SetColorAdjusted(x);
            });
            this.DesiredContrastRatio.Subscribe(x =>
            {
                SetDesiredContrastRatio(x);
            });
            this.ContrastValue.Subscribe(x =>
            {
                SetContrastValue(x);
            });
            this.ColorSelectionValue.Subscribe(x =>
            {
            });

            if (paletteHelper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += (_, e) =>
                {
                    this.IsDarkTheme.Value = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
                };
            }
            this.IsDarkTheme.Value = config.IsDarkTheme;
            this.IsColorAdjusted.Value = config.IsColorAdjusted;
            this.DesiredContrastRatio.Value = config.DesiredContrastRatio;
            this.ContrastValue.Value = config.ContrastValue;
            this.ColorSelectionValue.Value = config.ColorSelectionValue;
        }

        private static bool GetDefaultIsColorAdjusted(ITheme theme)
        {
            if (theme is Theme internalTheme)
            {
                return internalTheme.ColorAdjustment is not null;
            }
            else
            {
                return false;
            }
        }
        private static float GetDefaultDesiredContrastRatio(ITheme theme)
        {
            if (theme is Theme internalTheme)
            {
                var colorAdjustment = internalTheme.ColorAdjustment ?? new ColorAdjustment();
                return colorAdjustment.DesiredContrastRatio;
            }
            else
            {
                return 4.5f;
            }
        }
        private static Contrast GetDefaultContrastValue(ITheme theme)
        {
            if (theme is Theme internalTheme)
            {
                var colorAdjustment = internalTheme.ColorAdjustment ?? new ColorAdjustment();
                return colorAdjustment.Contrast;
            }
            else
            {
                return Contrast.Medium;
            }
        }
        private static ColorSelection GetDefaultColorSelectionValue(ITheme theme)
        {
            if (theme is Theme internalTheme)
            {
                var colorAdjustment = internalTheme.ColorAdjustment ?? new ColorAdjustment();
                return colorAdjustment.Colors;
            }
            else
            {
                return ColorSelection.All;
            }
        }
        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
}