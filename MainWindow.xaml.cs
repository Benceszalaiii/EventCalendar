using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace EventCalendar;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow()
    {
        SystemThemeWatcher.Watch(this, WindowBackdropType.Auto);
        InitializeComponent();
        ThemeButton.Content = ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark ? "Dark" : "Light";
        ApplicationThemeManager.Changed += OnThemeChanged;
    }

    public void OnThemeChanged(ApplicationTheme currentTheme, Color systemAccent)
    {
        ThemeButton.Content = currentTheme == ApplicationTheme.Dark ? "Dark" : "Light";
    }


    private void ChangeThemeEventHandler(object sender, RoutedEventArgs e)
    {
        ApplicationThemeManager.Apply(ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark ? ApplicationTheme.Light : ApplicationTheme.Dark);
    }
}