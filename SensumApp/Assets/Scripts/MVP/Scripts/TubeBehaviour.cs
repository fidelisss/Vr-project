using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TubeBehaviour : Item
{

    public LiquidBehaviour liquid;
    public GameObject liquidStream;

    public float innerRadius = 1f;
    public float outerRadius = 1f;

    public Text volumeText;
    public Transform uiParent;
    public Transform canvasPivot;
    public LineRenderer lineRenderer;

    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if (liquid.fill > 0)
        {
            if (WillPour())
            {
                // liquidStream.GetComponent<LiquidStream>().tilt = 1 - (transform.up.y/liquid.fill);

                if (!liquidStream.activeSelf)
                    liquidStream.SetActive(true);

                liquid.AddVolume(-liquidStream.GetComponent<LiquidStream>().bandwidth*Time.deltaTime);
            }
            else
            {
                liquidStream.SetActive(false);
            }
        }
        else
            liquidStream.SetActive(false);
        

        UpdateUI();
    }

    bool WillPour()
    {
        return (liquid.WaterLevel > liquidStream.GetComponent<LiquidStream>().FindEdge(innerRadius).y);
    }


    public override float CalcMass()
    {
        return mass + liquid.CalcMass();
    }    
    

    void UpdateUI()
    {
        if (liquid.fill <= 0)
        {
            uiParent.gameObject.SetActive(false);
            return;
        }
        else
        {
            uiParent.gameObject.SetActive(true);
        }
            

        uiParent.position = transform.position + Camera.main.transform.right * 0.16f;
        uiParent.LookAt(Camera.main.transform);
        uiParent.eulerAngles = new Vector3(0, uiParent.eulerAngles.y - 180, 0);
        uiParent.position = transform.position;

        canvasPivot.LookAt(Camera.main.transform);
        canvasPivot.eulerAngles = new Vector3(-canvasPivot.eulerAngles.x, uiParent.eulerAngles.y, 0);

        Vector3[] positions = new Vector3[3];
        positions[0] = new Vector3(lineRenderer.transform.position.x, liquid.WaterLevel, lineRenderer.transform.position.z);
        positions[1] = positions[0] + lineRenderer.transform.right*0.02f;
        positions[2] = canvasPivot.position;
        lineRenderer.SetPositions(positions);

        string text = ((liquid.fill + liquid.CalcFullAddedFill()) * liquid.volume).ToString("F3");
        volumeText.text = text.Replace(',', '.');
    }
}
