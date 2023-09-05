using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarWithTitle : MonoBehaviour
{
    public ProgressBar ProgressBar;
    [SerializeField] private TextMeshProUGUI title;

    public void SetTile(string text)
    {
        if (title == null)
            return;

        title.SetText(text);
    }
}
