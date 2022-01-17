using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SwingOfCenterMass : MonoBehaviour
{
    [SerializeField] private float _swingMoveDuration;
    [SerializeField] private GameObject _centerMassShower;

    private SwingPlane _swingPlane;
    private SwingObjectContainer _objectContainer;

    private Coroutine _coroutine;
    private float _result;

    private void Awake()
    {
        _swingPlane = GetComponentInChildren<SwingPlane>();
        _objectContainer = GetComponentInChildren<SwingObjectContainer>();
    }

    private void OnEnable()
    {
        _objectContainer.FoundCenterOfMass += OnFoundCenterOfMass;
        _objectContainer.ObjectMoved += OnFoundCenterOfMass;
    }

    private void OnDisable()
    {
        _objectContainer.FoundCenterOfMass -= OnFoundCenterOfMass;
        _objectContainer.ObjectMoved -= OnFoundCenterOfMass;
    }

    private void OnFoundCenterOfMass()
    {
        _coroutine = StartCoroutine(CenterMass());
    }

    private IEnumerator CenterMass()
    {
        yield return new WaitForSeconds(0.5f);
        _result = GetCenterMass();
        Debug.Log(_result);
        _centerMassShower.transform.DOLocalMoveZ(_result, _swingMoveDuration);
    }

    private float GetCenterMass()
    {
        float first = 0;
        float second = 0;

        var objects =
            _objectContainer.Objects.Where(o => o.GetRelativeSpeed(_swingPlane.transform) < 0.01f).ToArray();

        var massesOfObjects = objects.Select(o => o.Mass).ToArray();
        var coordinateOfObjects =
            objects.Select(o => o.GetFromCenterCoordinate(_swingPlane.Center.transform)).ToArray();

        try
        {
            for (var i = 0; i < _objectContainer.Objects.Count; i++)
            {
                first += massesOfObjects[i] * coordinateOfObjects[i];
                second += massesOfObjects[i];
            }
        }
        catch (Exception)
        {
            Debug.LogWarning("Array of objects bounds");
        }

        return second == 0 ? 0 : first / second;
    }
}