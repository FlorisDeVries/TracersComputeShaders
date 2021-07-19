﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.RayTracer
{
    public struct Sphere
    {
        public Vector3 Pos;
        public float Radius;
        public Vector3 Albedo;
        public float Specular;

        public Sphere(Vector3 pos, float radius, Vector3 col, float specular)
        {
            Pos = pos;
            Radius = radius;
            Albedo = col;
            Specular = specular;
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

        public static Sphere[] GenerateSphereArray(int count)
        {
            var dimension = (int)Math.Ceiling(Mathf.Sqrt(count));
            var spheres = new Sphere[count];

            for (var x = 0; x < dimension; x++)
            {
                for (var y = 0; y < dimension; y++)
                {
                    if (x * dimension + y >= count)
                        return spheres;

                    var randomSphere = RandomSphere(1, new Vector2(1,1));
                    randomSphere.Pos = new Vector3(x * 3, 1, y * 3);
                    spheres[x * dimension + y] = randomSphere;
                }
            }

            return spheres;
        }

        public static Sphere[] GenerateSphereCircle(int count, float radius, Vector2 sizeRange)
        {
            var spheres = new Sphere[count];

            for (var i = 0; i < count; i++)
            {
                var randomSphere = RandomSphere(radius, sizeRange);
                var randomPos = Random.insideUnitCircle * radius;
                randomSphere.Pos = new Vector3(randomPos.x, 1, randomPos.y);

                var retries = 100;
                while (randomSphere.IntersectsOtherSphere(spheres))
                {
                    randomSphere = RandomSphere(radius, sizeRange);
                    randomPos = Random.insideUnitCircle * radius;
                    randomSphere.Pos = new Vector3(randomPos.x, 1, randomPos.y);

                    retries--;
                    if (retries >= 1) continue;

                    Debug.LogWarning("The added sphere intersects another sphere.");
                    break;
                }

                spheres[i] = randomSphere;
            }

            return spheres;
        }

        private static bool IntersectsOtherSphere(this Sphere sphere, Sphere[] otherSpheres)
        {
            return otherSpheres.Any(otherSphere => Vector3.Distance(otherSphere.Pos, sphere.Pos) < otherSphere.Radius + sphere.Radius);
        }

        private static Sphere RandomSphere(float posRange, Vector2 sizeRange)
        {
            var sphere = new Sphere();
            sphere.Pos = RandomVec3(-posRange, posRange);
            sphere.Radius = Random.Range(sizeRange.x, sizeRange.y);

            var col = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            sphere.Albedo = new Vector3(col.r, col.g, col.b);

            sphere.Specular = Random.Range(0, .8f);
            if (sphere.Specular < .1f)
            {
                sphere.Specular = 0;
            }

            return sphere;
        }

        private static Vector3 RandomVec3(float min, float max)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max) / 10, Random.Range(min, max));
        }
    }
}