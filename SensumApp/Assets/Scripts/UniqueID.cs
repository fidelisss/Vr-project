using System;
using UnityEngine;

public class UniqueID : MonoBehaviour
{
    [SerializeField] private string m_ID = Guid.NewGuid().ToString();
     public string ID => m_ID;
 
     [ContextMenu("Generate new ID")]
     private void RegenerateGUID () => m_ID = Guid.NewGuid().ToString();
}
