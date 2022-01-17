using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager
{
    public readonly static LocalizationManager I;
    private StringPersistProperty localKey = new StringPersistProperty("ru", "Localization");
    private StringPersistProperty localKeyEn = new StringPersistProperty("en", "Localization");
    public string LocaleKey => localKey.Value;

    private Dictionary<string, string> localization;
    public event Action onLocalChange;
    static LocalizationManager()
    {
        I = new LocalizationManager();
    }
    public LocalizationManager()
    {
        LoadLocal(localKey.Value);
    }
    private void LoadLocal(string localToLoad)
    {
        var def = Resources.Load<LocalDef>($"Localization/{localToLoad}");
        localization = def.GetData();
        localKey.Value = localToLoad;
        onLocalChange?.Invoke();
    }
    public string Localize(string key)
    {
        return localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
    }
    public void SetLocal(string localeKey)
    {
        LoadLocal(localeKey);
    }
}
