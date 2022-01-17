using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Photon.Pun;
using UnityEngine.UI;

public class CalculatorModern : MonoBehaviour
{
    public string screen = "";
    public Text screenText;
    public Text ansText;
    public Transform buttons;

    public float ans = 0;

    float timeout = 0;

    bool fract = false;
    
    void Update()
    {
        if (timeout > 0)
            timeout -= Time.deltaTime;
    }

    float Calculate(string exp)
    {
        DataTable dt = new DataTable();
        return float.Parse(dt.Compute(exp, "").ToString());
    }

    public void SyncInput(string someInput)
    {
        Input(someInput);
    }
    
    [PunRPC]
    public void Input(string input)
    {
        if (timeout > 0)
            return;
        
        timeout = 0.1f;

        switch (input)
        {
            case "c":
                screen = "0";
                fract = false;
                break;

            case "back":
                if (screen[screen.Length - 1] == '.')
                    fract = false;
                screen = screen.Remove(screen.Length - 1);
                if (screen == "")
                    screen = "0";
                break;

            case "=":
                screen = Calculate(screen).ToString();
                break;

            case ".":
                if ("+-*/".Contains(screen[screen.Length - 1].ToString()))
                    screen += "0";
                if (!fract)
                {
                    screen += input;
                    fract = true;
                }
                break;

            case "+":
            case "-":
            case "*":
            case "/":
                if ("+-*/".Contains(screen[screen.Length - 1].ToString()))
                {
                    screen = screen.Remove(screen.Length - 1);
                }
                screen += input;
                fract = false;
                break;

            default:
                if (screen == "0")
                    screen = "";
                screen += input;
                break;
        }
        
        screenText.text = screen;

        ans = Calculate(screen);
        ansText.text = "=" + ans.ToString();
    }
}
