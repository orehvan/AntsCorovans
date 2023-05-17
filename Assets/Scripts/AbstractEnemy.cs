using UnityEngine;

namespace DefaultNamespace
{
    public abstract class AbstractEnemy: MonoBehaviour
    {
        [SerializeField] protected GameObject path;
        [SerializeField] protected bool isNavmeshPath;
        [SerializeField] protected Transform navmeshTarget;
        public abstract void GetDamage(float damage);

        public abstract void SetTarget(Transform navmeshTransform);
    }
}