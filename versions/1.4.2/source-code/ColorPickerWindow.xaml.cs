using PalettePicker.Resources.ColorPickerWindowResources;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace PalettePicker
{
    /// <summary>
    /// Interaction logic for ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        private string originalHex = "#000000";
        private float hue = 0;
        private float saturation = 100;
        private float luminance = 50;

        public int editingNum = -1;

        private string currentUserHexInput = string.Empty;

        private bool isProgressSaved = true;
        private bool closing = false;

        public ColorPickerWindow()
        {
            InitializeComponent();
            InfoUpdate(this);
            DataContext = this;
            SetLanguage(MainWindow.currentLanguage, this);
        }

        public static void SetLanguage(int languageID, ColorPickerWindow instance)
        {
            string[] cultures = { "en-UK", "de-DE", "es-ES", "fr-FR", "zh-CN", "pt-PT", "ru-RU" };

            if (languageID < 0 || languageID >= cultures.Length)
            {
                languageID = 0;
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultures[languageID]);
            UpdateUI(instance);
        }

        private static void UpdateUI(ColorPickerWindow instance)
        {
            string[] titles =
            [
                PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.Primary1Text,
                PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.Primary2Text,
                PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.Secondary1Text,
                PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.Secondary2Text,
                PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.TextText
            ];

            instance.Title = instance.editingNum == -1 ? PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.WindowTitle : instance.isProgressSaved ? titles[instance.editingNum] : "* " + titles[instance.editingNum];

            instance.Txt_Hue_Info.Text = PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.HueText + instance.hue.ToString();
            instance.Txt_Saturation_Info.Text = PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.SaturationText + instance.saturation.ToString();
            instance.Txt_Luminance_Info.Text = PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.LuminanceText + instance.luminance.ToString();

            InfoUpdate(instance);
        }

        private static void InfoUpdate(ColorPickerWindow instance)
        {
            instance.Txt_Hue_Info.Text = PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.HueText + Math.Round(instance.hue).ToString();
            instance.Txt_Saturation_Info.Text = PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.SaturationText + Math.Round(instance.saturation).ToString();
            instance.Txt_Luminance_Info.Text = PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.LuminanceText + Math.Round(instance.luminance).ToString();

            instance.Rct_ColorPreview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(instance.hue, instance.saturation, instance.luminance, instance)));

            instance.Rct_Brd_Hue_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(instance.hue, 100, 50, instance)));
            double hue_margin = ((instance.hue / 360.0) * 158.0) - 79;
            instance.Brd_Hue_Preview.Margin = new Thickness(hue_margin, instance.Brd_Hue_Preview.Margin.Top, instance.Brd_Hue_Preview.Margin.Right, instance.Brd_Hue_Preview.Margin.Bottom);

            instance.Rct_Brd_Saturation_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(instance.hue, instance.saturation, 50, instance)));
            double saturation_margin = ((instance.saturation / 100.0) * 158.0) - 79;
            instance.Brd_Saturation_Preview.Margin = new Thickness(saturation_margin, instance.Brd_Saturation_Preview.Margin.Top, instance.Brd_Saturation_Preview.Margin.Right, instance.Brd_Saturation_Preview.Margin.Bottom);

            instance.Rct_Brd_Luminance_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(instance.hue, instance.saturation, instance.luminance, instance)));
            double luminance_margin = ((instance.luminance / 100.0) * 158.0) - 79;
            instance.Brd_Luminance_Preview.Margin = new Thickness(luminance_margin, instance.Brd_Luminance_Preview.Margin.Top, instance.Brd_Luminance_Preview.Margin.Right, instance.Brd_Luminance_Preview.Margin.Bottom);

            instance.GrS_Color_Luminace_Normal.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(instance.hue, instance.saturation, 50, instance));
            instance.GrS_Color_Saturation_Max.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(instance.hue, 100, instance.luminance, instance));
            instance.GrS_Color_Saturation_Min.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(instance.hue, 0, instance.luminance, instance));

            instance.Txb_HexColor.Text = GetHexColor(instance.hue, instance.saturation, instance.luminance, instance);

            if (GetHexColor(instance.hue, instance.saturation, instance.luminance, instance) != instance.originalHex)
            {
                if (!instance.Title.StartsWith("* "))
                {
                    instance.Title = "* " + instance.Title;
                }
                instance.isProgressSaved = false;
            }
            else
            {
                instance.isProgressSaved = true;

                if (instance.Title.StartsWith("* "))
                {
                    instance.Title = instance.Title.Substring(2);
                }
            }
        }

        public void ColorPickInit(string hex, int editing)
        {
            editingNum = editing;

            Txb_HexColor.Text = hex;
            originalHex = hex;
            editingNum = editing;

            hue = GetHslFromHex(hex).hue;
            saturation = GetHslFromHex(hex).saturation;
            luminance = GetHslFromHex(hex).lightness;
            InfoUpdate(this);
        }

        private static string GetHexColor(float h, float s, float l, ColorPickerWindow instance)
        {
            h = instance.hue % 360;
            s /= 100;
            l /= 100;

            float c = (1f - Math.Abs(2f * l - 1f)) * s;
            float x = c * (1f - Math.Abs((h / 60f) % 2f - 1f));
            float m = l - c / 2f;

            float r = 0;
            float g = 0;
            float b = 0;

            if (h >= 0 && h < 60)
            {
                r = c; g = x; b = 0;
            }
            else if (h >= 60 && h < 120)
            {
                r = x; g = c; b = 0;
            }
            else if (h >= 120 && h < 180)
            {
                r = 0; g = c; b = x;
            }
            else if (h >= 180 && h < 240)
            {
                r = 0; g = x; b = c;
            }
            else if (h >= 240 && h < 300)
            {
                r = x; g = 0; b = c;
            }
            else if (h >= 300 && h < 360)
            {
                r = c; g = 0; b = x;
            }

            int red = (int)Math.Round((r + m) * 255);
            int green = (int)Math.Round((g + m) * 255);
            int blue = (int)Math.Round((b + m) * 255);

            return $"#{red:X2}{green:X2}{blue:X2}";
        }

        private (float hue, float saturation, float lightness) GetHslFromHex(string hexColor)
        {
            hexColor = hexColor.TrimStart('#');

            int red = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int green = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int blue = Convert.ToInt32(hexColor.Substring(4, 2), 16);

            float r = red / 255f;
            float g = green / 255f;
            float b = blue / 255f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));
            float delta = max - min;

            float l = (max + min) / 2f;

            float h = 0f;
            float s = 0f;

            if (delta != 0)
            {
                s = l > 0.5f ? delta / (2f - max - min) : delta / (max + min);

                if (max == r)
                {
                    h = (g - b) / delta + (g < b ? 6f : 0f);
                }
                else if (max == g)
                {
                    h = (b - r) / delta + 2f;
                }
                else if (max == b)
                {
                    h = (r - g) / delta + 4f;
                }
                h *= 60f;
            }

            h = h < 0 ? h + 360 : h;

            return (h, s * 100f, l * 100f);
        }

        #region ColorButtons

        #region Hue

        private void Btn_Hue_Increase_Click(object sender, RoutedEventArgs e)
        {
            hue++;
            if (hue > 360) hue = 0;

            double left = ((hue / 360.0) * 158.0) - 79;
            Brd_Hue_Preview.Margin = new Thickness(left, Brd_Hue_Preview.Margin.Top, Brd_Hue_Preview.Margin.Right, Brd_Hue_Preview.Margin.Bottom);

            InfoUpdate(this);
        }

        private void Btn_Hue_Decrease_Click(object sender, RoutedEventArgs e)
        {
            hue--;
            if (hue < 0) hue = 360;

            double left = ((hue / 360.0) * 158.0) - 79;
            Brd_Hue_Preview.Margin = new Thickness(left, Brd_Hue_Preview.Margin.Top, Brd_Hue_Preview.Margin.Right, Brd_Hue_Preview.Margin.Bottom);

            InfoUpdate(this);
        }

        #endregion

        #region Saturation

        private void Btn_Saturation_Increase_Click(object sender, RoutedEventArgs e)
        {
            saturation++;
            if (saturation > 100) saturation = 0;

            double left = ((saturation / 100.0) * 158.0) - 79;
            Brd_Saturation_Preview.Margin = new Thickness(left, Brd_Saturation_Preview.Margin.Top, Brd_Saturation_Preview.Margin.Right, Brd_Saturation_Preview.Margin.Bottom);

            InfoUpdate(this);
        }

        private void Btn_Stauration_Decrease_Click(object sender, RoutedEventArgs e)
        {
            saturation--;
            if (saturation < 0) saturation = 100;

            double left = ((saturation / 100.0) * 158.0) - 79;
            Brd_Saturation_Preview.Margin = new Thickness(left, Brd_Saturation_Preview.Margin.Top, Brd_Saturation_Preview.Margin.Right, Brd_Saturation_Preview.Margin.Bottom);

            InfoUpdate(this);
        }

        #endregion

        #region Luminance

        private void Btn_Luminance_Increase_Click(object sender, RoutedEventArgs e)
        {
            luminance++;
            if (luminance > 100) luminance = 0;

            double left = ((luminance / 100.0) * 158.0) - 158;
            Brd_Luminance_Preview.Margin = new Thickness(left, Brd_Luminance_Preview.Margin.Top, Brd_Luminance_Preview.Margin.Right, Brd_Luminance_Preview.Margin.Bottom);

            InfoUpdate(this);
        }

        private void Btn_Luminance_Decrease_Click(object sender, RoutedEventArgs e)
        {
            luminance--;
            if (luminance < 0) luminance = 100;

            double left = ((luminance / 100.0) * 158.0) - 158;
            Brd_Luminance_Preview.Margin = new Thickness(left, Brd_Luminance_Preview.Margin.Top, Brd_Luminance_Preview.Margin.Right, Brd_Luminance_Preview.Margin.Bottom);

            InfoUpdate(this);
        }

        #endregion

        #endregion

        #region HexInput

        private void Txb_HexColor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            currentUserHexInput = Txb_HexColor.Text;
        }

        private void Txb_HexColor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!ValidateHex(currentUserHexInput))
            {
                if (closing)
                {
                    this.Visibility = Visibility.Hidden;
                }

                MessageBox.Show(PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.InputWarningTitle, PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.InputWarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);

                Txb_HexColor.Text = originalHex;
                currentUserHexInput = originalHex;
            }

            hue = GetHslFromHex(currentUserHexInput).hue;
            saturation = GetHslFromHex(currentUserHexInput).saturation;
            luminance = GetHslFromHex(currentUserHexInput).lightness;

            InfoUpdate(this);
        }

        private bool ValidateHex(string hex)
        {
            string validHexChars = "ABCDEF";
            hex = hex.ToUpper();

            if (string.IsNullOrEmpty(hex)) return false;
            if (hex[0] != '#') return false;
            if (hex.Length != 7) return false;

            foreach (char c in hex.Skip(1))
            {
                if (!char.IsDigit(c))
                {
                    if (!validHexChars.Contains(c))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            string newColor = GetHexColor(hue, saturation, luminance, this);

            Action<string>[] setters = {
                val => MainWindow.Primary1 = val,
                val => MainWindow.Primary2 = val,
                val => MainWindow.Secondary1 = val,
                val => MainWindow.Secondary2 = val,
                val => MainWindow.Text = val
                };

            if (editingNum >= 0 && editingNum < setters.Length)
            {
                setters[editingNum](newColor);
            }

            isProgressSaved = true;
            this.Close();
        }

        private void Txb_HexColor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Txb_HexColor_LostFocus(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true;

            MainWindow.UpdateGridInfos((MainWindow)Application.Current.MainWindow);
            MainWindow.alreadyEditing[editingNum] = false;

            if (!isProgressSaved)
            {
                MessageBoxResult result = MessageBox.Show(PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.ExitConfirmationText, PalettePicker.Resources.ColorPickerWindowResources.ColorPickerWindow.ExitConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }

                if (GetHexColor(hue, saturation, luminance, this) != originalHex)
                {
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    MainWindow.isProgressSaved = false;
                    MainWindow.SetWindowTitle(MainWindow.currentLanguage, MainWindow.currentEditingName, mainWindow);
                }
            }
        }
    }
}
