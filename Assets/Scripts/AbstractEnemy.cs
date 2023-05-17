using UnityEngine;

namespace DefaultNamespace
{
    public abstract class AbstractEnemy: MonoBehaviour
    {
        [SerializeField] protected GameObject path;
        [SerializeField] protected bool isNavmeshPath;
        [SerializeField] protected Transform navmeshTarget;
        [SerializeField] protected BaseBehavior baseObj;
        public abstract void GetDamage(float damage);

        public abstract void SetTarget(Transform navmeshTransform);

        public void SetBaseTarget(BaseBehavior baseObj)
        {
            this.baseObj = baseObj;
        }
    }
}