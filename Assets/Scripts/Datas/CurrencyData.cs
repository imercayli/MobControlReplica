using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Currency Type", menuName = "Scriptable Objects/Currency")]
public class CurrencyData : ScriptableObject
{
    [SerializeField] private long defaultCurrencyAmount;
    [SerializeField] private string currencyName;
    [SerializeField] private CurrencyType currencyType;
    
    [Header("UI Attributes")]
    [SerializeField] private Sprite currencyIcon;
    
    public CurrencyType CurrencyType => currencyType;
    public Sprite CurrencyIcon => currencyIcon;

    public UnityAction OnCurrencyUpdate;
    public long CurrencyAmount
    {
        get => long.Parse(PlayerPrefs.GetString(currencyName, defaultCurrencyAmount.ToString()));
        private set
        {
            PlayerPrefs.SetString(currencyName, value.ToString());
            OnCurrencyUpdate?.Invoke();
        }
    }
    
    public void SetAmount(long amount)
    {
        CurrencyAmount += amount;
    }

    public void ResetAmount()
    {
        CurrencyAmount = defaultCurrencyAmount;
    }
    
    public bool IsCurrencyEnough(int amount)
    {
        return CurrencyAmount >= amount;
    }

}
