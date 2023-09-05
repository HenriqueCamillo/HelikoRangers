using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipedGuns : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Gun playerGun;
    [SerializeField] private Image currentGun;
    [SerializeField] private Image otherGun;
    [SerializeField] private TextMeshProUGUI bulletsInClip;

    private void Awake()
    {
        UpdateGuns(playerState.CurrentGun, playerState.GunNotBeingUsed);
    }

    private void OnEnable()
    {
        playerState.OnUpdatedGuns += UpdateGuns;
        playerGun.OnAmmoClipValuesChanged += UpdateBullets;
    }

    private void OnDisable()
    {
        playerState.OnUpdatedGuns -= UpdateGuns;
        playerGun.OnAmmoClipValuesChanged -= UpdateBullets;
    }

    private void UpdateGuns(GunTemplate currentGunTemplate, GunTemplate otherGunTemplate)
    {
        currentGun.sprite = currentGunTemplate != null ? currentGunTemplate.Sprite : null;
        otherGun.sprite = otherGunTemplate != null ? otherGunTemplate.Sprite : null;
        
        currentGun.gameObject.SetActive(currentGun.sprite != null);
        otherGun.gameObject.SetActive(otherGun.sprite != null);
    }

    private void UpdateBullets(int ammoInClip, int capacity)
    {
        bulletsInClip.gameObject.SetActive(ammoInClip <= capacity);
        bulletsInClip.SetText($"{ammoInClip}/{capacity}");
    }
}