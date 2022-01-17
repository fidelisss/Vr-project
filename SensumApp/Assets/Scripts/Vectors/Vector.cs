using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour
{
    // Defines if vecotor will ignore object rotation or not
    [SerializeField] private bool _isStatic;
    private Transform _vectorEndTransform;
    [SerializeField] private GameObject _markerPrefab;
    [SerializeField] private GameObject _lineTipPrefab;

    private Transform _mainCamera;

    private void Start()
    {
        _vectorEndTransform = transform.GetChild(0);
        DrawVector();
        DrawTip();
        _mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (_isStatic)
        {
            transform.up = Vector3.up;
        }
        _vectorEndTransform.LookAt(transform);
        
    }

    private void  DrawVector()
    {
        StartCoroutine(DrawLineCoroutine(transform, _vectorEndTransform));
    }

    private void DrawTip()
    {
        Instantiate(_lineTipPrefab, _vectorEndTransform.position, _vectorEndTransform.rotation, _vectorEndTransform);
    }

    // "yield return null" is here to give unity time to initialize the marker and calculate its way
    private IEnumerator DrawLineCoroutine(Transform start, Transform end)
    {
        // create marker to draw a line
        Vector3 startPosition = start.position;
        MarkerController markerController = Instantiate(_markerPrefab, startPosition, Quaternion.identity).GetComponent<MarkerController>();

        if (markerController.mode != MarkerController.Mode.Line) markerController.SwitchMode();

        foreach (Transform m in markerController.transform)
        {
            if (start.GetComponent<HoloTransmitter>())
                m.GetComponent<HoloMarker>().holodesk = start.GetComponent<HoloTransmitter>().Holodesk;
        }

        yield return null;

        // Draw a line
        start.position = startPosition;
        markerController.StartDraw();

        GameObject line = markerController.activeMarker.GetComponent<HoloMarker>().trailTransform.gameObject;
        line.GetComponent<Trail>().erasable = false;
        line.GetComponent<Trail>().snapA = start.GetComponent<SnapPoint>();
        markerController.transform.position = end.position;

        yield return null;

        line.GetComponent<Trail>().snapB = end.GetComponent<SnapPoint>();

        // Finish drawing
        markerController.StopDraw();
        Destroy(markerController.gameObject);
    }
}
