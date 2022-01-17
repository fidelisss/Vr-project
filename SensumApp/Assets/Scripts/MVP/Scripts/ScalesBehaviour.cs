using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScalesBehaviour : MonoBehaviour
{
    public List<string> ignoreTags = new List<string> {
        "Liquid"
    };
    
    // keys = GameObjects, values = is stationary
    public Hashtable weights = new Hashtable();

    public float claculatedMass = 0f;
    float smoothedMass = 0f;

    public Text screenText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckVelocities();
        claculatedMass = AddMasses();
        smoothedMass += claculatedMass - Mathf.Lerp(smoothedMass, claculatedMass, 0.8f);
        screenText.text = smoothedMass.ToString("F3");
    }


    // Only stationary objects are calculated on the weights
    void CheckVelocities()
    {
        List<GameObject> weightsKeys = new List<GameObject>(weights.Keys.Cast<GameObject>().ToList());

        foreach (GameObject key in weightsKeys)
        {
            if (key.GetComponent<Rigidbody>().velocity.magnitude < 0.0005f)
                weights[key] = true;
            else
                weights[key] = false;
        }
    }


    float AddMasses()
    {
        List<GameObject> weightsKeys = new List<GameObject>(weights.Keys.Cast<GameObject>().ToList());

        float mass = 0;

        foreach (GameObject key in weightsKeys)
        {
            if ((bool)weights[key])
                mass += key.GetComponent<Item>().CalcMass();
        }

        return mass;
    }
    


    void OnTriggerEnter(Collider col)
    {
        if (!ignoreTags.Contains(col.gameObject.tag))
            if (col.gameObject.GetComponent<Item>())
                if (weights[col.gameObject] == null)
                    weights.Add(col.gameObject, false);
    }


    void OnTriggerExit(Collider col)
    {
        if (!ignoreTags.Contains(col.gameObject.tag))
            if (col.gameObject.GetComponent<Item>())
                weights.Remove(col.gameObject);
    }
}
