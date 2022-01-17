using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convertor
{
    public static string Convert(string s)
    {
        if (s.Contains("e") || s.Contains("E"))
            return Convertor.ConvertExp(s);
        else if (s.Length >= 4)
            return Convertor.ConvertBig(s);
        else return s;
    }

    private static string ConvertExp(string s)
    {
        string[] strings = s.Split('e', 'E');
        float number = float.Parse(strings[0]);
        int pow = int.Parse(strings[1]);

        return number.ToString("F2") + " * 10^" + pow.ToString();
    }

    private static string ConvertBig(string s)
    {
        int pow = s.Length - 1;
        float number = float.Parse(s) / Mathf.Pow(10, pow);

        return number.ToString("F2") + " * 10^" + pow.ToString();
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
