using System.ComponentModel;
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

        #region CurrentEditingConfig

        private bool readOnly = false;
        private bool homeVisible = false;
        private bool pinned = false;

        #endregion

        private string currentEditingName = string.Empty;

        public static bool[] alreadyEditing = new bool[5];

        public static string editingFilePath = string.Empty;

        public static int currentLanguage = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        public static void SetLanguage(int languageID, MainWindow instance)
        {
            switch (languageID)
            {
                case 0:
                    instance.Title = "Palette Picker";
                    instance.Btn_Create.Content = "Create";
                    instance.Btn_Save.Content = "Save";
                    instance.Btn_Select.Content = "Select";
                    instance.Btn_Options.Content = "Options";
                    instance.Txt_PaletteNameTitle.Text = "Palette Name:";
                    instance.Txt_Primary1Title.Text = "Primary 1";
                    instance.Txt_Primary2Title.Text = "Primary 2";
                    instance.Txt_Secondary1Title.Text = "Secondary 1";
                    instance.Txt_Secondary2Title.Text = "Secondary 2";
                    instance.Txt_TextTitle.Text = "Text";
                    instance.Btn_Primary1_Edit.Content = "Edit";
                    instance.Btn_Primary2_Edit.Content = "Edit";
                    instance.Btn_Secondary1_Edit.Content = "Edit";
                    instance.Btn_Secondary2_Edit.Content = "Edit";
                    instance.Btn_Text_Edit.Content = "Edit";
                    break;

                case 1:
                    instance.Title = "Palettenauswahl";
                    instance.Btn_Create.Content = "Erstellen";
                    instance.Btn_Save.Content = "Speichern";
                    instance.Btn_Select.Content = "Auswählen";
                    instance.Btn_Options.Content = "Einstellungen";
                    instance.Txt_PaletteNameTitle.Text = "Palettenname:";
                    instance.Txt_Primary1Title.Text = "Primär 1";
                    instance.Txt_Primary2Title.Text = "Primär 2";
                    instance.Txt_Secondary1Title.Text = "Sekundär 1";
                    instance.Txt_Secondary2Title.Text = "Sekundär 2";
                    instance.Txt_TextTitle.Text = "Text";
                    instance.Btn_Primary1_Edit.Content = "Bearbeiten";
                    instance.Btn_Primary2_Edit.Content = "Bearbeiten";
                    instance.Btn_Secondary1_Edit.Content = "Bearbeiten";
                    instance.Btn_Secondary2_Edit.Content = "Bearbeiten";
                    instance.Btn_Text_Edit.Content = "Bearbeiten";
                    break;

                case 2:
                    instance.Title = "Selector de paleta";
                    instance.Btn_Create.Content = "Crear";
                    instance.Btn_Save.Content = "Ahorrar";
                    instance.Btn_Select.Content = "Seleccionar";
                    instance.Btn_Options.Content = "Opciones";
                    instance.Txt_PaletteNameTitle.Text = "Nombre de la paleta:";
                    instance.Txt_Primary1Title.Text = "Primario 1";
                    instance.Txt_Primary2Title.Text = "Primario 2";
                    instance.Txt_Secondary1Title.Text = "Secundario 1";
                    instance.Txt_Secondary2Title.Text = "Secundario 2";
                    instance.Txt_TextTitle.Text = "Texto";
                    instance.Btn_Primary1_Edit.Content = "Editar";
                    instance.Btn_Primary2_Edit.Content = "Editar";
                    instance.Btn_Secondary1_Edit.Content = "Editar";
                    instance.Btn_Secondary2_Edit.Content = "Editar";
                    instance.Btn_Text_Edit.Content = "Editar";
                    break;

                case 3:
                    instance.Title = "Sélecteur de palette";
                    instance.Btn_Create.Content = "Créer";
                    instance.Btn_Save.Content = "Sauvegarder";
                    instance.Btn_Select.Content = "Sélectionner";
                    instance.Btn_Options.Content = "Options";
                    instance.Txt_PaletteNameTitle.Text = "Nom de la palette:";
                    instance.Txt_Primary1Title.Text = "Primaire 1";
                    instance.Txt_Primary2Title.Text = "Primaire 2";
                    instance.Txt_Secondary1Title.Text = "Secondaire 1";
                    instance.Txt_Secondary2Title.Text = "Secondaire 2";
                    instance.Txt_TextTitle.Text = "Texte";
                    instance.Btn_Primary1_Edit.Content = "Éditer";
                    instance.Btn_Primary2_Edit.Content = "Éditer";
                    instance.Btn_Secondary1_Edit.Content = "Éditer";
                    instance.Btn_Secondary2_Edit.Content = "Éditer";
                    instance.Btn_Text_Edit.Content = "Éditer";
                    break;

                case 4:
                    instance.Title = "调色板选择器";
                    instance.Btn_Create.Content = "创建";
                    instance.Btn_Save.Content = "保存";
                    instance.Btn_Select.Content = "选择";
                    instance.Btn_Options.Content = "选项";
                    instance.Txt_PaletteNameTitle.Text = "调色板名称:";
                    instance.Txt_Primary1Title.Text = "主要 1";
                    instance.Txt_Primary2Title.Text = "主要 2";
                    instance.Txt_Secondary1Title.Text = "次要 1";
                    instance.Txt_Secondary2Title.Text = "次要 2";
                    instance.Txt_TextTitle.Text = "文本";
                    instance.Btn_Primary1_Edit.Content = "编辑";
                    instance.Btn_Primary2_Edit.Content = "编辑";
                    instance.Btn_Secondary1_Edit.Content = "编辑";
                    instance.Btn_Secondary2_Edit.Content = "编辑";
                    instance.Btn_Text_Edit.Content = "编辑";
                    break;

                case 5:
                    instance.Title = "Seletor de paleta";
                    instance.Btn_Create.Content = "Criar";
                    instance.Btn_Save.Content = "Salvar";
                    instance.Btn_Select.Content = "Selecionar";
                    instance.Btn_Options.Content = "Opções";
                    instance.Txt_PaletteNameTitle.Text = "Nome da paleta:";
                    instance.Txt_Primary1Title.Text = "Primário 1";
                    instance.Txt_Primary2Title.Text = "Primário 2";
                    instance.Txt_Secondary1Title.Text = "Secundário 1";
                    instance.Txt_Secondary2Title.Text = "Secundário 2";
                    instance.Txt_TextTitle.Text = "Texto";
                    instance.Btn_Primary1_Edit.Content = "Editar";
                    instance.Btn_Primary2_Edit.Content = "Editar";
                    instance.Btn_Secondary1_Edit.Content = "Editar";
                    instance.Btn_Secondary2_Edit.Content = "Editar";
                    instance.Btn_Text_Edit.Content = "Editar";
                    break;

                case 6:
                    instance.Title = "Выбор палитры";
                    instance.Btn_Create.Content = "Создать";
                    instance.Btn_Save.Content = "Сохранить";
                    instance.Btn_Select.Content = "Выбрать";
                    instance.Btn_Options.Content = "Настройки";
                    instance.Txt_PaletteNameTitle.Text = "Имя палитры:";
                    instance.Txt_Primary1Title.Text = "Основной 1";
                    instance.Txt_Primary2Title.Text = "Основной 2";
                    instance.Txt_Secondary1Title.Text = "Вторичный 1";
                    instance.Txt_Secondary2Title.Text = "Вторичный 2";
                    instance.Txt_TextTitle.Text = "Текст";
                    instance.Btn_Primary1_Edit.Content = "Редактировать";
                    instance.Btn_Primary2_Edit.Content = "Редактировать";
                    instance.Btn_Secondary1_Edit.Content = "Редактировать";
                    instance.Btn_Secondary2_Edit.Content = "Редактировать";
                    instance.Btn_Text_Edit.Content = "Редактировать";
                    break;
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            SetRandomColors();
            Txb_PalleteName.Text = GetRandomPalleteName();
            currentEditingName = Txb_PalleteName.Text;
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
            colorPickerWindow.SetLanguage(currentLanguage, colorPickerWindow);
            colorPickerWindow.Show();
        }

        #region HeadBarControls

        private void Btn_Create_Click(object sender, RoutedEventArgs e)
        {
            SetRandomColors();
            Txb_PalleteName.Text = GetRandomPalleteName();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            Save.SaveFile(currentEditingName, Primary1 ?? string.Empty, Primary2 ?? string.Empty, Secondary1 ?? string.Empty, Secondary2 ?? string.Empty, Text ?? string.Empty, false, true, true);
        }

        private void Btn_Select_Click(object sender, RoutedEventArgs e)
        {
            Save.SelectFile();
            if (!string.IsNullOrEmpty(editingFilePath))
            {
                currentEditingName = Save.GetSaveInfo(editingFilePath).name;
                Txb_PalleteName.Text = currentEditingName;
                Primary1 = Save.GetSaveInfo(editingFilePath).primary1;
                Primary2 = Save.GetSaveInfo(editingFilePath).primary2;
                Secondary1 = Save.GetSaveInfo(editingFilePath).secondary1;
                Secondary2 = Save.GetSaveInfo(editingFilePath).secondary2;
                Text = Save.GetSaveInfo(editingFilePath).text;
                readOnly = Save.GetSaveInfo(editingFilePath).readOnly;
                homeVisible = Save.GetSaveInfo(editingFilePath).homeVisible;
                pinned = Save.GetSaveInfo(editingFilePath).pinned;
                UpdateGridInfos(this);
            }
        }

        private void Btn_Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Show();
        }

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
                MessageBox.Show("This color is already being edited.", "Edit Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[0] = true;
            InitColorPick(Primary1 ?? string.Empty, 0);
        }

        private void Btn_Primary2_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[1])
            {
                MessageBox.Show("This color is already being edited.", "Edit Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[1] = true;

            InitColorPick(Primary2 ?? string.Empty, 1);
        }

        private void Btn_Secondary1_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[2])
            {
                MessageBox.Show("This color is already being edited.", "Edit Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[2] = true;
            InitColorPick(Secondary1 ?? string.Empty, 2);
        }

        private void Btn_Secondary2_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[3])
            {
                MessageBox.Show("This color is already being edited.", "Edit Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[3] = true;
            InitColorPick(Secondary2 ?? string.Empty, 3);
        }

        private void Btn_Text_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (alreadyEditing[4])
            {
                MessageBox.Show("This color is already being edited.", "Edit Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            alreadyEditing[4] = true;
            InitColorPick(Text ?? string.Empty, 4);
        }

        #endregion

        #region PalleteNameControls

        private void Txb_PalleteName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Keyboard.ClearFocus();
            }
        }

        private string GetRandomPalleteName()
        {
            Random random = new Random();

            string[] prefixes = { "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega", "Mega", "Giga", "Tera", "Peta", "Exa", "Zetta", "Yotta", "Eco", "Neo", "Retro", "Urban", "Quantum", "Cosmic", "Solar", "Lunar", "Astro", "Electro", "Hydro", "Geo", "Cyber", "Nano", "Bio", "Techno", "Ultra", "Hyper", "Infra", "Inter", "Intra", "Sub", "Super", "Semi", "Pseudo", "Trans", "Over", "Under", "Iso", "Anti", "Turbo", "Velo", "Rocket", "Sonic", "Cipher", "Vector", "Primal", "Aero", "Mystique", "Crystal", "Stellar", "Astra", "Solaris", "Celestio", "Cyclonic", "Temporal", "Galactic", "Seraphic", "Phantom", "Spectral", "Virtual", "Digital", "Analog", "Magnetic", "Radiant", "Glacial", "Voltaic", "Harmonic", "Dynamic", "Infinite", "Eternal", "Arcane", "Mystery", "Futuristic", "Prismatic", "Optic", "Chromatic", "Vital", "Raw" };
            string[] names = { "Aurora", "Borealis", "Celestial", "Dusk", "Eclipse", "Frost", "Glimmer", "Harmony", "Illusion", "Jubilant", "Kaleidoscope", "Luminous", "Mirage", "Nebula", "Oasis", "Paradigm", "Quartz", "Radiance", "Serenity", "Tranquil", "Utopia", "Vivid", "Whisper", "Xanadu", "Zenith", "Abyss", "Brilliance", "Cascade", "Drift", "Euphoria", "Flare", "Gossamer", "Halo", "Iridescence", "Jade", "Kismet", "Lucid", "Mystic", "Nimbus", "Opal", "Pulse", "Quiver", "Rhapsody", "Saffron", "Tidal", "Unity", "Vortex", "Wonder", "Xenial", "Yonder", "Zephyr", "Amber", "Bliss", "Celeste", "Dawn", "Ember", "Flora", "Gleam", "Harbor", "Infusion", "Jubilee", "Kiara", "Luxe", "Melody", "Nova", "Orchid", "Phoenix", "Quasar", "Reverie", "Solstice", "Tangerine", "Umbra", "Velvet", "Wisp", "Xenon", "Zest", "Aerial", "Brisk", "Chroma", "Dapple", "Elegance", "Fable", "Gusto", "Haven", "Ivory", "Jolt", "Keen", "Lush", "Majestic", "Noir", "Opus", "Prism", "Quintessence", "Ripple", "Sparrow", "Tonic", "Ultraviolet", "Vantage", "Wanderlust", "Ethereal" };
            string[] suffixes = { "Mist", "Shade", "Tint", "Bloom", "Rush", "Wave", "Dream", "Spark", "Drift", "Gleam", "Glint", "Glow", "Pulse", "Surge", "Breeze", "Whisper", "Echo", "Fusion", "Sparkle", "Frost", "Blaze", "Shimmer", "Burst", "Stream", "Aurora", "Cascade", "Dusk", "Dawn", "Haze", "Flux", "Vibe", "Bolt", "Drizzle", "Murmur", "Ripple", "Glaze", "Crush", "Sizzle", "Twist", "Spiral", "Glide", "Sweep", "Slide", "Crackle", "Flicker", "Shine", "Luster", "Glisten", "Beam", "Roar", "Sway", "Swirl", "Flurry", "Quiver", "Lilt", "Zephyr", "Flutter", "Scintilla", "Fleck", "Patter", "Tingle", "Dapple", "Vortex", "Swell", "Curl", "Drape", "Veil", "Hush", "Gush", "Spill", "Drop", "Crest", "Clash", "Slick", "Fume", "Trace", "Twinge", "Quake", "Wisp", "Flick", "Blush", "Smolder", "Hollow", "Drum", "Ember", "Fervor", "Glimmer", "Cadence", "Undertone", "Overtone", "Undercurrent", "Afterglow", "Nightfall", "Daybreak", "Moonrise", "Twilight", "Sunset", "Sunrise", "Nocturne", "Reverie" };

            return $"{prefixes[random.Next(prefixes.Length)]}-{names[random.Next(names.Length)]}-{suffixes[random.Next(suffixes.Length)]}";
        }

        #endregion

        private void StackPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            currentEditingName = Txb_PalleteName.Text;
        }
    }
}