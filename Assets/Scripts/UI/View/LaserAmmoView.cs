using System.Collections.Generic;
using MVVM;
using UnityEngine;
using UnityEngine.UI;

public class LaserAmmoView : MonoBehaviour
{
    [SerializeField] private Image _laseImage;

    private readonly List<Image> _laserImages = new();
    private int _currentAmmoAmount;

    [Method("MaxAmmo")] 
    public void SetMaxHealth(int value)
    {
        _currentAmmoAmount = value;
        
        for (int i = 0; i < value; i++)
        {
            _laserImages.Add(Instantiate(_laseImage, transform));
        }
    }

    [Method("Ammo")] 
    public void OnAmmoChanged(int value)
    {
        _currentAmmoAmount = value;
        
        for (int i = 0; i < _laserImages.Count; i++)
        {
            _laserImages[i].fillAmount = i < value ?  1f : 0f;
        }
    }

    [Method("ReloadPercent")] 
    public void OnReload(float percentage)
    {
        _laserImages[_currentAmmoAmount].fillAmount = percentage;
    }
}