using Colina.Design.Abstraction.Interfaces;
using Colina.Design.Settings;
using Colina.Models.Abstraction.Designs;
using System.Diagnostics;

namespace Colina.Design.Drawings
{
    public class ImageDrawer : IImageDrawer
    {
        private readonly string _runnable =
            "/c bash -c 'cd /mnt/f/tools/torch/distro; " +
            "./run.sh -pathToImages {0} -pathToVector {1} -pathToObjects {2};'";

        private readonly DesignSettings _settings;
        public ImageDrawer(DesignSettings settings)
        {
            _settings = settings;
        }

        public void Draw(Environment environment)
        {
            RunDrawer();
        }

        private void RunDrawer()
        {
            var command = string.Format(_runnable, _settings.Unix.PathToImages,
                $"{_settings.Unix.PathToData}/objVector.col", _settings.Unix.PathToObjects);

            var processInfo = new ProcessStartInfo();
            processInfo.FileName = @"C:\Windows\System32\cmd.exe";            
            processInfo.Arguments = command;              
            processInfo.UseShellExecute = false;

            var process = Process.Start(processInfo);  
            process.WaitForExit();
        }
    }
}
