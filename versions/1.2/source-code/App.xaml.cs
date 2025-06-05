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
        public void SetDarkTheme()
        {
            Application.Current.Resources["P1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#070d0d"));
            Application.Current.Resources["P2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#002147"));
            Application.Current.Resources["S1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0055bf"));
            Application.Current.Resources["S2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#87cefa"));
            Application.Current.Resources["T"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#eefafb"));
        }

        public void SetLightTheme()
        {
            Application.Current.Resources["P1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#eefafb"));
            Application.Current.Resources["P2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#87cefa"));
            Application.Current.Resources["S1"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0055bf"));
            Application.Current.Resources["S2"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#002147"));
            Application.Current.Resources["T"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#070d0d"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
