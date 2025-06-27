using PalettePicker.Resources.MainWindowResources;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PalettePicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region CurrentColors

        public static string? Primary1;
        public static string? Primary2;
        public static string? Secondary1;
        public static string? Secondary2;
        public static string? Text;

        #endregion

        public static string currentEditingName = string.Empty;

        public static bool[] alreadyEditing = new bool[5];

        public static string editingFilePath = string.Empty;

        public static int currentLanguage = 0;
        public static bool isProgressSaved = true;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;   
        }

        private static void SetProgressSaved(bool saved, MainWindow instance)
        {
            isProgressSaved = saved;
            SetWindowTitle(currentLanguage, currentEditingName, instance);
        }

        public static void SetWindowTitle(int languageID, string name, MainWindow instance)
        {
            string text;

            if (string.IsNullOrEmpty(name))
            {
                text = PalettePicker.Resources.MainWindowResources.MainWindow.WindowTitle;
            }
            else
            {
                text = name;
            }

            if (!isProgressSaved)
            {
                text = "* " + text;
            }

            instance.Title = text;
        }

        public static void SetLanguage(int languageID, MainWindow instance)
        {
            string[] cultures = { "en-UK", "de-DE", "es-ES", "fr-FR", "zh-CN", "pt-PT", "ru-RU" };

            if (languageID < 0 || languageID >= cultures.Length)
            {
                languageID = 0;
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultures[languageID]);
            SetWindowTitle(languageID, currentEditingName, instance);
            UpdateUI(instance);
        }

        private static void UpdateUI(MainWindow instance)
        {
            instance.Btn_Select.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Select;
            instance.Btn_Save.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Save;
            instance.Btn_Generate.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Generate;
            instance.Btn_Options.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Options;

            instance.Txt_Primary1Title.Text = PalettePicker.Resources.MainWindowResources.MainWindow.Primary1;
            instance.Txt_Primary2Title.Text = PalettePicker.Resources.MainWindowResources.MainWindow.Primary2;
            instance.Txt_Secondary1Title.Text = PalettePicker.Resources.MainWindowResources.MainWindow.Secondary1;
            instance.Txt_Secondary2Title.Text = PalettePicker.Resources.MainWindowResources.MainWindow.Secondary2;
            instance.Txt_TextTitle.Text = PalettePicker.Resources.MainWindowResources.MainWindow.Text;

            instance.Btn_Primary1_Edit.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Edit;
            instance.Btn_Primary2_Edit.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Edit;
            instance.Btn_Secondary1_Edit.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Edit;
            instance.Btn_Secondary2_Edit.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Edit;
            instance.Btn_Text_Edit.Content = PalettePicker.Resources.MainWindowResources.MainWindow.Edit;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            SetRandomColors();
        }

        private void SetRandomColors()
        {

            for (int i = 0; i < 5; i++)
            {
                string color = GetRandomColor();
                switch (i)
                {
                    case 0:
                        Grd_Primary1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                        Txt_Primary1.Text = color;
                        Primary1 = color;
                        break;

                    case 1:
                        Grd_Primary2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                        Txt_Primary2.Text = color;
                        Primary2 = color;
                        break;

                    case 2:
                        Grd_Secondary1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                        Txt_Secondary1.Text = color;
                        Secondary1 = color;
                        break;

                    case 3:
                        Grd_Secondary2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                        Txt_Secondary2.Text = color;
                        Secondary2 = color;
                        break;

                    case 4:
                        Grd_Text.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                        Txt_Text.Text = color;
                        Text = color;
                        break;
                }
            }
        }

        public static void UpdateGridInfos(MainWindow instance)
        {
           instance.Grd_Primary1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Primary1));
            instance.Grd_Primary2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Primary2));
            instance.Grd_Secondary1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Secondary1));
            instance.Grd_Secondary2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Secondary2));
            instance.Grd_Text.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Text));

            instance.Txt_Primary1.Text = Primary1;
            instance.Txt_Primary2.Text = Primary2;
            instance.Txt_Secondary1.Text = Secondary1;
            instance.Txt_Secondary2.Text = Secondary2;
            instance.Txt_Text.Text = Text;
        }

        private string GetRandomColor()
        {
            Random random = new Random();

            string r = random.Next(0, 256).ToString("X2");
            string g = random.Next(0, 256).ToString("X2");
            string b = random.Next(0, 256).ToString("X2");

            return "#" + r + g + b;
        }

        private void InitColorPick(string currentColor, int editing)
        {
            ColorPickerWindow colorPickerWindow = new ColorPickerWindow();
            colorPickerWindow.ColorPickInit(currentColor, editing);
            ColorPickerWindow.SetLanguage(currentLanguage, colorPickerWindow);
            colorPickerWindow.Show();
        }

        #region HeadBarControls

        #region BtnGenerate

        private void Btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            SetRandomColors();
            SetProgressSaved(false, this);
        }

        #endregion

        #region BtnSave

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string name = Save.SaveFile(Primary1 ?? string.Empty, Primary2 ?? string.Empty, Secondary1 ?? string.Empty, Secondary2 ?? string.Empty, Text ?? string.Empty, false, true, true);
            currentEditingName = name;
            SetProgressSaved(true, this);
        }

        #endregion

        #region BtnSelect

        private void Btn_Select_Click(object sender, RoutedEventArgs e)
        {
            Save.SelectFile();
            if (!string.IsNullOrEmpty(editingFilePath))
            {
                currentEditingName = Save.GetSaveInfo(editingFilePath).name;
                Primary1 = Save.GetSaveInfo(editingFilePath).primary1;
                Primary2 = Save.GetSaveInfo(editingFilePath).primary2;
                Secondary1 = Save.GetSaveInfo(editingFilePath).secondary1;
                Secondary2 = Save.GetSaveInfo(editingFilePath).secondary2;
                Text = Save.GetSaveInfo(editingFilePath).text;
                SetProgressSaved(true, this);
                UpdateGridInfos(this);
            }
        }

        #endregion

        #region BtnOptions  

        private void Btn_Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Show();
        }

        #endregion

        #endregion

        #region ColorGrids

        [STAThread]
        private void Btn_Primary1_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Primary1);
        }

        [STAThread]
        private void Btn_Primary2_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Primary2);
        }

        [STAThread]
        private void Btn_Secondary1_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Secondary1);
        }

        [STAThread]
        private void Btn_Secondary2_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Secondary2);
        }

        [STAThread]
        private void Btn_Text_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Text);
        }

        private void Btn_Primary1_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[0])
            {
                MessageBox.Show(PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoText, PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[0] = true;
            InitColorPick(Primary1 ?? string.Empty, 0);
        }

        private void Btn_Primary2_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[1])
            {
                MessageBox.Show(PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoText, PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[1] = true;
            InitColorPick(Primary2 ?? string.Empty, 1);
        }

        private void Btn_Secondary1_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[2])
            {
                MessageBox.Show(PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoText, PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[2] = true;
            InitColorPick(Secondary1 ?? string.Empty, 2);
        }

        private void Btn_Secondary2_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[3])
            {
                MessageBox.Show(PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoText, PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[3] = true;
            InitColorPick(Secondary2 ?? string.Empty, 3);
        }

        private void Btn_Text_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[4])
            {
                MessageBox.Show(PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoText, PalettePicker.Resources.MainWindowResources.MainWindow.EditInfoTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[4] = true;
            InitColorPick(Text ?? string.Empty, 4);
        }

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isProgressSaved)
            {
                MessageBoxResult result = MessageBox.Show(PalettePicker.Resources.MainWindowResources.MainWindow.ExitConfirmationText, PalettePicker.Resources.MainWindowResources.MainWindow.ExitConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}