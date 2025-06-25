using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
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
            other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        }  
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTime(float delay)
    {  
        yield return new WaitForSeconds(delay-3);
        Destroy(gameObject);
    }
}
