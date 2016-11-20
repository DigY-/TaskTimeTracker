using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskTimeTracker.Client.Configuration;
using TaskTimeTracker.Client.Contract;
using TaskTimeTracker.Client.Navigation;
using TaskTimeTracker.Client.Ui.MainWindow;

namespace TaskTimeTracker.Client.Ui.MainNavigation
{
  public class MainNavigationViewModel : INotifyPropertyChanged
  {
    private ViewModelBase _selectedViewModel;

    public ViewModelBase SelectedViewModel {
      get { return _selectedViewModel; }
      set
      {
        _selectedViewModel = value;
        OnPropertyChanged(nameof(SelectedViewModel));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}