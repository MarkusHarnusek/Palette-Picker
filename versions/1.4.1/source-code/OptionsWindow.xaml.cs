using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace PalettePicker
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private int language = 0;
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
            switch (languageID)
            {
                case 0:
                    Txt_WindowTitle.Text = "Options";
                    Btn_Language.Content = "Language";
                    Txt_Language_Title.Text = "Language";
                    this.Title = "Options";

                    break;

                case 1:
                    Txt_WindowTitle.Text = "Einstellungen";
                    Btn_Language.Content = "Sprache";
                    Txt_Language_Title.Text = "Sprache";
                    this.Title = "Einstellungen";

                    break;

                case 2:
                    Txt_WindowTitle.Text = "Ajustes";
                    Btn_Language.Content = "Idioma";
                    Txt_Language_Title.Text = "Idioma";
                    this.Title = "Ajustes";

                    break;

                case 3:
                    Txt_WindowTitle.Text = "Paramètres";
                    Btn_Language.Content = "Langue";
                    Txt_Language_Title.Text = "Langue";
                    this.Title = "Paramètres";

                    break;

                case 4:
                    Txt_WindowTitle.Text = "设置";
                    Btn_Language.Content = "语言";
                    Txt_Language_Title.Text = "语言";
                    this.Title = "设置";

                    break;

                case 5:
                    Txt_WindowTitle.Text = "Configurações";
                    Btn_Language.Content = "Idioma";
                    Txt_Language_Title.Text = "Idioma";
                    this.Title = "Configurações";

                    break;

                case 6:
                    Txt_WindowTitle.Text = "Настройки";
                    Btn_Language.Content = "Язык";
                    Txt_Language_Title.Text = "Язык";
                    this.Title = "Настройки";

                    break;
            }
        }

        //private (string msg, string title) GetTranslatedEmptyLanguageSelectionErrorMessage(int languageId)
        //{
        //    if (Lst_Language.SelectedItem == null)
        //    {
        //        string msg = string.Empty;
        //        string title = string.Empty;

        //        switch (languageId)
        //        {
        //            case 0:
        //                msg = "Please select a language.";
        //                title = "Error";
        //                break;

        //            case 1:
        //                msg = "Bitte wählen Sie eine Sprache aus.";
        //                title = "Fehler";
        //                break;

        //            case 2:
        //                msg = "Por favor, seleccione un idioma.";
        //                title = "Error";
        //                break;

        //            case 3:
        //                msg = "Veuillez sélectionner une langue.";
        //                title = "Erreur";
        //                break;

        //            case 4:
        //                msg = "请选择一种语言。";
        //                title = "错误";
        //                break;

        //            case 5:
        //                msg = "Por favor, selecione um idioma.";
        //                title = "Erro";
        //                break;

        //            case 6:
        //                msg = "Пожалуйста, выберите язык.";
        //                title = "Ошибка";
        //                break;
        //        }

        //        return (msg, title);
        //    }

        //    return (string.Empty, string.Empty);
        //}

        //private void Btn_Apply_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Lst_Language.SelectedItem == null)
        //    {
        //        MessageBox.Show(GetTranslatedEmptyLanguageSelectionErrorMessage(MainWindow.currentLanguage).msg, GetTranslatedEmptyLanguageSelectionErrorMessage(MainWindow.currentLanguage).title, MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //    else
        //    {
        //        MainWindow.currentLanguage = Lst_Language.SelectedIndex;
        //        SetLanguage(MainWindow.currentLanguage);

        //        foreach (Window window in Application.Current.Windows)
        //        {
        //            if (window is ColorPickerWindow colorPickerWindow)
        //            {
        //                colorPickerWindow.SetLanguage(MainWindow.currentLanguage, colorPickerWindow);
        //            }
        //            else if (window is MainWindow mainWindow)
        //            {
        //                MainWindow.SetLanguage(MainWindow.currentLanguage, mainWindow);
        //            }
        //        }

        //    }
        //}

        private void Brd_Btn_Preferences_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Border border)
            {
                if (border.Tag is not double)
                {
                    border.Tag = border.Height;
                }

                double originalHeight = (double)border.Tag;
                double targetHeight = originalHeight + 3;

                var animation = new System.Windows.Media.Animation.DoubleAnimation
                {
                    From = border.ActualHeight,
                    To = targetHeight,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                    EasingFunction = new System.Windows.Media.Animation.CubicEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut }
                };
                border.BeginAnimation(HeightProperty, animation, System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace);
            }
        }

        private void Brd_Btn_Preferences_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Border border && border.Tag is double originalHeight)
            {
                var animation = new System.Windows.Media.Animation.DoubleAnimation
                {
                    From = border.ActualHeight,
                    To = originalHeight,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                    EasingFunction = new System.Windows.Media.Animation.CubicEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut }
                };
                border.BeginAnimation(HeightProperty, animation, System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace);
            }
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
                        colorPickerWindow.SetLanguage(MainWindow.currentLanguage, colorPickerWindow);
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
