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

        public ColorPickerWindow()
        {
            InitializeComponent();
            InfoUpdate();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.UpdateGridInfos((MainWindow)Application.Current.MainWindow);
            MainWindow.alreadyEditing[editingNum] = false;
        }

        private void InfoUpdate()
        {
            Txt_Hue_Info.Text = $"Color Hue: {Math.Round(hue).ToString()}";
            Txt_Saturation_Info.Text = $"Color Saturation: {Math.Round(saturation).ToString()}";
            Txt_Luminance_Info.Text = $"Color Luminance: {Math.Round(luminance).ToString()}";

            Rct_ColorPreview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, luminance)));
            Rct_Hue_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, 100, 50)));
            Rct_Saturation_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, 50)));
            Rct_Luminance_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, luminance)));

            GrS_Color_Luminace_Normal.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, 50));
            GrS_Color_Saturation_Max.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(hue, 100, luminance));
            GrS_Color_Saturation_Min.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(hue, 0, luminance));

            Txb_HexColor.Text = GetHexColor(hue, saturation, luminance);
        }

        public void ColorPickInit(string hex, int editing)
        {
            switch (editing)
            {
                case 0: Txt_CurrentEditingColor.Text = "Primary 1"; break;
                case 1: Txt_CurrentEditingColor.Text = "Primary 2"; break;
                case 2: Txt_CurrentEditingColor.Text = "Secondary 1"; break;
                case 3: Txt_CurrentEditingColor.Text = "Secondary 2"; break;
                case 4: Txt_CurrentEditingColor.Text = "Text"; break;
            }

            Txb_HexColor.Text = hex;
            originalHex = hex;
            editingNum = editing;

            hue = GetHslFromHex(hex).hue;
            saturation = GetHslFromHex(hex).saturation;
            luminance = GetHslFromHex(hex).lightness;
            InfoUpdate();
        }

        private string GetHexColor(float h, float s, float l)
        {
            h = hue % 360;
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

            InfoUpdate();
        }

        private void Btn_Hue_Decrease_Click(object sender, RoutedEventArgs e)
        {
            hue--;
            if (hue < 0) hue = 360;

            InfoUpdate();
        }


        #endregion

        #region Saturation

        private void Btn_Saturation_Increase_Click(object sender, RoutedEventArgs e)
        {
            saturation++;
            if (saturation > 100) saturation = 0;

            InfoUpdate();
        }

        private void Btn_Stauration_Decrease_Click(object sender, RoutedEventArgs e)
        {
            saturation--;
            if (saturation < 0) saturation = 100;

            InfoUpdate();
        }

        #endregion

        #region Luminance

        private void Btn_Luminance_Increase_Click(object sender, RoutedEventArgs e)
        {
            luminance++;
            if (luminance > 100) luminance = 0;

            InfoUpdate();
        }

        private void Btn_Luminance_Decrease_Click(object sender, RoutedEventArgs e)
        {
            luminance--;
            if (luminance < 0) luminance = 100;

            InfoUpdate();
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
                MessageBox.Show("The inputed hex was not in the right format. Reverting to the original color.", "Input Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Txb_HexColor.Text = originalHex;
                currentUserHexInput = originalHex;
            }

            hue = GetHslFromHex(currentUserHexInput).hue;
            saturation = GetHslFromHex(currentUserHexInput).saturation;
            luminance = GetHslFromHex(currentUserHexInput).lightness;

            InfoUpdate();
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
            switch (editingNum)
            {
                case 0:
                    MainWindow.Primary1 = GetHexColor(hue, saturation, luminance);
                    break;

                case 1:
                    string hexColor = GetHexColor(hue, saturation, luminance);
                    MainWindow.Primary2 = GetHexColor(hue, saturation, luminance);
                    break;

                case 2:
                    MainWindow.Secondary1 = GetHexColor(hue, saturation, luminance);
                    break;

                case 3:
                    MainWindow.Secondary2 = GetHexColor(hue, saturation, luminance);
                    break;

                case 4:
                    MainWindow.Text = GetHexColor(hue, saturation, luminance);
                    break;
            }

            this.Close();
        }

        private void Txb_HexColor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Txb_HexColor_LostFocus(sender, e);
            }
        }
    }
}
