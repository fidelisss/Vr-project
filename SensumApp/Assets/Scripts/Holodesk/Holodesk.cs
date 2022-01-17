using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holodesk : MonoBehaviour
{
    public float scaleMultiplier = 5f;
    public Transform projector;
    public Transform referencePoint;

    public static Holodesk instance { get; private set; }

    void Awake()
    {
        instance = this;
    }
}
