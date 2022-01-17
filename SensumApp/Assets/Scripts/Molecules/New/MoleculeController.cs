using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Molecules.New
{
    public sealed class MoleculeController : MonoBehaviour
    {
        [SerializeField] private float _durationOfMoleculesShake;
        [SerializeField] private float _strengthOfMoleculesShake;
        [SerializeField] private int _vibratoOfMoleculesShake;

        [SerializeField] private float _temperature;
        
        public float DurationOfMoleculesShake
        {
            get => _durationOfMoleculesShake;
            set
            {
                _durationOfMoleculesShake = value;
                ChangeParamsOfMolecules(ref _durationOfMoleculesShake, ref _strengthOfMoleculesShake,
                    ref _vibratoOfMoleculesShake);
            }
        }

        public float StrengthOfMoleculesShake
        {
            get => _strengthOfMoleculesShake;
            set
            {
                _strengthOfMoleculesShake = value;
                ChangeParamsOfMolecules(ref _durationOfMoleculesShake, ref _strengthOfMoleculesShake,
                    ref _vibratoOfMoleculesShake);
            }
        }
        
        public int VibratoOfMoleculesShake
        {
            get => _vibratoOfMoleculesShake;
            set
            {
                _vibratoOfMoleculesShake = value;
                ChangeParamsOfMolecules(ref _durationOfMoleculesShake, ref _strengthOfMoleculesShake,
                    ref _vibratoOfMoleculesShake);
            }
        }
        
        public float Temperature => _temperature;

        public MoleculeSegment[] Molecules { get; private set; }

        private float _defaultStrength;
        private Coroutine _coroutine;
        private bool _isFire;

        
        private void Awake()
        {
            InitMolecules();
            _defaultStrength = _strengthOfMoleculesShake;
            _isFire = false;
        }

        private void Start() => ChangeParamsOfMolecules(ref _durationOfMoleculesShake, ref _strengthOfMoleculesShake,
            ref _vibratoOfMoleculesShake);

        private void Update()
        {
            /*if (!_isFire && Math.Abs(_strengthOfMoleculesShake - _defaultStrength) > 0.01f)
            {
                _strengthOfMoleculesShake = _defaultStrength;
            }*/
        }
        
        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out NewFire fire)) 
                StrengthOfMoleculesShake = 0.6f;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out NewFire fire)) return;
            
            StrengthOfMoleculesShake = _defaultStrength;
            foreach (var moleculeSegment in Molecules) 
                moleculeSegment.ForceCenterView();
        }

        public void InitMolecules() => Molecules = GetComponentsInChildren<MoleculeSegment>();

        public void ChangeParamsOfMolecules(ref float duration, ref float strength, ref int vibrato)
        {
            foreach (var shaker in Molecules)
            {
                shaker.Duration = duration;
                shaker.Strength = strength;
                shaker.Vibrato = vibrato;
                shaker.StartShake();
            }
        }

        private IEnumerator HotMoleculeStrength(float temperature)
        {
            const float cooldown = 3f;
            yield return new WaitForSeconds(cooldown);
            StrengthOfMoleculesShake += temperature;
        }
        
        private IEnumerator ColdMoleculeStrength(float temperature)
        {
            const float cooldown = 3f;
            yield return new WaitForSeconds(cooldown);
            StrengthOfMoleculesShake -= temperature;
        }

        private float GetAmplitude(float temperature) => Mathf.Pow(1.03f, temperature - 273) * 0.1f;
    }
}