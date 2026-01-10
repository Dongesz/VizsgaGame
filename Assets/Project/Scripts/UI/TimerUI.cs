// @desc: Updates timer control UI (start/stop and speed icons) based on TimerManager state
// @lastWritten: 2025-07-1
// @upToDate: true
using CastL.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace CastL.UI
{
    public class TimerUI : MonoBehaviour
    {

        [SerializeField] private Image startStopImage;


        [SerializeField] private Sprite spritePlay;
        [SerializeField] private Sprite spritePause;

        private void Start()
        {
            GameLoopManager.Instance.OnStateChanged += UpdateSprite;
            UpdateSprite(GameLoopManager.Instance.current);
        }

        public void UpdateSprite(GameLoopManager.GameState state)
        {
            startStopImage.sprite = (state == GameLoopManager.GameState.Running) ? spritePause : spritePlay;
        }
    }
}
