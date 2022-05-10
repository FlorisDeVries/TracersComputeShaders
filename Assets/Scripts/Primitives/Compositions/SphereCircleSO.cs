using UnityEngine;

namespace Assets.Scripts.Primitives.Compositions
{
    [CreateAssetMenu(fileName = "SphereCircle", menuName = "ScriptableObjects/Compositions/SphereCircle")]
    public class SphereCircleSO : APrimitiveComposition<Sphere>
    {
        [Header("Properties")]
        [Range(1, 150)]
        [SerializeField] private int SphereCount = 25;
        [Range(10, 100)]
        [SerializeField] private float SphereRadius = 10;
        [SerializeField] private Vector2 SphereSize = new Vector2(.1f, 2f);

        protected override bool CheckRemake()
        {
            return primitives.Length != SphereCount;
        }

        protected override void CreatePrimitives()
        {
            primitives = new Sphere[SphereCount];

            for (var i = 0; i < SphereCount; i++)
            {
                Sphere randomSphere;

                var retries = 100;
                do
                {
                    randomSphere = Sphere.RandomSphere(SphereRadius, SphereSize);
                    var randomPos = Random.insideUnitCircle * SphereRadius;
                    randomSphere.Position = new Vector3(randomPos.x, 1, randomPos.y);

                    retries--;
                    if (retries >= 1) continue;

                    Debug.LogWarning("The added sphere intersects another sphere.");
                    break;
                    
                } while (Sphere.IntersectsOtherSphere(randomSphere, primitives));

                primitives[i] = randomSphere;
            }
        }
    }
}
