using System;
using UnityEngine;

namespace Assets.Scripts.Primitives.Compositions
{
    [CreateAssetMenu(fileName = "SphereArray", menuName = "ScriptableObjects/Compositions/SphereArray")]
    public class SphereArraySO : ASphereComposition
    {
        [Header("Properties")]
        [Range(1, 150)]
        public int SphereCount = 25;

        public override Sphere[] GetSpheres()
        {
            if (spheres == null || spheres.Length != SphereCount)
            {
                CreateSpheres();
            }

            return spheres;
        }

        private void CreateSpheres()
        {
            var dimension = (int)Math.Ceiling(Mathf.Sqrt(SphereCount));
            spheres = new Sphere[SphereCount];

            for (var x = 0; x < dimension; x++)
            {
                for (var y = 0; y < dimension; y++)
                {
                    if (x * dimension + y >= SphereCount)
                        return;

                    var randomSphere = Sphere.RandomSphere(1, new Vector2(1, 1));
                    randomSphere.Pos = new Vector3(x * 3, 1, y * 3);
                    spheres[x * dimension + y] = randomSphere;
                }
            }
        }
    }
}
