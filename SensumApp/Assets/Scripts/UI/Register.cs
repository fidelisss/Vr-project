using System;
using System.Linq;
using KeyboardVr.Modules;
using UnityEngine;
using UnityEngine.UI;

public class Register : FieldWindow
{
   [SerializeField] private Keyboard _keyboard;
   [SerializeField] private Image _imageUsername;
   [SerializeField] private Image _imagePassword;
   
   protected override void Awake()
   {
      base.Awake();
      InitWindow();
   }

   private void OnEnable() => _keyboard.InitField();
   
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