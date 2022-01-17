using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class SwitchButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectToSwitch;

    private Toggle toggle;

    private void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
    }
    public void OnSwitch()
    {
        if (toggle.isOn == false)
        {
           
            foreach (GameObject g in ObjectToSwitch)
            {
                g.SetActive(false); 
                
            }
           
        }
        else if (toggle.isOn == true)
        {
            
            foreach (GameObject g in ObjectToSwitch)
            {
                g.SetActive(true);
            }

            
        }
    }
}
