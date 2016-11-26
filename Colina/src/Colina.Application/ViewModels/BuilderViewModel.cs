using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.ViewModels
{
    public class BuilderViewModel
    {
        public byte[] Content { get; set; }

        public BuilderViewModel(byte[] content)
        {
            Content = content;
        }
    }
}
