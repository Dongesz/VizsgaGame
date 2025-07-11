// @desc: Manages visual feedback for plots, including hover highlighting and sprite visibility control.
// @lastWritten: 2025-07-05
// @upToDate: true
using CastL.Gameplay;
using CastL.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastL.UI
{
    public class PlotUI : MonoBehaviour
    {
        

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color startColor;
        private TowerBuilder towerBuilder;

        private void Start()
        {
            // Szín beállítása típus szerint
            if (gameObject.CompareTag("stonespawn"))
                startColor = Color.white;
            else
                startColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // átlátszó szürke

            spriteRenderer.color = startColor;
        }
        private void OnMouseEnter()
        {
            spriteRenderer.color = hoverColor;
        }
        private void OnMouseExit()
        {
            spriteRenderer.color = startColor;
        }

        public void ResetPlotColor()
        {
            spriteRenderer.color = startColor;
            spriteRenderer.enabled = true;
        }

        public void DisableSprite()
        {
            spriteRenderer.enabled = false;
        }
    }
}
