// @desc: Controls bullet movement, damage application, and auto-destruction after delay or collision
// @lastWritten: 2025-07-05
// @upToDate: true
using CastL.Prefabs;
using CastL.System;
using System.Collections;
using UnityEngine;

namespace CastL.Gameplay
{
    public class BasicBullet : MonoBehaviour
    {
        [SerializeField] public float bulletSpeed = 5f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private int bulletDamage = 10;
        [Tooltip("Ha a nyùl sprite felfelù nùz: -90. Ha jobbra: 0. ùllùtsd Inspectorban ha rossz irùnyba nùz.")]
        [SerializeField] private float rotationOffsetDegrees = 90f;

        private Transform target;
        private Vector2 _lastDirection;

        private void Start()
        {
            StartCoroutine(DestroyAfterTime(5f));
            if (rb != null)
                rb.freezeRotation = false; // Kell, hogy a transform.rotation eltartsa
        }

        public void SetTarget(Transform _target)
        {
            target = _target;
            if (_target != null)
                ApplyRotation(((Vector2)(_target.position - transform.position)).normalized);
        }

        private void ApplyRotation(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.001f) return;
            _lastDirection = direction;
            ApplyRotationInternal(direction);
        }

        private void ApplyRotationInternal(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffsetDegrees;
            if (rb != null)
            {
                rb.angularVelocity = 0f;
                rb.MoveRotation(angle);
            }
            else
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void LateUpdate()
        {
            if (_lastDirection.sqrMagnitude > 0.001f)
                ApplyRotationInternal(_lastDirection);
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

            // Nyùl hegye az irùnyba nùz (sprite alapbùl jobbra)
            ApplyRotation(direction);
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