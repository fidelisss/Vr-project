using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    // we need two sliders
    // we need min and max values of each

    private Slider scaleSlider;
    public float scaleMinValue;
    public float scaleMaxValue;

    

    public Slider sl;




    void Start()
    {
        // find the sliders by name
        //initialize the max and min value when starting
        // Add a listener to the slider when value is changed


        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        //scaleSlider.minValue = scaleMinValue;
        //scaleSlider.maxValue = scaleMaxValue;

        //scaleSlider.onValueChanged.AddListener(ScaleSliderUpdate);

    }

    public void OnTriggerEnter(Collider collision)
    {
        //transform.localScale -= new Vector3(graw,graw,graw,Time.deltaTime/1f);
    }
}


