using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentManager : MonoBehaviour
{
    public static ParentManager instance { get; private set; }

    void Awake()
    {
        instance = this;
    }
    
    public Transform GetParent(string ID)
    {
        if (ID != "")
        {
            UniqueID[] ids = FindObjectsOfType<UniqueID>();
            foreach (UniqueID id in ids)
            {
                if (id.ID == ID) return id.transform;
            }
            print("No objects with such ID");
            return null;
        }
        else
        {
            print("Parent not set");
            return null;
        }
    }
}
