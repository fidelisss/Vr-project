using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    // 0 : not held
    // 1 : held in right
    // 2 : held in left
    int isHeld = 0;

    [Header("Input actions")]
    public InputActionReference toggleUIReferenceRight;
    public InputActionReference thumbstickReferenceRight;
    public InputActionReference toggleUIReferenceLeft;
    public InputActionReference thumbstickReferenceLeft;

    [Header("UI panels")]
    public GameObject panelUI;

    bool isPressed = false;
    float deadzone = 0.3f;

    void Update()
    {
        CheckThumbstick();
    }

    void CheckThumbstick()
    {
        // Read the thumbstick values
        Vector2 stickPosition = Vector2.zero;
        if (isHeld == 1)
        {
            stickPosition = thumbstickReferenceRight.action.ReadValue<Vector2>();
        }
        else if (isHeld == 2)
        {
            stickPosition = thumbstickReferenceLeft.action.ReadValue<Vector2>();
        }

        // Return to the center to press again
        if (stickPosition.magnitude < deadzone) isPressed = false;
        if (isPressed) return;

        // Press
        if (stickPosition.magnitude >= deadzone)
        {
            isPressed = true;

            float angle = Vector2.SignedAngle(stickPosition, new Vector2(-1,1));

            Debug.Log(angle);
            if (angle >= 0 && angle < 90)
                ButtonClick(0);
            else if (angle >= 90 && angle < 180)
                ButtonClick(1);
            else if (angle >= -180 && angle < -90)
                ButtonClick(2);
            else if (angle >= -90 && angle < 0)
                ButtonClick(3);
        }
        
    }

    void OnDestroy()
    {
        toggleUIReferenceRight.action.started -= ToggleUI;
    }

    public void SelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactor.gameObject.tag == "RightHand")
        {
            isHeld = 1;
            toggleUIReferenceRight.action.started += ToggleUI;
        }
        else if (args.interactor.gameObject.tag == "LeftHand")
        {
            isHeld = 2;
            toggleUIReferenceLeft.action.started += ToggleUI;
        } 

        Debug.Log(isHeld);
    }

    public void SelectExit(SelectExitEventArgs args)
    {
        isHeld = 0;

        if (args.interactor.gameObject.tag == "RightHand")
        {
            toggleUIReferenceRight.action.started -= ToggleUI;
        }
        else if (args.interactor.gameObject.tag == "LeftHand")
        {
            toggleUIReferenceLeft.action.started -= ToggleUI;
        } 
    }


    void ToggleUI(InputAction.CallbackContext context)
    {
        panelUI.SetActive(!panelUI.activeSelf);
    }

    void ButtonClick(int index)
    {
        Button btn = panelUI.GetComponent<UIWheelController>().buttons[index]; 
        btn.onClick.Invoke();
        StartCoroutine(ButtonPressedCoroutine(btn));
    }

    // Change button color
    IEnumerator ButtonPressedCoroutine(Button btn)
    {
        ColorBlock cb = btn.colors;

        Color originalColor = btn.colors.selectedColor;
        Color pressedColor = btn.colors.pressedColor;

        cb.normalColor = pressedColor;

        while (cb.normalColor != originalColor)
        {
            cb.normalColor = Color.Lerp(cb.normalColor, originalColor, Time.deltaTime);
            btn.colors = cb;
            yield return null;
        }
    }
}
