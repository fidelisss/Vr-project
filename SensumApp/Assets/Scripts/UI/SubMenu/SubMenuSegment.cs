using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SubMenuSegment : MonoBehaviour
{
    
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;

    private float _animationDuration;
    private SubMenu _menu;
    
    private void Awake()
    {
        _menu = GetComponentInParent<SubMenu>();
        _animationDuration = _menu.SubMenuSystem.AnimationDuration;
    }

    public void SetEndPosition() =>  _endPosition = transform.localPosition;

    public void SetStartPosition() => 
        _startPosition  = transform.localPosition;

    public void Show()
    {
        gameObject.SetActive(true);
        //StartCoroutine(ShowCoroutine());
    }
    
    public void Show(SubMenuAnimationType animationType)
    {
        switch (animationType)
        {
            case SubMenuAnimationType.Simple:
                break;
            case SubMenuAnimationType.StepByStep:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
        }
    }

    public void Hide()
    {
        //StartCoroutine(HideCoroutine());
        gameObject.SetActive(false);
    }
    
    public void Hide(SubMenuAnimationType animationType)
    {
        switch (animationType)
        {
            case SubMenuAnimationType.Simple:
                break;
            case SubMenuAnimationType.StepByStep:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
        }
    }

    private IEnumerator ShowCoroutine()
    {
        transform.DOLocalMove(_endPosition, _animationDuration);
        yield return new WaitForSeconds(_animationDuration);
    }

    private IEnumerator HideCoroutine()
    {
        transform.DOLocalMove(_startPosition, _animationDuration);
        yield return new WaitForSeconds(_animationDuration);
    }
}