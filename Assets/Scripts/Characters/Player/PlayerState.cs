using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private Dictionary<AmmoType, int> ammo = new Dictionary<AmmoType, int>();
    private List<GunTemplate> guns = new List<GunTemplate>();
    private int selectedGun;
    public int SelectedGun
    {
        get => selectedGun;
        set 
        {
            if (selectedGun < 0 || selectedGun >= guns.Count)
                return;

            selectedGun = value;
        }
    }

    public GunTemplate CurrentGun => guns.Count > SelectedGun ? guns[SelectedGun] : null;
    public GunTemplate GunNotBeingUsed => guns.Count > 1 ? guns[(selectedGun + 1) % guns.Count] : null;

    // TODO: Move to config file
    [SerializeField] private int defaultAmmo = 50;
    [SerializeField] private int maxAmmo = 300;

    private void Awake()
    {
        // TODO: Make better container contructions
        ammo.Clear();
        ammo.Add(AmmoType.Heavy, defaultAmmo);
        ammo.Add(AmmoType.Medium, defaultAmmo);
        ammo.Add(AmmoType.Light, defaultAmmo);

        guns.Clear();
        guns.Add(null);
        guns.Add(null);
    }

    public int GetAmmo(AmmoType ammoType)
    {
        return ammo.ContainsKey(ammoType) ? ammo[ammoType] : 0;
    }

    public void SetAmmo(AmmoType ammoType, int value)
    {
        ammo[ammoType] = Mathf.Clamp(value, 0, maxAmmo);
    }

    public void AddGun(GunTemplate gunTemplate)
    {
        if (GunNotBeingUsed != null)
            return;

        int index = guns.Count - 1;
        guns[index] = gunTemplate;
    }

    public void ReplaceCurrentGun(GunTemplate gunTemplate)
    {
        guns[SelectedGun] = gunTemplate;
    }

    public void SwitchGuns()
    {
        SelectedGun = (SelectedGun + 1) % guns.Count;
    }
}
 