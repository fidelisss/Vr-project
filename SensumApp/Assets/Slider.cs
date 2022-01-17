using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    //Script used in Lecture arrow buttons
   
    private content[] _content;
    public void Awake()
    {
        _content = parent.GetComponentsInChildren<content>(true);           //Searches for every object with content component in parent

        _content[0].gameObject.SetActive(true);
        for (int t = 1; t < _content.Length; t++)
        {
            _content[t].gameObject.SetActive(false);         //sets each object except the very first one to disabled
        }

    }

    //public void Update() //Inspector Quaility of life shortcuts to check functionality
    //{
    //    if (Input.GetKeyDown("p"))
    //    {
    //        rightButton();
    //    } 
    //    if (Input.GetKeyDown("o"))
    //    {
    //        leftButton();
    //    }
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        Debug.Log(_content.Length);
                
    //    }
    //}
    public void rightButton()   //method for Onclick events; skims every content children of parent, looks for active and not last one. 
    {                           //Then perform switch to the next one by disabling current active children, and activating next one in the array.

        for (int i = 0;  i < _content.Length; i++)
        {
            if (_content[i].gameObject.activeSelf == true && _content[i + 1] != null)
            {
                _content[i].gameObject.SetActive(false);
                _content[i+1].gameObject.SetActive(true);
                break;
            }
        }
    }
    public void leftButton()  //Same as Rightbutton but in the other way when finds active child. activates child in the back of the current one.
    {
        for (int i = 0; i < _content.Length; i++)
        {
            if (_content[i].gameObject.activeSelf == true && _content[i - 1] != null)
            {
                _content[i].gameObject.SetActive(false);
                _content[i-1].gameObject.SetActive(true);
                break;
            }

        }

    }
        
}
