using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Globalization;
using PalettePicker.Resources.OptionsWindowResources;

namespace PalettePicker
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public ObservableCollection<string> MyItems { get; } = new ObservableCollection<string> {
            "Select a language...",
            "English",
            "Deutsch",
            "Español",
            "Français",
            "中国人",
            "Português",
            "Русский"
        };
        public OptionsWindow()
        {
            InitializeComponent();
            DataContext = this;
            SetLanguage(MainWindow.currentLanguage);
        }

        private void ResetGrdVisibility()
        {
            Grd_Appearance.Visibility = Visibility.Collapsed;
            Grd_Language.Visibility = Visibility.Collapsed;
            Grd_None.Visibility = Visibility.Collapsed;
        }

        private void SetLanguage(int languageID)
        {
            string[] cultures = { "en-UK", "de-DE", "es-ES", "fr-FR", "zh-CN", "pt-PT", "ru-RU" };

            if (languageID < 0 || languageID >= cultures.Length)
            {
                languageID = 0;
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultures[languageID]);
            UpdateUI();
        }

        private void UpdateUI()
        {
            Title = PalettePicker.Resources.OptionsWindowResources.OptionsWindow.WindowTitle;
            Txt_WindowTitle.Text = PalettePicker.Resources.OptionsWindowResources.OptionsWindow.WindowTitle;
            Btn_Language.Content = PalettePicker.Resources.OptionsWindowResources.OptionsWindow.LanguageGridButton;
            Txt_Language_Title.Text = PalettePicker.Resources.OptionsWindowResources.OptionsWindow.LanguageSideButton;
        }

        #region MenuSelectionControls

        private void Btn_Language_Click(object sender, RoutedEventArgs e)
        {
            ResetGrdVisibility();
            Grd_Language.Visibility = Visibility.Visible;
        }

        private void Btn_Appearance_Click(object sender, RoutedEventArgs e)
        {
            ResetGrdVisibility();
            Grd_Appearance.Visibility = Visibility.Visible;
        }

        #endregion

        private void Cmb_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cmb_Language.SelectedItem != null && Cmb_Language.SelectedIndex != 0)
            {
                MainWindow.currentLanguage = Cmb_Language.SelectedIndex - 1;
                SetLanguage(MainWindow.currentLanguage);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is ColorPickerWindow colorPickerWindow)
                    {
                        ColorPickerWindow.SetLanguage(MainWindow.currentLanguage, colorPickerWindow);
                    }
                    else if (window is MainWindow mainWindow)
                    {
                        MainWindow.SetLanguage(MainWindow.currentLanguage, mainWindow);
                    }
                }
            }
        }
    }
}
