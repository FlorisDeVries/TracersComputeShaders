using System;
using UnityEngine;

namespace Assets.Scripts.Primitives.Compositions
{
    [CreateAssetMenu(fileName = "SphereArray", menuName = "ScriptableObjects/Compositions/SphereArray")]
    public class SphereArraySO : APrimitiveComposition<Sphere>
    {
        [Header("Properties")]
        [Range(1, 150)]
        [SerializeField] private int SphereCount = 25;
        
        protected override bool CheckRemake()
        {
            return primitives.Length != SphereCount;
        }

        protected override void CreatePrimitives()
        {
            var dimension = (int)Math.Ceiling(Mathf.Sqrt(SphereCount));
            primitives = new Sphere[SphereCount];

            for (var x = 0; x < dimension; x++)
            {
                for (var y = 0; y < dimension; y++)
                {
                    if (x * dimension + y >= SphereCount)
                        return;

                    var randomSphere = Sphere.RandomSphere(1, new Vector2(1, 1));
                    randomSphere.Position = new Vector3(x * 3, 1, y * 3);
                    primitives[x * dimension + y] = randomSphere;
                }
            }
        }
    }
}
