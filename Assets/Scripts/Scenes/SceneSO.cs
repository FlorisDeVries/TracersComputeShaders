using Assets.Scripts.Primitives;
using Assets.Scripts.Primitives.Compositions;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    [CreateAssetMenu(fileName = "Scene", menuName = "ScriptableObjects/Scene")]
    public class SceneSO : ScriptableObject
    {
        [Header("General")]
        public Texture SkyboxTexture = default;
        public int Seed = 0;

        [Header("Primitives")]
        public ASphereComposition SphereComposition = null;

        [Header("Lights")]
        public Light DirectionalLight = default;

        #region Properties
        public Sphere[] Spheres => SphereComposition != null ? SphereComposition.GetSpheres() : new Sphere[0];

        public int SphereCount => SphereComposition.GetSphereCount();

        #endregion
    }
}
