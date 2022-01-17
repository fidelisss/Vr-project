using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace KeyboardVr.Modules
{
    public abstract class FieldWindow : MonoBehaviour
    {
        [SerializeField] private FieldSticker[] _fields;
        public IEnumerable<FieldSticker> Fields => _fields;

        private PointerEventData EventData { get; set; }
        private GraphicRaycaster Raycaster { get; set; }
        private Canvas Canvas { get; set; }
        private List<RaycastResult> Result { get; set; }

        private FieldSticker CurrentField { get; set; }

        protected virtual void Awake()
        {
            InitWindow();
            Canvas = GetComponentInParent<Canvas>();
            Raycaster = Canvas.GetComponent<GraphicRaycaster>();
            EventData = new PointerEventData(EventSystem.current);
            Result = new List<RaycastResult>();

            CurrentField = _fields[0];
            CurrentField.ReadyChange = true;
        }

        public void RemoveLetter()
        {
            //foreach (var fieldSticker in Fields.Where(field => field.ReadyChange))
            //{
         CurrentField.InputField.text =
                    CurrentField.InputField.text.Remove(CurrentField.InputField.text.Length - 1, 1);
            
            //}
        }

        public void ChangeField(int index)
        {
            if (!_fields[index].ReadyChange)
            {
                CurrentField.ReadyChange = false;
                CurrentField = _fields[index];
                CurrentField.ReadyChange = true;
            }
        }

        public void AppendLetter(char[] letter, int index)
        {
            foreach (var fieldSticker in Fields.Where(field => field.ReadyChange))
            {
                fieldSticker.InputField.text += letter[index];
            }
        }
        
        protected void InitWindow() => _fields = GetComponentsInChildren<FieldSticker>();
    }
}