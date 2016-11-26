using Colina.Design.Abstraction.Interfaces;
using Colina.Design.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Design.Drawings
{
    public class ImageReader : IImageReader
    {
        private readonly DesignSettings _settings;
        public ImageReader(DesignSettings settings)
        {
            _settings = settings;
        }
        public byte[] Read(string path)
        {
            var imagePath = $"{_settings.Windows.PathToImages}/{path}";
            return File.ReadAllBytes(imagePath);
        }
    }
}
