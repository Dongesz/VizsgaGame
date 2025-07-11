// @desc: Controls bullet movement, damage application, and auto-destruction after delay or collision
// @lastWritten: 2025-07-05
// @upToDate: true
using CastL.Prefab;
using CastL.System;
using System.Collections;
using UnityEngine;

namespace CastL.Gameplay
{
    public class BasicBullet : MonoBehaviour
    {
        [SerializeField] public float bulletSpeed = 5f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private int bulletDamage = 1;

        private Transform target;

        private void Start()
        {
            StartCoroutine(DestroyAfterTime(5f));
        }

        public void SetTarget(Transform _target)
        {
            target = _target;
        }
        public void setDmg(int newDmg)
        {
            bulletDamage = newDmg;
        }

        private void FixedUpdate()
        {
            if (!target) return;
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                var behaviour = other.gameObject.GetComponent<EnemyBehaviour>();
                if (behaviour != null)
                {
                    behaviour.TakeDamage(bulletDamage);
                }
            }
            Destroy(gameObject);
        }

        private IEnumerator DestroyAfterTime(float delay)
        {
            yield return new WaitForSeconds(delay - 3);
            Destroy(gameObject);
        }
    }
}
