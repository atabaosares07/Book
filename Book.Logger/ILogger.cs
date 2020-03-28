using System;
using System.Collections.Generic;
using System.Text;

namespace Book.LoggerProvider
{
    public interface ILogger
    {
        string Log(string message);
    }
}
