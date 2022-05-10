using UnityEngine;

namespace Assets.Scripts.Primitives.Compositions
{
    [CreateAssetMenu(fileName = "Plane", menuName = "ScriptableObjects/Compositions/Plane")]
    public class PlaneSO : APrimitiveComposition<Plane>
    {
        [Header("Properties")] 
        [SerializeField]
        private Vector3 Position = new Vector3(0,0,0);

        [SerializeField]
        private Vector3 Normal = new Vector3(0,1,0);
        
        protected override bool CheckRemake()
        {
            return primitives[0].Position != Position || primitives[0].Normal != Normal;
        }

        protected override void CreatePrimitives()
        {
            var groundPlane = Plane.GetPlane(Position, Normal);

            primitives = new Plane[1];
            primitives[0] = groundPlane;
        }
    }
}
