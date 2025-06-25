using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    // Adott mozgásirány kezeléséhez.
    private Vector3 lastPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }

        // A kezdeti pozíció mentése.
        lastPosition = transform.position;
    }

    void Update()
    {
        AnimateSprite();
        AdjustSpriteDirection();
    }

    void AnimateSprite()
    {
        if (sprites.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer -= animationSpeed;
            currentFrame = (currentFrame + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentFrame];
        }
    }

    void AdjustSpriteDirection()
    {
        // Az aktuális mozgásirány kiszámítása.
        Vector3 direction = (transform.position - lastPosition).normalized;

        if (direction != Vector3.zero)
        {
            // Ha a mozgás jobb vagy bal irányba történik, tükrözzük a sprite-ot a megfelelõ tengely mentén.
            if (direction.x > 0)
            {
                // Mozgás jobbra, tehát nem tükrözzük.
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                // Mozgás balra, tükrözzük.
                spriteRenderer.flipX = true;
            }
        }

        // Az új pozíció mentése a következõ frame-hez.
        lastPosition = transform.position;
    }
}
