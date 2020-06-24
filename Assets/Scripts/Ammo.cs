using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 25;
    private int currentAmmo;

    public event EventHandler<AmmoChangedEventArgs> OnAmmoChanged;

    public int MaxAmmo => maxAmmo;
    public int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            currentAmmo = Mathf.Clamp(value, 0, maxAmmo);

            OnAmmoChanged?.Invoke(this, new AmmoChangedEventArgs
            {
                CurrentAmmo = currentAmmo,
                MaxAmmo = maxAmmo
            });
        }
    }
    private void Start() => CurrentAmmo = maxAmmo;
    public void Fire(int value)
    {
        value = Mathf.Max(value, 0);
        CurrentAmmo -= value;
    }
    public class AmmoChangedEventArgs : EventArgs
    {
        public int CurrentAmmo { get; set; }
        public int MaxAmmo { get; set; }
    }
}
