using System.Collections;
using System.Collections.Generic;
using Photon.Pun;   
using UnityEngine;

public class CalculatorButton : MonoBehaviour
{
    public CalculatorModern calculator;

    public void SyncInputButton(string sometext)
    {
        calculator.Input(sometext);
    }

}
