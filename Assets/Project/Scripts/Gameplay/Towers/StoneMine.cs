// @desc: Controls the visibility of the resource bar on mouse hover over a stone mine
// @lastWritten: 2025-06-27
// @upToDate: false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMine : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bar;
    
    
    private void OnMouseEnter()
    {
        bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1f);
    }
    private void OnMouseExit()
    {
        bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0f);

    }
}
