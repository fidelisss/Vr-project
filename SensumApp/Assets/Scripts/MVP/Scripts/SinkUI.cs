using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkUI : MonoBehaviour
{
    public LiquidStream stream;
    public Image image;
    public bool isOn = false;

    float timeOut = 0.3f;
    float time = 0f;

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.layer != 2) && time <= 0 && (col.gameObject.tag == "Hand"))
        {
            time = timeOut;

            isOn = isOn ? false : true;

            Color c1 = isOn ? new Color(0.801f, 0.07f, 0.049f, 1f) : new Color(0.098f, 0.632f, 0.147f, 1f);
            Color c2 = isOn ? new Color(0.098f, 0.632f, 0.147f, 1f) : new Color(0.801f, 0.07f, 0.049f, 1f);

            StartCoroutine(ColorCoroutine(c1, c2)); 
            
            stream.gameObject.SetActive(isOn);
        }
    }

    IEnumerator ColorCoroutine(Color c1, Color c2)
    {
        while (time > 0)
        {
            image.color = Color.Lerp(c1, c2, time/timeOut);
            time -= Time.deltaTime;
            yield return null;
        }
    }
}
