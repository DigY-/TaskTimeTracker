﻿using System.IO;
using System.Text;
using System.Windows.Input;
using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Configuration {
  public class TaskTimeTrackerConfigurationController : XmlConfigurationController<XmlConfigurationSerializer<IConfiguration>, IConfigurationWindowViewModel>{
    public TaskTimeTrackerConfigurationController(XmlConfigurationSerializer<IConfiguration> serializer)
      : base(serializer) {
    }

    public override void Save() {
      using (FileStream fileStream = new FileStream(this.ConfigPath, FileMode.OpenOrCreate)) {
        using (XmlTextWriter writer = new XmlTextWriter(fileStream, Encoding.UTF8)) {
          writer.Formatting = Formatting.Indented;
          this.Serializer.Serialize(writer, this.Configuration);
        }
      }
    }

    public override IConfiguration Load() {
      ITaskTimeTrackerConfiguration result = null;

      if (File.Exists(this.ConfigPath)) {
        using (FileStream fileStream = new FileStream(this.ConfigPath, FileMode.Open)) {
          XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
          xmlReaderSettings.IgnoreWhitespace = true;
          using (XmlReader reader = XmlReader.Create(new XmlTextReader(fileStream), xmlReaderSettings)) {
            result = (ITaskTimeTrackerConfiguration)this.Serializer.Deserialize(reader);
          }
        }
      } else {
        result = new TaskTimeTrackerConfiguration();
        result.ControlIsChecked = true;
        result.WindowsIsChecked = true;
        result.AltIsChecked = false;
        result.KeyOne = Key.N;
      }

      this.Configuration = result;
      return result;
    }


    public override void CreateFromViewModel(IConfigurationWindowViewModel viewModel) {
      this.Configuration = new TaskTimeTrackerConfiguration();
      this.Configuration.KeyOne = viewModel.KeyOne;
      this.Configuration.AltIsChecked = viewModel.AltIsChecked;
      this.Configuration.ControlIsChecked = viewModel.ControlIsChecked;
      this.Configuration.WindowsIsChecked = viewModel.WindowsIsChecked;
      this.Configuration.StartupStampText = viewModel.StartupStampText;
      this.Configuration.SetStampOnStartupIsChecked = viewModel.SetStampOnStartupIsChecked;
    }
  }
}