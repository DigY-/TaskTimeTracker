using System;
using System.Collections.Generic;
using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client.Navigation
{
  /// <summary>
  /// Holds a Collection of ViewModels.
  /// </summary>
  public class ViewModelNavigationCollection
  {
    private Dictionary<Type, ViewModelBase> _viewModels;

    public ViewModelNavigationCollection()
    {
      this._viewModels = new Dictionary<Type, ViewModelBase>();
    }

    public ViewModelBase this[Type t]
    {
      get
      {
        if (this._viewModels.ContainsKey(t))
        {
          return this._viewModels[t];
        }

        return null;
      }
    }

    /// <summary>
    /// Adds the ViewModel to the ViewModelCollection
    /// </summary>
    /// <param name="type"></param>
    /// <param name="viewModel"></param>
    public void AddViewModel(Type type, ViewModelBase viewModel)
    {
      if (!this._viewModels.ContainsKey(type))
      {
        this._viewModels.Add(type, viewModel);
      }
    }

    public bool ContainsKey(Type key)
    {
      return this._viewModels.ContainsKey(key);
    }
  }
}