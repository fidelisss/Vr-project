using System;
using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
public class SubMenuSystem : MonoBehaviour
{
    [Header("Trigger Object")]
    [SerializeField] private GameObject _triggerObject;
    [Header("Animation (Beta)")]
    [SerializeField] private SubMenuAnimationType _animationType;
    [SerializeField] private float _animationDuration;
    [Header("Colors")]
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;
    
    [Header("Trigger Type")]
    [SerializeField] private bool _isSerializeObject;
    [SerializeField] private bool _isXRHand;

    private SubMenu[] _subMenus;
    private SubMenu _currentMenu;
    private SubMenu _previousMenu;

    public GameObject TriggerObject => _triggerObject;
    
    public float AnimationDuration => _animationDuration;
    public bool IsSerializeObject => _isSerializeObject;
    public bool IsXRHand => _isXRHand;


    private void Awake()
    {
        _subMenus = GetComponentsInChildren<SubMenu>();

    }

    private void Start()
    {
        foreach (var subMenu in _subMenus)
        {
            subMenu.SetActive(true);
            subMenu.Renderer.material.color = _defaultColor;
        }
    }

    public void SwitchButton(SubMenu subMenu)
    {
        _previousMenu = _currentMenu;
        _currentMenu = subMenu;
        Select(_currentMenu);
        Deselect(_previousMenu);
    }

    private void Select(SubMenu subMenu)
    {
        subMenu.Renderer.material.color = _selectedColor;
        foreach (var segments in subMenu.SubMenusSegments)
            segments.Show();

        subMenu.SetActive(true);
        subMenu.SetStateSelect(true);
    }

    private void Deselect(SubMenu subMenu)
    {
        subMenu.Renderer.material.color = _defaultColor;
        foreach (var segments in subMenu.SubMenusSegments)
            segments.Hide();

        subMenu.SetActive(true);
        subMenu.SetStateSelect(false);
    }
}