using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Color color;

    private float progress;
    public float Progress 
    {
        get => progress;
        set
        {
            progress = Mathf.Clamp(value, 0.0f, 1.0f);
            fill.fillAmount = progress;
        }
    }

    private void Awake()
    {
        fill.color = color;
    }
}
