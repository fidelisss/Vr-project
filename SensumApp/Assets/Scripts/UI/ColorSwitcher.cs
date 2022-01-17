using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    public Image image;
    int currentColor = 0;
    
    public void SwitchColor()
    {
        currentColor++;
        if (currentColor >= _colors.Length) currentColor = 0;

        image.color = _colors[currentColor];
    }

}
