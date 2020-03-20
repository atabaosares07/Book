using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Logger
{
    public interface ILogger
    {
        string Log(string message);
    }
}
