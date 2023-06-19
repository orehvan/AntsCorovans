using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public abstract class AbstractEnemy: MonoBehaviour
    {
        [SerializeField] protected GameObject path;
        [SerializeField] protected bool isNavmeshPath;
        [SerializeField] protected Transform navmeshTarget;
        [SerializeField] protected BaseBehavior baseObj;
        protected NavMeshAgent agent;
        
        protected void Awake()
        {
            if (isNavmeshPath)
            {
                agent = gameObject.GetComponent<NavMeshAgent>();
            }
        }
        
        public abstract void GetDamage(float damage);

        public abstract void SetTarget(Transform navmeshTransform);

        public void SetBaseTarget(BaseBehavior baseObj)
        {
            this.baseObj = baseObj;
        }

        public void SetSlowness(float slowness)
        {
            agent.speed *= slowness;
        }
    }
}