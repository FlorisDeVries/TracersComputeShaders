using UnityEngine;

namespace Assets.Scripts.Primitives
{
    public struct Plane
    {
        public Vector3 Normal;
        public Vector3 Position;

        public Vector3 Albedo;
        public Vector3 Emission;
        public float Specular;
        public float Smoothness;

        public static int NumberOfFloats => 14;

        public static Plane GetPlane(Vector3 pos, Vector3 normal)
        {
            var randomPlane = new Plane
            {
                Normal = normal,
                Position = pos,
                Albedo = new Vector3(.1f, .1f, .1f),
                Specular = 0.3f,
                Smoothness = 1.0f,
                Emission = new Vector3(0.0f, 0.0f, 0.0f)
            };

            return randomPlane;
        }
    }
}
