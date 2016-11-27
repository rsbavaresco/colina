using System;

namespace Colina.Language.ViewModels
{
    public class BuilderViewModel
    {
        public byte[] Content { get; set; }
        public Guid PaletteObject { get; set; }

        public BuilderViewModel(byte[] content, Guid paletteObject)
        {
            Content = content;
            PaletteObject = paletteObject;
        }
    }
}
