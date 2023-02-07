using WSA_ADBShell.Services.Interfaces;

namespace WSA_ADBShell.Events.ViewModelsEvent;

public class TokenEvent
{
    public ITaskToken RemoveTask { get; set; }
}
