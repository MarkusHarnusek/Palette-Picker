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
            switch (MainWindow.currentLanguage)
            {
                case 0:
                    Txt_Hue_Info.Text = $"Color Hue: {Math.Round(hue).ToString()}";
                    Txt_Saturation_Info.Text = $"Color Saturation: {Math.Round(saturation).ToString()}";
                    Txt_Luminance_Info.Text = $"Color Luminance: {Math.Round(luminance).ToString()}";
                    break;

                case 1:
                    Txt_Hue_Info.Text = $"Farbton: {Math.Round(hue).ToString()}";
                    Txt_Saturation_Info.Text = $"Farbintensität: {Math.Round(saturation).ToString()}";
                    Txt_Luminance_Info.Text = $"Farbhelligkeit: {Math.Round(luminance).ToString()}";
                    break;

                case 2:
                    Txt_Hue_Info.Text = $"Tono de color: {Math.Round(hue).ToString()}";
                    Txt_Saturation_Info.Text = $"Saturación de color: {Math.Round(saturation).ToString()}";
                    Txt_Luminance_Info.Text = $"Luminosidad del color: {Math.Round(luminance).ToString()}";
                    break;

                case 3:
                    Txt_Hue_Info.Text = $"Teinte de couleur: {Math.Round(hue).ToString()}";
                    Txt_Saturation_Info.Text = $"Saturation de couleur: {Math.Round(saturation).ToString()}";
                    Txt_Luminance_Info.Text = $"Luminosité de la couleur: {Math.Round(luminance).ToString()}";
                    break;

                case 4:
                    Txt_Hue_Info.Text = $"颜色色调: {Math.Round(hue).ToString()}";
                    Txt_Saturation_Info.Text = $"颜色饱和度: {Math.Round(saturation).ToString()}";
                    Txt_Luminance_Info.Text = $"颜色亮度: {Math.Round(luminance).ToString()}";
                    break;

                case 5:
                    Txt_Hue_Info.Text = $"Cor Hue: {Math.Round(hue).ToString()}";
                    Txt_Saturation_Info.Text = $"Cor Saturação: {Math.Round(saturation).ToString()}";
                    Txt_Luminance_Info.Text = $"Cor Luminosidade: {Math.Round(luminance).ToString()}";
                    break;

                case 6:
                    Txt_Hue_Info.Text = $"Цветовой оттенок: {Math.Round(hue).ToString()}";
                    Txt_Saturation_Info.Text = $"Цветовая насыщенность: {Math.Round(saturation).ToString()}";
                    Txt_Luminance_Info.Text = $"Цветовая яркость: {Math.Round(luminance).ToString()}";
                    break;

            }

            Rct_ColorPreview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, luminance)));
            Rct_Hue_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, 100, 50)));
            Rct_Saturation_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, 50)));
            Rct_Luminance_Preview.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, luminance)));

            GrS_Color_Luminace_Normal.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(hue, saturation, 50));
            GrS_Color_Saturation_Max.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(hue, 100, luminance));
            GrS_Color_Saturation_Min.Color = (Color)ColorConverter.ConvertFromString(GetHexColor(hue, 0, luminance));

            Txb_HexColor.Text = GetHexColor(hue, saturation, luminance);
        }

        public void SetLanguage(int languageId, ColorPickerWindow instance)
        {
            InfoUpdate();

            switch (languageId)
            {
                case 0:
                    instance.Title = "Color Picker";

                    switch (editingNum)
                    {
                        case 0: instance.Txt_CurrentEditingColor.Text = "Currently editing: Primary 1"; break;
                        case 1: instance.Txt_CurrentEditingColor.Text = "Currently editing: Primary 2"; break;
                        case 2: instance.Txt_CurrentEditingColor.Text = "Currently editing: Secondary 1"; break;
                        case 3: instance.Txt_CurrentEditingColor.Text = "Currently editing: Secondary 2"; break;
                        case 4: instance.Txt_CurrentEditingColor.Text = "Currently editing: Text"; break;
                    }

                    instance.Txt_ColorPickerTitle.Text = "Pick your desired color";
                    instance.Btn_OK.Content = "OK";

                    break;

                case 1:
                    instance.Title = "Farbwähler";

                    switch (editingNum)
                    {
                        case 0: instance.Txt_CurrentEditingColor.Text = "Derzeit bearbeitet: Primär 1"; break;
                        case 1: instance.Txt_CurrentEditingColor.Text = "Derzeit bearbeitet: Primär 2"; break;
                        case 2: instance.Txt_CurrentEditingColor.Text = "Derzeit bearbeitet: Sekundär 1"; break;
                        case 3: instance.Txt_CurrentEditingColor.Text = "Derzeit bearbeitet: Sekundär 2"; break;
                        case 4: instance.Txt_CurrentEditingColor.Text = "Derzeit bearbeitet: Text"; break;
                    }

                    instance.Txt_ColorPickerTitle.Text = "Wähle die gewünschte Farbe";
                    instance.Btn_OK.Content = "OK";

                    break;

                case 2:
                    instance.Title = "Selector de color";

                    switch (editingNum)
                    {
                        case 0: instance.Txt_CurrentEditingColor.Text = "Actualmente editando: Primario 1"; break;
                        case 1: instance.Txt_CurrentEditingColor.Text = "Actualmente editando: Primario 2"; break;
                        case 2: instance.Txt_CurrentEditingColor.Text = "Actualmente editando: Secundario 1"; break;
                        case 3: instance.Txt_CurrentEditingColor.Text = "Actualmente editando: Secundario 2"; break;
                        case 4: instance.Txt_CurrentEditingColor.Text = "Actualmente editando: Texto"; break;
                    }

                    instance.Txt_ColorPickerTitle.Text = "Seleccione su color deseado";
                    instance.Btn_OK.Content = "OK";

                    break;

                case 3:
                    instance.Title = "Sélecteur de couleur";

                    switch (editingNum)
                    {
                        case 0: instance.Txt_CurrentEditingColor.Text = "Actuellement en cours d'édition : Primaire 1"; break;
                        case 1: instance.Txt_CurrentEditingColor.Text = "Actuellement en cours d'édition : Primaire 2"; break;
                        case 2: instance.Txt_CurrentEditingColor.Text = "Actuellement en cours d'édition : Secondaire 1"; break;
                        case 3: instance.Txt_CurrentEditingColor.Text = "Actuellement en cours d'édition : Secondaire 2"; break;
                        case 4: instance.Txt_CurrentEditingColor.Text = "Actuellement en cours d'édition : Texte"; break;
                    }

                    instance.Txt_ColorPickerTitle.Text = "Sélectionnez la couleur souhaitée";
                    instance.Btn_OK.Content = "OK";

                    break;

                case 4:
                    instance.Title = "颜色选择器";

                    switch (editingNum)
                    {
                        case 0: instance.Txt_CurrentEditingColor.Text = "当前编辑：主色1"; break;
                        case 1: instance.Txt_CurrentEditingColor.Text = "当前编辑：主色2"; break;
                        case 2: instance.Txt_CurrentEditingColor.Text = "当前编辑：辅助色1"; break;
                        case 3: instance.Txt_CurrentEditingColor.Text = "当前编辑：辅助色2"; break;
                        case 4: instance.Txt_CurrentEditingColor.Text = "当前编辑：文本"; break;
                    }

                    instance.Txt_ColorPickerTitle.Text = "选择您想要的颜色";
                    instance.Btn_OK.Content = "确定";

                    break;

                case 5:
                    instance.Title = "Seletor de cores";

                    switch (editingNum)
                    {
                        case 0: instance.Txt_CurrentEditingColor.Text = "Editando atualmente: Primário 1"; break;
                        case 1: instance.Txt_CurrentEditingColor.Text = "Editando atualmente: Primário 2"; break;
                        case 2: instance.Txt_CurrentEditingColor.Text = "Editando atualmente: Secundário 1"; break;
                        case 3: instance.Txt_CurrentEditingColor.Text = "Editando atualmente: Secundário 2"; break;
                        case 4: instance.Txt_CurrentEditingColor.Text = "Editando atualmente: Texto"; break;
                    }

                    instance.Txt_ColorPickerTitle.Text = "Selecione a cor desejada";
                    instance.Btn_OK.Content = "OK";

                    break;

                case 6:
                    instance.Title = "Выбор цвета";

                    switch (editingNum)
                    {
                        case 0: instance.Txt_CurrentEditingColor.Text = "В данный момент редактируется: Основной 1"; break;
                        case 1: instance.Txt_CurrentEditingColor.Text = "В данный момент редактируется: Основной 2"; break;
                        case 2: instance.Txt_CurrentEditingColor.Text = "В данный момент редактируется: Вторичный 1"; break;
                        case 3: instance.Txt_CurrentEditingColor.Text = "В данный момент редактируется: Вторичный 2"; break;
                        case 4: instance.Txt_CurrentEditingColor.Text = "В данный момент редактируется: Текст"; break;
                    }

                    instance.Txt_ColorPickerTitle.Text = "Выберите желаемый цвет";
                    instance.Btn_OK.Content = "ОК";

                    break;
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

                switch (MainWindow.currentLanguage)
                {
                    case 0:
                        MessageBox.Show("The inputed hex was not in the right format. Reverting to the original color.", "Input Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;

                    case 1:
                        MessageBox.Show("Der eingegebene Hex-Code war nicht im richtigen Format. Rückkehr zur Originalfarbe.", "Eingabewarnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;

                    case 2:
                        MessageBox.Show("El hex ingresado no estaba en el formato correcto. Volviendo al color original.", "Advertencia de entrada", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;

                    case 3:
                        MessageBox.Show("Le code hexadécimal saisi n'était pas au bon format. Retour à la couleur d'origine.", "Avertissement d'entrée", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;

                    case 4:
                        MessageBox.Show("输入的十六进制格式不正确。恢复到原始颜色。", "输入警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;

                    case 5:
                        MessageBox.Show("O hex inserido não estava no formato correto. Revertendo para a cor original.", "Aviso de entrada", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;

                    case 6:
                        MessageBox.Show("Введенный hex-код был неверного формата. Возврат к оригинальному цвету.", "Предупреждение о вводе", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                }

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
