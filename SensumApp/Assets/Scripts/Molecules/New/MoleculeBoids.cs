using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Molecules.New
{
    [RequireComponent(typeof(MoleculeController)), SelectionBase]
    public class MoleculeBoids : MonoBehaviour
    {
        [SerializeField] private int _numberOfMolecule;

        private NativeArray<Vector3> _positions;
        private NativeArray<Vector3> _velocities;

        private TransformAccessArray _transformAccess;
        private MoleculeController _controller;
        
        private void Awake() => _controller = GetComponent<MoleculeController>();
        
        private void Start()
        {
            _numberOfMolecule = _controller.Molecules.Length;
            
            _positions = new NativeArray<Vector3>(_numberOfMolecule, Allocator.Persistent);
            _velocities = new NativeArray<Vector3>(_numberOfMolecule, Allocator.Persistent);

            var transforms = new Transform[_numberOfMolecule];

            _transformAccess = new TransformAccessArray(transforms);
            _controller.InitMolecules();
        }

        private void Update()
        {
            var moveJob = new MoleculeJob()
            {
                Positions = _positions,
                Velocities = _velocities,
            };
        }
    }
}