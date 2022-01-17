using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightChanger : MonoBehaviour
{
    [SerializeField] GameObject _leftWeight;  //References to both weights
    public GameObject LeftWeight
    {
        get { return _leftWeight; }
    }
    [SerializeField] GameObject _rightWeight;
    public GameObject RightWeight
    {
        get { return _rightWeight; }
    }
   
    private Slider3dContraint _sliderValue;

    private void Awake()
    {
        _sliderValue = GetComponent<Slider3dContraint>();  //Reference to SLiderComponent inside handle
    }
    private void Update()
    {
        WieghtMove();
    }
    private void WieghtMove()
    {
        _rightWeight.transform.localPosition = new Vector3(0, 0, 0 + _sliderValue.Value);  //Weights z value changed to the Slider Value 
        _leftWeight.transform.localPosition =  new Vector3(0, 0, 0-_sliderValue.Value); 
       
    }
  

}


    

