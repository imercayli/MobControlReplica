using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonUpgradeObstacle : CounterBoxObstacle
{
    [SerializeField] protected GameObject canonUpgradeObject;
    private bool isUpgrade;

    protected override void Start()
    {
        base.Start();
        canonUpgradeObject.transform.SetParent(null);
    }

    protected override void DestoryBox()
    {
       if(isUpgrade) return;
        EnvironmentManager.Instance.Canon.UpgradeCanon(canonUpgradeObject);
        isUpgrade = true;
        base.DestoryBox();
    }
}
