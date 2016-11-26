using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Design.Settings
{
    public class DesignSettings
    {
        public UnixSettings Unix  { get; set; }
        public WindowsSettings Windows { get; set; }
    }
}
