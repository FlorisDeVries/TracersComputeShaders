using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Primitives
{
    public struct Sphere
    {
        public Vector3 Pos;
        public float Radius;
        public Vector3 Albedo;
        public float Specular;
        public float Smoothness;
        public Vector3 Emission;
        public float RefractionIndex;

        public static int NumberOfFloats => 13;

        public static Sphere RandomSphere(float posRange, Vector2 sizeRange)
        {
            var randomSphere = new Sphere
            {
                Pos = RandomVec3(-posRange, posRange),
                Radius = Random.Range(sizeRange.x, sizeRange.y)
            };

            var col = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            randomSphere.Albedo = new Vector3(col.r, col.g, col.b);

            randomSphere.Specular = Random.Range(0, .8f);
            if (randomSphere.Specular < .1f)
            {
                randomSphere.Specular = 0;
            }

            randomSphere.Smoothness = Random.Range(0, 1f);

            if (Random.Range(0, 1f) > .8f)
            {
                randomSphere.Emission = RandomVec3(1f, 10f);
            }

            randomSphere.RefractionIndex = 0f;

            return randomSphere;
        }

        public static Vector3 RandomVec3(float min, float max)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max) / 10, Random.Range(min, max));
        }

        public static bool IntersectsOtherSphere(Sphere sphere, Sphere[] otherSpheres)
        {
            return otherSpheres.Any(otherSphere => Vector3.Distance(otherSphere.Pos, sphere.Pos) < otherSphere.Radius + sphere.Radius);
        }
    }
}