using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TaskTimeTracker.Client.Configuration;
using TaskTimeTracker.Client.Contract;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Navigation;
using TaskTimeTracker.Client.Ui.Commands;
using TaskTimeTracker.Client.Ui.ConfigurationWindow;
using TaskTimeTracker.Client.Ui.Inbox;

namespace TaskTimeTracker.Client.Ui.MainWindow {
  internal class MainWindowViewModel : ViewModelBase {
    private ObservableCollection<Task> _tasks;
    private Visibility _mainWindowVisibility;
    public Task SelectedTask { get; set; }

    public ObservableCollection<Task> Tasks {
      get { return this._tasks; }
      set {
        this._tasks = value;
        OnPropertyChanged();
      }
    }

    public Visibility MainWindowVisibility {
      get { return this._mainWindowVisibility; }
      set {
        this._mainWindowVisibility = value;
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Adds a TaskStamp
    /// </summary>
    public ICommand AddCommand { get; set; }

    /// <summary>
    /// Removes a TaskStamp
    /// </summary>
    public ICommand RemoveCommand { get; set; }

    /// <summary>
    /// Command to open the ConfigurationWindow
    /// </summary>
    public ICommand ConfigCommand { get; set; }

    public ICommand MouseDoubleClick { get; set; }

    public MainWindowViewModel() {
      this.Tasks = new ObservableCollection<Task>();
      this.AddCommand = new RelayCommand(AddExecute);
      this.RemoveCommand = new RelayCommand(RemoveExecute);
      this.ConfigCommand = new RelayCommand(ConfigExecute);
      this.MouseDoubleClick = new RelayCommand(this.MouseDoubleClickExecute);
      this.MainWindowVisibility = Visibility.Visible;
    }

    private void MouseDoubleClickExecute(object o) {
      if (this.SelectedTask == null || this.SelectedTask.EditMode) {
        return;
      }

      this.SelectedTask.EnterEditMode();
    }

    private void ConfigExecute(object obj) {
      Navigator.Instance.NavigateTo(typeof(ConfigurationWindowViewModel));
    }

    private void RemoveExecute(object obj) {
      if (MessageBox.Show("Sure wanna delete?", "check", MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No) != MessageBoxResult.Yes) {
        return;
      }

      this.Tasks.Remove(obj as Task);
    }

    private void AddExecute(object o) {
      Inbox.Inbox inbox = new Inbox.Inbox(Application.Current.MainWindow);
      InboxViewModel inboxViewModel = new InboxViewModel(inbox);
      inbox.DataContext = inboxViewModel;
      bool? dialogResult = inbox.ShowDialog();

      if (!dialogResult.HasValue || !dialogResult.Value) { return; }

      string text = inboxViewModel.Text;

      DateTime dateTime = DateTime.Now;
      Task newTask = new Task(dateTime, text);
      this.Tasks.Add(newTask);
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}