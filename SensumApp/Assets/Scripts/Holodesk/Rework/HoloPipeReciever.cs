using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloPipeReciever : HoloReciever
{
    // Please fill the array in the right order
    [SerializeField] private Transform[] _sliders;
    private Slider3dContraint[] _originalSliders;
    // Store parents for better calculation of localPosition (XR Grab is always messing with original parents)
    private List<Transform> _orginalSlidersParents = new List<Transform>();

    protected override void Start()
    {
        base.Start();
        _originalSliders = Transmitter.GetComponentsInChildren<Slider3dContraint>();
        
        foreach (Slider3dContraint slider in _originalSliders)
        {
            _orginalSlidersParents.Add(slider.transform.parent);
        }
    } 

    protected override void ApplyCustomData()
    {
        int i = 0;
        foreach (Slider3dContraint slider in _originalSliders)
        {
            // InverseTransformPoint will help in calculating real localPosition (XR Grab is always messing with original parents)
            _sliders[i].localPosition = _orginalSlidersParents[i].InverseTransformPoint(slider.transform.position);
            i++;
        }
    }
}
