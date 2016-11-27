using Colina.Design.Abstraction.Interfaces;
using Colina.Design.Settings;
using Colina.Models.Abstraction.Designs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Colina.Design.Drawings
{
    public class ImageDrawer : IImageDrawer
    {
        private readonly string _runnable =
            "/c bash -c 'cd /mnt/f/tools/torch/distro; " +
            "./run.sh -pathToImages {0} -pathToVector {1} -pathToObjects {2};'";

        private readonly DesignSettings _settings;
        private Environment _environment;        

        public ImageDrawer(DesignSettings settings)
        {
            _settings = settings;
        }

        public void Draw(Environment environment)
        {
            _environment = environment;

            CreateDrawMetadata();
            RunDrawer();
        }

        private void CreateDrawMetadata()
        {
            var serializable = new Dictionary<System.Guid, object>();

            foreach (var obj in _environment.Drawings)
                serializable.Add(obj.Object.Identifier, obj.Position);

            var content = JsonConvert.SerializeObject(serializable, Formatting.Indented);

            var colFilePath = $"{_settings.Windows.PathToData}\\{_environment.SessionId}.col";
            
            File.WriteAllText(colFilePath, content);
        }

        private void RunDrawer()
        {
            var colFilePath = $"{_settings.Unix.PathToData}/{_environment.SessionId}.col";

            var command = string.Format(_runnable, 
                _settings.Unix.PathToImages, 
                colFilePath, 
                _settings.Unix.PathToObjects);

            var processInfo = new ProcessStartInfo();
            processInfo.FileName = @"C:\Windows\System32\cmd.exe";            
            processInfo.Arguments = command;              
            processInfo.UseShellExecute = false;

            var process = Process.Start(processInfo);  
            process.WaitForExit();
        }
    }
}
