using System;
using System.Collections.Generic;
using System.Text;

namespace AdbShell.Models;

public class AdbResultBase
{
    public bool Success { get; set; }

    public string Message { get; set; }
}

public class AdbCommandResult: AdbResultBase
{

    public object result { get;set; }
}

public class AdbDataResult<T>: AdbResultBase
    where T : class
{
    public T Data { get; set; }
}
