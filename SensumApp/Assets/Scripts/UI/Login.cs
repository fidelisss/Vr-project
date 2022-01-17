using System;
using System.Collections;
using System.Collections.Generic;
using KeyboardVr.Modules;
using UnityEngine;
using UnityEngine.UI;

public class Login : FieldWindow
{
    [SerializeField] private Keyboard _keyboard;
    [SerializeField] private Image _imageUsername;
    [SerializeField] private Image _imagePassword;
    private void OnEnable()
    {
        _keyboard.InitField();
    }

    public void ChangeColorOfUsername()
    {
        _imagePassword.color = Color.gray;
        _imageUsername.color = Color.white;
    }
    
    public void ChangeColorOfPassword()
    {
        _imageUsername.color = Color.gray;
        _imagePassword.color = Color.white;
    }
}
