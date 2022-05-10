using UnityEngine;

namespace Assets.Scripts.Primitives.Compositions
{

    public abstract class APrimitiveComposition<T> : ScriptableObject, IPrimitiveComposition<T>
    {
        protected T[] primitives = default;

        public virtual T[] GetPrimitives()
        {
            if (primitives == null || CheckRemake())
            {
                CreatePrimitives();
            }

            return primitives;
        }

        protected abstract bool CheckRemake();

        protected abstract void CreatePrimitives();

        public virtual int GetPrimitiveCount()
        {
            return primitives.Length;
        }
    }

    public interface IPrimitiveComposition<T>
    {
        T[] GetPrimitives();
        int GetPrimitiveCount();
    }
}
