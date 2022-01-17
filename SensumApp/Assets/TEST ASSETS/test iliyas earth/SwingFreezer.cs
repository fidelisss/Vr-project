using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingFreezer : MonoBehaviour
{
    private bool spring = false;
    private Rigidbody rg;
    private Quaternion offset;

    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
        rg.sleepThreshold = 0;
        //    rg.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        offset = Quaternion.Euler(0, 0, 0);
    }

    public void StateShange()
    {
        if (spring == false)
        {
            StartCoroutine(AnimateRotationTowards(this.transform, offset, 1f));
            spring = true;
        }
        else
        {
            // rg.freezeRotation = false;
            rg.isKinematic = false;
            spring = false;
        }
    }


    private IEnumerator AnimateRotationTowards(Transform target, Quaternion _offset, float dur)
    {
        float t = 0f;
        Quaternion start = target.localRotation;
        while (t < dur)
        {
            target.localRotation = Quaternion.Slerp(start, _offset, t / dur);
            yield return null;
            t += Time.deltaTime;

            // rg.freezeRotation = true;
            rg.isKinematic = true;
        }

        target.localRotation = _offset;
    }
}