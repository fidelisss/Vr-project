using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidStream : MonoBehaviour
{
    public GameObject splash;
    public GameObject bottleNeck;
    public TubeBehaviour tube;
    public LiquidBehaviour liquid;
    public float bandwidth = 1;
    public float maxBandwidth = 0.5f;
    public float tiltMultiplier = 0.1f;
    // public float tilt;
    public float streamWidthMultiplier;

    public GameObject selfCollider;

    // public float innerRadius = 1f;
    // public float outerRadius = 1f;

    public float maxWidth;

    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splash = Instantiate(splash, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bottleNeck)
            CalcBandwidth();
            // bandwidth = tilt * tiltMultiplier;
        // if (liquid)
            // bandwidth *= liquid.fill;

        bandwidth = Mathf.Clamp(bandwidth, 0f, maxBandwidth);
        lineRenderer.startWidth = Mathf.Clamp(bandwidth * streamWidthMultiplier, 0, tube ? tube.innerRadius*2 : 1);

        Vector3 start = FindStartPoint();
        Vector3 end = FindEndPoint(start);

        MoveToPosition(0, start);
        MoveToPosition(1, end);

        splash.transform.position = end;
        var sh = splash.GetComponent<ParticleSystem>().shape;
        var mm = splash.GetComponent<ParticleSystem>().main;
        sh.radius = lineRenderer.startWidth;
        mm.startSize = lineRenderer.startWidth;
    }

    void MoveToPosition(int index, Vector3 target)
    {
        lineRenderer.SetPosition(index, target);
    }


    Vector3 FindStartPoint()
    {
        Vector3 startPoint = transform.position;

        if (bottleNeck)
        {
            startPoint = FindEdge(tube.outerRadius * (1 - lineRenderer.startWidth/tube.innerRadius/2));
        }

        return startPoint;
    }


    Vector3 FindEndPoint(Vector3 startPoint)
    {
        RaycastHit hit;
        Ray ray = new Ray(startPoint, Vector3.down);

        ToggleSelfCollision(false);
        Physics.Raycast(ray, out hit, 2.0f);
        ToggleSelfCollision(true);

        Vector3 added = hit.point;

        if (hit.transform)
        {
            if (hit.transform.tag == "BottleNeck")
            {
                LiquidBehaviour target = hit.transform.GetComponent<BottleNeck>().liquid;
                FillTarget(bandwidth*Time.deltaTime, target);

                // added.y = target.ObjectHeight * (target.fill-1);
                added.y = target.WaterLevel;
            }
        }
        

        Vector3 endPoint = hit.collider ? added : ray.GetPoint(2.0f);

        return endPoint;
    }


    void FillTarget(float volume, LiquidBehaviour target)
    {
        target.AddVolume(volume);
    }


    public Vector3 FindEdge(float rad)
    {
        Vector3 proj1 = Vector3.Project(Vector3.down, transform.forward);
        Vector3 proj2 = Vector3.Project(Vector3.down, transform.right);
        // Vector3 radiusVector = tube.GetComponent<Collider>().ClosestPoint(Vector3.Normalize(proj1 + proj2));
        Vector3 radiusVector = transform.position + Vector3.Normalize(proj1 + proj2) * rad;
        // return tube.GetComponent<Collider>().ClosestPoint(radiusVector);
        return radiusVector;
    }


    void ToggleSelfCollision(bool state)
    {
        if (selfCollider && bottleNeck)
        {
            foreach (Transform child in selfCollider.transform)
            {
                child.gameObject.layer = state ? 0 : 2;
            }
            bottleNeck.layer = state ? 0 : 2;
        }
    }


    void CalcBandwidth()
    {
        float overflow = liquid.WaterLevel - FindEdge(tube.innerRadius).y;
        float sine = Mathf.Sqrt(1 - Mathf.Pow(transform.up.y, 2));
        bandwidth = overflow / sine * tiltMultiplier;
        
        if (bandwidth < 0.01f)
            bandwidth = 0;
        
    }
}
