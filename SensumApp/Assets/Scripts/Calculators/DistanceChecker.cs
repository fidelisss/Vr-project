using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DistanceChecker : AbstractChecker
{
    private void Update()
    {
        string answer = ((int)GetDistance()).ToString();
        answer = Convertor.Convert(answer);
        lineInfo.text.text = $"{answer} {_notation}";
    }

    private float GetDistance()
    {
        return (lineInfo.endPoint.position - lineInfo.startPoint.position).magnitude * scaleFactor;
    }
}
