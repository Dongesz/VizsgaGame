// @desc: WorkInPogress
// @lastWritten: 2025-07-08
// @upToDate: true
using CastL.Managers;
using System.Collections;
using UnityEngine;

namespace CastL.UI
{
    public class CooldownBarAnimator : MonoBehaviour
    {
        public SpriteRenderer cooldownRenderer;
        public Sprite[] cooldownSprites;
        public float cooldownDuration = 2f;

        private float timePerFrame;
        private int currentFrame;
        private float timer;

        public bool finished;

        private Coroutine routine;

        private void Start()
        {
            timePerFrame = cooldownDuration / cooldownSprites.Length;
            routine = StartCoroutine(AnimateCooldown());
        }

        private IEnumerator AnimateCooldown()
        {
            currentFrame = 0;
            timer = 0f;
            finished = false;
            cooldownRenderer.sprite = cooldownSprites[0];

            while (true)
            {
                if (GameLoopManager.Instance.current != GameLoopManager.GameState.Running)
                {
                    yield return null;
                    continue;
                }

                timer += Time.deltaTime;

                if (timer >= timePerFrame)
                {
                    timer -= timePerFrame;
                    currentFrame++;

                    if (currentFrame >= cooldownSprites.Length)
                    {
                        currentFrame = 0;
                        finished = true;  
                    }

                    cooldownRenderer.sprite = cooldownSprites[currentFrame];
                }

                yield return null;
            }
        }
    }

}
