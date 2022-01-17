using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LiquidBehaviour : Item
{
    public float fill = 0.5f;

    public List<SubmergedBehaviour> addedItems = new List<SubmergedBehaviour>();
    [SerializeField] private TempUI _tempUI;

    Material material;
    Collider liquidCollider;
    Renderer liquidRenderer;

    public Transform pendulum;
    private float currentMassOfWater;
    public float CurrentMassOfWater => currentMassOfWater;

    Vector3 objectCenter;

    float waterLevel;
    public float WaterLevel
    {
        get { return waterLevel; }
        // set { addedFill = value; }
    }

    float addedWaterLevel;
    public float AddedWaterLevel
    {
        get { return addedWaterLevel; }
        // set { addedFill = value; }
    }

    float objectHeight;
    public float ObjectHeight
    {
        get { return objectHeight; }
        // set { objectHeight = value; }
    }
    
    void Start()
    {
        liquidRenderer = GetComponent<Renderer>();
        material = liquidRenderer.material;
        liquidCollider = GetComponent<Collider>();

        // initiate center
        material.SetVector("_ObjectCenter", liquidRenderer.bounds.center);

        InitiateShader();
    }

    void FixedUpdate()
    {
        CalcWobble();
        CalcCenter();
        CalcHeight();
        CalcWaterLevel();
        CalcFill();
    }


    void InitiateShader()
    {
        // calculate the height of the object to fill percentally
        CalcHeight();

        // initiate center
        CalcCenter();

        // disable preview mode
        material.SetInt("_Preview", 0);
    }


    // calculate and set wobble according to pendulum rotation
    void CalcWobble()
    {
        Vector3 rotation = FixEuler(pendulum.rotation.eulerAngles);

        Vector3 wobbleAdd = -1 * rotation / 90 * Mathf.Sin((fill + CalcFullAddedFill())*Mathf.PI);


        material.SetVector("_Wobble", wobbleAdd);
    }


    // fix eulerAngels and remap them to [-90:90]
    public Vector3 FixEuler(Vector3 rotation)
    {
        // extend if needed
        if (rotation.x >= 180)
            rotation.x -= 360;
        if (rotation.z >= 180)
            rotation.z -= 360;

        float cosY = Mathf.Cos(rotation.y * Mathf.Deg2Rad);
        float sinY = Mathf.Sin(rotation.y * Mathf.Deg2Rad);

        // compensate the influence of Y rotation
        Vector3 fixedRotation = new Vector3(cosY*rotation.x + sinY*rotation.z, rotation.y, sinY*rotation.x + cosY*rotation.z);

        return fixedRotation;
    }


    // calculate water level of the liquid to adjust the volume
    void CalcWaterLevel()
    {
        float currentHeight = liquidRenderer.bounds.size.y;
        waterLevel = objectCenter.y + currentHeight*(fill + CalcFullAddedFill() -0.5f);
        _tempUI.DecreaseTempAfterAddFill();
        // addedWaterLevel = objectCenter.y + currentHeight*(fill+addedFill-0.5f) - waterLevel;
    }


    // calculate and set the center of the object for objects with pivot offset
    void CalcCenter()
    {
        objectCenter = liquidRenderer.bounds.center;
        material.SetVector("_ObjectCenter", objectCenter);
    }


    // calculate the current height of the object (according to transforms) for precise filling
    void CalcHeight()
    {
        objectHeight = liquidCollider.bounds.size.y;
        material.SetFloat("_ObjectHeight", objectHeight);
    }


    // set the right ammount of liquid
    void CalcFill()
    {
        // float addedFill = Mathf.Clamp(CalcFullAddedFill(), 0, 0.99f);
        float addedFill = CalcFullAddedFill();
        _tempUI.CalculateEnergy();
        _tempUI.TempBalance();
        fill = Mathf.Clamp(fill, 0, 0.99f - addedFill);

        material.SetFloat("_Fill", fill+addedFill);

        if (fill <= 0.005f)
        {
            liquidRenderer.enabled = false;
        }
        else
            liquidRenderer.enabled = true;
    }


    public void AddVolume(float addedVolume)
    {
        currentMassOfWater = fill * volume;
        fill += addedVolume/volume;
    }


    public float CalcFullAddedFill()
    {
        float fullAddedVol = 0f;

        foreach (SubmergedBehaviour addedItem in addedItems)
        {
            fullAddedVol += addedItem.volume * addedItem.CalcSubmerged();
            
            print(fullAddedVol);
        }

        float fullAddedFill = Mathf.Clamp(fullAddedVol / volume, 0, 0.99f);
        return fullAddedFill;
    }


    public override float CalcMass()
    {
        // float submergedMass = addedItems.Sum(item => item.mass);
         
        return mass * fill; // + submergedMass; 

        // submerged mass is working only when objects are floating (which is still not the case)
    }
}
