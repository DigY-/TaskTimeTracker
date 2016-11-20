using System;
using System.Threading;
using System.Windows;
using TaskTimeTracker.Client.Navigation;
using TaskTimeTracker.Client.Ui.MainWindow;

namespace TaskTimeTracker.Client {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    private readonly System.Windows.Forms.NotifyIcon _notifyIcon;

    public App() {
      System.Windows.Forms.MenuItem menuItem = new System.Windows.Forms.MenuItem();
      var iconStream = GetType().Assembly.GetManifestResourceStream("TaskTimeTracker.Client.Clock.ico");

      if (iconStream == null) { throw new InvalidOperationException(); }

      menuItem.Click += Close;
      menuItem.Text = "Close";

      System.Windows.Forms.MenuItem[] menuItems = { menuItem };

      this._notifyIcon = new System.Windows.Forms.NotifyIcon();
      this._notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(menuItems);
      this._notifyIcon.Icon = new System.Drawing.Icon(iconStream);
      this._notifyIcon.DoubleClick += NotifyIconOnDoubleClick;
      this._notifyIcon.Visible = true;

      var navigator = Navigator.Instance;
    }

    private void NotifyIconOnDoubleClick(object sender, EventArgs e) {
      this.MainWindow.Visibility = Visibility.Visible;
    }

    private void Close(object sender, EventArgs e) {
      this._notifyIcon.Visible = false;
      Environment.Exit(0);
    }

    private void ApplicationStartup(object sender, StartupEventArgs e) {
      ResourceDictionary dict = new ResourceDictionary();
      switch (Thread.CurrentThread.CurrentCulture.ToString()) {
        case "de-DE":
          dict.Source = new Uri("../Multilingual/German.xaml", UriKind.Relative);
          break;
        default:
          dict.Source = new Uri("../Multilingual/English.xaml", UriKind.Relative);
          break;
      }

      Application.Current.Resources.MergedDictionaries.Add(dict);
    }
  }
}
