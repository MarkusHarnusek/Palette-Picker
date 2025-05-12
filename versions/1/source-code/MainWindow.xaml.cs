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

        public MainWindow()
        {
            InitializeComponent();
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