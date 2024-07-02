using ExcelConverter.Models;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using R3;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ExcelConverter.ViewModels
{
    public class ThemeBoxViewModel : ViewModelBase
    {
        public ReactiveCommand<Swatch> ApplyPrimaryCommand { get; }
        public ReactiveCommand<Swatch> ApplyAccentCommand { get; }
        public ReactiveCommand<Unit> SaveCommand { get; }
        public BindableReactiveProperty<Color> PrimaryColor { get; }
        public BindableReactiveProperty<Color> AccentColor { get; }
        public ThemeConfig Config { get; }
        public ThemeSettingsViewModel ThemeSettingsViewModel { get; }
        public ThemeBoxViewModel(ThemeConfig config) : base()
        {
            this.ThemeSettingsViewModel = new ThemeSettingsViewModel(config);
            this.Config = config;

            this.AccentColor = new BindableReactiveProperty<Color>(config.AccentColor);
            this.PrimaryColor = new BindableReactiveProperty<Color>(config.PrimaryColor);

            this.SaveCommand = new ReactiveCommand<Unit>();
            this.SaveCommand.Subscribe(_ =>
            {
                this.Config.IsDarkTheme = this.ThemeSettingsViewModel.IsDarkTheme.Value;
                this.Config.IsColorAdjusted = this.ThemeSettingsViewModel.IsColorAdjusted.Value;
                this.Config.ContrastValue = this.ThemeSettingsViewModel.ContrastValue.Value;
                this.Config.DesiredContrastRatio = this.ThemeSettingsViewModel.DesiredContrastRatio.Value;
                this.Config.ColorSelectionValue = this.ThemeSettingsViewModel.ColorSelectionValue.Value;
                this.Config.AccentColor = this.AccentColor.Value;
                this.Config.PrimaryColor = this.PrimaryColor.Value;
            });
            ModifyTheme(theme => theme.SetSecondaryColor(config.AccentColor));
            ModifyTheme(theme => theme.SetPrimaryColor(config.PrimaryColor));

            Swatches = new SwatchesProvider().Swatches;
            this.ApplyAccentCommand = new ReactiveCommand<Swatch>();
            this.ApplyAccentCommand.Subscribe(x =>
            {
                ApplyAccent((Swatch)x!);
                if (x is { AccentExemplarHue: not null })
                {
                    this.AccentColor.Value = (x.AccentExemplarHue.Color);
                }
            });
            this.ApplyPrimaryCommand = new ReactiveCommand<Swatch>();
            this.ApplyPrimaryCommand.Subscribe(x =>
            {
                ApplyPrimary((Swatch)x!);
                this.PrimaryColor.Value = x.ExemplarHue.Color;
            });
        }

        public IEnumerable<Swatch> Swatches { get; }

        private static void ApplyPrimary(Swatch swatch)
            => ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));

        private static void ApplyAccent(Swatch swatch)
        {
            if (swatch is { AccentExemplarHue: not null })
            {
                ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
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
