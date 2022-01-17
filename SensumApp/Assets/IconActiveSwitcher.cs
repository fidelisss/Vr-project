using UnityEngine;

public class IconActiveSwitcher : MonoBehaviour
{
    [SerializeField] private bool _setActive;

    public bool SetActive => _setActive;

    public void OnActiveSwitch()
    {
        if (_setActive)
        {
            _setActive = false;
            gameObject.SetActive(false);
        }
        else
        {
            _setActive = true;
            gameObject.SetActive(true);
        }
    }
}