using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public abstract class AbstractEnemy: MonoBehaviour
    {
        public abstract void GetDamage(float damage);

        public abstract void DoDamage(float damage);
    }
}