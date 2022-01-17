using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SubMenu : MonoBehaviour
{
    public SubMenuSegment[] SubMenusSegments { get; private set; }
    public Renderer Renderer { get; private set; }

    public bool Selected { get; private set; }

    public SubMenuSystem SubMenuSystem { get; private set; }

    private void Awake()
    {
        SubMenuSystem = GetComponentInParent<SubMenuSystem>();
        SubMenusSegments = GetComponentsInChildren<SubMenuSegment>(true);
        Renderer = GetComponent<Renderer>();

        foreach (var segments in SubMenusSegments)
            segments.Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Selected) return;
        if (SubMenuSystem.TriggerObject != null && other.gameObject == SubMenuSystem.TriggerObject)
        {
            SubMenuSystem.SwitchButton(this);
            return;
        }

        if (SubMenuSystem.IsXRHand &&
            other.gameObject.TryGetComponent(out XRDirectInteractor _))
        {
            SubMenuSystem.SwitchButton(this);
        }
    }

    public void SetStateSelect(bool flag) => Selected = flag;
}