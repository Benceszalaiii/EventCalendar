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
using EventCalendar.ViewModels;
using EventCalendar.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace EventCalendar.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView : FluentWindow
{
    public MainView(MainViewModel viewModel)
    {

        InitializeComponent();
        DataContext = viewModel;
        Loaded += (_, _) => 
        {
            // Navigate to your landing page
            RootNavigation.Navigate(typeof(DashboardPage));
        };

    }
    
}