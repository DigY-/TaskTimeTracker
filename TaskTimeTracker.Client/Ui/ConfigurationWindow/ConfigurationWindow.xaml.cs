using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using TaskTimeTracker.Client.Configuration;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Navigation;
using TaskTimeTracker.Client.Ui.MainNavigation;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  /// <summary>
  /// Interaction logic for ConfigurationWindow.xaml
  /// </summary>
  public partial class ConfigurationWindow : UserControl {

    public IConfigurationWindowViewModel ViewModel { get; set; }
    public TaskTimeTrackerConfigurationController ConfigurationController { get; set; }

    public ConfigurationWindow() {
      InitializeComponent();

      var vm = Navigator.Instance.CurrentViewModel;
      this.ViewModel = (IConfigurationWindowViewModel)vm;
      this.DataContext = vm;

      this.Loaded += delegate { ViewModel.OnLoaded(); };
      this.Unloaded += delegate { ViewModel.OnUnLoaded(); };
    }

    private void OnShortCutInput(object sender, KeyEventArgs e) {
      this.ViewModel.Controller.SetKey(e.Key, this.ViewModel);
      e.Handled = true;
    }
  }
}
