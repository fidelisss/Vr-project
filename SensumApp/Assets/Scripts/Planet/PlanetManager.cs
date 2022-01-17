using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlanetManager : MonoBehaviour
{
    public List<Planet> planets;
    public GameObject distanceCanvasPrefab;
    public GameObject forceCanvasPrefab;
    public GameObject markerPrefab;
    public float scaleFactor;
    public List<GameObject> lines = new List<GameObject>();
    

    public void AddPlanet(Planet p)
    {
        if (!planets.Contains(p))
        {
            planets.Add(p);
            p.planetManager = this;
            DrawGraphics(p);
            print("Planet Added");
        }
        else
        {
            print("The planet is alredy in system");
        }

        // if (p.transform.parent != transform)
        //     p.transform.parent = transform;
    }

    public void AddPlanet(GameObject g)
    {
        if (g.GetComponent<Planet>())
            AddPlanet(g.GetComponent<Planet>());
        else
            print("Object is not a Planet");
    }

    public void RemovePlanet(Planet p)
    {
        if (planets.Contains(p))
        {
            DestroyLines(p);
            planets.Remove(p);
        }
    }

    private void DestroyLines(Planet p)
    {
        List<GameObject> toDestroy = new List<GameObject>();
        foreach (GameObject line in lines)
            if (line)
            {
                if (line.GetComponent<Trail>().snapA.gameObject == p.gameObject 
                    || line.GetComponent<Trail>().snapB.gameObject == p.gameObject)
                    toDestroy.Add(line);
            }
            else toDestroy.Add(line);

        foreach (GameObject g in toDestroy)
        {
            lines.Remove(g);
            Destroy(g);
        }
    }

    void DrawGraphics(Planet p)
    {
        foreach(Planet neighbour in planets)
        {
            if (p.planetManager && p != neighbour)
            {
                DrawLine(p, neighbour);
                DrawDistanceCanvases(p, neighbour);
            }
        }
    }

    void DrawLine(Planet p, Planet neighbour)
    {
        // p.DrawLine(neighbour);
        StartCoroutine(DrawLineCoroutine(p, neighbour));
    }

    void DrawDistanceCanvases(Planet p, Planet neighbour)
    {
        if (p.GetComponent<PhotonView>().IsMine && neighbour.GetComponent<PhotonView>().IsMine)
        {
            bool holo = (p.GetComponent<HoloTransmitter>() && neighbour.GetComponent<HoloTransmitter>());
            Vector3 pos = (p.transform.position + neighbour.transform.position) / 2;
            GameObject distanceUI = PhotonNetwork.Instantiate(distanceCanvasPrefab.name, pos, Quaternion.identity);
            GameObject forceUI = PhotonNetwork.Instantiate(forceCanvasPrefab.name, pos, Quaternion.identity);

            // if (holo)
            // {
            //     // distanceUI.GetComponent<HoloUI>().enabled = true;
            //     // forceUI.GetComponent<HoloUI>().enabled = true;
            // }

            // distanceUI.GetComponent<DistanceChecker>().scaleFactor = scaleFactor;
            // forceUI.GetComponent<ForceChecker>().scaleFactor = scaleFactor;

            LineInfoUI distanceLineInfo = distanceUI.GetComponent<LineInfoUI>();
            distanceLineInfo.startPoint = p.transform;
            distanceLineInfo.endPoint = neighbour.transform;
            // distanceLineInfo.verticalBias = -0.05f;

            LineInfoUI forceLineInfo = forceUI.GetComponent<LineInfoUI>();
            forceLineInfo.startPoint = p.transform;
            forceLineInfo.endPoint = neighbour.transform;
            // forceLineInfo.verticalBias = 0.05f;

            // HoloUI distanceHoloUI = distanceUI.GetComponent<HoloUI>();
            // if (p.GetComponent<HoloTransmitter>()) distanceHoloUI.holodesk = p.GetComponent<HoloTransmitter>().Holodesk;

            // HoloUI forceHoloUI = forceUI.GetComponent<HoloUI>();
            // if (p.GetComponent<HoloTransmitter>()) forceHoloUI.holodesk = p.GetComponent<HoloTransmitter>().Holodesk;
        }
    }

    // "yield return null" is here to give unity time to initialize the marker and calculate its way
    private IEnumerator DrawLineCoroutine(Planet p, Planet neighbour)
    {
        // create marker to draw a line
        Vector3 planetPosition = p.transform.position;
        GameObject marker = Instantiate(markerPrefab, planetPosition, Quaternion.identity);
        MarkerController markerController = marker.GetComponent<MarkerController>();
        foreach (Transform m in marker.transform)
        {
            if (p.GetComponent<HoloTransmitter>())
                m.GetComponent<HoloMarker>().holodesk = p.GetComponent<HoloTransmitter>().Holodesk;
        }
        // markerController.snap = true;

        yield return null;

        // Draw a line
        p.transform.position = planetPosition;
        markerController.StartDraw();

        GameObject line = markerController.activeMarker.GetComponent<HoloMarker>().trailTransform.gameObject;
        lines.Add(line);
        line.GetComponent<Trail>().erasable = false;
        line.GetComponent<Trail>().snapA = p.GetComponent<SnapPoint>();
        marker.transform.position = neighbour.transform.position;
        yield return null;
        line.GetComponent<Trail>().snapB = neighbour.GetComponent<SnapPoint>();

        // Finish drawing
        print("Destroy2");
        markerController.StopDraw();
        print("Destroy2");
        Destroy(marker);
    }
}
