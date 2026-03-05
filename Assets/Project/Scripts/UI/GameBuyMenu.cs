// @desc: Controls game menu UI, stat display, sprite switching, and animation toggle
// @lastWritten: 2025-07-03
// @upToDate: True
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CastL.Data;
using CastL.Managers;

namespace CastL.UI
{
    public class GameBuyMenu : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI currencyUI;
        [SerializeField] TextMeshProUGUI killsUI;
        [SerializeField] Animator anim;
        [SerializeField] private UnityEngine.UI.Image imageComponent;
        [SerializeField] private Sprite sprite1;
        [SerializeField] private Sprite sprite2;

        private bool isMenuOpen = true;
        private bool isSprite1Active = true;

        public void ToggleMenu()
        {
            isMenuOpen = !isMenuOpen;
            anim.SetBool("MenuOpen", isMenuOpen);
        }
        public void SwitchImg()
        {
            if (imageComponent != null)
            {
                if (isSprite1Active)
                {
                    imageComponent.sprite = sprite2;
                }
                else
                {
                    imageComponent.sprite = sprite1;
                }

                isSprite1Active = !isSprite1Active;
            }
        }

        private void OnGUI()
        {
            currencyUI.text = PlayerStatsManager.Instance.currency.ToString();
            killsUI.text = PlayerStatsManager.Instance.kills.ToString();
        }
    }

}
