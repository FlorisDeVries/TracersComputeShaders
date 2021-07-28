using UnityEngine;

namespace Assets.Scripts.Primitives.Compositions
{
    public abstract class ASphereComposition : ScriptableObject, ISphereComposition
    {
        protected Sphere[] spheres = default;

        public abstract Sphere[] GetSpheres();

        public virtual int GetSphereCount()
        {
            return spheres.Length;
        }
    }

    public interface ISphereComposition
    {
        Sphere[] GetSpheres();
        int GetSphereCount();
    }
}
