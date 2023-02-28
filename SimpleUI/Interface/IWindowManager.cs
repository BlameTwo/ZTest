using System;

namespace SimpleUI.Interface;

public interface IWindowManager
{
    public void Register<T>(string Key);

    public void Register(string Key, Type type);

    public void Remove(string Key);

    public void Show(string key);

}
