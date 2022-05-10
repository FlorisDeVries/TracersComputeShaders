using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Primitives
{
    public struct Sphere
    {
        public Vector3 Position;
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
                Position = RandomVec3(-posRange, posRange),
                Radius = Random.Range(sizeRange.x, sizeRange.y)
            };

            var col = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            randomSphere.Albedo = new Vector3(col.r, col.g, col.b);

            randomSphere.Specular = Random.Range(0, .8f);
            if (randomSphere.Specular < .1f)
                randomSphere.Specular = 0;

            randomSphere.Smoothness = Random.Range(0, 1f);

            randomSphere.Emission = Vector3.zero;
            if (Random.Range(0, 1f) > .8f)
                randomSphere.Emission = RandomVec3(1f, 10f);
            
            var refractionIndex = RandomRefractionIndex();
            if (refractionIndex != 0)
            {
                randomSphere.RefractionIndex = refractionIndex;
                randomSphere.Specular = 1f;
                randomSphere.Smoothness = 1f;
                randomSphere.Emission = Vector3.zero;
            }

            return randomSphere;
        }

        public static Vector3 RandomVec3(float min, float max)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max) / 10, Random.Range(min, max));
        }

        public static bool IntersectsOtherSphere(Sphere sphere, Sphere[] otherSpheres)
        {
            return otherSpheres.Any(otherSphere => Vector3.Distance(otherSphere.Position, sphere.Position) < otherSphere.Radius + sphere.Radius);
        }

        public static float RandomRefractionIndex()
        {
            var indices = new List<float>();
            indices.Add(1.33f); // Glass
            indices.Add(1f); // Air, on air will create a filter effect
            indices.Add(1.52f); // Window Glass
            indices.Add(2.42f); // Diamond
            indices.Add(0f); // Disabled
            indices.Add(0f); // Disabled
            indices.Add(0f); // Disabled
            indices.Add(0f); // Disabled
            indices.Add(0f); // Disabled


            return indices[Random.Range(0, indices.Count)];
        }
    }
}