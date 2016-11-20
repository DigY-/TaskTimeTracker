using System.Windows;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfigurationViewModelController {

    void SetKey(Key key, IConfigurationWindowViewModel viewModel);

    /// <summary>
    /// Creates a ViewModel from a Configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    IConfigurationWindowViewModel FromConfiguration(ITaskTimeTrackerConfiguration configuration);

    void ExecuteCancel(object obj);

    void ExecuteOk(object obj);
    void OnLoaded();
    void OnUnLoaded();
    IConfigurationWindowViewModel ViewModel { get; set; }
  }
}