using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Instance { get; private set; }

    public List<SnapPoint> SnapPoints;

    private void Awake()
    {
        SnapPoints = new List<SnapPoint>();

        if (Instance != null) 
            Instance = this;
    }
}
