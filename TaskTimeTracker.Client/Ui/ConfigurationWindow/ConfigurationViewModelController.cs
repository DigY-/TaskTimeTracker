using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TaskTimeTracker.Client.Configuration;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Navigation;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationViewModelController : IConfigurationViewModelController
  {
    private readonly TaskTimeTrackerConfigurationController _configurationController;
    public bool IsAborted;
    private IConfigurationWindowViewModel _onLoadedState;

    public ConfigurationViewModelController(TaskTimeTrackerConfigurationController controller) {
      this._configurationController = controller;
    }

    /// <summary>
    /// Sets the given key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="viewModel"></param>
    public void SetKey(Key key, IConfigurationWindowViewModel viewModel) {
      viewModel.KeyOne = key;
      viewModel.KeyOneString = key.ToString();
    }

    public IConfigurationWindowViewModel FromConfiguration(ITaskTimeTrackerConfiguration configuration) {
      IConfigurationWindowViewModel result = new ConfigurationWindowViewModel(this);
      result.AltIsChecked = configuration.AltIsChecked;
      result.ControlIsChecked = configuration.ControlIsChecked;
      result.WindowsIsChecked = configuration.WindowsIsChecked;
      result.StartupStampText = configuration.StartupStampText;
      result.SetStampOnStartupIsChecked = configuration.SetStampOnStartupIsChecked;
      SetKey(configuration.KeyOne, result);
      return result;
    }

    public void ExecuteCancel(object obj)
    {
      this.IsAborted = true;
      Navigator.Instance.Back();
    }

    public void ExecuteOk(object obj)
    {
      this.IsAborted = false;
      Navigator.Instance.Back();
    }

    public void OnLoaded()
    {
      // Save a copy of the current ViewModel.
      this._onLoadedState = this.ViewModel.Clone();
    }

    public void OnUnLoaded()
    {
      // If the Abort flag is set -> Reset fields to the copyed values from OnLoaded()
      if (IsAborted)
      {
        this.ViewModel.AltIsChecked = this._onLoadedState.AltIsChecked;
        this.ViewModel.KeyOne = this._onLoadedState.KeyOne;
        this.ViewModel.ControlIsChecked = this._onLoadedState.ControlIsChecked;
        this.ViewModel.KeyOneString = this._onLoadedState.KeyOneString;
        this.ViewModel.SetStampOnStartupIsChecked = this._onLoadedState.SetStampOnStartupIsChecked;
        this.ViewModel.WindowsIsChecked = this._onLoadedState.WindowsIsChecked;
        this.ViewModel.StartupStampText = this._onLoadedState.StartupStampText;
      }
      // else save the changes to the Configuration
      else
      {
        this._configurationController.CreateFromViewModel(this.ViewModel);
        this._configurationController.Configuration.Version = new Version(1,0,0,0); // Is this needed ?
        this._configurationController.Save();
      }

      this._onLoadedState = null;
    }

    public IConfigurationWindowViewModel ViewModel { get; set; }
  }
}