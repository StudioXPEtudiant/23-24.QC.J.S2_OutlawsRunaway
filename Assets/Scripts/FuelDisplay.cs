using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class FuelDisplay : MonoBehaviour
{
    [SerializeField] private Image _fuelBarImage;
 
    public void UpdateFuelBar(float maxFuel, float currentFuel)
    {
        _fuelBarImage.fillAmount = currentFuel / maxFuel; 
    }
}