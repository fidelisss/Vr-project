using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public void SetLanguage(string key)
    {
        LocalizationManager.I.SetLocal(key);
    }
}
