using UnityEngine;

namespace DefaultNamespace
{
    public abstract class AbstractTurret : MonoBehaviour
    {
        public BoxCollider2D turretCollider;
        void Start()
        {
            turretCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        }
    }
}