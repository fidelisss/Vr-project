using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Molecules.New
{
    public class MoleculeSegment : MonoBehaviour
    {
        public float Duration { get; set; }
        public float Strength { get; set; }
        public int Vibrato { get; set; }

        public Vector3 Default { get; set; }

        private void Awake()
        {
            Default = transform.localPosition;
        }

        public void ForceCenterView() => transform.position = Default;

        public void StartShake()
        {
            transform.LoopShake(Random.Range(Duration / 2, Duration), Random.Range(Strength / 2, Strength),
                Random.Range(Vibrato / 2, Vibrato));
        }
    }
}