using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventCalendar.Interfaces;
using EventCalendar.Models;
using EventCalendar.Models.Main;
using EventCalendar.Views.Pages;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace EventCalendar.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainModel model;
    
    
    [ObservableProperty]
    private ICollection<object> _menuItems = new ObservableCollection<object>();
    

    public MainViewModel()
    {
        model = new MainModel();
        
        // WPF-UI navigation
        MenuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("Dashboard", SymbolRegular.Home24, typeof(DashboardPage)),
            new NavigationViewItem("Settings", SymbolRegular.Settings24, typeof(SettingsPage))
        };
        
    }

    

}