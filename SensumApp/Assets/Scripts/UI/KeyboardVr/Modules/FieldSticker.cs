using UnityEngine;
using UnityEngine.UI;

namespace KeyboardVr.Modules
{
    [RequireComponent(typeof(InputField))]
    public class FieldSticker : MonoBehaviour
    {
        [SerializeField] private bool _readyChange;

        public InputField InputField { get; private set; }

        public Button FocusButton { get; private set; }

        private void Awake()
        {
            InputField = GetComponent<InputField>();
            FocusButton = GetComponentInChildren<Button>();
        }

        public bool ReadyChange
        {
            get => _readyChange;
            set => _readyChange = value;
        }
    }
}