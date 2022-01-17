using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloEngineReciever : HoloReciever
{
    // private PlaybackTimeController[] _originalControllers;
    private Animator[] _originalAnimators;
    private Animator[] _animators;

    protected override void Start()
    {
        base.Start();
        // _originalControllers = Transmitter.GetComponentsInChildren<PlaybackTimeController>();
        _originalAnimators = Transmitter.GetComponentsInChildren<Animator>();
        _animators = GetComponentsInChildren<Animator>();
    } 

    protected override void ApplyCustomData()
    {
        int i = 0;
        // foreach (PlaybackTimeController controller in _originalControllers)
        // {
        //     _animators[i].SetFloat("PlaybackTime", controller.CurrentTime);
        //     i++;
        // }

        foreach (Animator animator in _originalAnimators)
        {
            _animators[i].SetFloat("PlaybackTime", animator.GetFloat("PlaybackTime"));
            i++;
        }
    }
}
