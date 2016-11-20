using System;
using TaskTimeTracker.Client.Configuration;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Navigation;
using TaskTimeTracker.Client.Ui.ConfigurationWindow;
using TaskTimeTracker.Client.Ui.MainWindow;

namespace TaskTimeTracker.Client
{
  public class AppInitializer
  {
    private ViewModelNavigationCollection _viewModelCollection;

    public ViewModelNavigationCollection InitializeViewModels() {
      this._viewModelCollection = new ViewModelNavigationCollection();

      var mvm = AddMainViewModel();
      var cvm = AddConfigurationViewModel();

      AppInitilisation(mvm, cvm);

      return this._viewModelCollection;
    }

    private void AppInitilisation(MainWindowViewModel mvm, IConfigurationWindowViewModel cvm) {
      if (!cvm.SetStampOnStartupIsChecked) {
        return;
      }

      mvm.Tasks.Add(new Task(DateTime.Now, cvm.StartupStampText));
    }

    private MainWindowViewModel AddMainViewModel() {
      MainWindowViewModel mainViewModel = new MainWindowViewModel();
      this._viewModelCollection.AddViewModel(typeof(MainWindowViewModel), mainViewModel);
      return mainViewModel;
    }

    private IConfigurationWindowViewModel AddConfigurationViewModel() {
      TaskTimeTrackerConfigurationSerializer serializer = new TaskTimeTrackerConfigurationSerializer();
      TaskTimeTrackerConfigurationController controller = new TaskTimeTrackerConfigurationController(serializer);
      controller.Load();

      ConfigurationViewModelController configurationViewModelController = new ConfigurationViewModelController(controller);
      IConfigurationWindowViewModel configViewModel = configurationViewModelController.FromConfiguration(controller.Configuration);
      this._viewModelCollection.AddViewModel(typeof(ConfigurationWindowViewModel), configViewModel);
      return configViewModel;
    }
  }
}