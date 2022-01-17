using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    private Item _item;
    
    private void Awake() => _item = GetComponent<Item>();

    private void Start()
    {
        if (_item)
            DrawManager.Instance.SnapPoints.Add(this);
    }

    private void OnDestroy() => DrawManager.Instance.SnapPoints.Remove(this);
}
