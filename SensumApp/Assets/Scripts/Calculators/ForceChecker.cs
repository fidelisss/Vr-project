using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ForceChecker : AbstractChecker
{
    const double G = 6.6743E-11;

    private void Update()
    {
        string answer = (GetForce()).ToString("G");
        answer = Convertor.Convert(answer);
        lineInfo.text.text = $"{answer} {_notation}";
    }

    public string GetAnswerForce()
    {
        string answer = (GetForce()).ToString("G");
        answer = Convertor.Convert(answer);
        return answer;
    }
    
    private double GetForce()
    {
        Item i1 = lineInfo.startPoint.GetComponent<Item>();
        Item i2 = lineInfo.endPoint.GetComponent<Item>();
        double distance = (lineInfo.endPoint.position - lineInfo.startPoint.position).magnitude * scaleFactor;
        double force = G * i1.mass * i2.mass / Mathf.Pow((float)distance, 2);
        return force;
    }
}
