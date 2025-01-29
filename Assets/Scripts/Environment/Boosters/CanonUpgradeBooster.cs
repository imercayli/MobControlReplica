using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonUpgradeBooster : CounterBoxObstacle
{
    [SerializeField] protected GameObject canonUpgradeObject;
    
    protected override void DestoryBox()
    {
        EnvironmentManager.Instance.CanonLevelController.UpgradeCanon(canonUpgradeObject);
        base.DestoryBox();
    }
}
