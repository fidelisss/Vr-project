using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    TrailRenderer trailRenderer;
    LineRenderer lineRenderer;
    public bool erasable = true;

    [SerializeField] private SnapPoint _snapA;
    public SnapPoint snapA
    {
        get { return _snapA; }
        set
        {
            _snapA = value;

            if (GetComponent<LineRenderer>() && _snapA)
            {
                relativePointA = GetComponent<LineRenderer>().GetPosition(0) + transform.position - _snapA.transform.position;
                initialRotationA = _snapA.transform.rotation;
                SnapAftermathA();
            }
        }
    }
    public Vector3 relativePointA;
    public Quaternion initialRotationA;


    public SnapPoint _snapB;
    public SnapPoint snapB
    {
        get { return _snapB; }
        set
        {
            _snapB = value;

            if (GetComponent<LineRenderer>() && _snapB)
            {
                relativePointB = GetComponent<LineRenderer>().GetPosition(1) + transform.position - _snapB.transform.position;
                initialRotationB = _snapB.transform.rotation;
                SnapAftermathB();
            }
        }
    }
    public Vector3 relativePointB;
    public Quaternion initialRotationB;



    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (lineRenderer)
        {
            if (snapA) lineRenderer.SetPosition(0, CalcPointPosition(relativePointA, initialRotationA, snapA.transform));
            if (snapB) lineRenderer.SetPosition(1, CalcPointPosition(relativePointB, initialRotationB, snapB.transform));
        }
    }


    // place cube colliders along the trail
    public virtual void FinishTrail()
    {
        if (!erasable)
        {
            return;
        }

        Vector3 lastPos = Vector3.zero;
        if (trailRenderer)
            lastPos = trailRenderer.GetPosition(0);
        else if (lineRenderer)
            lastPos = lineRenderer.GetPosition(0) + transform.position;

        float minDist = 0.03f;
        float dist = minDist;

        int posCount = 0;
        if (trailRenderer)
            posCount = trailRenderer.positionCount;
        else if (lineRenderer)
            posCount = lineRenderer.positionCount;

        for (int i = 0; i < posCount; i++)
        {
            Vector3 pos = Vector3.zero;
            if (trailRenderer)
                pos = trailRenderer.GetPosition(i);
            else if (lineRenderer)
                pos = lineRenderer.GetPosition(i) + transform.position;
                
            dist += Vector3.Distance(pos, lastPos);

            if (dist >= minDist)
            {
                if (dist >= 2*minDist)
                {
                    int mul = (int)(dist/minDist);
                    for (int j = 1; j < mul; j++)
                        CreateCollider((j*pos + (mul-j)*lastPos)/mul);
                }
                CreateCollider(pos);
                dist = 0;
            }
            lastPos = pos;
        }

        if (lineRenderer && (snapA || snapB) && snapCollidersInstance == null)
        {
            snapCollidersInstance = SnapColliders();
            StartCoroutine(snapCollidersInstance);
        }

        //gameObject.tag = "Drawing";
    }


    IEnumerator snapCollidersInstance = null;
    IEnumerator SnapColliders()
    {
        while (true)
        {
            Vector3 oldA = lineRenderer.GetPosition(0);
            Vector3 oldB = lineRenderer.GetPosition(1);

            yield return new WaitForSeconds(0.25f);

            Vector3 newA = lineRenderer.GetPosition(0);
            Vector3 newB = lineRenderer.GetPosition(1);

            if (oldA != newA || oldB != newB)
            {
                foreach (Transform child in transform)
                    Destroy(child.gameObject);

                FinishTrail();
            }
        }
    }


    void CreateCollider(Vector3 pos)
    {
        GameObject colObject = new GameObject("trailCol", typeof(BoxCollider));
        colObject.GetComponent<BoxCollider>().isTrigger = true;
        colObject.transform.parent = transform;
        colObject.transform.localScale *= 0.02f;
        colObject.transform.position = pos;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Eraser")
        {
            Destroy(gameObject);
        }
    }


    Vector3 CalcPointPosition(Vector3 relativePoint, Quaternion initialRotation, Transform snap)
    {
        Quaternion rotDif = snap.rotation * Quaternion.Inverse(initialRotation);
        Vector3 transformedPosition = rotDif * relativePoint - transform.position + snap.position;
        return transformedPosition;
    }

    public virtual void SnapAftermathA() 
    {

    }
    
    public virtual void SnapAftermathB() 
    {

    }
}
