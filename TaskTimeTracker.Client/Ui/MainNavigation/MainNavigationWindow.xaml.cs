using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using TaskTimeTracker.Client.Navigation;
using TaskTimeTracker.Client.Ui.MainWindow;

namespace TaskTimeTracker.Client.Ui.MainNavigation {
  /// <summary>
  /// Interaction logic for MainNavigationWindow.xaml
  /// </summary>
  public partial class MainNavigationWindow : MetroWindow {
    public MainNavigationWindow() {
      InitializeComponent();

      this.DataContext = new MainNavigationViewModel();
    }

    private void MainNavigationWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      Navigator.Instance.NavigateTo(typeof(MainWindowViewModel));
    }

    private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
      e.Cancel = true;
      this.Visibility = Visibility.Hidden;
    }
  }
}
