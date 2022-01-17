using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmergedBehaviour : Item
{
    // closest liquid (should be calculated based on distance)
    public LiquidBehaviour closestLiquid;

    Material material;
    Renderer rend;
    Material closestLiquidMaterial;

    [SerializeField] private float _heatCapacity;
    public float HeatCapacity
    {
        get => _heatCapacity;
        set => _heatCapacity = value;
    }

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        rend = GetComponent<Renderer>();
        material = rend.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (closestLiquid)
        {
            material.SetInt("_IsSubmerged", 1);

            InheritMaterialParameters();
            // closestLiquid.ChangeWaterLevel(volume, CalcSubmerged());
        } 
        else
            material.SetInt("_IsSubmerged", 0); 
    }


    // inherit material parameters from liquid for better submerge faking
    void InheritMaterialParameters()
    {
        material.SetFloat("_Fill", closestLiquidMaterial.GetFloat("_Fill"));
        material.SetFloat("_ObjectHeight", closestLiquidMaterial.GetFloat("_ObjectHeight"));
        material.SetVector("_ObjectCenter", closestLiquidMaterial.GetVector("_ObjectCenter"));
        material.SetVector("_Wobble", closestLiquidMaterial.GetVector("_Wobble"));
        material.SetColor("_LiquidColor", closestLiquidMaterial.GetColor("_FresnelColor"));
    }


    // calculate how much submerged the object is
    public float CalcSubmerged()
    {
        float height = rend.bounds.size.y;
        float waterLevel = closestLiquid.WaterLevel;
        float bottom = rend.bounds.min.y;

        float submerged = Mathf.Clamp((waterLevel - bottom) / height, 0, 1);
        
        return submerged;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "LiquidZone")
        {
            closestLiquid = col.transform.parent.GetComponent<LiquidBehaviour>();
            closestLiquidMaterial = closestLiquid.GetComponent<Renderer>().material;

            if (!closestLiquid.addedItems.Contains(this))
                closestLiquid.addedItems.Add(this);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "LiquidZone")
        {
            closestLiquid.addedItems.Remove(this);
            closestLiquid = null;
        }
    }

    void OnDestroy()
    {
        if (closestLiquid)
        {
            closestLiquid.addedItems.Remove(this);
        }
    }
}
