using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyService : BaseService<CurrencyService>
{
    private List<CurrencyData> currencyDatas;

    public override void Initialize()
    {
        base.Initialize();
        currencyDatas = new List<CurrencyData>(Resources.LoadAll<CurrencyData>("Currencies"));
    }
    
    public CurrencyData GetCurrencyData(CurrencyType currencyType)
    {
        return currencyDatas.Find(o => o.CurrencyType == currencyType);
    }
}
