using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskTimeTracker.Client.Configuration;
using TaskTimeTracker.Client.Contract;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Ui.ConfigurationWindow;
using TaskTimeTracker.Client.Ui.MainNavigation;
using TaskTimeTracker.Client.Ui.MainWindow;

namespace TaskTimeTracker.Client.Navigation {

  public class Navigator {
    private static Navigator _instance;

    public static Navigator Instance {
      get {
        if (_instance == null) {
          _instance = new Navigator();
          AppInitializer initializer = new AppInitializer();
          var viewModels = initializer.InitializeViewModels();
          _instance._viewModelCollection = viewModels;
          _instance._navigationStack = new Stack<Type>();
        }

        return _instance;
      }
    }

    private ViewModelNavigationCollection _viewModelCollection;
    private Stack<Type> _navigationStack;
    
    /// <summary>
    /// Given the type of the ViewModel, navigates to the regarding View.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ViewModelBase NavigateTo(Type type) {
      if (!this._viewModelCollection.ContainsKey(type)) {
        return null;
      }

      if (this.CurrentViewModel != null) {
        this._navigationStack.Push(this.CurrentViewModel.GetType());
      }
      this.CurrentViewModel = this._viewModelCollection[type];
      return this._viewModelCollection[type];
    }

    /// <summary>
    /// Gets the current SelectedViewModel from MainNavigationViewModel
    /// </summary>
    public ViewModelBase CurrentViewModel {
      get {
        return _mainNavigationViewModel.SelectedViewModel;
      }
      private set {
        _mainNavigationViewModel.SelectedViewModel = value;
      }
    }

    private MainNavigationViewModel _mainNavigationViewModel {
      get {
        Window window = Application.Current.MainWindow;
        MainNavigationWindow navigationWindow = window as MainNavigationWindow;
        return navigationWindow.DataContext as MainNavigationViewModel;
      }
    }

    /// <summary>
    /// Navigates one ViewModel back in the Stack
    /// </summary>
    /// <returns></returns>
    public ViewModelBase Back() {
      if (this._navigationStack.Any())
      {
        var vm = this._viewModelCollection[this._navigationStack.Pop()];
        this.CurrentViewModel = vm;
        return vm;
      }

      return null;
    }
  }
}