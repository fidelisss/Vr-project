using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
//en https://docs.google.com/spreadsheets/d/e/2PACX-1vRlZuvgSIJamguRTQiGm-7iNnik9P884j_Er344CzpQa0QhvuDRdfJIptMfqyit5OFXul5Khw9bq_R2/pub?gid=0&single=true&output=tsv
//ru https://docs.google.com/spreadsheets/d/e/2PACX-1vRlZuvgSIJamguRTQiGm-7iNnik9P884j_Er344CzpQa0QhvuDRdfJIptMfqyit5OFXul5Khw9bq_R2/pub?gid=1587098247&single=true&output=tsv
[CreateAssetMenu (menuName = "Localization", fileName = "Localization")]
    public class LocalDef : ScriptableObject
    {
    [SerializeField] private string url;
    [SerializeField] private List<LocalItem> localItems;
    private UnityWebRequest request;
    public Dictionary<string, string> GetData()
    {
        var dictionary = new Dictionary<string, string>();
        foreach (LocalItem localItem in localItems)
        {
            dictionary.Add(localItem.key, localItem.value);
        }
        return dictionary;
    }
    [ContextMenu("Update local")]
    public void UpdateLocals()
    {
        localItems.Clear();
        // if (request != null) return;
        request = UnityWebRequest.Get(url);
        request.SendWebRequest().completed += OnDataLoaded;
    }
    private void OnDataLoaded(AsyncOperation operation)
    {
        if (operation.isDone)
        {
            var rows = request.downloadHandler.text.Split('\n');
            foreach (var row in rows)
            {
                AddLocalItem(row);
            }
        }
    }
    private void AddLocalItem(string row_)
    {
        try
        {
            var parts = row_.Split('\t');
            localItems.Add(new LocalItem { key = parts[0], value = parts[1]});
        } catch (Exception e)
        {
            Debug.LogError($"row: {row_}\n {e}");
        }
    }
}
[Serializable]
public class LocalItem
{
    public string key;
    public string value;
}
