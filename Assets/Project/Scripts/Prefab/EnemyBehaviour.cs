// @desc: Controls per-enemy behaviour including initialization, damage handling, and lifecycle events.
// @lastWritten: 2025-07-05
// @upToDate: true
using CastL.Data;
using CastL.Managers;
using UnityEngine;

namespace CastL.Prefabs
{
    public class EnemyBehaviour : MonoBehaviour
    {
        protected Enemy enemyData;
        protected int currentHP;

        public virtual void Initialize(Enemy Data)
        {
            enemyData = Data;
            currentHP = enemyData.hitPoints;

            GetComponent<EnemyMovement>()?.Initialize(enemyData.moveSpeed);

            EnemyManager.Instance?.RegisterEnemy();
        }

        public virtual void TakeDamage(int dmg)
        {
            currentHP -= dmg;
            if (currentHP <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            if (AudioManager.Instance != null && enemyData.deathSfx != null)
                AudioManager.Instance.PlaySfx(enemyData.deathSfx);
            PlayerStatsManager.Instance.IncreaseCurrency(enemyData.currencyWorth);
            PlayerStatsManager.Instance.IncreaseKills(1);
            EnemyManager.Instance?.UnRegisterEnemy();
            Destroy(gameObject);
        }

        public virtual void ReachGoal()
        {
            PlayerStatsManager.Instance.DecreaseHp(enemyData.damage);
            EnemyManager.Instance?.UnRegisterEnemy();
            Destroy(gameObject);
        }
        private void OnDestroy()
        {
            Debug.Log(gameObject.name + " OnDestroy meghvdott.");
            EnemyManager.Instance?.UnRegisterEnemy();
        }
    }
}
