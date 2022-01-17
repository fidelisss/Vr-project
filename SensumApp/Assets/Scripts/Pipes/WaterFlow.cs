using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Make shure that WaterFlow object is a child of PipeScale object

public class WaterFlow : MonoBehaviour
{
    [Range(0, 16)]
    public float Speed = 1;
    [Range(0, 16)]
    public float SpeedMultiplier = 1;

    private float _time = 0;
    private Material _material;
    private float _pipeScale = 1;
    private float Pressure, Flow, RoundedFlow, pi=3.14f, d,RoundedSpeed;
    
    [SerializeField] private Text FlowTxt;
    [SerializeField] private Text SpeedTxt;
    private float _initialArrowWidth;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        // _pipeScale = transform.parent.GetComponent<PipeScale>();
        _initialArrowWidth = _material.GetFloat("_ArrowWidth");
    }

    private void FixedUpdate()
    {
        d = _pipeScale * 100;
        
        _material.SetFloat(MaterialController.Parameter._CustomTime, _time);
        _material.SetFloat(MaterialController.Parameter._ArrowWidth, _initialArrowWidth / (Mathf.Pow(_pipeScale, 0.5f)));

        Speed = 1 / Mathf.Pow(_pipeScale, 2);
        RoundedSpeed = (float)(Mathf.Round(Speed * 100) / 100.0);
        if (SpeedTxt) { SpeedTxt.text = RoundedSpeed.ToString() + " M/s"; }

        Flow = pi * ((d * d) / 4) * (Speed / 1000);
        RoundedFlow = (float)(Mathf.Round(Flow * 100) / 100.0);
        if (FlowTxt) { FlowTxt.text = RoundedFlow.ToString() + " L/s"; }

        //Pressure = (200 / d) * ((Speed * Speed) / 2);
        ////RoundedPressure = (float)(Mathf.Round(Pressure * 100) / 100.0);
        //PressureTxt.text = Pressure.ToString() + " Pa";

        _time += Time.fixedDeltaTime * Speed * SpeedMultiplier;
        
    }

    public void SetPipeScale(float pipeScale)
    {
        _pipeScale = pipeScale;
    }
    public float GetD()
    {
        return d;
    }
    public float GetPressure()
    {
        return Pressure;
    }
}