using UnityEngine;

public class SwingPlane : MonoBehaviour
{
    [SerializeField] private Transform _center;
    
    public SwingOfCenterMass SwingOfCenterMass { get; private set; }
    public Transform Center => _center;
    
    private void Awake() => SwingOfCenterMass = GetComponentInParent<SwingOfCenterMass>();
}
