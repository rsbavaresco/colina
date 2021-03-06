﻿using Colina.Models.Abstraction.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.Abstraction.Interfaces
{
    public interface ISentenceRecognizer
    {
        UserAction Recognize(string sentence);
    }
}
