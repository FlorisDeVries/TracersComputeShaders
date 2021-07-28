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
        [Range(1, 150)]
        public int SphereCount = 25;
        [Range(10, 100)]
        public float SphereRange = 10;
        public Vector2 SphereSize = new Vector2(.1f, 2f);


        [Header("Lights")]
        public Light DirectionalLight = default;
    }
}
