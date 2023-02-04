using System;
using System.Collections.Generic;
using System.Text;

namespace AdbShell.Models;

public class AdbCommandResult
{
    public bool Success { get; set; }

    public string Message { get; set; } 

    public object result { get;set; }
}
