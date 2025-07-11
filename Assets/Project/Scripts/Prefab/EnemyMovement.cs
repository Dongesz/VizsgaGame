// @desc: Controls enemy movement along path
// @lastWritten: 2025-07-05
// @upToDate: True
using CastL.Data;
using CastL.Managers;
using CastL.System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

namespace CastL.Prefab
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("References")]
        public EnemyBehaviour enemyBehaviour;


        [Header("Attributes")]
        [SerializeField]
        private Transform target;
        private int pathIndex = 0;

        private Rigidbody2D rb;
        private float moveSpeed;


        private void Awake()
        {
            enemyBehaviour = GetComponent<EnemyBehaviour>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            target = LevelDataHolder.Instance.path[pathIndex];
        }
        private void Update()
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                pathIndex++;
            }

            if (pathIndex == LevelDataHolder.Instance.path.Length)
            {
                enemyBehaviour.ReachGoal();
                return;
            }
            else
            {
                target = LevelDataHolder.Instance.path[pathIndex];
            }
        }

        private void FixedUpdate()
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }

        public void Initialize(float speed)
        {
            moveSpeed = speed;
        }
    }

}
