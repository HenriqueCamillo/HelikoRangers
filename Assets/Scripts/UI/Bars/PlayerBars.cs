using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBars : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private BarWithTitle lifeBar;
    [SerializeField] private BarWithTitle lightAmmoBar;
    [SerializeField] private BarWithTitle mediumAmmoBar;
    [SerializeField] private BarWithTitle heavyAmmoBar;

    private void OnEnable()
    {
        if (playerState == null)
        {
            Debug.LogError("Invalid player");
            return;
        }

        playerState.OnAmmoUpdated += OnAmmoUpdated;
        playerState.GetComponent<Entity>().OnLifeUpdated += OnLifeUpdated;
    }

    private void OnDisable()
    {
        playerState.OnAmmoUpdated -= OnAmmoUpdated;
        playerState.GetComponent<Entity>().OnLifeUpdated -= OnLifeUpdated;
    }

    private BarWithTitle GetAmmoBar(AmmoType type)
    {
        switch (type)
        {
            case AmmoType.Light:
                return lightAmmoBar;
            case AmmoType.Medium:
                return mediumAmmoBar;
            case AmmoType.Heavy:
                return heavyAmmoBar;
            default:
                return null;
        }
    }

    private void OnLifeUpdated(float value, float maxValue)
    {
        UpdateBar(lifeBar, value / maxValue);
    }

    private void OnAmmoUpdated(AmmoType ammoType, int value, int maxValue)
    {
        UpdateBar(GetAmmoBar(ammoType), (float) value / maxValue);
    }

    private void UpdateBar(BarWithTitle bar, float progress)
    {
        if (bar == null || bar.ProgressBar == null)
            return;

        bar.ProgressBar.Progress = progress;
    }
}
