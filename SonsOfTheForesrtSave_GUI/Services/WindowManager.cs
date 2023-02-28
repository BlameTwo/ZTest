using Accessibility;
using SimpleUI.Controls;
using SimpleUI.Helper;
using SimpleUI.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace SonsOfTheForesrtSave_GUI.Services;

public class WindowManager : IWindowManager
{
    private Dictionary<string, Type> _hashtable = new();

    public void Show(string key)
    {
        if (_hashtable.ContainsKey(key))
        {
            var win = App.GetService(_hashtable[key]);
        }
    }

    public void Register<T>(string Key)
    {
        if(!_hashtable.ContainsKey(Key))
        {
            _hashtable.Add(Key,typeof(T));
        }
    }

    public void Register(string Key, Type type)
    {
        if (!_hashtable.ContainsKey(Key))
        {
            _hashtable.Add(Key, type);
        }
    }

    public void Remove(string Key)
    {
        _hashtable.Remove(Key);
    }

    
}
