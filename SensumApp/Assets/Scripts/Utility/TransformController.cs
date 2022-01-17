using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Should be capable of changing Transform parameters with methods.
// For example, we can call methods of this script through events
public class TransformController : MonoBehaviour
{
    public float AnimationTime = 1;

    private Transform _transform;
    [HideInInspector] public Transform Parent;

    void Awake()
    {
        _transform = transform;
        Parent = _transform.parent;
    }

    public void SetRotation(Quaternion rotation)
    {
        _transform.rotation = rotation;
    }

    public void SetRotationX(float x)
    {
        _transform.rotation = Quaternion.Euler(x, _transform.rotation.eulerAngles.y, _transform.rotation.eulerAngles.z);
    }

    public void SetRotationY(float y)
    {
        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x, y, _transform.rotation.eulerAngles.z);
    }

    public void SetRotationZ(float z)
    {
        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y, z);
    }

    public void SetLocalRotation(Quaternion rotation)
    {
        _transform.localRotation = rotation;
    }

    public void SetLocalRotationX(float x)
    {
        _transform.localRotation = Quaternion.Euler(x, _transform.localRotation.eulerAngles.y, _transform.localRotation.eulerAngles.z);
    }

    public void SetLocalRotationY(float y)
    {
        _transform.localRotation = Quaternion.Euler(_transform.localRotation.eulerAngles.x, y, _transform.localRotation.eulerAngles.z);
    }

    public void SetLocalRotationZ(float z)
    {
        _transform.localRotation = Quaternion.Euler(_transform.localRotation.eulerAngles.x, _transform.localRotation.eulerAngles.y, z);
    }

    public void Rotate(Vector3 eulers)
    {
        _transform.Rotate(eulers);
    }

    public void RotateX(float x)
    {
        _transform.Rotate(x, 0, 0);
    }

    public void RotateY(float y)
    {
        _transform.Rotate(0 , y, 0);
    }

    public void SmoothRotateY(float y)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(SmoothRotateCoroutine(Quaternion.Euler(_transform.localRotation.eulerAngles.x, y, _transform.localRotation.eulerAngles.z)));
    }

    public void RotateZ(float z)
    {
        _transform.Rotate(0, 0, z);
    }

    IEnumerator SmoothRotateCoroutine(Quaternion targetRotation)
    {
        float t = 0;
        Quaternion initialRotation = _transform.localRotation;
        while (t < AnimationTime)
        {
            SetLocalRotation(Quaternion.Lerp(initialRotation, targetRotation, t/AnimationTime));
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void SetLocalPosition(Vector3 position)
    {
        _transform.localPosition = position;
    }

    public void SetLocalPositionX(float x)
    {
        _transform.localPosition = new Vector3(x, _transform.localPosition.y, _transform.localPosition.z);
    }

    public void SetLocalPositionY(float y)
    {
        _transform.localPosition = new Vector3(_transform.localPosition.x, y, _transform.localPosition.z);
    }

    public void SetLocalPositionZ(float z)
    {
        _transform.localPosition = new Vector3(_transform.localPosition.x, _transform.localPosition.y, z);
    }

    public void SmoothMove(Vector3 targetPosition)
    {
        if (gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothMoveCoroutine(targetPosition));
        }
    }

    public void SmoothMoveZ(float z)
    {
        SmoothMove(new Vector3(_transform.localPosition.x, _transform.localPosition.y, z));
    }

    public void SmoothMoveY(float y)
    {
        SmoothMove(new Vector3(_transform.localPosition.x, y, _transform.localPosition.z));
    }
    
    public void SmoothMoveX(float x)
    {
        SmoothMove(new Vector3(x,_transform.localPosition.y, _transform.localPosition.z));
    }

    IEnumerator SmoothMoveCoroutine(Vector3 targetPosition)
    {
        float t = 0;
        Vector3 initialPosition = _transform.localPosition;
        while (t < AnimationTime)
        {
            SetLocalPosition(Vector3.Lerp(initialPosition, targetPosition, t/AnimationTime));
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void MirrorLocalPosition(TransformController tc)
    {
        _transform.position = Parent.TransformPoint(-tc.Parent.InverseTransformPoint(tc.transform.position));
    }

    public void ScaleUniformly(float scale)
    {
        _transform.localScale = Vector3.one * scale;
    }

    // Add more methods later
}
