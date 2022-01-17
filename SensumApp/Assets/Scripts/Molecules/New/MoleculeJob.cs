using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
namespace Molecules.New
{
    public class MoleculeJob : IJobParallelForTransform
    {
        public NativeArray<Vector3> Positions;
        public NativeArray<Vector3> Velocities;

        public void Execute(int index, TransformAccess transform)
        {
            var velocity = Velocities[index];
            transform.position = Positions[index];
            transform.rotation = Quaternion.LookRotation(velocity);
        }

        private void Foo()
        {
        }
    }
}
