using Assets.Scripts.Primitives;
using Assets.Scripts.Primitives.Compositions;
using UnityEngine;
using Plane = Assets.Scripts.Primitives.Plane;

namespace Assets.Scripts.Scenes
{
    [CreateAssetMenu(fileName = "Scene", menuName = "ScriptableObjects/Scene")]
    public class SceneSO : ScriptableObject
    {
        [Header("General")]
        public Texture SkyboxTexture = default;
        public int Seed = 0;

        [Header("Primitives")]
        public APrimitiveComposition<Sphere> SphereComposition = null;
        public APrimitiveComposition<Plane> PlaneComposition = null;

        [Header("Lights")]
        public Light DirectionalLight = default;

        #region Properties
        public Sphere[] Spheres => SphereComposition != null ? SphereComposition.GetPrimitives() : new Sphere[0];
        public int SphereCount => SphereComposition.GetPrimitiveCount();

        public Plane[] Planes => PlaneComposition != null ? PlaneComposition.GetPrimitives() : new Plane[0];
        public int PlaneCount => PlaneComposition.GetPrimitiveCount();

        #endregion
    }
}
