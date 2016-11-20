using System.ComponentModel;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfigurationWindowViewModel : ViewModelBase {
    string KeyOneString { get; set; }
    Key KeyOne { get; set; }
    bool ControlIsChecked { get; set; }
    bool AltIsChecked { get; set; }
    bool WindowsIsChecked { get; set; }

    bool SetStampOnStartupIsChecked { get; set; }
    string StartupStampText { get; set; }

    ICommand CancelCommand { get; }
    ICommand OkCommand { get; }
    IConfigurationViewModelController Controller { get; set; }

    void OnLoaded();
    void OnUnLoaded();

    IConfigurationWindowViewModel Clone();
  }
}