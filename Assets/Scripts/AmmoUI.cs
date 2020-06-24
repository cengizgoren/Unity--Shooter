using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Ammo ammo = null;

    [Header("UI")]
    [SerializeField] private TMP_Text ammoText = null;
    
    private void OnEnable()
    {
        UpdateAmmoBar(ammo.CurrentAmmo, ammo.MaxAmmo);

        ammo.OnAmmoChanged += HandleAmmoChanged;
    }

    private void OnDisable()
    {
        ammo.OnAmmoChanged -= HandleAmmoChanged;
    }

    private void HandleAmmoChanged(object sender, Ammo.AmmoChangedEventArgs e)
    {
        UpdateAmmoBar(e.CurrentAmmo, e.MaxAmmo);
    }
    private void UpdateAmmoBar(int currentAmmo, int maxAmmo)
    {
        ammoText.text = $"{Mathf.Ceil(currentAmmo)}/{Mathf.Ceil(maxAmmo)}";
    }
}
