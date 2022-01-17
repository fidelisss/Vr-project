using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StapleContainer : MonoBehaviour
{
    public static StapleContainer Instance { get; private set; }

    public List<StaplePoint> StaplePoints = new List<StaplePoint>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log(StapleContainer.Instance);
    }
}
