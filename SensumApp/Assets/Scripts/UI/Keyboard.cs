using System.Linq;
using KeyboardVr.Modules;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    private FieldWindow FieldWindow;
    private Button[] Buttons { get; set; }
    private char[] Letters { get; set; }

    private void Awake()
    {
        Buttons = GetComponentsInChildren<Button>();
        Letters = InitLetters();
        InitLetterButtons();
        InitField();
        InitClearButton();
    }

    private void InitLetterButtons()
    {
        for (var i = 0; i < Buttons.Length - 1; i++)
        {
            var latterIndex = i;
            Buttons[i].onClick.AddListener(() => FieldWindow.AppendLetter(Letters, latterIndex));
            //Buttons[i].onClick.AddListener(() => FieldWindow.RemoveLetter());
        }
    }

    private void InitClearButton()
    {
        Buttons[Buttons.Length - 1].onClick.AddListener(() => FieldWindow.RemoveLetter());
    }

    public void InitField() => FieldWindow = FindObjectOfType<FieldWindow>();

    private static char[] InitLetters()
        => Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char) i).ToArray();
}