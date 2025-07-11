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
        /*
        public SpriteRenderer cooldownRenderer; // A SpriteRenderer komponens, amely a cooldown sprite-ot jeleníti meg
        public Sprite[] cooldownSprites; // A 7 sprite tárolása
        public float cooldownDuration = 2.0f; // Teljes cooldown idõ
        public float timePerFrame;
        public int currentFrame;
        public float timer;
        public bool finished;
        public void StartMiningAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(AnimateCooldown());
        }
        public void StopMiningAnimation()
        {
            StopAllCoroutines();
        }
        private IEnumerator AnimateCooldown()
        {
            timePerFrame = cooldownDuration / cooldownSprites.Length;
            currentFrame = 0;
            timer = 0f;

            while (WaveManager.Instance.isWaveActive)
            {
                timer += Time.deltaTime;

                if (timer >= timePerFrame)
                {
                    timer -= timePerFrame;
                    currentFrame = (currentFrame + 1) % cooldownSprites.Length;
                    cooldownRenderer.sprite = cooldownSprites[currentFrame];
                }

                if (currentFrame == 0)
                {
                    finished = true;
                }
                else
                {
                    finished = false;
                }

                yield return null;
            }
        }*/
    }

}
