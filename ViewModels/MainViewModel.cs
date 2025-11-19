using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventCalendar.Models;
using EventCalendar.Views.Pages;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace EventCalendar.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainModel model = new MainModel();
    

    [ObservableProperty]
    private ICollection<object> _menuItems = new ObservableCollection<object>();
    

    
    public MainViewModel()
    {
        
        
        
        // WPF-UI navigation
        MenuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("Home", SymbolRegular.Home24, typeof(DashboardPage)),
            new NavigationViewItem("Settings", SymbolRegular.Settings24, typeof(SettingsPage))
        };
    }
    

}