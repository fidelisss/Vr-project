using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TransformSnapPoint : MonoBehaviour
{
    [SerializeField] private float _threshold = 0.2f;

    // Interactor in the _snappedSnap is based on TransformSnapObject's Interactor field.
    // Be shure to assign it using the grabbable events of the object
    private GameObject _snappedObject;
    private TransformSnapObject _snappedSnap;

    private IEnumerator _moveToSnapInstance;
    private bool snapped = false;

    private void Update()
    {
        if (_snappedObject)
        {
            // Check if object is grabbed
            if (_snappedSnap.Interactor)
            {
                // Snap, if hand is close enough
                if (Vector3.Distance(_snappedSnap.Interactor.transform.position, transform.position) < _threshold)
                    { if (!snapped) StartSnap(); }
                else 
                    { if (snapped) StopSnap(); }
            }
            else if (snapped) StopSnap();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TransformSnapObject newSnapObject = other.GetComponent<TransformSnapObject>();
        if (!_snappedObject && newSnapObject)
        {
            _snappedObject = other.gameObject;
            _snappedSnap = newSnapObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _snappedObject)
        {
            if (snapped) StopSnap();
            _snappedObject = null;
            _snappedSnap = null;
        }
    }

    private void StartSnap()
    {
        snapped = true;

        // Disable tracking to fix object in place
        _snappedObject.GetComponent<XRGrabInteractable>().trackPosition = false;
        _snappedObject.GetComponent<XRGrabInteractable>().trackRotation = false;
        _snappedObject.GetComponent<XRGrabInteractable>().throwOnDetach = false;

        _moveToSnapInstance = MoveToSnap(_snappedObject.transform, transform.position, transform.rotation);
        StartCoroutine(_moveToSnapInstance);
    }

    private void StopSnap()
    {
        snapped = false;

        // Re-enable tracking
        _snappedObject.GetComponent<XRGrabInteractable>().trackPosition = true;
        _snappedObject.GetComponent<XRGrabInteractable>().trackRotation = true;
        _snappedObject.GetComponent<XRGrabInteractable>().throwOnDetach = true;

        // Force stop of the coroutine
        if (_moveToSnapInstance != null) StopCoroutine(_moveToSnapInstance);
    }

    IEnumerator MoveToSnap(Transform objTransform, Vector3 localPosition, Quaternion localRotation)
    {
        float time = 0;
        // Move smoothly until both position and rotation are close enough to targets, or time is up
        while(((objTransform.localPosition - localPosition).magnitude >= 0.001f || Quaternion.Angle(objTransform.localRotation, localRotation) > 1) && time < 0.3f)
        {
            time += Time.deltaTime;
            objTransform.localPosition = Vector3.Lerp(objTransform.localPosition, localPosition, Time.deltaTime*20);
            objTransform.localRotation = Quaternion.Lerp(objTransform.localRotation, localRotation, Time.deltaTime*20);
            yield return null;
        }

        // Finish movement with precision
        objTransform.position = localPosition;
        objTransform.rotation = localRotation;
        yield return null;
    }
}
