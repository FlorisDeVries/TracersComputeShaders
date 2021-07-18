using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.RayTracer
{
    public struct Sphere
    {
        public Vector3 pos;
        public float radius;
        public Vector3 col;

        public Sphere(Vector3 pos, float radius, Vector3 col){
            this.pos = pos;
            this.radius = radius;
            this.col = col;
        }
    }
    public static class Spheres
    {
        public static Sphere[] GenerateRandomSphere(int count, float posRange, Vector2 sizeRange)
        {
            var spheres = new Sphere[count];
            for (var i = 0; i < count; i++)
            {
                spheres[i] = RandomSphere(posRange, sizeRange);
            }

            return spheres;
        }

        public static Sphere[] GenerateSphereArray(int dimensionX)
        {
            var spheres = new Sphere[dimensionX * dimensionX];
            for (var x = 0; x < dimensionX; x++)
            {
                for (var y = 0; y < dimensionX; y++)
                {
                    spheres[x * dimensionX + y] = new Sphere(new Vector3(x * 3, 1, y * 3), 1, RandomVec3(0, 1));
                }
            }

            return spheres;
        }

        private static Sphere RandomSphere(float posRange, Vector2 sizeRange)
        {
            Sphere sphere = new Sphere();
            sphere.pos = RandomVec3(-posRange, posRange);
            sphere.radius = Random.Range(sizeRange.x, sizeRange.y);
            sphere.col = RandomVec3(0, 1);
            return sphere;
        }

        private static Vector3 RandomVec3(float min, float max)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max) / 10, Random.Range(min, max));
        }
    }
}