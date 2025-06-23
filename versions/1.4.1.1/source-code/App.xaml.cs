using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace PalettePicker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region ThemeSetMethods

        /// <summary>
        /// Applies the dark theme to the application.
        /// </summary>
        public void SetDarkTheme()
        {
            Current.Resources["P1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#373739"));
            Current.Resources["P2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#888888"));
            Current.Resources["S1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2157E5"));
            Current.Resources["S2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#90D5FF"));
            Current.Resources["T"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F5"));
        }

        /// <summary>
        /// Applies the ligth theme to the application.
        /// </summary>
        public void SetLightTheme()
        {
            Current.Resources["P1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F5"));
            Current.Resources["P2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#90D5FF"));
            Current.Resources["S1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2157E5"));
            Current.Resources["S2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#888888"));
            Current.Resources["T"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#373739"));
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
