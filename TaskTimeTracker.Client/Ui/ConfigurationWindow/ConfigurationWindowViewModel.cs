using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Navigation;
using TaskTimeTracker.Client.Ui.Commands;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationWindowViewModel : IConfigurationWindowViewModel {
    private string _keyOneString;
    private Key _keyOne;
    private bool _controlIsChecked;
    private bool _altIsChecked;
    private bool _windowsIsChecked;
    private string _startupStampText;
    private bool _setStampOnStartupIsChecked;

    public event PropertyChangedEventHandler PropertyChanged;

    public IConfigurationViewModelController Controller { get; set; }

    public bool SetStampOnStartupIsChecked {
      get { return this._setStampOnStartupIsChecked; }
      set {
        this._setStampOnStartupIsChecked = value;
        OnPropertyChanged(nameof(this.SetStampOnStartupIsChecked));
      }
    }

    public string StartupStampText {
      get { return this._startupStampText; }
      set {
        this._startupStampText = value;
        OnPropertyChanged(nameof(this.StartupStampText));
      }
    }

    public string KeyOneString {
      get { return this._keyOneString; }
      set {
        this._keyOneString = value;
        OnPropertyChanged(nameof(this.KeyOneString));
      }
    }

    public Key KeyOne {
      get { return this._keyOne; }
      set {
        this._keyOne = value;
        OnPropertyChanged(nameof(this.KeyOne));
      }
    }

    public bool ControlIsChecked {
      get { return this._controlIsChecked; }
      set {
        this._controlIsChecked = value;
        OnPropertyChanged(nameof(this.ControlIsChecked));
      }
    }

    public bool AltIsChecked {
      get { return this._altIsChecked; }
      set {
        this._altIsChecked = value;
        OnPropertyChanged(nameof(this.AltIsChecked));
      }
    }

    public bool WindowsIsChecked {
      get { return this._windowsIsChecked; }
      set {
        this._windowsIsChecked = value;
        OnPropertyChanged(nameof(this.WindowsIsChecked));
      }
    }

    public ICommand CancelCommand { get; }

    public ICommand OkCommand { get; }

    /// <summary>
    /// Method called when the view is loaded.
    /// </summary>
    public void OnLoaded()
    {
      this.Controller.OnLoaded();
    }

    /// <summary>
    /// Method called when the view is Unloaded
    /// </summary>
    public void OnUnLoaded() {
      this.Controller.OnUnLoaded();
    }

    public IConfigurationWindowViewModel Clone()
    {
      return (IConfigurationWindowViewModel)this.MemberwiseClone();
    }

    public ConfigurationWindowViewModel(IConfigurationViewModelController controller) {
      this.Controller = controller;
      this.Controller.ViewModel = this;
      this.CancelCommand = new RelayCommand(controller.ExecuteCancel);
      this.OkCommand = new RelayCommand(controller.ExecuteOk);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
