using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public GameObject display;

    public int hour;
    public int minutes;
   


    void Start()
    {
        hour = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
      

        display.GetComponent<Text>().text =  hour + ":" + minutes;
    }

    // Update is called once per frame
    void Update()
    {
        Clock();
    }
    void Clock()
    {
        hour = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;

        string minString = (minutes >= 10) ? minutes.ToString() : ('0' + minutes.ToString());


        display.GetComponent<Text>().text = hour + ":" + minString;

    }
}
