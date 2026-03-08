// @desc: Handles turret targeting, shooting logic, and enemy detection within range
// @lastWritten: 2025-07-03
// @upToDate: true
using UnityEngine;

namespace CastL.Gameplay
{
    public class BaseTurret : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firingPoint;
        [Header("Attribute")]
        [SerializeField] private float targetingRange = 5f;
        [SerializeField] private float bps = 1f;
        [SerializeField] private int TowerDmg = 50;
        [Header("Idle Settings")]

        private Transform target;
        private float timeUntilFire;

        private void Update()
        {
            if (target == null)
            {
                FindTarget();
            }


            if (!CheckTargetIsInRange())
            {
                target = null;
            }
            else
            {
                timeUntilFire += Time.deltaTime;

                if (timeUntilFire >= 1f / bps)
                {
                    Shoot();
                    timeUntilFire = 0f;
                }
            }
        }
        private void Shoot()
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            BasicBullet bulletScript = bulletObj.GetComponent<BasicBullet>();
            bulletScript.SetTarget(target);
            bulletScript.setDmg(TowerDmg);
        }
        private void FindTarget()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

            if (hits.Length > 0)
            {
                target = hits[0].transform;
            }
            else
            {
                target = null; 
            }
        }
        private bool CheckTargetIsInRange()
        {
            if (target == null)
                return false;

            return Vector2.Distance(target.position, transform.position) <= targetingRange;
        }
    }
}
