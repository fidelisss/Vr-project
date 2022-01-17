using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringPersistProperty : PrefsPersistentProperty<string>
{
    public StringPersistProperty(string defaultValue, string key) : base(defaultValue, key)
    {
        Init();
    }
    protected override void Write(string value)
    {
        PlayerPrefs.SetString(Key, value);
    }
    protected override string Read(string defaultValue)
    {
        return PlayerPrefs.GetString(Key, defaultValue);
    }
}
