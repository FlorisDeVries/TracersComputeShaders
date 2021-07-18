using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static Sphere[] GenerateRandomSphere(int count)
    {
        Sphere[] spheres = new Sphere[count];
        for (int i = 0; i < count; i++)
        {
            spheres[i] = RandomSphere();
        }

        return spheres;
    }

    private static Sphere RandomSphere()
    {
        Sphere sphere = new Sphere();
        sphere.pos = RandomVec3();
        sphere.radius = Random.Range(.1f, 2f);
        sphere.col = RandomVec3();
        return sphere;
    }

    private static Vector3 RandomVec3(float min = -10, float max = 10)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }
}