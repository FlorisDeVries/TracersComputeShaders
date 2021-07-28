using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Primitives.Compositions
{
    [CreateAssetMenu(fileName = "SphereCircle", menuName = "ScriptableObjects/Compositions/SphereCircle")]
    public class SphereCircleSO : ASphereComposition
    {
        [Header("Properties")]
        [Range(1, 150)]
        public int SphereCount = 25;
        [Range(10, 100)]
        public float SphereRadius = 10;
        public Vector2 SphereSize = new Vector2(.1f, 2f);

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
            spheres = new Sphere[SphereCount];

            for (var i = 0; i < SphereCount; i++)
            {
                Sphere randomSphere;

                var retries = 100;
                do
                {
                    randomSphere = Sphere.RandomSphere(SphereRadius, SphereSize);
                    var randomPos = Random.insideUnitCircle * SphereRadius;
                    randomSphere.Pos = new Vector3(randomPos.x, 1, randomPos.y);

                    retries--;
                    if (retries >= 1) continue;

                    Debug.LogWarning("The added sphere intersects another sphere.");
                    break;
                    
                } while (Sphere.IntersectsOtherSphere(randomSphere, spheres));

                spheres[i] = randomSphere;
            }
        }
    }
}
