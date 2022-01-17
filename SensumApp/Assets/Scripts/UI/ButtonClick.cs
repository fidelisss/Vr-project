using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private List<string> InteractiveTags;
    private float CoolDownTime = 0.3f;
    private bool CoolDown = false;
    //private InputAction selectAction;
    //[Space][SerializeField] private InputActionAsset XRcontroller;

    //private bool action = false;
    //private void Start()
    //{
    //  var gameplaytActionMap = XRcontroller.AddActionMap("XRI RightHand");
    //    selectAction = gameplaytActionMap.AddAction("Select");
    //    selectAction.started += SelectAction_started;
    //    selectAction.performed += SelectAction_performed;
    //}

    //private void SelectAction_performed(InputAction.CallbackContext obj)
    //{
    //    action = false;
    //    Debug.Log("preformed");
    //}

    //private void SelectAction_started(InputAction.CallbackContext obj)
    //{
    //    action = true;
    //    Debug.Log("started");
    //}


        private void OnTriggerStay(Collider other)
    {
        if (CoolDown && !InteractiveTags.Contains(other.gameObject.tag))
           
        {
            CoolDown = true;
            button.onClick.Invoke();
            StartCoroutine("CoolDownCoroutine");
        }

    }
    
    IEnumerator CoolDownCoroutine()
    {
        yield return new WaitForSeconds(CoolDownTime);
        CoolDown = false;
    }
}
